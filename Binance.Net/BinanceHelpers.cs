using System;

namespace Binance.Net
{
    public static class BinanceHelpers
    {
        public static decimal ClampQuantity(decimal minQuantity, decimal maxQuantity, decimal stepSize, decimal quantity)
        {
            quantity = Math.Min(maxQuantity, quantity);
            quantity = Math.Max(minQuantity, quantity);
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
