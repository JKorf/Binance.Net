using System;
using System.Collections.Generic;
using System.Linq;
using Binance.Net.Objects;
using Newtonsoft.Json;

namespace Binance.Net.Converters
{
    public class KlineIntervalConverter: JsonConverter
    {
        private bool quotes;

        public KlineIntervalConverter()
        {
            quotes = true;
        }

        public KlineIntervalConverter(bool useQuotes)
        {
            quotes = useQuotes;
        }

        private Dictionary<KlineInterval, string> values = new Dictionary<KlineInterval, string>()
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
            { KlineInterval.TwelfHour, "12h" },
            { KlineInterval.OneDay, "1d" },
            { KlineInterval.ThreeDay, "3d" },
            { KlineInterval.OneWeek, "1w" },
            { KlineInterval.OneMonth, "1M" },
        };

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(KlineInterval);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return values.Single(v => v.Value == (string)reader.Value).Key;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (quotes)
                writer.WriteValue(values[(KlineInterval)value]);
            else
                writer.WriteRawValue(values[(KlineInterval)value]);
        }
    }
}
