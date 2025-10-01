using Application.CreateDtos;
using Application.DetailDtos;
using Application.GetAllDtos;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<DetailOrderDto?> GetByIdAsync(int id);
        Task<OrderDto> CreateAsync(CreateOrderDto dto);
        Task<bool> ChangeStatusAsync(int orderId, OrderStatus newStatus, int? employeeId = null);
        Task<bool> DeleteAsync(int id);
    }
}
