using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.GetAllDtos
{
    public class ProductAdminDto
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public decimal Stock { get; set; }
        public int ProviderId { get; set; }
        public ProviderDto Provider { get; set; }
        public List<ProductImageDto> Images { get; set; } = new();
        public ProductGroupDto ProductGroup { get; set; }
        public BrandDto Brand { get; set; }
        public int ProductGroupId { get; set; }
        public int BrandId { get; set; }
    }
}
