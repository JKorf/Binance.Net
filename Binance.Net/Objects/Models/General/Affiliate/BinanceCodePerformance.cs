namespace Binance.Net.Objects.Models.General.Affiliate
{
    /// <summary>
    /// Binance Affiliate Code Performance
    /// </summary>
    [SerializationModel]
    public record BinanceCodePerformance
    {
        /// <summary>
        /// Code creation timestamp (ms since epoch)
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Custom note associated with this invitee
        /// </summary>
        [JsonPropertyName("note")]
        public string Note { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("clicks")]
        public int Clicks { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("referrals")]
        public int Referrals { get; set; }
        /// <summary>
        /// Total trading volume (formatted as decimal string)
        /// </summary>
        [JsonPropertyName("tradeVol")]
        public decimal TradeVolume { get; set; }
        /// <summary>
        /// Total commission earned from this invitee (formatted as decimal string)
        /// </summary>
        [JsonPropertyName("commission")]
        public decimal Commission { get; set; }
    }
}
