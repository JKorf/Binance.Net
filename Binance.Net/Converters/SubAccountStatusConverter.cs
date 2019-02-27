using System.Collections.Generic;
using Binance.Net.Objects;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    public class SubAccountStatusConverter : BaseConverter<SubAccountStatus>
    {
        public SubAccountStatusConverter() : this(true)
        {
        }

        public SubAccountStatusConverter(bool quotes) : base(quotes)
        {
        }

        protected override Dictionary<SubAccountStatus, string> Mapping => new Dictionary<SubAccountStatus, string>
        {
            {SubAccountStatus.Disabled, "disabled"},
            {SubAccountStatus.Enabled, "enabled"}
        };
    }
}
