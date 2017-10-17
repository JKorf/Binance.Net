using System;
using Binance.Net.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Binance.Net.Converters
{
    public class OrderBookEntryConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JArray arr = JArray.Load(reader);
            BinanceOrderBookEntry entry = new BinanceOrderBookEntry();
            entry.Price = (double)arr[0];
            entry.Quantity = (double)arr[1];
            return entry;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
