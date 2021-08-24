using Binance.Net.Objects.Shared;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Futures.MarketData
{
    /// <summary>
    /// Price statistics of the last 24 hours
    /// </summary>
    public class BinanceFuturesCoin24HPrice : Binance24HPriceBase
    {
        /// <inheritdoc />
        [JsonProperty("baseVolume")]
        public override decimal BaseVolume { get; set; }
        /// <inheritdoc />
        [JsonProperty("volume")]
        public override decimal QuoteVolume { get; set; }
    }
}
