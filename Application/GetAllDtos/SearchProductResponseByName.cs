using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.GetAllDtos
{
    public record SearchProductResponseByName
    (
        int Id,
        string ProductName,
        decimal Price,
        List<ProductImageDto> Images,
        ProductGroupDto ProductGroup,
        BrandDto Brand,
        int ProductGroupId,
        int BrandId
    );
}
