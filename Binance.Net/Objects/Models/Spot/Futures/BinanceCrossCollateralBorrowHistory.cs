using System;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Spot.Futures
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
        /// Asset
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
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
        /// Collateral asset
        /// </summary>
        [JsonProperty("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// Collateral quantity
        /// </summary>
        [JsonProperty("collateralAmount")]
        public decimal CollateralQuantity { get; set; }
        /// <summary>
        /// The status of the transfer
        /// </summary>
        [JsonConverter(typeof(FuturesTransferStatusConverter))]
        [JsonProperty("orderStatus")]
        public FuturesTransferStatus Status { get; set; }
    }
}
