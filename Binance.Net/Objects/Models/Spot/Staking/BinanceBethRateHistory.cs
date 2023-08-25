using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Rate history
    /// </summary>
    public class BinanceBethRateHistory
    {
        /// <summary>
        /// Exchange rate
        /// </summary>
        [JsonProperty("exchangeRate")]
        public decimal ExchangeRate { get; set; }
        /// <summary>
        /// Anual percentage rate
        /// </summary>
        [JsonProperty("annualPercentageRate")]
        public decimal AnnualPercentageRate { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }
    }
}
