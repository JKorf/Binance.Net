namespace Binance.Net.Enums
{
    /// <summary>
    /// Universal transfer type
    /// </summary>
    public enum UniversalTransferType
    {
        /// <summary>
        /// Main (spot) to Funding
        /// </summary>
        MainToFunding,
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
        /// Funding to Main (spot)
        /// </summary>
        FundingToMain,
        /// <summary>
        /// Funding to Usd futures
        /// </summary>
        FundingToUsdFutures,
        /// <summary>
        /// Funding to margin
        /// </summary>
        FundingToMargin,
        
        /// <summary>
        /// Usd futures to Main (spot)
        /// </summary>
        UsdFuturesToMain,
        /// <summary>
        /// Usd futures to Funding
        /// </summary>
        UsdFuturesToFunding,
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
        /// Margin to Funding
        /// </summary>
        MarginToFunding,

        /// <summary>
        /// Isolated margin to margin
        /// </summary>
        IsolatedMarginToMargin,
        /// <summary>
        /// Margin to isolated margin
        /// </summary>
        MarginToIsolatedMargin,
        /// <summary>
        /// Isolated margin to Isolated margin
        /// </summary>
        IsolatedMarginToIsolatedMargin,

        /// <summary>
        /// Mining to Main (spot)
        /// </summary>
        MiningToMain,
        /// <summary>
        /// Mining to Usd futures
        /// </summary>
        MiningToUsdFutures,
        /// <summary>
        /// Mining to Margin
        /// </summary>
        MiningToMargin,
        /// <summary>
        /// Funding to Coin futures
        /// </summary>
        FundingToCoinFutures,
        /// <summary>
        /// Coin futures to Funding
        /// </summary>
        CoinFuturesToFunding,
    }
}
