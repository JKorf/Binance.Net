using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net
{
    public static class BinanceHelpers
    {
        public static decimal ClampQuantity(decimal minQuantity, decimal maxQuantity, decimal stepSize, decimal quantity)
        {
            quantity = Math.Min(maxQuantity, quantity);
            quantity = Math.Max(minQuantity, quantity);
            quantity -= quantity % stepSize;
            return quantity;
        }

        public static decimal ClampPrice(decimal minPrice, decimal maxPrice, decimal tickSize, decimal price)
        {
            price = Math.Min(maxPrice, price);
            price = Math.Max(minPrice, price);
            price -= price % tickSize;
            return price;
        }
    }
}
