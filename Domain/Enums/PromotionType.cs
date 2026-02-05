using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum PromotionType
    {
        PercentageDiscount,   // %20 en varios productos
        BuyXGetYSameProduct,  // 2x1 en el mismo producto
        BuyXGetYMixedProducts //2X1 en productos diferentes(se descuenta el mas barato)
    }
}