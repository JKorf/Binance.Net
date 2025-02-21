using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Execution type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AutoInvestExecutionType>))] public  enum AutoInvestExecutionType
    {
        /// <summary>
        /// One time
        /// </summary>
        [Map("ONE_TIME")]
        OneTime,
        /// <summary>
        /// Recurring
        /// </summary>
        [Map("RECURRING")]
        Recurring
    }
}
