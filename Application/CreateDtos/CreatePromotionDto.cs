using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CreateDtos
{
    public class CreatePromotionDto
    {
        public string PromotionName { get; set; }
        public PromotionType Type { get; set; }
        public int Priority { get; set; } = 0;
        public int Value { get; set; }   // Ej: 50 para %50, 2 para "compra 2"
        public int Extra { get; set; }   // Ej: 1 para "lleva 1 gratis"
        public List<int> ProductIds { get; set; } = new List<int>();

    }
}