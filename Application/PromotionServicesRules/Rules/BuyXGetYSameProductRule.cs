using Application.PromotionsServicesRules.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PromotionsServicesRules.Rules
{
    public class BuyXGetYSameProductRule : IPromotionRule
    {
        public Promotion Promotion { get; }
        private readonly int _x;
        private readonly int _y;
        public BuyXGetYSameProductRule(Promotion promotion)
        {
            Promotion = promotion; _x = Convert.ToInt32(promotion.Value);
            _y = Convert.ToInt32(promotion.Extra);
        }
        public bool IsApplicable(Order order)
        {
            return order.Products.Any(op => Promotion.ProductPromotions.Any(pp => pp.ProductId == op.Product.Id) && op.Quantity >= _x);
        }
        public PromotionApplicationDetail Apply(Order order)
        {
            decimal discount = 0m; foreach (var op in order.Products.Where(op => Promotion.ProductPromotions.Any(pp => pp.ProductId == op.Product.Id)))
            {
                int sets = (int)(op.Quantity / _x);
                int freeItems = sets * _y;
                freeItems = Math.Min(freeItems, (int)op.Quantity);
                discount += freeItems * op.Product.Price;
            }
            return new PromotionApplicationDetail(Promotion.Id, Promotion.PromotionName, discount, "Buy X get Y same product");
        }
    }
}