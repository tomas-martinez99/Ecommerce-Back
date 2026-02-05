using Application.PromotionsServicesRules.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PromotionsServicesRules.Rules
{
    public class BuyXGetYMixedProductsRule : IPromotionRule
    {
        public Promotion Promotion { get; }
        private readonly int _x;
        private readonly int _y;
        public BuyXGetYMixedProductsRule(Promotion promotion)
        {
            Promotion = promotion; _x = Convert.ToInt32(promotion.Value);
            _y = Convert.ToInt32(promotion.Extra);
        }
        public bool IsApplicable(Order order)
        {
            var mixedItems = order.Products.Where(op => Promotion.ProductPromotions.Any(pp => pp.ProductId == op.Product.Id)).ToList();
            int totalQty = mixedItems.Sum(mi => (int)mi.Quantity);
            return totalQty >= _x;
        }
        public PromotionApplicationDetail Apply(Order order)
        {
            var mixedItems = order.Products
                .Where(op => Promotion.ProductPromotions.Any(pp => pp.ProductId == op.Product.Id))
                .Select(op => new { op.Product.Id, op.Product.Price, Quantity = (int)op.Quantity })
                .ToList();
            int totalQty = mixedItems.Sum(mi => mi.Quantity);
            int sets = totalQty / _x;
            int totalFree = sets * _y;
            if (totalFree <= 0) return new PromotionApplicationDetail(Promotion.Id, Promotion.PromotionName, 0m, "No freebies");

            var itemsOrdered = mixedItems.OrderBy(mi => mi.Price).ToList();
            int freebiesLeft = totalFree;
            decimal discount = 0m;
            foreach (var item in itemsOrdered)
            {
                if (freebiesLeft <= 0) break;
                int take = System.Math.Min(item.Quantity, freebiesLeft);
                discount += take * item.Price;
                freebiesLeft -= take;
            }
            return new PromotionApplicationDetail(Promotion.Id, Promotion.PromotionName, discount, $"BuyXGetY mixed applied, freebies {totalFree}");
        }
    }
}