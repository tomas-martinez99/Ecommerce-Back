using Application.GetAllDtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ISearchService
    {
        Task<Result<IEnumerable<SearchProductResponseByName>>> SearchProductResponseByName(string ProductName);
    }
}
