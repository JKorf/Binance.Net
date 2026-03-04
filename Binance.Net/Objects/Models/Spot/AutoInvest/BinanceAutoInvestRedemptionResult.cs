namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Redemption result
    /// </summary>
    [SerializationModel]
    public record BinanceAutoInvestRedemptionResult
    {
        /// <summary>
        /// The redemption identifier.
        /// </summary>
        [JsonPropertyName("redemptionId")]
        public long RedemptionId { get; set; }
    }

}
