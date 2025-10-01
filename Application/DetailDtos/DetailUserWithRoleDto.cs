using Application.GetAllDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DetailDtos
{
    public class DetailUserWithRoleDto
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public List<UserDto> Users { get; set; }
    }
}
