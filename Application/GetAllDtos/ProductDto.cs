using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.GetAllDtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; } 
        public List<ProductImageDto> Images { get; set; } = new();
        public ProductGroupDto ProductGroup { get; set; }
        public BrandDto Brand { get; set; }
        public int ProductGroupId { get; set; }
        public int BrandId { get; set; }
    }
}
