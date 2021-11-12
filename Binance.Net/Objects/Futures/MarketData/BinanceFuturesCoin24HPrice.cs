using Binance.Net.Objects.Shared;
using CryptoExchange.Net.ExchangeInterfaces;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Futures.MarketData
{
    /// <summary>
    /// Price statistics of the last 24 hours
    /// </summary>
    public class BinanceFuturesCoin24HPrice : Binance24HPriceBase, ICommonTicker
    {
        /// <summary>
        /// The pair the price is for
        /// </summary>
        public string Pair { get; set; } = string.Empty;

        /// <inheritdoc />
        [JsonProperty("baseVolume")]
        public override decimal Volume { get; set; }
        /// <inheritdoc />
        [JsonProperty("volume")]
        public override decimal QuoteVolume { get; set; }

        string ICommonTicker.CommonSymbol => Symbol;
        decimal ICommonTicker.CommonHighPrice => HighPrice;
        decimal ICommonTicker.CommonLowPrice => LowPrice;
        decimal ICommonTicker.CommonVolume => Volume;
    }
}
