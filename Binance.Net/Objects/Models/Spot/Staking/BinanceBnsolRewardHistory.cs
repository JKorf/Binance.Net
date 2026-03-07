namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// SOL rewards history
    /// </summary>
    [SerializationModel]
    public record BinanceBnsolRewardHistory
    {
        /// <summary>
        /// ["<c>time</c>"] The timestamp.
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>token</c>"] Asset name
        /// </summary>
        [JsonPropertyName("token")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Amount
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        /// <summary>
        /// ["<c>bnsolHolding</c>"] BN SOL holding
        /// </summary>
        [JsonPropertyName("bnsolHolding")]
        public decimal BnSolHolding { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
    }
}

