using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub account position risk
    /// </summary>
    [SerializationModel]
    public record BinanceSubAccountFuturesPositionRiskV2
    {
        /// <summary>
        /// ["<c>futurePositionRiskVos</c>"] Futures account response (USDT margined)
        /// </summary>
        [JsonPropertyName("futurePositionRiskVos")]
        public BinanceSubAccountFuturesPositionRisk[] UsdtMarginedFutures { get; set; } = Array.Empty<BinanceSubAccountFuturesPositionRisk>();

        /// <summary>
        /// ["<c>deliveryPositionRiskVos</c>"] Delivery account response (COIN margined)
        /// </summary>
        [JsonPropertyName("deliveryPositionRiskVos")]
        public BinanceSubAccountFuturesPositionRiskCoin[] CoinMarginedFutures { get; set; } = Array.Empty<BinanceSubAccountFuturesPositionRiskCoin>();
    }

    /// <summary>
    /// Sub account position risk
    /// </summary>
    public record BinanceSubAccountFuturesPositionRiskCoin
    {
        /// <summary>
        /// ["<c>entryPrice</c>"] The position entry price.
        /// </summary>
        [JsonPropertyName("entryPrice")]
        public decimal EntryPrice { get; set; }

        /// <summary>
        /// ["<c>markPrice</c>"] Mark price
        /// </summary>
        [JsonPropertyName("markPrice")]
        public decimal MarkPrice { get; set; }

        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }

        /// <summary>
        /// ["<c>isolated</c>"] Isolated
        /// </summary>
        [JsonPropertyName("isolated")]
        public bool Isolated { get; set; }

        /// <summary>
        /// ["<c>isolatedWallet</c>"] Isolated wallet
        /// </summary>
        [JsonPropertyName("isolatedWallet")]
        public decimal IsolatedWallet { get; set; }

        /// <summary>
        /// ["<c>isolatedMargin</c>"] Isolated margin
        /// </summary>
        [JsonPropertyName("isolatedMargin")]
        public decimal IsolatedMargin { get; set; }

        /// <summary>
        /// ["<c>isAutoAddMargin</c>"] Is auto add margin
        /// </summary>
        [JsonPropertyName("isAutoAddMargin")]
        public bool IsAutoAddMargin { get; set; }

        /// <summary>
        /// ["<c>positionSide</c>"] Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }

        /// <summary>
        /// ["<c>positionAmount</c>"] Position amount
        /// </summary>
        [JsonPropertyName("positionAmount")]
        public decimal PositionQuantity { get; set; }

        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>unrealizedProfit</c>"] Unrealized profit
        /// </summary>
        [JsonPropertyName("unrealizedProfit")]
        public decimal UnrealizedProfit { get; set; }
    }
}

