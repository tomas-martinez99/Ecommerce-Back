﻿using Domain.Entities;
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
        public User User { get; set; }
        public DateTime Created { get; set; }
    }
}
