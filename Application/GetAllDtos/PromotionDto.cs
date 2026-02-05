using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.GetAllDtos
{
    public class PromotionDto
    {
        public int Id { get; set; }
        public string PromotionName { get; set; }
        public PromotionType Type { get; set; }
        public int Value { get; set; }
        public int Extra { get; set; }
        public bool IsEnabled { get; set; }
        public List<ProductByProviderDto> Products { get; set; }

    }
}