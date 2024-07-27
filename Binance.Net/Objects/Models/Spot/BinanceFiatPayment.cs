using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Fiat payment info
    /// </summary>
    public record BinanceFiatPayment
    {
        /// <summary>
        /// Order number
        /// </summary>
        [JsonPropertyName("orderNo")]
        public string OrderNumber { get; set; } = string.Empty;
        /// <summary>
        /// The input quantity
        /// </summary>
        [JsonPropertyName("sourceAmount")]
        public decimal SourceQuantity { get; set; }
        /// <summary>
        /// The fiat asset
        /// </summary>
        [JsonPropertyName("fiatCurrency")]
        public string FiatAsset { get; set; } = string.Empty;
        /// <summary>
        /// The output quantity
        /// </summary>
        [JsonPropertyName("obtainAmount")]
        public decimal ObtainQuantity { get; set; }
        /// <summary>
        /// The crypto asset
        /// </summary>
        [JsonPropertyName("cryptoCurrency")]
        public string CryptoAsset { get; set; } = string.Empty;
        /// <summary>
        /// The total fee of the order
        /// </summary>
        [JsonPropertyName("totalFee")]
        public decimal TotalFee { get; set; }
        /// <summary>
        /// The price of the order
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// The status of the order
        /// </summary>
        [JsonPropertyName("status")]
        public FiatPaymentStatus Status { get; set; }
        /// <summary>
        /// The payment method
        /// </summary>
        [JsonPropertyName("paymentMethod")]
        public string PaymentMethod { get; set; } = string.Empty;
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Last update time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }
    }
}
