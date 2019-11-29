namespace Binance.Net.Objects
{
    internal class BinanceTradeRuleResult
    {
        public bool Passed { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }
        public string? ErrorMessage { get; set; }

        public static BinanceTradeRuleResult CreatePassed(decimal? quantity, decimal? price)
        {
            return new BinanceTradeRuleResult
            {
                Passed = true,
                Quantity = quantity,
                Price = price
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
