using Application.PromotionsServicesRules.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PromotionsServicesRules.Rules
{
    public class PercentageDiscountRule : IPromotionRule
    {
        public Promotion Promotion { get; }
        private readonly decimal _percentage;
        private readonly bool _appliesToWholeOrder;
        public PercentageDiscountRule(Promotion promotion, bool appliesToWholeOrder = false)
        {
            Promotion = promotion;
            _percentage = promotion.Value;
            _appliesToWholeOrder = appliesToWholeOrder;
        }
        public bool IsApplicable(Order order)
        {
            if (_appliesToWholeOrder)
                return true;
            return order.Products.Any(op => Promotion.ProductPromotions.Any(pp => pp.ProductId == op.Product.Id));
        }
        public PromotionApplicationDetail Apply(Order order)
        {
            decimal discount = 0m;
            if (_appliesToWholeOrder)
            {
                discount = Math.Round(order.Products.Sum(op => op.Product.Price * op.Quantity) * (_percentage / 100m), 2);
            }
            else
            {
                foreach (var op in order.Products.Where(op => Promotion.ProductPromotions.Any(pp => pp.ProductId == op.Product.Id)))
                {
                    discount += Math.Round(op.Product.Price * (_percentage / 100m), 2) * op.Quantity;
                }
            }
            return new PromotionApplicationDetail(Promotion.Id, Promotion.PromotionName, discount, "Percentage discount applied");
        }
    }
}