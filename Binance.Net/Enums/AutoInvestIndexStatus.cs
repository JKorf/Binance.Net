using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Index status
    /// </summary>
    [JsonConverter(typeof(PocAOTEnumConverter<AutoInvestIndexStatus>))] public  enum AutoInvestIndexStatus
    {
        /// <summary>
        /// Running
        /// </summary>
        [Map("RUNNING")]
        Running,
        /// <summary>
        /// Rebalancing
        /// </summary>
        [Map("REBALANCING")]
        Rebalancing,
        /// <summary>
        /// Paused
        /// </summary>
        [Map("PAUSED")]
        Paused
    }
}
