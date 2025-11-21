using Application.GetAllDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DetailDtos
{
    public class DetailProductGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductDto> Products { get; set; }
    }
}
