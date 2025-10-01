using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IOrderHistoryRepository : IGenericRepository<OrderHistory>
    {
        Task<IEnumerable<OrderHistory>> GetByOrderIdAsync(int orderId);
    }
}
