namespace Binance.Net.Objects.Models.General.Affiliate
{
    /// <summary>
    /// Binance Invitee Performance
    /// </summary>
    [SerializationModel]
    public record BinanceInviteePerformance
    {
        /// <summary>
        /// Registration timestamp (ms since epoch)
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("regTime")]
        public DateTime RegistrationTime { get; set; }
        /// <summary>
        /// The referral code used by this invitee
        /// </summary>
        [JsonPropertyName("referralCode")]
        public string ReferralCode { get; set; } = string.Empty;
        /// <summary>
        /// Custom note associated with this invitee
        /// </summary>
        [JsonPropertyName("note")]
        public string Note { get; set; } = string.Empty;
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
        /// <summary>
        /// KYC completion timestamp (ms since epoch)
        /// </summary>
        [JsonPropertyName("kycTime")]
        public long KYCTime { get; set; }
        /// <summary>
        /// First deposit timestamp (ms since epoch)
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("firstDepositTime")]
        public DateTime FirstDepositTime { get; set; }
        /// <summary>
        /// First trade timestamp (ms since epoch)
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("firstTradeTime")]
        public DateTime FirstTradeTime { get; set; }
        /// <summary>
        /// VIP level: 0, 1, 2, or 3 and above
        /// </summary>
        [JsonPropertyName("vipLevel")]
        public int VIPLevel { get; set; }
        /// <summary>
        /// Invitee's spot trading rebate rate (as percentage)
        /// </summary>
        [JsonPropertyName("inviteeSpotRate")]
        public string InviteeSpotRate { get; set; } = string.Empty;
        /// <summary>
        /// Invitee's futures trading rebate rate (as percentage)
        /// </summary>
        [JsonPropertyName("inviteeFuturesRate")]
        public string InviteeFuturesRate { get; set; } = string.Empty;
    }
}
