﻿using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Brokerage account type
    /// </summary>
    public enum BrokerageAccountType
    {
        /// <summary> 
        /// Spot 
        /// </summary>
        [Map("SPOT")]
        Spot,
        /// <summary> 
        /// Futures USDT
        /// </summary>
        [Map("USDT_FUTURE")]
        FuturesUsdt,
        /// <summary>
        /// Futures Coin
        /// </summary>
        [Map("COIN_FUTURE")]
        FuturesCoin
    }
}