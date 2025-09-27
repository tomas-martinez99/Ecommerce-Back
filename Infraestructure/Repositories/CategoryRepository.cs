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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository 
    {
        private readonly EcommerceDbContext _DbContextCategory;
        public CategoryRepository(EcommerceDbContext context) : base(context)
        {
            _DbContextCategory = context;
        }
        public async Task<Category?> GetByIdWithValuesAsync(int id)
        {
            return await _DbContextCategory.Categories
                                 .Include(c => c.Values)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
