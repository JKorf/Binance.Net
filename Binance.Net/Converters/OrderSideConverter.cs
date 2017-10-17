using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Binance.Net.Objects;

namespace Binance.Net.Converters
{
    public class OrderSideConverter : JsonConverter
    {
        private Dictionary<OrderSide, string> values = new Dictionary<OrderSide, string>()
        {
            { OrderSide.Buy, "BUY" },
            { OrderSide.Sell, "SELL" },
        };

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(OrderSide);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return values.Single(v => v.Value == (string)reader.Value).Key;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(values[(OrderSide)value]);
        }
    }
}
