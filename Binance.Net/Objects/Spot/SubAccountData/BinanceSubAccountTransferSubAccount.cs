using System;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.SubAccountData
{
    /// <summary>
    /// Sub account historic transfer
    /// </summary>
    public class BinanceSubAccountTransferSubAccount
    {
        /// <summary>
        /// Counter party of the transfer
        /// </summary>
        public string CounterParty { get; set; } = string.Empty;
        /// <summary>
        /// Email of the account
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// From account type
        /// </summary>
        public string FromAccountType { get; set; } = string.Empty;
        /// <summary>
        /// To account type
        /// </summary>
        public string ToAccountType { get; set; } = string.Empty;
        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Transfer type
        /// </summary>
        [JsonConverter(typeof(SubAccountTransferSubAccountTypeConverter))]
        public SubAccountTransferSubAccountType Type { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonProperty("tranId")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Timestamp of the transfer
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
    }
}
