using Application.GetAllDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DetailDtos
{
    public class DetailProductDto
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public List<ProductImageDto> Images { get; set; } = new();
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public decimal stock { get; set; }
        public int ProviderId { get; set; }
        public ProviderDto Provider { get; set; }
        public string Brand { get; set; }
        public string FamilyGroup { get; set; }
    }
}
