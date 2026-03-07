namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Sub Account
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageSubAccount : BinanceBrokerageSubAccountCommission
    {
        /// <summary>
        /// ["<c>email</c>"] Email
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>tag</c>"] Tag
        /// </summary>
        [JsonPropertyName("tag")]
        public string Tag { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>createTime</c>"] Create Date
        /// </summary>
        [JsonPropertyName("createTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateDate { get; set; }
    }
}