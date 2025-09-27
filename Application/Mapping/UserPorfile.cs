using Application.CreateDtos;
using Application.DetailDtos;
using Application.GetAllDtos;
using Application.UpdateDto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class UserPorfile: Profile
    {
        public UserPorfile()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<User, DetailUserDto>()
                .ForMember(d => d.Orders, opt => opt.MapFrom(s => s.Orders))
                .ForMember(d => d.AssignedOrders, opt => opt.MapFrom(s => s.AssignedOrders));
            CreateMap<CreateRoleDto, Role>();
            CreateMap<Role, RoleDto>();
            CreateMap<Role, DetailUserWithRoleDto>()
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users));
        }

    }
}
