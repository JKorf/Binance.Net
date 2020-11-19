using Binance.Net.Enums;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Futures.FuturesData
{
    /// <summary>
    /// Information about an account position
    /// </summary>
    public class BinanceFuturesAccountPosition
    {
        /// <summary>
        /// Is isolated
        /// </summary>
        public bool Isolated { get; set; }

        /// <summary>
        /// Leverage
        /// </summary>
        public int Leverage { get; set; }

        /// <summary>
        /// Initial margin
        /// </summary>
        public decimal InitialMargin { get; set; }

        /// <summary>
        /// Maint margin
        /// </summary>
        public decimal MaintMargin { get; set; }

        /// <summary>
        /// Open order initial margin
        /// </summary>
        public decimal OpenOrderInitialMargin { get; set; }

        /// <summary>
        /// Position initial margin
        /// </summary>
        public decimal PositionInitialMargin { get; set; }

        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = "";

        /// <summary>
        /// Unrealized profit
        /// </summary>
        public decimal UnrealizedProfit { get; set; }

        /// <summary>
        /// Position side
        /// </summary>
        public PositionSide PositionSide { get; set; }

        /// <summary>
        /// Entry Price
        /// </summary>
        public decimal EntryPrice { get; set; }

        /// <summary>
        /// Maximum available notional with current leverage
        /// </summary>
        public decimal MaxNotional { get; set; }

        /// <summary>
        /// Position amount
        /// </summary>
        [JsonProperty("positionAmt")]
        public decimal PositionAmount { get; set; }
    }
}
