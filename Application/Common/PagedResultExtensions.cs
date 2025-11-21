using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public static class PagedResultExtensions
    {
        public static async Task<PagedResult<TDest>> ToPagedResultAsync<TSource, TDest>(
            this IQueryable<TSource> query,
            int page,
            int pageSize,
            IConfigurationProvider mapperConfiguration)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var total = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<TDest>(mapperConfiguration)
                .ToListAsync();

            return new PagedResult<TDest>(items, total, page, pageSize);
        }
    }
}
