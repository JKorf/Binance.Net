namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// BNB burn for fee reduction status
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesBnbBurnStatus
    {
        /// <summary>
        /// ["<c>feeBurn</c>"] Fee burn status
        /// </summary>
        [JsonPropertyName("feeBurn")]
        public bool FeeBurn { get; set; }
    }
}

