using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Fiat payment info
    /// </summary>
    [SerializationModel]
    public record BinanceFiatWithdrawDeposit
    {
        /// <summary>
        /// ["<c>orderNo</c>"] The order number.
        /// </summary>
        [JsonPropertyName("orderNo")]
        public string OrderNumber { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fiatCurrency</c>"] The used asset
        /// </summary>
        [JsonPropertyName("fiatCurrency")]
        public string FiatAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] The quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>indicatedAmount</c>"] The indicated quantity
        /// </summary>
        [JsonPropertyName("indicatedAmount")]
        public decimal IndicatedQuantity { get; set; }
        /// <summary>
        /// ["<c>method</c>"] The method
        /// </summary>
        [JsonPropertyName("method")]
        public string Method { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>totalFee</c>"] The total fee of the order
        /// </summary>
        [JsonPropertyName("totalFee")]
        public decimal TotalFee { get; set; }
        /// <summary>
        /// ["<c>status</c>"] The status 
        /// </summary>
        [JsonPropertyName("status")]
        public FiatWithdrawDepositStatus Status { get; set; }
        /// <summary>
        /// ["<c>createTime</c>"] The creation time.
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>updateTime</c>"] Last update time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }
    }
}

