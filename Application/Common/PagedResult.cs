using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int Total { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public bool HasPrevious => Page > 1;
        public bool HasNext => Page * PageSize < Total;

        public PagedResult() { }

        public PagedResult(List<T> items, int total, int page, int pageSize)
        {
            Items = items ?? new List<T>();
            Total = total;
            Page = page;
            PageSize = pageSize;
        }
    }
}
