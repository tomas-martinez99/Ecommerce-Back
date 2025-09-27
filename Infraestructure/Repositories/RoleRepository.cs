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
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        private readonly EcommerceDbContext _DbContextCategory;
        public RoleRepository(EcommerceDbContext context) : base(context)
        {
            _DbContextCategory = context;
        }
        public async Task<Role?> GetByIdWithUsersAsync(int id)
        {
            return await _DbContextCategory.Roles
                                 .Include(r => r.Users)
                                 .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
