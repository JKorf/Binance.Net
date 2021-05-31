using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Universal transfer type
    /// </summary>
    public enum UniversalTransferType
    {
        /// <summary>
        /// Main (spot) to C2C
        /// </summary>
        MainToC2C,
        /// <summary>
        /// Main (spot) to Usd Futures
        /// </summary>
        MainToUsdFutures,
        /// <summary>
        /// Main (spot) to Coin Futures
        /// </summary>
        MainToCoinFutures,
        /// <summary>
        /// Main (spot) to Margin
        /// </summary>
        MainToMargin,
        /// <summary>
        /// Main (spot) to Mining
        /// </summary>
        MainToMining,

        /// <summary>
        /// C2C to Main (spot)
        /// </summary>
        C2CToMain,
        /// <summary>
        /// C2C to Usd futures
        /// </summary>
        C2CToUsdFutures,
        /// <summary>
        /// C2C to mining
        /// </summary>
        C2CToMining,
        /// <summary>
        /// C2C to margin
        /// </summary>
        C2CToMargin,
        
        /// <summary>
        /// Usd futures to Main (spot)
        /// </summary>
        UsdFuturesToMain,
        /// <summary>
        /// Usd futures to C2C
        /// </summary>
        UsdFuturesToC2C,
        /// <summary>
        /// Usd futures to Margin
        /// </summary>
        UsdFuturesToMargin,

        /// <summary>
        /// Coin futures to Main (spot)
        /// </summary>
        CoinFuturesToMain,
        /// <summary>
        /// Coin futures to Margin
        /// </summary>
        CoinFuturesToMargin,

        /// <summary>
        /// Margin to Main (spot)
        /// </summary>
        MarginToMain,
        /// <summary>
        /// Margin to Usd futures
        /// </summary>
        MarginToUsdFutures,
        /// <summary>
        /// Margin to Coin futures
        /// </summary>
        MarginToCoinFutures,
        /// <summary>
        /// Margin to Mining
        /// </summary>
        MarginToMining,
        /// <summary>
        /// Margin to C2C
        /// </summary>
        MarginToC2C,

        /// <summary>
        /// Mining to Main (spot)
        /// </summary>
        MiningToMain,
        /// <summary>
        /// Mining to Usd futures
        /// </summary>
        MiningToUsdFutures,
        /// <summary>
        /// Mining to C2C
        /// </summary>
        MiningToC2C,
        /// <summary>
        /// Mining to Margin
        /// </summary>
        MiningToMargin,

        /// <summary>
        /// Main to pay
        /// </summary>
        MainToPay,
        /// <summary>
        /// Pay to main
        /// </summary>
        PayToMain        
    }
}
