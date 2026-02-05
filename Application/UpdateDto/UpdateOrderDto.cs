using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UpdateDto
{
    public class UpdateOrderDto
    {
        public OrderStatus Status { get; set; }
        public int? EmployedId { get; set; }
        public BuyMethod BuyMethod { get; set; }
        public decimal Total { get; set; }
        public List<UpdateOrderProductDto> Products { get; set; } = new();
    }
}
