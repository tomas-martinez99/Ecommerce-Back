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
        Task<IReadOnlyList<Order>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Order?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(Order order, CancellationToken cancellationToken = default);
        Task<IEnumerable<OrderHistory>> GetHistoryByOrderIdAsync(int orderId, CancellationToken cancellationToken = default);
        Task<IEnumerable<OrderProduct>> GetProductsByOrderIdAsync(int orderId, CancellationToken cancellationToken = default);
    }
}

