using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product?> GetByIdWithRelationsAsync(int id);
        Task<Product?> GetBySkuAsync(string sku);
        Task<Product?> GetBySkuWithRelationsAsync(string sku);
        Task<Product?> GetByIdWithImagesAsync(int id);
        Task<IReadOnlyList<Product>> GetAllWithImagesAsync();
        IQueryable <Product> GetQueryable();
    }
}
