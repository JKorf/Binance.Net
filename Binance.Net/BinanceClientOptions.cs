using System;
using Binance.Net.Objects;
using CryptoExchange.Net;

namespace Binance.Net
{
    public class BinanceClientOptions: ExchangeOptions
    {
        public bool AutoTimestamp { get; set; } = false;
        public TradeRulesBehaviour TradeRulesBehaviour { get; set; } = TradeRulesBehaviour.None;
        public TimeSpan TradeRulesUpdateInterval { get; set; } = TimeSpan.FromSeconds(60);
    }

    public class BinanceSocketClientOptions : ExchangeOptions
    {
        public string BaseSocketAddress { get; set; } = "wss://stream.binance.com:9443/ws/";
    }
}
