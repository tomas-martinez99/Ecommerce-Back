using Application.GetAllDtos;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DetailDtos
{
    public class DetailOrderDto
    {
        public int Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public OrderStatus Status { get; set; }
        public UserDto User { get; set; }
        public UserDto? Employed { get; set; }
        public List<OrderProductDto> Products { get; set; }
        public List<OrderHistoryDto> History { get; set; }

        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public BuyMethod BuyMethod { get; set; }
        public List<OrderAppliedPromotionDto> AppliedPromotions { get; set; }
    }
}
