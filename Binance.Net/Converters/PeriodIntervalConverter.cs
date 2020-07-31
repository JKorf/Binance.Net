using System.Collections.Generic;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    internal class PeriodIntervalConverter : BaseConverter<PeriodInterval>
    {
        public PeriodIntervalConverter() : this(true) { }
        public PeriodIntervalConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<PeriodInterval, string>> Mapping => new List<KeyValuePair<PeriodInterval, string>>
        {
            new KeyValuePair<PeriodInterval, string>(PeriodInterval.FiveMinutes, "5m"),
            new KeyValuePair<PeriodInterval, string>(PeriodInterval.FifteenMinutes, "15m"),
            new KeyValuePair<PeriodInterval, string>(PeriodInterval.ThirtyMinutes, "30m"),
            new KeyValuePair<PeriodInterval, string>(PeriodInterval.OneHour, "1h"),
            new KeyValuePair<PeriodInterval, string>(PeriodInterval.TwoHour, "2h"),
            new KeyValuePair<PeriodInterval, string>(PeriodInterval.FourHour, "4h"),
            new KeyValuePair<PeriodInterval, string>(PeriodInterval.SixHour, "6h"),
            new KeyValuePair<PeriodInterval, string>(PeriodInterval.TwelveHour, "12h"),
            new KeyValuePair<PeriodInterval, string>(PeriodInterval.OneDay, "1d")
        };
    }
}
