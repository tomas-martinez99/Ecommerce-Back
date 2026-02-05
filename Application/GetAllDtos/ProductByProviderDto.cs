using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.GetAllDtos
{
    public class ProductByProviderDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Cost { get; set; }
        public BrandDto Brand { get; set; }
        public ProductGroupDto ProductGroup { get; set; }
        public ProviderDto Provider { get; set; }
    }
}
