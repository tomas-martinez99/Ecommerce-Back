using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CreateDtos
{
    public class CreateOrderDto
    {
        public int UserId { get; set; }
        public int? EmployedId { get; set; }
        public OrderStatus Status { get; set; }
        public List<CreateOrderProductDto> Products { get; set; }
    }

}
