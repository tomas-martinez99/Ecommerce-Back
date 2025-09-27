using Application.CreateDtos;
using Application.DetailDtos;
using Application.GetAllDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IValueCategoryService
    {
        Task<List<ValueCategoryDto>> GetByCategoryIdAsync(int categoryId);
        Task<ValueCategoryDto> CreateAsync(CreateValueCategoryDto dto, int categoryId);
        Task<bool> DeleteAsync(int id);
    }
}
