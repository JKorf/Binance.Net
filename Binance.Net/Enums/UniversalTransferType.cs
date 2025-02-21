using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Universal transfer type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<UniversalTransferType>))] public  enum UniversalTransferType
    {
        /// <summary>
        /// Main (spot) to Funding
        /// </summary>
        [Map("MAIN_FUNDING")]
        MainToFunding,
        /// <summary>
        /// Main (spot) to Usd Futures
        /// </summary>
        [Map("MAIN_UMFUTURE")]
        MainToUsdFutures,
        /// <summary>
        /// Main (spot) to Coin Futures
        /// </summary>
        [Map("MAIN_CMFUTURE")]
        MainToCoinFutures,
        /// <summary>
        /// Main (spot) to Margin
        /// </summary>
        [Map("MAIN_MARGIN")]
        MainToMargin,
        /// <summary>
        /// Main (spot) to Mining
        /// </summary>
        [Map("MAIN_MINING")]
        MainToMining,
        /// <summary>
        /// Main to isolated margin
        /// </summary>
        [Map("MAIN_ISOLATED_MARGIN")]
        MainToIsolatedMargin,

        /// <summary>
        /// Funding to Main (spot)
        /// </summary>
        [Map("FUNDING_MAIN")]
        FundingToMain,
        /// <summary>
        /// Funding to Usd futures
        /// </summary>
        [Map("FUNDING_UMFUTURE")]
        FundingToUsdFutures,
        /// <summary>
        /// Funding to margin
        /// </summary>
        [Map("FUNDING_MARGIN")]
        FundingToMargin,

        /// <summary>
        /// Usd futures to Main (spot)
        /// </summary>
        [Map("UMFUTURE_MAIN")]
        UsdFuturesToMain,
        /// <summary>
        /// Usd futures to Funding
        /// </summary>
        [Map("UMFUTURE_FUNDING")]
        UsdFuturesToFunding,
        /// <summary>
        /// Usd futures to Margin
        /// </summary>
        [Map("UMFUTURE_MARGIN")]
        UsdFuturesToMargin,

        /// <summary>
        /// Coin futures to Main (spot)
        /// </summary>
        [Map("CMFUTURE_MAIN")]
        CoinFuturesToMain,
        /// <summary>
        /// Coin futures to Margin
        /// </summary>
        [Map("CMFUTURE_MARGIN")]
        CoinFuturesToMargin,

        /// <summary>
        /// Margin to Main (spot)
        /// </summary>
        [Map("MARGIN_MAIN")]
        MarginToMain,
        /// <summary>
        /// Margin to Usd futures
        /// </summary>
        [Map("MARGIN_UMFUTURE")]
        MarginToUsdFutures,
        /// <summary>
        /// Margin to Coin futures
        /// </summary>
        [Map("MARGIN_CMFUTURE")]
        MarginToCoinFutures,
        /// <summary>
        /// Margin to Mining
        /// </summary>
        [Map("MARGIN_MINING")]
        MarginToMining,
        /// <summary>
        /// Margin to Funding
        /// </summary>
        [Map("MARGIN_FUNDING")]
        MarginToFunding,

        /// <summary>
        /// Isolated margin to margin
        /// </summary>
        [Map("ISOLATEDMARGIN_MARGIN")]
        IsolatedMarginToMargin,
        /// <summary>
        /// Margin to isolated margin
        /// </summary>
        [Map("MARGIN_ISOLATEDMARGIN")]
        MarginToIsolatedMargin,
        /// <summary>
        /// Isolated margin to Isolated margin
        /// </summary>
        [Map("ISOLATEDMARGIN_ISOLATEDMARGIN")]
        IsolatedMarginToIsolatedMargin,
        /// <summary>
        /// Isolated margin to main
        /// </summary>
        [Map("ISOLATED_MARGIN_MAIN")]
        IsolatedMarginToMain,

        /// <summary>
        /// Mining to Main (spot)
        /// </summary>
        [Map("MINING_MAIN")]
        MiningToMain,
        /// <summary>
        /// Mining to Usd futures
        /// </summary>
        [Map("MINING_UMFUTURE")]
        MiningToUsdFutures,
        /// <summary>
        /// Mining to Margin
        /// </summary>
        [Map("MINING_MARGIN")]
        MiningToMargin,
        /// <summary>
        /// Funding to Coin futures
        /// </summary>
        [Map("FUNDING_CMFUTURE")]
        FundingToCoinFutures,
        /// <summary>
        /// Coin futures to Funding
        /// </summary>
        [Map("CMFUTURE_FUNDING")]
        CoinFuturesToFunding,

        /// <summary>
        /// Main to Option
        /// </summary>
        [Map("MAIN_OPTION")]
        MainToOption,
        /// <summary>
        /// Option to Main
        /// </summary>
        [Map("OPTION_MAIN")]
        OptionToMain,
        /// <summary>
        /// Usd Futures to Option
        /// </summary>
        [Map("UMFUTURE_OPTION")]
        UsdFuturesToOption,
        /// <summary>
        /// Option to Usd Futures
        /// </summary>
        [Map("OPTION_UMFUTURE")]
        OptionToUsdFutures,
        /// <summary>
        /// Margin to Option
        /// </summary>
        [Map("MARGIN_OPTION")]
        MarginToOption,
        /// <summary>
        /// Option to Margin
        /// </summary>
        [Map("OPTION_MARGIN")]
        OptionToMargin,
        /// <summary>
        /// Funding to Option
        /// </summary>
        [Map("FUNDING_OPTION")]
        FundingToOption,
        /// <summary>
        /// Option to Funding
        /// </summary>
        [Map("OPTION_FUNDING")]
        OptionToFunding
    }
}