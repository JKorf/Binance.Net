﻿using System;
using System.Collections.Generic;
using System.Text;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.SubAccountData
{
    internal class BinanceSubAccountUniversalTransfersList
    {
        /// <summary>
        /// Transactions
        /// </summary>
        public IEnumerable<BinanceSubAccountUniversalTransferTransaction> Transactions { get; set; } =
            new List<BinanceSubAccountUniversalTransferTransaction>();

    }

    /// <summary>
    /// Binance sub account universal transaction
    /// </summary>
    public class BinanceSubAccountUniversalTransferTransaction
    {
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonProperty("transId")]
        public long TransactionId { get; set; }

        /// <summary>
        /// From email
        /// </summary>
        public string FromEmail { get; set; } = "";

        /// <summary>
        /// To email
        /// </summary>
        public string ToEmail { get; set; } = "";

        /// <summary>
        /// From account type
        /// </summary>
        [JsonConverter(typeof(BrokerageAccountTypeConverter))]
        public BrokerageAccountType FromAccountType { get; set; }

        /// <summary>
        /// To account type
        /// </summary>
        [JsonConverter(typeof(BrokerageAccountTypeConverter))]
        public BrokerageAccountType ToAccountType { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; } = "";

        /// <summary>
        /// Asset
        /// </summary>
        public string Asset { get; set; } = "";

        /// <summary>
        /// Amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// The time the universal transaction was created
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        [JsonProperty("createTimeStamp")]
        public DateTime CreateTime { get; set; }
    }
}
