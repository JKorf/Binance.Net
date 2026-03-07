using Binance.Net.Objects.Models.Spot.Margin;

namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub account margin trade details
    /// </summary>
    [SerializationModel]
    public record BinanceSubAccountMarginDetails
    {
        /// <summary>
        /// ["<c>email</c>"] The account email address.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>marginLevel</c>"] Margin level
        /// </summary>
        [JsonPropertyName("marginLevel")]
        public decimal MarginLevel { get; set; }
        /// <summary>
        /// ["<c>totalAssetOfBtc</c>"] Total asset in btc
        /// </summary>
        [JsonPropertyName("totalAssetOfBtc")]
        public decimal TotalAssetOfBtc { get; set; }
        /// <summary>
        /// ["<c>totalLiabilityOfBtc</c>"] Total liability
        /// </summary>
        [JsonPropertyName("totalLiabilityOfBtc")]
        public decimal TotalLiabilityOfBtc { get; set; }
        /// <summary>
        /// ["<c>totalNetAssetOfBtc</c>"] Total net asset value in BTC.
        /// </summary>
        [JsonPropertyName("totalNetAssetOfBtc")]
        public decimal TotalNetAssetOfBtc { get; set; }
        /// <summary>
        /// ["<c>marginTradeCoeffVo</c>"] Trade details
        /// </summary>
        [JsonPropertyName("marginTradeCoeffVo")]
        public BinanceMarginTradeCoeff? MarginTradeCoeff { get; set; }
        /// <summary>
        /// ["<c>marginUserAssetVoList</c>"] Asset list
        /// </summary>
        [JsonPropertyName("marginUserAssetVoList")]
        public BinanceMarginBalance[] MarginUserAssets { get; set; } = Array.Empty<BinanceMarginBalance>();
    }

    /// <summary>
    /// Margin trade detail
    /// </summary>
    public record BinanceMarginTradeCoeff
    {
        /// <summary>
        /// ["<c>forceLiquidationBar</c>"] The forced liquidation margin ratio.
        /// </summary>
        [JsonPropertyName("forceLiquidationBar")]
        public decimal ForceLiquidationBar { get; set; }
        /// <summary>
        /// ["<c>marginCallBar</c>"] The margin call ratio.
        /// </summary>
        [JsonPropertyName("marginCallBar")]
        public decimal MarginCallBar { get; set; }
        /// <summary>
        /// ["<c>normalBar</c>"] Initial margin ratio
        /// </summary>
        [JsonPropertyName("normalBar")]
        public decimal NormalBar { get; set; }
    }
}

