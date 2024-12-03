using Binance.Net.Enums;
using CryptoExchange.Net.Objects.Options;

namespace Binance.Net.Objects.Options
{
    /// <summary>
    /// Options for Binance Socket API
    /// </summary>
    public class BinanceSocketApiOptions : SocketApiOptions
    {
        /// <summary>
        /// Whether to check the trade rules when placing new orders and what to do if the trade isn't valid
        /// </summary>
        public TradeRulesBehaviour TradeRulesBehaviour { get; set; } = TradeRulesBehaviour.None;

        /// <summary>
        /// How often the trade rules should be updated. Only used when TradeRulesBehaviour is not None
        /// </summary>
        public TimeSpan TradeRulesUpdateInterval { get; set; } = TimeSpan.FromMinutes(60);

        internal BinanceSocketApiOptions Set(BinanceSocketApiOptions targetOptions)
        {
            targetOptions = base.Set<BinanceSocketApiOptions>(targetOptions);
            targetOptions.TradeRulesBehaviour = TradeRulesBehaviour;
            targetOptions.TradeRulesUpdateInterval = TradeRulesUpdateInterval;
            return targetOptions;
        }
    }
}
