namespace Binance.Net.Objects.Models.Spot.CopyTrading
{
    /// <summary>
    /// Copy trading user status
    /// </summary>
    [SerializationModel]
    public record BinanceCopyTradingUserStatus
    {
        /// <summary>
        /// ["<c>isLeadTrader</c>"] Is lead trader
        /// </summary>
        [JsonPropertyName("isLeadTrader")]
        public bool IsLeadTrader { get; set; }
        /// <summary>
        /// ["<c>time</c>"] Time
        /// </summary>
        [JsonPropertyName("time")]
        public long Timestamp { get; set; }
    }
}

