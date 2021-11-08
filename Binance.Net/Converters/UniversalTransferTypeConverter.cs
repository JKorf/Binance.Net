using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Binance.Net.Converters
{
    internal class UniversalTransferTypeConverter : BaseConverter<UniversalTransferType>
    {
        public UniversalTransferTypeConverter() : this(true) { }
        public UniversalTransferTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<UniversalTransferType, string>> Mapping => new List<KeyValuePair<UniversalTransferType, string>>
        {
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.MainToFunding, "MAIN_FUNDING"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.MainToUsdFutures, "MAIN_UMFUTURE"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.MainToCoinFutures, "MAIN_CMFUTURE"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.MainToMargin, "MAIN_MARGIN"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.MainToMining, "MAIN_MINING"),

            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.FundingToMain, "FUNDING_MAIN"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.FundingToUsdFutures, "FUNDING_UMFUTURE"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.FundingToMargin, "FUNDING_MARGIN"),

            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.UsdFuturesToMain, "UMFUTURE_MAIN"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.UsdFuturesToFunding, "UMFUTURE_FUNDING"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.UsdFuturesToMargin, "UMFUTURE_MARGIN"),

            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.CoinFuturesToMain, "CMFUTURE_MAIN"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.CoinFuturesToMargin, "CMFUTURE_MARGIN"),

            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.MarginToIsolatedMargin, "MARGIN_ISOLATEDMARGIN "),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.IsolatedMarginToMargin, "ISOLATEDMARGIN_MARGIN"),

            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.MarginToMain, "MARGIN_MAIN"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.MarginToUsdFutures, "MARGIN_UMFUTURE"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.MarginToCoinFutures, "MARGIN_CMFUTURE"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.MarginToMining, "MARGIN_MINING"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.MarginToFunding, "MARGIN_FUNDING"),

            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.MiningToMain, "MINING_MAIN"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.MiningToUsdFutures, "MINING_UMFUTURE"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.MiningToMargin, "MINING_MARGIN"),

            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.FundingToCoinFutures, "FUNDING_CMFUTURE"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.CoinFuturesToFunding, "CMFUTURE_FUNDING"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.IsolatedMarginToIsolatedMargin, "ISOLATEDMARGIN_ISOLATEDMARGIN"),
        };
    }
}
