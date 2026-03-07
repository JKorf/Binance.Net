using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.IsolatedMargin
{
    /// <summary>
    /// Isolated margin account info
    /// </summary>
    [SerializationModel]
    public record BinanceIsolatedMarginAccount
    {
        /// <summary>
        /// ["<c>assets</c>"] Account assets
        /// </summary>
        [JsonPropertyName("assets")]
        public BinanceIsolatedMarginAccountSymbol[] Assets { get; set; } = Array.Empty<BinanceIsolatedMarginAccountSymbol>();
        /// <summary>
        /// ["<c>totalAssetOfBtc</c>"] Total btc asset
        /// </summary>
        [JsonPropertyName("totalAssetOfBtc")]
        public decimal TotalAssetOfBtc { get; set; }
        /// <summary>
        /// ["<c>totalLiabilityOfBtc</c>"] Total liability
        /// </summary>
        [JsonPropertyName("totalLiabilityOfBtc")]
        public decimal TotalLiabilityOfBtc { get; set; }
        /// <summary>
        /// ["<c>totalNetAssetOfBtc</c>"] Total net asset
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
        /// ["<c>baseAsset</c>"] Base asset
        /// </summary>
        [JsonPropertyName("baseAsset")]
        public BinanceIsolatedMarginAccountAsset BaseAsset { get; set; } = default!;

        /// <summary>
        /// ["<c>quoteAsset</c>"] Quote asset
        /// </summary>
        [JsonPropertyName("quoteAsset")]
        public BinanceIsolatedMarginAccountAsset QuoteAsset { get; set; } = default!;

        /// <summary>
        /// ["<c>symbol</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>isolatedCreated</c>"] Isolated created
        /// </summary>
        [JsonPropertyName("isolatedCreated")]
        public bool IsolatedCreated { get; set; }
        /// <summary>
        /// ["<c>marginLevel</c>"] The margin level
        /// </summary>
        [JsonPropertyName("marginLevel")]
        public decimal MarginLevel { get; set; }
        /// <summary>
        /// ["<c>marginLevelStatus</c>"] Margin level status
        /// </summary>
        [JsonPropertyName("marginLevelStatus")]
        public MarginLevelStatus MarginLevelStatus { get; set; }
        /// <summary>
        /// ["<c>marginRatio</c>"] Margin ratio
        /// </summary>
        [JsonPropertyName("marginRatio")]
        public decimal MarginRatio { get; set; }
        /// <summary>
        /// ["<c>indexPrice</c>"] Index price
        /// </summary>
        [JsonPropertyName("indexPrice")]
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// ["<c>liquidatePrice</c>"] Liquidate price
        /// </summary>
        [JsonPropertyName("liquidatePrice")]
        public decimal LiquidatePrice { get; set; }
        /// <summary>
        /// ["<c>liquidateRate</c>"] Liquidate rate
        /// </summary>
        [JsonPropertyName("liquidateRate")]
        public decimal LiquidateRate { get; set; }
        /// <summary>
        /// ["<c>tradeEnabled</c>"] If trading is enabled
        /// </summary>
        [JsonPropertyName("tradeEnabled")]
        public bool TradeEnabled { get; set; }
        /// <summary>
        /// ["<c>enabled</c>"] Account is enabled
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
        /// ["<c>asset</c>"] Asset name
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>borrowEnabled</c>"] If borrow is enabled
        /// </summary>
        [JsonPropertyName("borrowEnabled")]
        public bool BorrowEnabled { get; set; }
        /// <summary>
        /// ["<c>borrowed</c>"] Borrowed
        /// </summary>
        [JsonPropertyName("borrowed")]
        public decimal Borrowed { get; set; }
        /// <summary>
        /// ["<c>free</c>"] Free
        /// </summary>
        [JsonPropertyName("free")]
        public decimal Free { get; set; }
        /// <summary>
        /// ["<c>interest</c>"] Interest
        /// </summary>
        [JsonPropertyName("interest")]
        public decimal Interest { get; set; }
        /// <summary>
        /// ["<c>locked</c>"] Locked
        /// </summary>
        [JsonPropertyName("locked")]
        public decimal Locked { get; set; }
        /// <summary>
        /// ["<c>netAsset</c>"] Net asset
        /// </summary>
        [JsonPropertyName("netAsset")]
        public decimal NetAsset { get; set; }
        /// <summary>
        /// ["<c>netAssetOfBtc</c>"] Net asset in btc
        /// </summary>
        [JsonPropertyName("netAssetOfBtc")]
        public decimal NetAssetOfBtc { get; set; }
        /// <summary>
        /// ["<c>repayEnabled</c>"] Is repay enabled
        /// </summary>
        [JsonPropertyName("repayEnabled")]
        public bool RepayEnabled { get; set; }
        /// <summary>
        /// ["<c>totalAsset</c>"] Total asset
        /// </summary>
        [JsonPropertyName("totalAsset")]
        public decimal TotalAsset { get; set; }
    }
}

