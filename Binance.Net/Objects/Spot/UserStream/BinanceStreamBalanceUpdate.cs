using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.UserStream
{
    /// <summary>
    /// Update when asset is withdrawn/deposited 
    /// </summary>
    public class BinanceStreamBalanceUpdate: BinanceStreamEvent
    {
        /// <summary>
        /// The asset which changed
        /// </summary>
        [JsonProperty("a")]
        public string Asset { get; set; } = "";
        /// <summary>
        /// The balance delta
        /// </summary>
        [JsonProperty("d")]
        public decimal BalanceDelta { get; set; }
        /// <summary>
        /// The time the deposit/withdrawal was cleared
        /// </summary>
        [JsonProperty("T"), JsonConverter(typeof(TimestampConverter))]
        public DateTime ClearTime { get; set; }
    }
}
