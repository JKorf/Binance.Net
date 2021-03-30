using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Converters
{
    internal class UniversalTransferTypeConverter : BaseConverter<UniversalTransferType>
    {
        public UniversalTransferTypeConverter() : this(true) { }
        public UniversalTransferTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<UniversalTransferType, string>> Mapping => new List<KeyValuePair<UniversalTransferType, string>>
        {
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.MainToC2C, "MAIN_C2C"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.MainToUsdFutures, "MAIN_UMFUTURE"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.MainToCoinFutures, "MAIN_CMFUTURE"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.MainToMargin, "MAIN_MARGIN"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.MainToMining, "MAIN_MINING"),

            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.C2CToMain, "C2C_MAIN"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.C2CToUsdFutures, "C2C_UMFUTURE"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.C2CToMining, "C2C_MINING"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.C2CToMargin, "C2C_MARGIN"),

            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.UsdFuturesToMain, "UMFUTURE_MAIN"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.UsdFuturesToC2C, "UMFUTURE_C2C"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.UsdFuturesToMargin, "UMFUTURE_MARGIN"),

            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.CoinFuturesToMain, "CMFUTURE_MAIN"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.CoinFuturesToMargin, "CMFUTURE_MARGIN"),

            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.MarginToMain, "MARGIN_MAIN"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.MarginToUsdFutures, "MARGIN_UMFUTURE"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.MarginToCoinFutures, "MARGIN_CMFUTURE"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.MarginToMining, "MARGIN_MINING"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.MarginToC2C, "MARGIN_C2C"),

            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.MiningToMain, "MINING_MAIN"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.MiningToUsdFutures, "MINING_UMFUTURE"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.MiningToC2C, "MINING_C2C"),
            new KeyValuePair<UniversalTransferType, string>(UniversalTransferType.MiningToMargin, "MINING_MARGIN"),
        };
    }
}
