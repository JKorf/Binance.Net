using System.Collections.Generic;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    internal class SubAccountFuturesTransferTypeConverter : BaseConverter<SubAccountFuturesTransferType>
    {
        public SubAccountFuturesTransferTypeConverter() : this(true)
        {
        }

        public SubAccountFuturesTransferTypeConverter(bool quotes) : base(quotes)
        {
        }

        protected override List<KeyValuePair<SubAccountFuturesTransferType, string>> Mapping =>
            new List<KeyValuePair<SubAccountFuturesTransferType, string>>
            {
                new KeyValuePair<SubAccountFuturesTransferType, string>(
                    SubAccountFuturesTransferType.FromSpotToUsdtFutures, "1"),
                new KeyValuePair<SubAccountFuturesTransferType, string>(
                    SubAccountFuturesTransferType.FromUsdtFuturesToSpot, "2"),
                new KeyValuePair<SubAccountFuturesTransferType, string>(
                    SubAccountFuturesTransferType.FromSpotToCoinFutures, "3"),
                new KeyValuePair<SubAccountFuturesTransferType, string>(
                    SubAccountFuturesTransferType.FromCoinFuturesToSpot, "4"),
            };
    }
}