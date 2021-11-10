using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Spot.LendingData
{
    /// <summary>
    /// Crypto loan income info
    /// </summary>
    public class BinanceCryptoLoanIncome
    {
        /// <summary>
        /// Asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Income type
        /// </summary>
        [JsonConverter(typeof(LoanIncomeTypeConverter))]
        public LoanIncomeType Type { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonProperty("tranId")]
        public string TransactionId { get; set; }
    }
}
