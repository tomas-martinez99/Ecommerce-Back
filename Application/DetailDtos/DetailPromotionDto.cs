using Application.GetAllDtos;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DetailDtos
{
    public class DetailPromotionDto
    {
        public int Id { get; set; }
        public string PromotionName { get; set; }
        public PromotionType Type { get; set; }
        public int Value { get; set; }
        public int Extra { get; set; }
        public int ProductId { get; set; }
        public bool IsEnabled { get; set; }
        public List<ProductByProviderDto> Products { get; set; } = new List<ProductByProviderDto>();

    }
}