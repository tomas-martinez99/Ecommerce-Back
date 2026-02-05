using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PromotionsServicesRules
{
    public record PromotionApplicationDetail(int PromotionId, string PromotionName, decimal DiscountAmount, string Details);
    public class PromotionResult
    {
        public decimal Subtotal { get; init; }
        public decimal TotalDiscount { get; init; }
        public decimal Total => Math.Max(0m, Subtotal - TotalDiscount);
        public IReadOnlyList<PromotionApplicationDetail> AppliedPromotions { get; init; } = Array.Empty<PromotionApplicationDetail>();
    }
}