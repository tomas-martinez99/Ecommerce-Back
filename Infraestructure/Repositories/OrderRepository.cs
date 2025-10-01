using Application.Interfaces.Repositories;
using Domain.Entities;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly EcommerceDbContext _context;

        public OrderRepository(EcommerceDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Order?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Employed)
                .Include(o => o.Products).ThenInclude(op => op.Product)
                .Include(o => o.History)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<OrderHistory>> GetHistoryByOrderIdAsync(int orderId)
        {
            return await _context.OrderHistory
                .Where(h => h.OrderId == orderId)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderProduct>> GetProductsByOrderIdAsync(int orderId)
        {
            return await _context.OrderProducts
                .Where(p => p.OrderId == orderId)
                .Include(p => p.Product)
                .ToListAsync();
        }
    }

    
}
