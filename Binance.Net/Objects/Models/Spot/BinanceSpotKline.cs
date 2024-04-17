using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Candlestick information for symbol
    /// </summary>
    [JsonConverter(typeof(ArrayConverter))]
    public class BinanceSpotKline: BinanceKlineBase
    {
        /// <summary>
        /// The volume traded during this candlestick
        /// </summary>
        [ArrayProperty(5)]
        public override decimal Volume { get; set; }
        /// <summary>
        /// The volume traded during this candlestick in the asset form
        /// </summary>
        [ArrayProperty(7)]
        public override decimal QuoteVolume { get; set; }
        /// <summary>
        /// Taker buy base asset volume
        /// </summary>
        [ArrayProperty(9)]
        public override decimal TakerBuyBaseVolume { get; set; }
        /// <summary>
        /// Taker buy quote asset volume
        /// </summary>
        [ArrayProperty(10)]
        public override decimal TakerBuyQuoteVolume { get; set; }
        /// <summary>
        /// Ignore
        /// </summary>
        [ArrayProperty(11)]
        public decimal? Ignore { get; set; }
    }
}
