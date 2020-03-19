using Binance.Net.Objects;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Converters
{
    internal class FuturesMarginChangeDirectionTypeConverter : BaseConverter<FuturesMarginChangeDirectionType>
    {
        public FuturesMarginChangeDirectionTypeConverter(): this(false) { }
        public FuturesMarginChangeDirectionTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<FuturesMarginChangeDirectionType, string>> Mapping => new List<KeyValuePair<FuturesMarginChangeDirectionType, string>>
        {
            new KeyValuePair<FuturesMarginChangeDirectionType, string>(FuturesMarginChangeDirectionType.Add, "1"),
            new KeyValuePair<FuturesMarginChangeDirectionType, string>(FuturesMarginChangeDirectionType.Reduce, "2")
        };
    }
}
