using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int? EmployedId { get; set; }
        public User? Employed { get; set; }
        public OrderStatus Status { get; set; }
        public ICollection<OrderProduct> Products { get; set; }
        public ICollection<OrderHistory> History { get; set; }

        public void ChangeStatus(OrderStatus newStatus, int? employeeId = null)
        {
            Status = newStatus;
            EmployedId = employeeId;
        }
    }
}
