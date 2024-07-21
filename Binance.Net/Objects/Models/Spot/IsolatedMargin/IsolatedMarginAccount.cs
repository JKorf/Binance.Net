using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.IsolatedMargin
{
    /// <summary>
    /// Isolated margin account info
    /// </summary>
    public record BinanceIsolatedMarginAccount
    {
        /// <summary>
        /// Account assets
        /// </summary>
        [JsonPropertyName("assets")]
        public IEnumerable<BinanceIsolatedMarginAccountSymbol> Assets { get; set; } = Array.Empty<BinanceIsolatedMarginAccountSymbol>();
        /// <summary>
        /// Total btc asset
        /// </summary>
        [JsonPropertyName("totalAssetOfBtc")]
        public decimal TotalAssetOfBtc { get; set; }
        /// <summary>
        /// Total liability
        /// </summary>
        [JsonPropertyName("totalLiabilityOfBtc")]
        public decimal TotalLiabilityOfBtc { get; set; }
        /// <summary>
        /// Total net asset
        /// </summary>
        [JsonPropertyName("totalNetAssetOfBtc")]
        public decimal TotalNetAssetOfBtc { get; set; }
    }

    /// <summary>
    /// Isolated margin account symbol
    /// </summary>
    public record BinanceIsolatedMarginAccountSymbol
    {
        /// <summary>
        /// Base asset
        /// </summary>
        [JsonPropertyName("baseAsset")]
        public BinanceIsolatedMarginAccountAsset BaseAsset { get; set; } = default!;

        /// <summary>
        /// Quote asset
        /// </summary>
        [JsonPropertyName("quoteAsset")]
        public BinanceIsolatedMarginAccountAsset QuoteAsset { get; set; } = default!;

        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Isolated created
        /// </summary>
        [JsonPropertyName("isolatedCreated")]
        public bool IsolatedCreated { get; set; }
        /// <summary>
        /// The margin level
        /// </summary>
        [JsonPropertyName("marginLevel")]
        public decimal MarginLevel { get; set; }
        /// <summary>
        /// Margin level status
        /// </summary>
        [JsonPropertyName("marginLevelStatus")]
        public MarginLevelStatus MarginLevelStatus { get; set; }
        /// <summary>
        /// Margin ratio
        /// </summary>
        [JsonPropertyName("marginRatio")]
        public decimal MarginRatio { get; set; }
        /// <summary>
        /// Index price
        /// </summary>
        [JsonPropertyName("indexPrice")]
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// Liquidate price
        /// </summary>
        [JsonPropertyName("liquidatePrice")]
        public decimal LiquidatePrice { get; set; }
        /// <summary>
        /// Liquidate rate
        /// </summary>
        [JsonPropertyName("liquidateRate")]
        public decimal LiquidateRate { get; set; }
        /// <summary>
        /// If trading is enabled
        /// </summary>
        [JsonPropertyName("tradeEnabled")]
        public bool TradeEnabled { get; set; }
        /// <summary>
        /// Account is enabled
        /// </summary>
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }
    }

    /// <summary>
    /// Isolated margin account asset
    /// </summary>
    public record BinanceIsolatedMarginAccountAsset
    {
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// If borrow is enabled
        /// </summary>
        [JsonPropertyName("borrowEnabled")]
        public bool BorrowEnabled { get; set; }
        /// <summary>
        /// Borrowed
        /// </summary>
        [JsonPropertyName("borrowed")]
        public decimal Borrowed { get; set; }
        /// <summary>
        /// Free
        /// </summary>
        [JsonPropertyName("free")]
        public decimal Free { get; set; }
        /// <summary>
        /// Interest
        /// </summary>
        [JsonPropertyName("interest")]
        public decimal Interest { get; set; }
        /// <summary>
        /// Locked
        /// </summary>
        [JsonPropertyName("locked")]
        public decimal Locked { get; set; }
        /// <summary>
        /// Net asset
        /// </summary>
        [JsonPropertyName("netAsset")]
        public decimal NetAsset { get; set; }
        /// <summary>
        /// Net asset in btc
        /// </summary>
        [JsonPropertyName("netAssetOfBtc")]
        public decimal NetAssetOfBtc { get; set; }
        /// <summary>
        /// Is repay enabled
        /// </summary>
        [JsonPropertyName("repayEnabled")]
        public bool RepayEnabled { get; set; }
        /// <summary>
        /// Total asset
        /// </summary>
        [JsonPropertyName("totalAsset")]
        public decimal TotalAsset { get; set; }
    }
}
