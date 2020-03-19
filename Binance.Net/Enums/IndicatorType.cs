using Binance.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Types of indicators
    /// </summary>
    [JsonConverter(typeof(IndicatorTypeConverter))]
    public enum IndicatorType
    {
        /// <summary>
        /// Unfilled ratio
        /// </summary>
        UnfilledRatio,
        /// <summary>
        /// Expired orders ratio
        /// </summary>
        ExpirationRatio,
        /// <summary>
        /// Cancelled orders ratio
        /// </summary>
        CancellationRatio
    }
}
