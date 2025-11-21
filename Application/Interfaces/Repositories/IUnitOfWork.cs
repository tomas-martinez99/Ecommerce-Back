using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        IProductImageRepository ProductImages { get; }
        IProviderRepository Providers { get; }
        IBrandRepository Brands { get; }
        IProductGroupRepository ProductGroups { get; }   // 👈 Agregar esto
        Task<int> SaveChangesAsync();
    }
}
