using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IInventoryService
    {
        Task<bool> HasSufficientStockAsync(IEnumerable<(int ProductId, decimal Quantity)> items, CancellationToken cancellationToken = default);
        Task<bool> AdjustStockAsync(int productId, decimal delta, CancellationToken cancellationToken = default);
    }

}
