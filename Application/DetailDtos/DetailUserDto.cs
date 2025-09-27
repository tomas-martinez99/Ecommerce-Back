using Application.GetAllDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DetailDtos
{
    public class DetailUserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public RoleDto? Role { get; set; }
        public List<OrderDto> Orders { get; set; }
        public List<OrderDto> AssignedOrders { get; set; }
    }
}
