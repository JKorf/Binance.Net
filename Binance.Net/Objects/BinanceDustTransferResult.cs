using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Result of dust transfer
    /// </summary>
    public class BinanceDustTransferResult
    {
        /// <summary>
        /// Total service charge
        /// </summary>
        [JsonProperty("totalServiceCharge")]
        public decimal TotalServiceCharge { get; set; }
        /// <summary>
        /// Total transferred
        /// </summary>
        [JsonProperty("totalTransfered")]
        public decimal TotalTransferred { get; set; }
        /// <summary>
        /// Transfer entries
        /// </summary>
        public IEnumerable<BinanceDustTransferResultEntry> TransferResult { get; set; } = new List<BinanceDustTransferResultEntry>();
    }

    /// <summary>
    /// Dust transfer entry
    /// </summary>
    public class BinanceDustTransferResultEntry
    {
        /// <summary>
        /// Amount of dust
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        public string FromAsset { get; set; } = "";
        /// <summary>
        /// Timestamp of conversion
        /// </summary>
        [JsonConverter(typeof(TimestampConverter)), JsonProperty("operateTime")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Service charge
        /// </summary>
        public decimal ServiceChargeAmount { get; set; }
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonProperty("tranId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// BNB result amount
        /// </summary>
        [JsonProperty("TransferedAmount")]
        public decimal TransferredAmount { get; set; }
    }
}
