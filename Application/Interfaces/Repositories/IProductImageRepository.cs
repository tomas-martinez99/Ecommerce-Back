using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IProductImageRepository : IGenericRepository<ProductImage>
    {
        Task<IReadOnlyList<ProductImage>> GetByProductIdAsync(int productId);
        Task<ProductImage?> GetMainImageAsync(int productId);
        Task SetMainImageAsync(int productId, int imageId);
    }
}
