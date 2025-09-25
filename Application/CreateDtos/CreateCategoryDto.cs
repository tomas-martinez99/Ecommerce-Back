using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CreateDtos
{
    public class CreateCategoryDto
    {
        public string CategoryName { get; set; }
        public List<CreateValueCategoryDto> Values { get; set; } = new();
    }
}
