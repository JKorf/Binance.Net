using System;
using System.Collections.Generic;
using System.Text;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Blvt
{
    /// <summary>
    /// Leveraged token info
    /// </summary>
    public class BinanceBlvtInfo
    {
        /// <summary>
        /// Name of the token
        /// </summary>
        public string TokenName { get; set; } = "";
        /// <summary>
        /// Description of the token
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// Underlying asset
        /// </summary>
        public string Underlying { get; set; } = "";
        /// <summary>
        /// Token issued
        /// </summary>
        public decimal TokenIssued { get; set; }
        /// <summary>
        /// Basket
        /// </summary>
        public string Basket { get; set; } = "";
        /// <summary>
        /// Nav
        /// </summary>
        public decimal Nav { get; set; }
        /// <summary>
        /// Real leverage
        /// </summary>
        public decimal RealLeverage { get; set; }
        /// <summary>
        /// Funding rate
        /// </summary>
        public decimal FundingRate { get; set; }
        /// <summary>
        /// Daily management fee
        /// </summary>
        public decimal DailyManagementFee { get; set; }
        /// <summary>
        /// Data timestamp
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
    }
}
