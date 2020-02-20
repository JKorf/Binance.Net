using Newtonsoft.Json;
using System;
using CryptoExchange.Net.Converters;
using Binance.Net.Converters;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Futures income history result
    /// </summary>
    public class BinanceFuturesIncomeHistory
    {
        /// <summary>
        /// Symbol for the resulting income history, may be null if not associated with a trading pair
        /// </summary>
        public string? Symbol { get; set; }
        /// <summary>
        /// Type of income
        /// </summary>
        [JsonProperty("incomeType"), JsonConverter(typeof(IncomeTypeConverter))]
        public IncomeType IncomeType { get; set; }
        /// <summary>
        /// Amount of income
        /// </summary>
        public decimal Income { get; set; }
        /// <summary>
        /// Base asset for the income
        /// </summary>
        public string? Asset { get; set; }
        /// <summary>
        /// Additional info
        /// </summary>
        public string? Info { get; set; }
        /// <summary>
        /// Time of the income
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Time { get; set; }
    }

}
