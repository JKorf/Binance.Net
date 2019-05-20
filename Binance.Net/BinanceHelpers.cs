using System;
using System.Collections.Generic;
using System.Linq;

namespace Binance.Net
{
    public static class BinanceHelpers
    {
        public static int? UsedWeight(this IEnumerable<Tuple<string, string>>  headers)
        {
            if (int.TryParse(headers?.SingleOrDefault(s => s.Item1 == "X-MBX-USED-WEIGHT")?.Item2, out var value))
                return value;
            return null;
        }

        public static decimal ClampQuantity(decimal minQuantity, decimal maxQuantity, decimal stepSize, decimal quantity)
        {
            quantity = Math.Min(maxQuantity, quantity);
            quantity = Math.Max(minQuantity, quantity);
            if (stepSize == 0)
                return quantity;
            quantity -= quantity % stepSize;
            quantity = Floor(quantity);
            return quantity;
        }

        public static decimal ClampPrice(decimal minPrice, decimal maxPrice, decimal price)
        {
            price = Math.Min(maxPrice, price);
            price = Math.Max(minPrice, price);
            return price;
        }

        public static decimal FloorPrice(decimal tickSize, decimal price)
        {
            price -= price % tickSize;
            price = Floor(price);
            return price;
        }

        private static decimal Floor(decimal number)
        {
            return Math.Floor(number * 100000000) / 100000000;
        }
    }
}
