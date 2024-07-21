using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Fiat payment info
    /// </summary>
    public record BinanceFiatWithdrawDeposit
    {
        /// <summary>
        /// Order number
        /// </summary>
        [JsonPropertyName("orderNo")]
        public string OrderNumber { get; set; } = string.Empty;
        /// <summary>
        /// The used asset
        /// </summary>
        [JsonPropertyName("fiatCurrency")]
        public string FiatAsset { get; set; } = string.Empty;
        /// <summary>
        /// The quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The indicated quantity
        /// </summary>
        [JsonPropertyName("indicatedAmount")]
        public decimal IndicatedQuantity { get; set; }
        /// <summary>
        /// The method
        /// </summary>
        public string Method { get; set; } = string.Empty;
        /// <summary>
        /// The total fee of the order
        /// </summary>
        public decimal TotalFee { get; set; }
        /// <summary>
        /// The status 
        /// </summary>
        public FiatWithdrawDepositStatus Status { get; set; }
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
