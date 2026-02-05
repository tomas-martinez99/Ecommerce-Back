using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PromotionsServicesRules.Interfaces
{
    public interface IPromotionRuleFactory
    {
        IPromotionRule Create(Promotion promotion);
    }
}