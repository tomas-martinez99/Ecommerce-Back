using Application.GetAllDtos;
using Application.Interfaces.Repositories;
using FluentResults;
using Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Errors;
using AutoMapper;

namespace Application.Services
{
    public class SearchService : ISearchService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public SearchService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<SearchProductResponseByName>>> SearchProductResponseAsync(SearchProductRequest request)
        {
            var prod = await _productRepository.SearchAsync(request);
            if (prod == null || !prod.Any()) return Result.Fail(new NotFoundError("There are some error when trying to get the product"));

            var prodDto = _mapper.Map<IEnumerable<SearchProductResponseByName>>(prod);

            var response = prodDto;

            return Result.Ok(response);
        }
    }
}
