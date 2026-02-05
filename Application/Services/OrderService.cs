using Application.CreateDtos;
using Application.DetailDtos;
using Application.GetAllDtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.PromotionsServicesRules;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IMapper _mapper;
        private readonly IPromotionRepository _promotionRepo;
        private readonly PromotionEngine _promotionEngine;
        private readonly IInventoryService _inventoryService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepo;
        public OrderService(IProductRepository productRepo, IOrderRepository orderRepo, IMapper mapper, IPromotionRepository promotionRepo, IInventoryService inventoryService, IUnitOfWork unitOfWork, PromotionEngine promotionEngine)
        {
            _orderRepo = orderRepo;
            _mapper = mapper;
            _promotionEngine = promotionEngine;
            _promotionRepo = promotionRepo;
            _unitOfWork = unitOfWork;
            _inventoryService = inventoryService;
            _productRepo = productRepo;

        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var orders = await _orderRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<DetailOrderDto?> GetByIdAsync(int id)
        {
            var order = await _orderRepo.GetByIdWithDetailsAsync(id);
            return order is null
                ? null
                : _mapper.Map<DetailOrderDto>(order);
        }

        public async Task<OrderDto> CreateAsync(CreateOrderDto dto, CancellationToken cancellationToken = default)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));
            if (dto.Products == null || !dto.Products.Any())
                throw new ArgumentException("La orden debe contener productos.", nameof(dto));

            var order = _mapper.Map<Order>(dto);
            foreach (var op in order.Products)
            {
                var product = await _productRepo.GetByIdAsync(op.ProductId, cancellationToken);
                if (product == null)
                    throw new InvalidOperationException($"Producto {op.ProductId} no existe.");
                op.Product = product; // 🔥 inicializa la navegación
                op.UnitPrice = product.Price; // si tenés campo UnitPrice en OrderProduct
            }
            order.Created = DateTime.UtcNow;

            var promotions = await _promotionRepo.GetActivePromotionsAsync(cancellationToken);

            // 3. Aplicar promociones en memoria (PromotionEngine devuelve subtotal, descuentos y detalles)
            var promoResult = _promotionEngine.ApplyPromotions(order, promotions);

            // 4. Registrar resultado en la entidad para persistencia/auditoría
            order.Total = promoResult.Total;
            order.Subtotal = promoResult.Subtotal; // si tienes campo Subtotal
            order.AppliedPromotions = promoResult.AppliedPromotions
                .Select(d => new OrderAppliedPromotion
                {
                    PromotionId = d.PromotionId,
                    DiscountAmount = d.DiscountAmount,
                    Details = d.Details,
                    AppliedAt = DateTimeOffset.UtcNow
                })
                .ToList();

            // 5. (Opcional) Validaciones adicionales: stock, límites, pagos, etc.
            // Si tienes un servicio de inventario, valida disponibilidad antes de persistir.
            if (_inventoryService != null)
            {
                var items = order.Products.Select(p => (ProductId: p.Product.Id, Quantity: (decimal)p.Quantity)).ToList();
                var hasStock = await _inventoryService.HasSufficientStockAsync(items, cancellationToken);
                if (!hasStock)
                    throw new InvalidOperationException("Stock insuficiente para alguno de los productos.");
            }

            // 6. Persistir todo en una transacción
            // Nota: si tu IUnitOfWork expone BeginTransactionAsync/CommitAsync/RollbackAsync, úsalo.
            // Si no, el bloque try/catch con SaveChangesAsync sigue siendo válido.
            try
            {
                // Sin transacción explícita disponible
                await _orderRepo.AddAsync(order, cancellationToken);

                if (_inventoryService != null)
                {
                    foreach (var op in order.Products)
                    {
                        await _inventoryService.AdjustStockAsync(op.Product.Id, -op.Quantity, cancellationToken);
                    }
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                // Si hay rollback explícito disponible, intenta usarlo
                if (_unitOfWork is ITransactionalUnitOfWork transactionalRollback)
                {
                    try { await transactionalRollback.RollbackAsync(cancellationToken); } catch { /* log */ }
                }
                throw;
            }

            // 7. Mapear a DTO de salida y devolver
            var resultDto = _mapper.Map<OrderDto>(order);
            return resultDto;
        }

        public async Task<bool> ChangeStatusAsync(int orderId, OrderStatus newStatus, int? employeeId = null, CancellationToken cancellationToken = default)
        {
            var order = await _orderRepo.GetByIdWithDetailsAsync(orderId);
            if (order is null) return false;

            var oldStatus = order.Status;
            if (oldStatus == newStatus) return true;

            var needsReserve = oldStatus != OrderStatus.asignada && newStatus == OrderStatus.asignada;
            var needsRelease = newStatus == OrderStatus.cancelada;

            // Si no afecta stock, solo cambiar estado
            if (!needsReserve && !needsRelease)
            {
                order.ChangeStatus(newStatus, employeeId);
                order.History.Add(new OrderHistory { OrderId = order.Id, OldStatus = oldStatus, NewStatus = newStatus, ChangedByEmployeeId = employeeId, ChangedAt = DateTime.UtcNow });
                await _orderRepo.UpdateAsync(order);
                await _orderRepo.SaveChangesAsync(cancellationToken);
                return true;
            }

            var items = order.Products.Select(p => (ProductId: p.ProductId, Quantity: p.Quantity)).ToList();

            // Iniciar transacción (usa la UoW o DbContext)
            using var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                if (needsReserve)
                {
                    var ok = await _inventoryService.HasSufficientStockAsync(items, cancellationToken);
                    if (!ok)
                    {
                        await tx.RollbackAsync(cancellationToken);
                        return false; // o devolver un resultado con motivo
                    }

                    foreach (var it in items)
                    {
                        var success = await _inventoryService.AdjustStockAsync(it.ProductId, -it.Quantity, cancellationToken);
                        if (!success)
                        {
                            await tx.RollbackAsync(cancellationToken);
                            return false;
                        }
                    }
                }
                else if (needsRelease)
                {
                    foreach (var it in items)
                    {
                        await _inventoryService.AdjustStockAsync(it.ProductId, it.Quantity, cancellationToken);
                    }
                }

                order.ChangeStatus(newStatus, employeeId);
                order.History.Add(new OrderHistory { OrderId = order.Id, OldStatus = oldStatus, NewStatus = newStatus, ChangedByEmployeeId = employeeId, ChangedAt = DateTime.UtcNow });

                await _orderRepo.UpdateAsync(order);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await tx.CommitAsync(cancellationToken);
                return true;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _orderRepo.GetByIdAsync(id);
            if (order is null) return false;

            await _orderRepo.DeleteAsync(order);
            await _orderRepo.SaveChangesAsync();

            return true;
        }
    }
}
