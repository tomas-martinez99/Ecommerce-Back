using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.GetAllDtos
{
    public class OrderAppliedPromotionDto
    {
        public int PromotionId { get; set; }
        public decimal DiscountAmount { get; set; }
        public string Details { get; set; }
        public DateTimeOffset AppliedAt { get; set; }
    }
}