using System.Collections.Generic;
using Binance.Net.Objects;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    public class KlineIntervalConverter: BaseConverter<KlineInterval>
    {
        public KlineIntervalConverter(): this(true) { }
        public KlineIntervalConverter(bool quotes) : base(quotes) { }

        protected override Dictionary<KlineInterval, string> Mapping => new Dictionary<KlineInterval, string>
        {
            { KlineInterval.OneMinute, "1m" },
            { KlineInterval.ThreeMinutes, "3m" },
            { KlineInterval.FiveMinutes, "5m" },
            { KlineInterval.FiveteenMinutes, "15m" },
            { KlineInterval.ThirtyMinutes, "30m" },
            { KlineInterval.OneHour, "1h" },
            { KlineInterval.TwoHour, "2h" },
            { KlineInterval.FourHour, "4h" },
            { KlineInterval.SixHour, "6h" },
            { KlineInterval.EightHour, "8h" },
            { KlineInterval.TwelveHour, "12h" },
            { KlineInterval.OneDay, "1d" },
            { KlineInterval.ThreeDay, "3d" },
            { KlineInterval.OneWeek, "1w" },
            { KlineInterval.OneMonth, "1M" }
        };
    }
}
