using Binance.Net.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Binance.Net.Converters
{
    public class OrderResponseTypeConverter: JsonConverter
    {
        private readonly bool quotes;

        public OrderResponseTypeConverter()
        {
            quotes = true;
        }

        public OrderResponseTypeConverter(bool useQuotes)
        {
            quotes = useQuotes;
        }

        private readonly Dictionary<OrderResponseType, string> values = new Dictionary<OrderResponseType, string>()
        {
            { OrderResponseType.Acknowledge, "ACK" },
            { OrderResponseType.Result, "RESULT" },
            { OrderResponseType.Full, "FULL" },
        };

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(OrderResponseType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return values.Single(v => v.Value == (string)reader.Value).Key;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (quotes)
                writer.WriteValue(values[(OrderResponseType)value]);
            else
                writer.WriteRawValue(values[(OrderResponseType)value]);
        }
    }
}
