using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Fiat payment info
    /// </summary>
    [SerializationModel]
    public record BinanceFiatPayment
    {
        /// <summary>
        /// ["<c>orderNo</c>"] The order number.
        /// </summary>
        [JsonPropertyName("orderNo")]
        public string OrderNumber { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>sourceAmount</c>"] The input quantity
        /// </summary>
        [JsonPropertyName("sourceAmount")]
        public decimal SourceQuantity { get; set; }
        /// <summary>
        /// ["<c>fiatCurrency</c>"] The fiat asset
        /// </summary>
        [JsonPropertyName("fiatCurrency")]
        public string FiatAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>obtainAmount</c>"] The output quantity
        /// </summary>
        [JsonPropertyName("obtainAmount")]
        public decimal ObtainQuantity { get; set; }
        /// <summary>
        /// ["<c>cryptoCurrency</c>"] The crypto asset
        /// </summary>
        [JsonPropertyName("cryptoCurrency")]
        public string CryptoAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>totalFee</c>"] The total fee of the order
        /// </summary>
        [JsonPropertyName("totalFee")]
        public decimal TotalFee { get; set; }
        /// <summary>
        /// ["<c>price</c>"] The price of the order
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>status</c>"] The status of the order
        /// </summary>
        [JsonPropertyName("status")]
        public FiatPaymentStatus Status { get; set; }
        /// <summary>
        /// ["<c>paymentMethod</c>"] The payment method
        /// </summary>
        [JsonPropertyName("paymentMethod")]
        public string PaymentMethod { get; set; } = string.Empty;
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

