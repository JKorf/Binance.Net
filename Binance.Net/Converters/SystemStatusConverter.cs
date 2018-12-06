using System.Collections.Generic;
using Binance.Net.Objects;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    public class SystemStatusConverter : BaseConverter<SystemStatus>
    {
        public SystemStatusConverter() : this(true)
        {
        }

        public SystemStatusConverter(bool quotes) : base(quotes)
        {
        }

        protected override Dictionary<SystemStatus, string> Mapping => new Dictionary<SystemStatus, string>
        {
            {SystemStatus.Normal, "0"},
            {SystemStatus.Maintenance, "1"}
        };
    }
}
