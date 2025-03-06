using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub account position risk
    /// </summary>
    public record BinanceSubAccountFuturesPositionRiskV2
    {
        /// <summary>
        /// Futures account response (USDT margined)
        /// </summary>
        [JsonPropertyName("futurePositionRiskVos")]
        public BinanceSubAccountFuturesPositionRisk[] UsdtMarginedFutures { get; set; } = Array.Empty<BinanceSubAccountFuturesPositionRisk>();

        /// <summary>
        /// Delivery account response (COIN margined)
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
        /// The entry price
        /// </summary>
        [JsonPropertyName("entryPrice")]
        public decimal EntryPrice { get; set; }

        /// <summary>
        /// Mark price
        /// </summary>
        [JsonPropertyName("markPrice")]
        public decimal MarkPrice { get; set; }

        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }

        /// <summary>
        /// Isolated
        /// </summary>
        [JsonPropertyName("isolated")]
        public bool Isolated { get; set; }

        /// <summary>
        /// Isolated wallet
        /// </summary>
        [JsonPropertyName("isolatedWallet")]
        public decimal IsolatedWallet { get; set; }

        /// <summary>
        /// Isolated margin
        /// </summary>
        [JsonPropertyName("isolatedMargin")]
        public decimal IsolatedMargin { get; set; }

        /// <summary>
        /// Is auto add margin
        /// </summary>
        [JsonPropertyName("isAutoAddMargin")]
        public bool IsAutoAddMargin { get; set; }

        /// <summary>
        /// Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }

        /// <summary>
        /// Position amount
        /// </summary>
        [JsonPropertyName("positionAmount")]
        public decimal PositionQuantity { get; set; }

        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Unrealized profit
        /// </summary>
        [JsonPropertyName("unrealizedProfit")]
        public decimal UnrealizedProfit { get; set; }
    }
}
