using System.Collections.Generic;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    internal class KlineIntervalConverter: BaseConverter<KlineInterval>
    {
        public KlineIntervalConverter(): this(true) { }
        public KlineIntervalConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<KlineInterval, string>> Mapping => new List<KeyValuePair<KlineInterval, string>>
        {
            new KeyValuePair<KlineInterval, string>(KlineInterval.OneSecond, "1s"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.OneMinute, "1m"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.ThreeMinutes, "3m"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.FiveMinutes, "5m"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.FifteenMinutes, "15m"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.ThirtyMinutes, "30m"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.OneHour, "1h"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.TwoHour, "2h"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.FourHour, "4h"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.SixHour, "6h"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.EightHour, "8h"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.TwelveHour, "12h"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.OneDay, "1d"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.ThreeDay, "3d"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.OneWeek, "1w"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.OneMonth, "1M")
        };
    }
}
