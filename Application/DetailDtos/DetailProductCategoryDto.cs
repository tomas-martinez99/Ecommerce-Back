using Application.GetAllDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DetailDtos
{
    public class DetailProductCategoryDto
    {
        public ProductDto Product { get; set; }
        public CategoryDto Category { get; set; }
    }
}
