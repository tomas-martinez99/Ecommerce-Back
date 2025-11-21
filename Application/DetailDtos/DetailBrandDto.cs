using Application.GetAllDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DetailDtos
{
    public class DetailBrandDto
    {
        public int Id { get; set; }
        public string BrandName { get; set; }

        public ICollection<ProductDto> Products { get; set; }
    }
}
