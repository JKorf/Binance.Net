using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Converters
{
    internal class TransferAccountTypeConverter : BaseConverter<TransferAccountType>
    {
        public TransferAccountTypeConverter() : this(true) { }
        public TransferAccountTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<TransferAccountType, string>> Mapping => new List<KeyValuePair<TransferAccountType, string>>
        {
            new KeyValuePair<TransferAccountType, string>(TransferAccountType.Spot, "SPOT"),
            new KeyValuePair<TransferAccountType, string>(TransferAccountType.UsdtFuture, "USDT_FUTURE"),
            new KeyValuePair<TransferAccountType, string>(TransferAccountType.CoinFuture, "COIN_FUTURE"),
        };
    }
}
