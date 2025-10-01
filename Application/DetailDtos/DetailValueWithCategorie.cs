using Application.GetAllDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DetailDtos
{
    public class DetailValueWithCategorie
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public List<ValueCategoryDto> Values { get; set; } = new List<ValueCategoryDto>();

    }
}
