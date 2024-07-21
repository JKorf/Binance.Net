using Binance.Net.Objects.Models.Spot.Margin;

namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub account margin trade details
    /// </summary>
    public record BinanceSubAccountMarginDetails
    {
        /// <summary>
        /// Email of the account
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Margin level
        /// </summary>
        public decimal MarginLevel { get; set; }
        /// <summary>
        /// Total asset in btc
        /// </summary>
        public decimal TotalAssetOfBtc { get; set; }
        /// <summary>
        /// Total liability
        /// </summary>
        public decimal TotalLiabilityOfBtc { get; set; }
        /// <summary>
        /// Total net asset
        /// </summary>
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
        public IEnumerable<BinanceMarginBalance> MarginUserAssets { get; set; } = Array.Empty<BinanceMarginBalance>();
    }

    /// <summary>
    /// Margin trade detail
    /// </summary>
    public record BinanceMarginTradeCoeff
    {
        /// <summary>
        /// Liquidation margin ratio
        /// </summary>
        public decimal ForceLiquidationBar { get; set; }
        /// <summary>
        /// Margin record margin ratio
        /// </summary>
        public decimal MarginCallBar { get; set; }
        /// <summary>
        /// Initial margin ratio
        /// </summary>
        public decimal NormalBar { get; set; }
    }
}
