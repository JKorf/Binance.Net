using Newtonsoft.Json;

namespace Binance.Net.Objects.Futures.FuturesData
{
    /// <summary>
    /// Information about an account
    /// </summary>
    public class BinanceFuturesAccountBalance
    {
        /// <summary>
        /// Account alias
        /// </summary>
        public string AccountAlias { get; set; } = "";

        /// <summary>
        /// The asset this balance is for
        /// </summary>
        public string? Asset { get; set; }

        /// <summary>
        /// The total balance of this asset
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Crossed wallet balance
        /// </summary>
        public decimal CrossWalletBalance { get; set; }

        /// <summary>
        /// Unrealized profit of crossed positions
        /// </summary>
        [JsonProperty("crossUnPnl")]
        public decimal CrossPositionsUnrealizedProfitAndLoss { get; set; }

        /// <summary>
        /// Available balance
        /// </summary>
        public decimal AvailableBalance { get; set; }

        /// <summary>
        /// Maximum amount for transfer out
        /// </summary>
        public decimal MaxWithdrawAvailable { get; set; }
    }

}
