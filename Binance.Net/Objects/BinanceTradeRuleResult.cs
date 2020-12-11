namespace Binance.Net.Objects
{
    internal class BinanceTradeRuleResult
    {
        public bool Passed { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? StopPrice { get; set; }
        public string? ErrorMessage { get; set; }

        public static BinanceTradeRuleResult CreatePassed(decimal? quantity, decimal? price, decimal? stopPrice)
        {
            return new BinanceTradeRuleResult
            {
                Passed = true,
                Quantity = quantity,
                Price = price,
                StopPrice = stopPrice
            };
        }

        public static BinanceTradeRuleResult CreateFailed(string message)
        {
            return new BinanceTradeRuleResult
            {
                Passed = false,
                ErrorMessage = message
            };
        }
    }
}
