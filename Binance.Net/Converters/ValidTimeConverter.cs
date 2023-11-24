using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Binance.Net.Converters
{
    internal class ValidTimeConverter : BaseConverter<ValidTime>
    {
        public ValidTimeConverter() : this(true) { }
        public ValidTimeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<ValidTime, string>> Mapping => new List<KeyValuePair<ValidTime, string>>
        {
            new KeyValuePair<ValidTime, string>(ValidTime.TenSeconds, "0"),
            new KeyValuePair<ValidTime, string>(ValidTime.ThirtySeconds, "1"),
            new KeyValuePair<ValidTime, string>(ValidTime.OneMinute, "2"),
            new KeyValuePair<ValidTime, string>(ValidTime.TwoMinutes, "3")
        };
    }
}
