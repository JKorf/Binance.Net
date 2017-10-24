using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Binance.Net.Objects;

namespace Binance.Net.Converters
{
    public class TimeInForceConverter : JsonConverter
    {
        private bool quotes; 

        public TimeInForceConverter()
        {
            quotes = true;
        }

        public TimeInForceConverter(bool useQuotes)
        {
            quotes = useQuotes;
        }

        private Dictionary<TimeInForce, string> values = new Dictionary<TimeInForce, string>()
        {
            { TimeInForce.GoodTillCancel, "GTC" },
            { TimeInForce.ImmediateOrCancel, "IOC" },
        };

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TimeInForce);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return values.Single(v => v.Value == (string)reader.Value).Key;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if(quotes)
                writer.WriteValue(values[(TimeInForce)value]);
            else
                writer.WriteRawValue(values[(TimeInForce)value]);
        }
    }
}
