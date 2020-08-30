﻿using System;
using System.Collections.Generic;
using System.Text;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Futures.FuturesData
{
    /// <summary>
    /// Basis info
    /// </summary>
    public class BinanceFuturesBasis
    {
        /// <summary>
        /// The pair
        /// </summary>
        public string Pair { get; set; } = "";
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonConverter(typeof(ContractTypeConverter))]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// Futures price
        /// </summary>
        public decimal FuturesPrice { get; set; }
        /// <summary>
        /// Index price
        /// </summary>
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// Basis
        /// </summary>
        public decimal Basis { get; set; }
        /// <summary>
        /// Basis rate
        /// </summary>
        public decimal BasisRate { get; set; }
        /// <summary>
        /// Data timestamp
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
    }
}
