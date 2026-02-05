using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Promotion
    {
        public int Id { get; set; }
        public string PromotionName { get; set; }
        public PromotionType Type { get; set; }
        public decimal Value { get; set; }// puede ser porcentaje o la canidad a comprar
        public decimal Extra { get; set; } //  la cantidad extra a llevar
        public bool Stackable { get; set; } = true;
        public int Priority { get; set; } = 0;
        public string ConditionJson { get; set; } = "";
        public string ActionJson { get; set; } = "";
        public bool IsEnabled { get; set; } = true;


        public ICollection<ProductPromotion> ProductPromotions { get; set; } = new List<ProductPromotion>();



        // 🔹 Método calculado
        public bool IsActive() => IsEnabled;
    }
}