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
        /// Email of the account
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Margin level
        /// </summary>
        [JsonPropertyName("marginLevel")]
        public decimal MarginLevel { get; set; }
        /// <summary>
        /// Total asset in btc
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
        /// <summary>
        /// Trade details
        /// </summary>
        [JsonPropertyName("marginTradeCoeffVo")]
        public BinanceMarginTradeCoeff? MarginTradeCoeff { get; set; }
        /// <summary>
        /// Asset list
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
        /// Liquidation margin ratio
        /// </summary>
        [JsonPropertyName("forceLiquidationBar")]
        public decimal ForceLiquidationBar { get; set; }
        /// <summary>
        /// Margin record margin ratio
        /// </summary>
        [JsonPropertyName("marginCallBar")]
        public decimal MarginCallBar { get; set; }
        /// <summary>
        /// Initial margin ratio
        /// </summary>
        [JsonPropertyName("normalBar")]
        public decimal NormalBar { get; set; }
    }
}
