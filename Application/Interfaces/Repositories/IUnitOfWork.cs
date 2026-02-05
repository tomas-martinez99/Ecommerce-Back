using System;
using Microsoft.EntityFrameworkCore.Storage;
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
        IProductGroupRepository ProductGroups { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    }

}
