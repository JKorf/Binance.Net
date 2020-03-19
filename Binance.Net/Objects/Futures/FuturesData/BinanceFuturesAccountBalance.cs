using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects.Futures.FuturesData
{
    /// <summary>
    /// Information about an account
    /// </summary>
    public class BinanceFuturesAccountBalance
    {
        /// <summary>
        /// Account alias
        /// </summary>
        public string AccountAlias { get; set; }
        /// <summary>
        /// The asset this balance is for
        /// </summary>
        public string? Asset { get; set; }
        /// <summary>
        /// The total balance of this asset
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// The total balance available for withdraw for this asset
        /// </summary>
        public decimal WithdrawAvailable { get; set; }

        /// <summary>
        /// The time of balance was updated
        /// </summary>
        [JsonProperty("updateTime"), JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdateTime { get; set; }
    }

}
