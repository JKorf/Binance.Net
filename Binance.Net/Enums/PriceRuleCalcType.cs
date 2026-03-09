using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Price calc type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PriceRuleCalcType>))]
    public enum PriceRuleCalcType
    {
        /// <summary>
        /// ["<c>ARITHMETIC_MEAN</c>"] Calculated by matching engine
        /// </summary>
        [Map("ARITHMETIC_MEAN")]
        ArithmeticMean,
        /// <summary>
        /// ["<c>EXTERNAL</c>"] Calculated outside the matching engine
        /// </summary>
        [Map("EXTERNAL")]
        External,
    }

}

