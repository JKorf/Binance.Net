using System.Collections.Generic;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    internal class ProjectStatusConverter : BaseConverter<ProjectStatus>
    {
        public ProjectStatusConverter() : this(true) { }
        public ProjectStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<ProjectStatus, string>> Mapping => new List<KeyValuePair<ProjectStatus, string>>
        {
            new KeyValuePair<ProjectStatus, string>(ProjectStatus.Holding, "HOLDING"),
            new KeyValuePair<ProjectStatus, string>(ProjectStatus.Redeemed, "REDEEMED")
        };
    }
}
