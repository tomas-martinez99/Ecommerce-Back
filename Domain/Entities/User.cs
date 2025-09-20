﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Order> AssignedOrders { get; set; }
        public int? RoleId { get; set; }
    }
}
