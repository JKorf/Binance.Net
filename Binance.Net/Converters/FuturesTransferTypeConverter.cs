using System.Collections.Generic;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    internal class FuturesTransferTypeConverter: BaseConverter<FuturesTransferType>
    {
        public FuturesTransferTypeConverter() : this(true) { }
        public FuturesTransferTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<FuturesTransferType, string>> Mapping => new List<KeyValuePair<FuturesTransferType, string>>
        {
            new KeyValuePair<FuturesTransferType, string>(FuturesTransferType.FromCoinFuturesToSpot, "4"),
            new KeyValuePair<FuturesTransferType, string>(FuturesTransferType.FromUsdtFuturesToSpot, "2"),
            new KeyValuePair<FuturesTransferType, string>(FuturesTransferType.FromSpotToCoinFutures, "3"),
            new KeyValuePair<FuturesTransferType, string>(FuturesTransferType.FromSpotToUsdtFutures, "1"),
        };
    }
}
