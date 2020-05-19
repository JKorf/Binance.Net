using System;
using System.Collections.Generic;
using System.Text;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    internal class ProjectTypeConverter : BaseConverter<ProjectType>
    {
        public ProjectTypeConverter() : this(true) { }
        public ProjectTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<ProjectType, string>> Mapping => new List<KeyValuePair<ProjectType, string>>
        {
            new KeyValuePair<ProjectType, string>(ProjectType.CustomizedFixed, "CUSTOMIZED_FIXED"),
            new KeyValuePair<ProjectType, string>(ProjectType.Regular, "REGULAR")
        };
    }
}
