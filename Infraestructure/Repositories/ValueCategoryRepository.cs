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
    public class ValueCategoryRepository : GenericRepository<ValueCategory>, IValueCategoryRepository
    {
        private readonly EcommerceDbContext _DbContextValueCategory;
        public ValueCategoryRepository(EcommerceDbContext dbContextValueCategory)   : base(dbContextValueCategory)
        {
            _DbContextValueCategory = dbContextValueCategory;
        }

        public async Task<List<ValueCategory>> GetValuesByCategoryIdAsync(int categoryId)
        {
            return await _DbContextValueCategory.ValueCategories
                .Where(vc => vc.CategoryId == categoryId)
                .ToListAsync();
        }
    }
}
