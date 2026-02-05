using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Application.CreateDtos
{
    public class CreateBrandDto
    {
        [Required]
        public string BrandName { get; set; }
    }
}
