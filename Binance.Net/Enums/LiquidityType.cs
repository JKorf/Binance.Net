﻿using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Add/Remove liquidity type
    /// </summary>
    [JsonConverter(typeof(PocAOTEnumConverter<LiquidityType>))] public  enum LiquidityType
    {
        /// <summary>
        /// Add/Remove single asset
        /// </summary>
        [Map("SINGLE")]
        Single,
        /// <summary>
        /// Add/Remove combination of all coins
        /// </summary>
        [Map("COMBINATION")]
        Combined
    }
}
