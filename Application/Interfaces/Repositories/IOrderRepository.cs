using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Order?> GetByIdWithDetailsAsync(int id);
        Task<IEnumerable<OrderHistory>> GetHistoryByOrderIdAsync(int orderId);
        Task<IEnumerable<OrderProduct>> GetProductsByOrderIdAsync(int orderId);
    }
}
