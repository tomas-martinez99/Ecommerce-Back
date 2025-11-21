using Application.GetAllDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DetailDtos
{
    public class DetailProviderDto
    {
        public int Id { get; set; }
        public string ProviderName { get; set; }
        public List<ProductByProviderDto> Products { get; set; } = new();
    }
}
