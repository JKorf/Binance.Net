using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Asset info for dust conversion
    /// </summary>
    public class BinanceMarginDustAsset
    {
        /// <summary>
        /// Total btc
        /// </summary>
        public decimal TotalTransferBtc { get; set; }
        /// <summary>
        /// Total bnb
        /// </summary>
        public decimal TotalTransferBnb { get; set; }
        /// <summary>
        /// Dribblet percentage
        /// </summary>
        public decimal DribbletPercentage { get; set; }
        /// <summary>
        /// Details
        /// </summary>
        public IEnumerable<BinanceMarginDustAssetDetails> Details { get; set; } = Array.Empty<BinanceMarginDustAssetDetails>();
    }

    /// <summary>
    /// Asset dust details
    /// </summary>
    public class BinanceMarginDustAssetDetails
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Asset full name
        /// </summary>
        [JsonProperty("assetFullName")]
        public string AssetFullName { get; set; } = string.Empty;
        /// <summary>
        /// Quantity fee
        /// </summary>
        [JsonProperty("amountFree")]
        public decimal QuantityFree { get; set; }
        /// <summary>
        /// To btc
        /// </summary>
        [JsonProperty("toBTC")]
        public decimal ToBtc { get; set; }
        /// <summary>
        /// To bnb
        /// </summary>
        [JsonProperty("toBNB")]
        public decimal ToBnb { get; set; }
        /// <summary>
        /// To bnb off exchange
        /// </summary>
        [JsonProperty("toBNBOffExchange")]
        public decimal ToBnbOffExchange { get; set; }
        /// <summary>
        /// Exchange
        /// </summary>
        [JsonProperty("exchange")]
        public decimal Exchange { get; set; }
    }
}
