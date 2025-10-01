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

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepo, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _mapper = mapper;
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

        public async Task<OrderDto> CreateAsync(CreateOrderDto dto)
        {
            var order = _mapper.Map<Order>(dto);
            order.Created = DateTime.UtcNow;

            await _orderRepo.AddAsync(order);
            await _orderRepo.SaveChangesAsync();

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<bool> ChangeStatusAsync(int orderId, OrderStatus newStatus, int? employeeId = null)
        {
            var order = await _orderRepo.GetByIdWithDetailsAsync(orderId);
            if (order is null) return false;

            var oldStatus = order.Status;
            order.ChangeStatus(newStatus, employeeId);

            order.History.Add(new OrderHistory
            {
                OrderId = order.Id,
                OldStatus = oldStatus,
                NewStatus = newStatus,
                ChangedByEmployeeId = employeeId,
                ChangedAt = DateTime.UtcNow
            });

            await _orderRepo.UpdateAsync(order);
            await _orderRepo.SaveChangesAsync();

            return true;
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
