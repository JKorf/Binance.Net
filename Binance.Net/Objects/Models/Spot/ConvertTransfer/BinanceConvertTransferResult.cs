﻿namespace Binance.Net.Objects.Models.Spot.ConvertTransfer
{
    /// <summary>
    /// Result of a convert transfer operation
    /// </summary>
    public class BinanceConvertTransferResult
    {
        /// <summary>
        /// Transfer id
        /// </summary>
        [JsonProperty("tranId")]
        public long TransferId { get; set; }
        /// <summary>
        /// Status of the transfer (definitions currently unknown)
        /// </summary>
        public string Status { get; set; } = string.Empty;
    }
}
