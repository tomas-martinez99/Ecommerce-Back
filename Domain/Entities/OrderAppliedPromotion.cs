using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderAppliedPromotion
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int PromotionId { get; set; }
        public decimal DiscountAmount { get; set; }
        public string Details { get; set; } = "";
        public DateTimeOffset AppliedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}