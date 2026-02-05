using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PromotionsServicesRules.Interfaces
{
    public interface IPromotionRule
    {
        Promotion Promotion { get; }
        bool IsApplicable(Order order);
        PromotionApplicationDetail Apply(Order order);
    }
}