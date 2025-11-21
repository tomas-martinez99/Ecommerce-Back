using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.GetAllDtos
{
    public class SearchProductResponseByName
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ImgUrl { get; set; }
        public decimal Price { get; set; }

    }
}
