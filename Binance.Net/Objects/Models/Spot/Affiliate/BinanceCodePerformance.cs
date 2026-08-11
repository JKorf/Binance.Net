namespace Binance.Net.Objects.Models.Spot.Affiliate
{
    [SerializationModel]
    public record BinanceAffiliateResponse<T>
    {
        /// <summary>
        /// The data returned by the API
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }
        /// <summary>
        /// The data returned by the API
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }
        /// <summary>
        /// The data returned by the API
        /// </summary>
        [JsonPropertyName("code")]
        public string code { get; set; }
        /// <summary>
        /// The data returned by the API
        /// </summary>
        [JsonPropertyName("data")]
        public T Data { get; set; } 
    }

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
        public string Note { get; set; }
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
        public string TradeVolume { get; set; }
        /// <summary>
        /// Total commission earned from this invitee (formatted as decimal string)
        /// </summary>
        [JsonPropertyName("commission")]
        public string Commission { get; set; }
    }

}
