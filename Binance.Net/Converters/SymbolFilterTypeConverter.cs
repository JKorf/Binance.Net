using Binance.Net.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Binance.Net.Converters
{
    public class SymbolFilterTypeConverter : JsonConverter
    {
        private readonly bool quotes;

        public SymbolFilterTypeConverter()
        {
            quotes = true;
        }

        public SymbolFilterTypeConverter(bool useQuotes)
        {
            quotes = useQuotes;
        }

        private readonly Dictionary<SymbolFilterType, string> values = new Dictionary<SymbolFilterType, string>()
        {
            { SymbolFilterType.LotSize, "LOT_SIZE" },
            { SymbolFilterType.MinNotional, "MIN_NOTIONAL" },
            { SymbolFilterType.PriceFilter, "PRICE_FILTER" },
        };

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(SymbolFilterType);
        }

        public SymbolFilterType ReadString(string data)
        {
            return values.Single(v => v.Value == data).Key;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return values.Single(v => v.Value == (string)reader.Value).Key;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (quotes)
                writer.WriteValue(values[(SymbolFilterType)value]);
            else
                writer.WriteRawValue(values[(SymbolFilterType)value]);
        }
    }
}
