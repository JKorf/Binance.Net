using System;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Futures
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
        /// Type of income as string
        /// </summary>
        [JsonProperty("incomeType")]
        public string? IncomeTypeString { get; set; }

        /// <summary>
        /// Type of income
        /// </summary>
        public IncomeType? IncomeType => IncomeTypeString != null ? new IncomeTypeConverter().ReadString(IncomeTypeString) : (IncomeType?)null;

        /// <summary>
        /// Quantity of income
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
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Transaction id if relevant
        /// </summary>
        [JsonProperty("tranId")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// Trade id if existing
        /// </summary>
        public string TradeId { get; set; } = string.Empty;
    }

}
