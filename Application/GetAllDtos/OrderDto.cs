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
        public DateTime Created { get; set; }
        public OrderStatus Status { get; set; }
        public int UserId { get; set; }
        public int? EmployedId { get; set; }
    }
}
