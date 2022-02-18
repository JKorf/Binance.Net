using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Asset dusts that can be converted to BNB
    /// </summary>
    public class BinanceElligableDusts
    {
        /// <summary>
        /// Total BTC value
        /// </summary>
        [JsonProperty("totalTransferBtc")]
        public decimal TotalTransferBTC { get; set; }
        /// <summary>
        /// Total BNB value
        /// </summary>
        [JsonProperty("totalTransferBNB")]
        public decimal TotalTransferBNB { get; set; }
        /// <summary>
        /// Commission fee
        /// </summary>
        [JsonProperty("dribbletPercentage")]
        public decimal FeePercentage { get; set; }
        /// <summary>
        /// Assets
        /// </summary>
        public IEnumerable<BinanceElligableDust> Details { get; set; } = Array.Empty<BinanceElligableDust>();
    }

    /// <summary>
    /// Asset which can be converted
    /// </summary>
    public class BinanceElligableDust
    {
        /// <summary>
        /// Asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Full name of the asset
        /// </summary>
        public string AssetFullName { get; set; } = string.Empty;
        /// <summary>
        /// Amount free
        /// </summary>
        [JsonProperty("amountFree")]
        public decimal QuantityFree { get; set; }
        /// <summary>
        /// BTC value
        /// </summary>
        public decimal ToBTC { get; set; }
        /// <summary>
        /// BNB value without fee
        /// </summary>
        public decimal ToBNB { get; set; }
        /// <summary>
        /// BNB value with fee
        /// </summary>
        public decimal ToBNBOffExchange { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonProperty("exchange")]
        public decimal Fee { get; set; }
    }
}
