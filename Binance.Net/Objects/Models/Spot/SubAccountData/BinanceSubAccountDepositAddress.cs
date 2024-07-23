﻿namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Deposit address info for a sub-account
    /// </summary>
    public record BinanceSubAccountDepositAddress
    {
        /// <summary>
        /// The deposit address
        /// </summary>
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Asset type
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Tag for the deposit address
        /// </summary>
        public string Tag { get; set; } = string.Empty;
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; } = string.Empty;
    }
}
