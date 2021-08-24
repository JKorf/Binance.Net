using System;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.BSwap
{
    /// <summary>
    /// Operation record
    /// </summary>
    public class BinanceBSwapOperation
    {
        /// <summary>
        /// Operation id
        /// </summary>
        public long OperationId { get; set; }
        /// <summary>
        /// Pool id
        /// </summary>
        public string PoolId { get; set; } = string.Empty;

        /// <summary>
        /// Pool name
        /// </summary>
        public string PoolName { get; set; } = string.Empty;
        /// <summary>
        /// Operation
        /// </summary>
        [JsonConverter(typeof(BSwapOperationConverter))]
        public BSwapOperation Operation { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonConverter(typeof(BSwapStatusConverter))]
        public BSwapStatus Status { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// Share amount
        /// </summary>
        public decimal ShareAmount { get; set; }
    }
}
