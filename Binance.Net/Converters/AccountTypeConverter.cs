using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Binance.Net.Converters
{
    internal class AccountTypeConverter : BaseConverter<AccountType>
    {
        public AccountTypeConverter() : this(true) { }
        public AccountTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<AccountType, string>> Mapping => new List<KeyValuePair<AccountType, string>>
        {
            new KeyValuePair<AccountType, string>(AccountType.Spot, "SPOT"),
            new KeyValuePair<AccountType, string>(AccountType.Margin, "MARGIN"),
            new KeyValuePair<AccountType, string>(AccountType.Futures, "FUTURES"),
            new KeyValuePair<AccountType, string>(AccountType.Leveraged, "LEVERAGED")
        };
    }
}
