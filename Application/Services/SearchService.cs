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

namespace Application.Services
{
    public class SearchService
    {
        private readonly IProductRepository _productRepository;
        public SearchService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
    }
}
