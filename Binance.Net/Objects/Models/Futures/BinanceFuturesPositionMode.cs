namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// User's position mode
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesPositionMode
    {
        /// <summary>
        /// ["<c>dualSidePosition</c>"] Whether hedge mode is enabled (`true`) or one-way mode is used (`false`).
        /// </summary>
        [JsonPropertyName("dualSidePosition")]
        public bool IsHedgeMode { get; set; }
    }
}

