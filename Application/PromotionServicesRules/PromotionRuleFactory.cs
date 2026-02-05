using Application.PromotionsServicesRules.Interfaces;
using Application.PromotionsServicesRules.Rules;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PromotionsServicesRules
{
    public class PromotionRuleFactory : IPromotionRuleFactory
    {
        public IPromotionRule Create(Promotion promotion)
        {
            return promotion.Type switch
            {
                PromotionType.PercentageDiscount => new PercentageDiscountRule(promotion, appliesToWholeOrder: false),
                PromotionType.BuyXGetYSameProduct => new BuyXGetYSameProductRule(promotion),
                PromotionType.BuyXGetYMixedProducts => new BuyXGetYMixedProductsRule(promotion),
                _ => throw new NotSupportedException($"Promotion type {promotion.Type} not supported")
            };
        }
    }
}