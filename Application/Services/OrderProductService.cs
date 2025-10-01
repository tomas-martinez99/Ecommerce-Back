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
    public class OrderProductService : IOrderProductService
    {
        private readonly IOrderProductRepository _productRepo;
        private readonly IMapper _mapper;

        public OrderProductService(IOrderProductRepository productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderProductDto>> GetByOrderIdAsync(int orderId)
        {
            var products = await _productRepo.GetByOrderIdAsync(orderId);
            return _mapper.Map<IEnumerable<OrderProductDto>>(products);
        }
    }
}
