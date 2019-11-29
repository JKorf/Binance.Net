using CryptoExchange.Net.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Binance.Net.Objects
{
    internal class BinanceAssetDetailsWrapper
    {
        [JsonProperty("assetDetail")]
        public Dictionary<string, BinanceAssetDetails>? Data { get; set; }
        public bool Success { get; set; }
        [JsonProperty("msg")]
        [JsonOptionalProperty]
        public string? Message { get; set; }
    }

    /// <summary>
    /// Asset details
    /// </summary>
    public class BinanceAssetDetails
    {
        /// <summary>
        /// Minimal amount you can withdraw
        /// </summary>
        [JsonProperty("minWithdrawAmount")]
        public decimal MinimalWithdrawAmount { get; set; }
        /// <summary>
        /// Whether deposits are enabled
        /// </summary>
        public bool DepositStatus { get; set; }
        /// <summary>
        /// Whether withdrawing is enabled
        /// </summary>
        public bool WithdrawStatus { get; set; }
        /// <summary>
        /// Fee for withdrawing
        /// </summary>
        public decimal WithdrawFee { get; set; }
        /// <summary>
        /// Status string for deposit
        /// </summary>
        public string? DepositTip { get; set; }
    }
}
