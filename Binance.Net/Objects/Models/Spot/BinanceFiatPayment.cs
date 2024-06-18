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
        [JsonProperty("orderNo")]
        public string OrderNumber { get; set; } = string.Empty;
        /// <summary>
        /// The input quantity
        /// </summary>
        [JsonProperty("sourceAmount")]
        public decimal SourceQuantity { get; set; }
        /// <summary>
        /// The fiat asset
        /// </summary>
        [JsonProperty("fiatCurrency")]
        public string FiatAsset { get; set; } = string.Empty;
        /// <summary>
        /// The output quantity
        /// </summary>
        [JsonProperty("obtainAmount")]
        public decimal ObtainQuantity { get; set; }
        /// <summary>
        /// The crypto asset
        /// </summary>
        [JsonProperty("cryptoCurrency")]
        public string CryptoAsset { get; set; } = string.Empty;
        /// <summary>
        /// The total fee of the order
        /// </summary>
        public decimal TotalFee { get; set; }
        /// <summary>
        /// The price of the order
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// The status of the order
        /// </summary>
        [JsonConverter(typeof(FiatPaymentStatusConverter))]
        public FiatPaymentStatus Status { get; set; }
        /// <summary>
        /// The payment method
        /// </summary>
        [JsonProperty("paymentMethod")]
        public string PaymentMethod { get; set; } = string.Empty;
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Last update time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateTime { get; set; }
    }
}
