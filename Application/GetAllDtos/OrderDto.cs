using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.GetAllDtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public OrderStatus Status { get; set; }
        public int UserId { get; set; }
        public int? EmployedId { get; set; }
        public UserByOrderDto User { get; set; }
        public UserByOrderDto? Employed { get; set; }
        public decimal Total { get; set; }
        public decimal Subtotal { get; set; }
        public BuyMethod BuyMethod { get; set; }
        public ICollection<OrderProductDto> Products { get; set; }
        public List<OrderAppliedPromotionDto> AppliedPromotions { get; set; }
    }
}
