namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// User's position mode
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesPositionMode
    {
        /// <summary>
        /// true": Hedge Mode mode; "false": One-way Mode
        /// </summary>
        [JsonPropertyName("dualSidePosition")]
        public bool IsHedgeMode { get; set; }
    }
}
