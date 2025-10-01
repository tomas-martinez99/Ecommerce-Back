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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly EcommerceDbContext _context;

        public UserRepository(EcommerceDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetDetailByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Orders)
                .Include(u => u.AssignedOrders)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
