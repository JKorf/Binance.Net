using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Customize margin call result
    /// </summary>
    public class BinanceCryptoLoanMarginCallResult
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Collateral asset
        /// </summary>
        [JsonProperty("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// Pre margin call 
        /// </summary>
        [JsonProperty("preMarginCall")]
        public decimal PreMarginCall { get; set; }
        /// <summary>
        /// After margin call
        /// </summary>
        [JsonProperty("afterMarginCall")]
        public decimal AfterMarginCall { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("customizeTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
