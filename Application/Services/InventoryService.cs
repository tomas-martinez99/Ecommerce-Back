using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IUnitOfWork _unitOfWork; public InventoryService(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }
        public async Task<bool> HasSufficientStockAsync(IEnumerable<(int ProductId, decimal Quantity)>
            items, CancellationToken cancellationToken = default)
        {
            foreach (var it in items)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(it.ProductId);
                if (product == null || product.Stock < it.Quantity)
                    return false;
            }
            return true;
        }
        public async Task<bool> AdjustStockAsync(int productId, decimal delta, CancellationToken cancellationToken = default)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId);
            if (product == null)
                return false;

            var newStock = product.Stock + delta;
            if (newStock < 0)
                return false;

            product.Stock = newStock;
            await _unitOfWork.Products.UpdateAsync(product);

            // No hacemos SaveChanges aquí: lo hará el OrderService dentro de la transacción
            return true;
        }

    }
}