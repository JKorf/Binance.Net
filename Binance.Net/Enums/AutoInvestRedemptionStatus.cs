using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Redemption status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AutoInvestRedemptionStatus>))]
    public enum AutoInvestRedemptionStatus
    {
        /// <summary>
        /// Success
        /// </summary>
        [Map("SUCCESS")]
        Success,
        /// <summary>
        /// Failed
        /// </summary>
        [Map("FAILED")]
        Failed
    }
}
