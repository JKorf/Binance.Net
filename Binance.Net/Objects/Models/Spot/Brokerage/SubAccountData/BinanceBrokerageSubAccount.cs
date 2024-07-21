﻿namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Sub Account
    /// </summary>
    public record BinanceBrokerageSubAccount : BinanceBrokerageSubAccountCommission
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Tag
        /// </summary>
        public string Tag { get; set; } = string.Empty;

        /// <summary>
        /// Create Date
        /// </summary>
        [JsonPropertyName("createTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateDate { get; set; }
    }
}