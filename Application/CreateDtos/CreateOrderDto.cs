using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Application.GetAllDtos;


namespace Application.CreateDtos
{
    public class CreateOrderDto
    {
        public int UserId { get; set; }
        public BuyMethod BuyMethod { get; set; }
        public List<CreateOrderProductDto> Products { get; set; }

    }
}
