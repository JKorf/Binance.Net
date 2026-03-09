using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Universal transfer type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<UniversalTransferType>))]
    public enum UniversalTransferType
    {
        /// <summary>
        /// ["<c>MAIN_FUNDING</c>"] Main (spot) to Funding
        /// </summary>
        [Map("MAIN_FUNDING")]
        MainToFunding,
        /// <summary>
        /// ["<c>MAIN_UMFUTURE</c>"] Main (spot) to Usd Futures
        /// </summary>
        [Map("MAIN_UMFUTURE")]
        MainToUsdFutures,
        /// <summary>
        /// ["<c>MAIN_CMFUTURE</c>"] Main (spot) to Coin Futures
        /// </summary>
        [Map("MAIN_CMFUTURE")]
        MainToCoinFutures,
        /// <summary>
        /// ["<c>MAIN_MARGIN</c>"] Main (spot) to Margin
        /// </summary>
        [Map("MAIN_MARGIN")]
        MainToMargin,
        /// <summary>
        /// ["<c>MAIN_MINING</c>"] Main (spot) to Mining
        /// </summary>
        [Map("MAIN_MINING")]
        MainToMining,
        /// <summary>
        /// ["<c>MAIN_ISOLATED_MARGIN</c>"] Main to isolated margin
        /// </summary>
        [Map("MAIN_ISOLATED_MARGIN")]
        MainToIsolatedMargin,

        /// <summary>
        /// ["<c>FUNDING_MAIN</c>"] Funding to Main (spot)
        /// </summary>
        [Map("FUNDING_MAIN")]
        FundingToMain,
        /// <summary>
        /// ["<c>FUNDING_UMFUTURE</c>"] Funding to Usd futures
        /// </summary>
        [Map("FUNDING_UMFUTURE")]
        FundingToUsdFutures,
        /// <summary>
        /// ["<c>FUNDING_MARGIN</c>"] Funding to margin
        /// </summary>
        [Map("FUNDING_MARGIN")]
        FundingToMargin,

        /// <summary>
        /// ["<c>UMFUTURE_MAIN</c>"] Usd futures to Main (spot)
        /// </summary>
        [Map("UMFUTURE_MAIN")]
        UsdFuturesToMain,
        /// <summary>
        /// ["<c>UMFUTURE_FUNDING</c>"] Usd futures to Funding
        /// </summary>
        [Map("UMFUTURE_FUNDING")]
        UsdFuturesToFunding,
        /// <summary>
        /// ["<c>UMFUTURE_MARGIN</c>"] Usd futures to Margin
        /// </summary>
        [Map("UMFUTURE_MARGIN")]
        UsdFuturesToMargin,

        /// <summary>
        /// ["<c>CMFUTURE_MAIN</c>"] Coin futures to Main (spot)
        /// </summary>
        [Map("CMFUTURE_MAIN")]
        CoinFuturesToMain,
        /// <summary>
        /// ["<c>CMFUTURE_MARGIN</c>"] Coin futures to Margin
        /// </summary>
        [Map("CMFUTURE_MARGIN")]
        CoinFuturesToMargin,

        /// <summary>
        /// ["<c>MARGIN_MAIN</c>"] Margin to Main (spot)
        /// </summary>
        [Map("MARGIN_MAIN")]
        MarginToMain,
        /// <summary>
        /// ["<c>MARGIN_UMFUTURE</c>"] Margin to Usd futures
        /// </summary>
        [Map("MARGIN_UMFUTURE")]
        MarginToUsdFutures,
        /// <summary>
        /// ["<c>MARGIN_CMFUTURE</c>"] Margin to Coin futures
        /// </summary>
        [Map("MARGIN_CMFUTURE")]
        MarginToCoinFutures,
        /// <summary>
        /// ["<c>MARGIN_MINING</c>"] Margin to Mining
        /// </summary>
        [Map("MARGIN_MINING")]
        MarginToMining,
        /// <summary>
        /// ["<c>MARGIN_FUNDING</c>"] Margin to Funding
        /// </summary>
        [Map("MARGIN_FUNDING")]
        MarginToFunding,

        /// <summary>
        /// ["<c>ISOLATEDMARGIN_MARGIN</c>"] Isolated margin to margin
        /// </summary>
        [Map("ISOLATEDMARGIN_MARGIN")]
        IsolatedMarginToMargin,
        /// <summary>
        /// ["<c>MARGIN_ISOLATEDMARGIN</c>"] Margin to isolated margin
        /// </summary>
        [Map("MARGIN_ISOLATEDMARGIN")]
        MarginToIsolatedMargin,
        /// <summary>
        /// ["<c>ISOLATEDMARGIN_ISOLATEDMARGIN</c>"] Isolated margin to Isolated margin
        /// </summary>
        [Map("ISOLATEDMARGIN_ISOLATEDMARGIN")]
        IsolatedMarginToIsolatedMargin,
        /// <summary>
        /// ["<c>ISOLATED_MARGIN_MAIN</c>"] Isolated margin to main
        /// </summary>
        [Map("ISOLATED_MARGIN_MAIN")]
        IsolatedMarginToMain,

        /// <summary>
        /// ["<c>MINING_MAIN</c>"] Mining to Main (spot)
        /// </summary>
        [Map("MINING_MAIN")]
        MiningToMain,
        /// <summary>
        /// ["<c>MINING_UMFUTURE</c>"] Mining to Usd futures
        /// </summary>
        [Map("MINING_UMFUTURE")]
        MiningToUsdFutures,
        /// <summary>
        /// ["<c>MINING_MARGIN</c>"] Mining to Margin
        /// </summary>
        [Map("MINING_MARGIN")]
        MiningToMargin,
        /// <summary>
        /// ["<c>FUNDING_CMFUTURE</c>"] Funding to Coin futures
        /// </summary>
        [Map("FUNDING_CMFUTURE")]
        FundingToCoinFutures,
        /// <summary>
        /// ["<c>CMFUTURE_FUNDING</c>"] Coin futures to Funding
        /// </summary>
        [Map("CMFUTURE_FUNDING")]
        CoinFuturesToFunding,

        /// <summary>
        /// ["<c>MAIN_OPTION</c>"] Main to Option
        /// </summary>
        [Map("MAIN_OPTION")]
        MainToOption,
        /// <summary>
        /// ["<c>OPTION_MAIN</c>"] Option to Main
        /// </summary>
        [Map("OPTION_MAIN")]
        OptionToMain,
        /// <summary>
        /// ["<c>UMFUTURE_OPTION</c>"] Usd Futures to Option
        /// </summary>
        [Map("UMFUTURE_OPTION")]
        UsdFuturesToOption,
        /// <summary>
        /// ["<c>OPTION_UMFUTURE</c>"] Option to Usd Futures
        /// </summary>
        [Map("OPTION_UMFUTURE")]
        OptionToUsdFutures,
        /// <summary>
        /// ["<c>MARGIN_OPTION</c>"] Margin to Option
        /// </summary>
        [Map("MARGIN_OPTION")]
        MarginToOption,
        /// <summary>
        /// ["<c>OPTION_MARGIN</c>"] Option to Margin
        /// </summary>
        [Map("OPTION_MARGIN")]
        OptionToMargin,
        /// <summary>
        /// ["<c>FUNDING_OPTION</c>"] Funding to Option
        /// </summary>
        [Map("FUNDING_OPTION")]
        FundingToOption,
        /// <summary>
        /// ["<c>OPTION_FUNDING</c>"] Option to Funding
        /// </summary>
        [Map("OPTION_FUNDING")]
        OptionToFunding
    }
}
