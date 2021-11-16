using System;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.BSwap
{
    /// <summary>
    /// Swap record
    /// </summary>
    public class BinanceBSwapRecord
    {
        /// <summary>
        /// Swap id
        /// </summary>
        public long SwapId { get; set; }
        /// <summary>
        /// Time of the swap
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime SwapTime { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonConverter(typeof(BSwapStatusConverter))]
        public BSwapStatus Status { get; set; }

        /// <summary>
        /// Base asset
        /// </summary>
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Quote asset
        /// </summary>
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// Base quantity
        /// </summary>
        [JsonProperty("baseQty")]
        public decimal BaseQuantity { get; set; }
        /// <summary>
        /// Quote quantity
        /// </summary>
        [JsonProperty("quoteQty")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        public decimal Fee { get; set; }
    }
}
