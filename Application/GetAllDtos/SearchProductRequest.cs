using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.GetAllDtos
{
    public record SearchProductRequest
    (
        string? ProductName,
        decimal? Price,
        string? Brand,
        string? ProductGroup
    );
}
