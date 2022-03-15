using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Information about an account
    /// </summary>
    public class BinanceFuturesCoinAccountBalance
    {
        /// <summary>
        /// Account alias
        /// </summary>
        public string AccountAlias { get; set; } = string.Empty;

        /// <summary>
        /// The asset this balance is for
        /// </summary>
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// The total balance of this asset
        /// </summary>
        [JsonProperty("balance")]
        public decimal WalletBalance { get; set; }

        /// <summary>
        /// Crossed wallet balance
        /// </summary>
        public decimal CrossWalletBalance { get; set; }

        /// <summary>
        /// Unrealized profit of crossed positions
        /// </summary>
        [JsonProperty("crossUnPnl")]
        public decimal CrossUnrealizedPnl { get; set; }

        /// <summary>
        /// Available balance
        /// </summary>
        public decimal AvailableBalance { get; set; }

        /// <summary>
        /// Maximum quantity for transfer out
        /// </summary>
        [JsonProperty("withdrawAvailable")]
        public decimal MaxWithdrawQuantity { get; set; }

        /// <summary>
        /// Last update time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateTime { get; set; }
    }
}
