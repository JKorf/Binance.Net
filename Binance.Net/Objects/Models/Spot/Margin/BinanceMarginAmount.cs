﻿namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// The result quantity of getting maxBorrowable or maxTransferable 
    /// </summary>
    public class BinanceMarginAmount
    {
        /// <summary>
        /// The quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// The borrow limit
        /// </summary>
        public decimal BorrowLimit { get; set; }
    }
}
