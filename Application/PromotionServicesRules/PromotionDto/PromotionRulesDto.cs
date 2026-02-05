using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PromotionsServicesRules.PromotionsDtos
{
    public class PromotionRulesDto
    {
        public record PercentageActionDto(decimal Percentage);
        public record FixedActionDto(decimal Amount);
        public record BuyXGetYActionDto(int X, int Y);
        public record PromotionApplicationDetailDto(int PromotionId, string PromotionName, decimal DiscountAmount, string Details);
    }
}