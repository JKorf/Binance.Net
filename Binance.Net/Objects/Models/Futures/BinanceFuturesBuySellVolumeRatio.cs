using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Buy Sell Volume Ratio Info
    /// </summary>
    public class BinanceFuturesBuySellVolumeRatio
    {
        /// <summary>
        /// buy/sell ratio
        /// </summary>
        public decimal BuySellRatio { get; set; }

        /// <summary>
        /// buy volume
        /// </summary>
        [JsonProperty("buyVol")]
        public decimal BuyVolume { get; set; }

        /// <summary>
        /// sell volume
        /// </summary>
        [JsonProperty("sellVol")]
        public decimal SellVolume { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("timestamp"), JsonConverter(typeof(TimestampConverter))]
        public DateTime? Timestamp { get; set; }
    }
}