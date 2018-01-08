using Binance.Net.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Binance.Net.Converters
{
    public class SymbolStatusConverter : JsonConverter
    {
        private readonly bool quotes;

        public SymbolStatusConverter()
        {
            quotes = true;
        }

        public SymbolStatusConverter(bool useQuotes)
        {
            quotes = useQuotes;
        }

        private readonly Dictionary<SymbolStatus, string> values = new Dictionary<SymbolStatus, string>()
        {
            { SymbolStatus.AuctionMatch, "AUCTION_MATCH" },
            { SymbolStatus.Break, "BREAK" },
            { SymbolStatus.EndOfDay, "END_OF_DAY" },
            { SymbolStatus.Halt, "HALT" },
            { SymbolStatus.PostTrading, "POST_TRADING" },
            { SymbolStatus.PreTrading, "PRE_TRADING" },
            { SymbolStatus.Trading, "TRADING" },
        };

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(SymbolStatus);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return values.Single(v => v.Value == (string)reader.Value).Key;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (quotes)
                writer.WriteValue(values[(SymbolStatus)value]);
            else
                writer.WriteRawValue(values[(SymbolStatus)value]);
        }
    }
}
