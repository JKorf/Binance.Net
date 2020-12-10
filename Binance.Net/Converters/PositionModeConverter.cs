using Binance.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Converters
{
    internal class PositionModeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            writer.WriteValue(value?.ToString() == PositionMode.Hedge.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            return ((bool)reader.Value!) ? PositionMode.Hedge : PositionMode.OneWay;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool);
        }
    }
}
