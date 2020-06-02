using System.Collections.Generic;

namespace Binance.Net.Objects.Futures.MarketData
{
    /// <summary>
    /// Notional and Leverage Brackets
    /// </summary>
    public class BinanceFuturesSymbolBracket
    {
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = "";

        /// <summary>
        /// Brackets
        /// </summary>
        public IEnumerable<BinanceFuturesBracket> Brackets { get; set; } = new List<BinanceFuturesBracket>();

    }

    /// <summary>
    /// Bracket
    /// </summary>
    public class BinanceFuturesBracket
    {
        /// <summary>
        /// Notianl bracket
        /// </summary>
        public int Bracket { get; set; }

        /// <summary>
        /// Max initial leverge for this bracket
        /// </summary>
        public int InitialLeverage { get; set; }

        /// <summary>
        /// Cap notional of this bracket
        /// </summary>
        public long NotionalCap { get; set; }

        /// <summary>
        /// Notionl threshold of this bracket
        /// </summary>
        public int NotionalFloor { get; set; }

        /// <summary>
        /// Maintenance ratio for this bracket
        /// </summary>
        public decimal MaintMarginRatio { get; set; }
    }
}