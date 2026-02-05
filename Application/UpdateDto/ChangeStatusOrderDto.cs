using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UpdateDto
{
    public class ChangeStatusOrderDto
    {
        public OrderStatus NewStatus { get; set; }
        public int? EmployeeId { get; set; } //
    }
}