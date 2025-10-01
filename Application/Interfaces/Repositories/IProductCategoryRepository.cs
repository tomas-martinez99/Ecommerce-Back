using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IProductCategoryRepository : IGenericRepository<ProductCategory>
    {
        Task<ProductCategory?> GetByIdsAsync(int productId, int categoryId);
        Task<IEnumerable<ProductCategory>> GetByProductIdAsync(int productId);
        Task<IEnumerable<ProductCategory>> GetByCategoryIdAsync(int categoryId);
    }
}
