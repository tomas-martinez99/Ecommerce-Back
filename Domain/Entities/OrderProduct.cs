using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderProduct
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal FinalUnitiPrice { get; set; }
        public decimal FreeQuantity { get; set; }
        public int? PromotionId { get; set; }
        public Promotion? Promotion { get; set; }

    }
}
