using Binance.Net.Enums;

namespace Binance.Net.Converters
{
    internal class FuturesOrderTypeConverter : JsonConverter
    {
        private readonly bool quotes;

        public FuturesOrderTypeConverter()
        {
            quotes = true;
        }

        public FuturesOrderTypeConverter(bool useQuotes)
        {
            quotes = useQuotes;
        }

        private readonly Dictionary<FuturesOrderType, string> values = new Dictionary<FuturesOrderType, string>
        {
            { FuturesOrderType.Limit, "LIMIT" },
            { FuturesOrderType.Market, "MARKET" },
            { FuturesOrderType.TakeProfit, "TAKE_PROFIT" },
            { FuturesOrderType.TakeProfitMarket, "TAKE_PROFIT_MARKET" },
            { FuturesOrderType.Stop, "STOP" },
            { FuturesOrderType.StopMarket, "STOP_MARKET" },
            { FuturesOrderType.TrailingStopMarket, "TRAILING_STOP_MARKET" },
            { FuturesOrderType.Liquidation, "LIQUIDATION" }
        };

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(FuturesOrderType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            return values.Single(v => v.Value == (string?)reader.Value).Key;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (quotes)
                writer.WriteValue(values[(FuturesOrderType)value!]);
            else
                writer.WriteRawValue(values[(FuturesOrderType)value!]);
        }
    }
}
