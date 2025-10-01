using Application.GetAllDtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderHistoryService : IOrderHistoryService
    {
        private readonly IOrderHistoryRepository _historyRepo;
        private readonly IMapper _mapper;

        public OrderHistoryService(IOrderHistoryRepository historyRepo, IMapper mapper)
        {
            _historyRepo = historyRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderHistoryDto>> GetByOrderIdAsync(int orderId)
        {
            var history = await _historyRepo.GetByOrderIdAsync(orderId);
            return _mapper.Map<IEnumerable<OrderHistoryDto>>(history);
        }
    }
}
