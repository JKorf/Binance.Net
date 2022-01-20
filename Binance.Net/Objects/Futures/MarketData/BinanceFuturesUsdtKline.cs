using Binance.Net.Objects.Shared;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Futures.MarketData
{
    /// <summary>
    /// Candlestick information for symbol
    /// </summary>
    [JsonConverter(typeof(ArrayConverter))]
    public class BinanceFuturesUsdtKline : BinanceKlineBase
    {
        /// <inheritdoc/>
        [ArrayProperty(5)]
        public override decimal BaseVolume { get; set; }
        /// <inheritdoc/>
        [ArrayProperty(7)]
        public override decimal QuoteVolume { get; set; }
        /// <inheritdoc/>
        [ArrayProperty(9)]
        public override decimal TakerBuyBaseVolume { get; set; }
        /// <inheritdoc/>
        [ArrayProperty(10)]
        public override decimal TakerBuyQuoteVolume { get; set; }
    }
}
