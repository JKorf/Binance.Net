using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Fiat
{
    /// <summary>
    /// Fiat payment info
    /// </summary>
    public class BinanceFiatWithdrawDeposit
    {
        /// <summary>
        /// Order number
        /// </summary>
        [JsonProperty("orderNo")]
        public string OrderNumber { get; set; } = string.Empty;
        /// <summary>
        /// The used currency
        /// </summary>
        public string FiatCurrency { get; set; } = string.Empty;
        /// <summary>
        /// The amount
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// The indicated amount
        /// </summary>
        public decimal IndicatedAmount { get; set; }
        /// <summary>
        /// The crypto currency
        /// </summary>
        public string Method { get; set; } = string.Empty;
        /// <summary>
        /// The total fee of the order
        /// </summary>
        public decimal TotalFee { get; set; }
        /// <summary>
        /// The status 
        /// </summary>
        [JsonConverter(typeof(FiatWithdrawDepositStatusConverter))]
        public FiatWithdrawDepositStatus Status { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Last update time
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdateTime { get; set; }
    }
}
