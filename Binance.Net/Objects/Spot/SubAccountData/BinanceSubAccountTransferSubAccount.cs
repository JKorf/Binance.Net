using System;
using System.Collections.Generic;
using System.Text;
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
        public string CounterParty { get; set; } = "";
        /// <summary>
        /// Email of the account
        /// </summary>
        public string Email { get; set; } = "";
        /// <summary>
        /// Transfer type
        /// </summary>
        [JsonConverter(typeof(SubAccountTransferSubAccountTypeConverter))]
        public SubAccountTransferSubAccountType Type { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        public string Asset { get; set; } = "";
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
