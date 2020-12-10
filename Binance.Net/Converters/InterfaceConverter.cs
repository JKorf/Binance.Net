using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Binance.Net.Converters
{
    internal class InterfaceConverter<TImp>: JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<TImp>(reader);
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
