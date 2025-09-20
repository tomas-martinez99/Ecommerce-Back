using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public ICollection<ValueCategory> Values { get; set; }
        public ICollection<ProductCategory> Products { get; set; }
    }
}
