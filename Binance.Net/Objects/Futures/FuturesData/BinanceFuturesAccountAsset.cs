namespace Binance.Net.Objects.Futures.FuturesData
{
    /// <summary>
    /// Information about an account asset
    /// </summary>
    public class BinanceFuturesAccountAsset
    {
        /// <summary>
        /// Asset
        /// </summary>
        public string Asset { get; set; } = "";

        /// <summary>
        /// Initial Margin
        /// </summary>
        public decimal InitialMargin { get; set; }

        /// <summary>
        /// Maint Margin
        /// </summary>
        public decimal MaintMargin { get; set; }

        /// <summary>
        /// Margin Balance
        /// </summary>
        public decimal MarginBalance { get; set; }

        /// <summary>
        /// Maximum Withdraw Amount
        /// </summary>
        public decimal MaxWithdrawAmount { get; set; }

        /// <summary>
        /// Open Order Initial Margin
        /// </summary>
        public decimal OpenOrderInitialMargin { get; set; }

        /// <summary>
        /// Position Initial Margin
        /// </summary>
        public decimal PositionInitialMargin { get; set; }

        /// <summary>
        /// Unrealized Profit
        /// </summary>
        public decimal UnrealizedProfit { get; set; }

        /// <summary>
        /// Wallet Balance
        /// </summary>
        public decimal WalletBalance { get; set; }
    }
}
