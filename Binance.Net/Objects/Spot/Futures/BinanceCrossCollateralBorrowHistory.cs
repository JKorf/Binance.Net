﻿using System;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.Futures
{
    /// <summary>
    /// Borrow history
    /// </summary>
    public class BinanceCrossCollateralBorrowHistory
    {
        /// <summary>
        /// Id
        /// </summary>
        public string BorrowId { get; set; } = string.Empty;
        /// <summary>
        /// Time of confirmation
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime ConfirmedTime { get; set; }

        /// <summary>
        /// Coin
        /// </summary>
        public string Coin { get; set; } = string.Empty;
        /// <summary>
        /// The collateral rate
        /// </summary>
        public decimal CollateralRate { get; set; }
        /// <summary>
        /// Total left
        /// </summary>
        public decimal LeftTotal { get; set; }
        /// <summary>
        /// Principal left
        /// </summary>
        public decimal LeftPrincipal { get; set; }
        /// <summary>
        /// Dead line
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime DeadLine { get; set; }
        /// <summary>
        /// Collateral coin
        /// </summary>
        public string CollateralCoin { get; set; } = string.Empty;
        /// <summary>
        /// Collateral amount
        /// </summary>
        public decimal CollateralAmount { get; set; }
        /// <summary>
        /// The status of the transfer
        /// </summary>
        [JsonConverter(typeof(FuturesTransferStatusConverter))]
        public FuturesTransferStatus Status { get; set; }
    }
}
