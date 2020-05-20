using System.Collections.Generic;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    internal class SubAccountStatusConverter : BaseConverter<SubAccountStatus>
    {
        public SubAccountStatusConverter() : this(true)
        {
        }

        public SubAccountStatusConverter(bool quotes) : base(quotes)
        {
        }

        protected override List<KeyValuePair<SubAccountStatus, string>> Mapping => new List<KeyValuePair<SubAccountStatus, string>>
        {
            new KeyValuePair<SubAccountStatus, string>(SubAccountStatus.Disabled, "disabled"),
            new KeyValuePair<SubAccountStatus, string>(SubAccountStatus.Enabled, "enabled")
        };
    }
}
