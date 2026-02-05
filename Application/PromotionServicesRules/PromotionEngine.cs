using Application.PromotionsServicesRules.Interfaces;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PromotionsServicesRules
{
    public class PromotionEngine
    {
        private readonly IPromotionRuleFactory _factory;
        public PromotionEngine(IPromotionRuleFactory factory)
        {
            _factory = factory;
        }
        public PromotionResult ApplyPromotions(Order order, IEnumerable<Promotion> promotions)
        {
            var subtotal = order.Products.Sum(op => op.Product.Price * op.Quantity);
            var activePromos = promotions
                .Where(p => p.IsActive())
                .OrderByDescending(p => p.Priority)
                .ToList();
            var appliedDetails = new List<PromotionApplicationDetail>();
            var appliedPromotionIds = new HashSet<int>();
            decimal totalDiscount = 0m;
            foreach (var promo in activePromos)
            {
                if (appliedPromotionIds.Contains(promo.Id)) continue;

                var rule = _factory.Create(promo);
                if (!rule.IsApplicable(order)) continue;

                var detail = rule.Apply(order);
                if (detail.DiscountAmount <= 0m)
                {
                    appliedPromotionIds.Add(promo.Id);
                    if (!promo.Stackable) break;
                    continue;
                }

                totalDiscount += detail.DiscountAmount;
                appliedDetails.Add(detail);
                appliedPromotionIds.Add(promo.Id);

                if (!promo.Stackable) break;
            }
            return new PromotionResult
            {
                Subtotal = subtotal,
                TotalDiscount = totalDiscount,
                AppliedPromotions = appliedDetails
            };
        }
    }
}