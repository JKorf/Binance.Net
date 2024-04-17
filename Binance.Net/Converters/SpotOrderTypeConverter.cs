using Binance.Net.Enums;

namespace Binance.Net.Converters
{
    internal class SpotOrderTypeConverter : JsonConverter
    {
        private readonly bool quotes;

        public SpotOrderTypeConverter()
        {
            quotes = true;
        }

        public SpotOrderTypeConverter(bool useQuotes)
        {
            quotes = useQuotes;
        }

        private readonly Dictionary<SpotOrderType, string> values = new Dictionary<SpotOrderType, string>
        {
            { SpotOrderType.Limit, "LIMIT" },
            { SpotOrderType.Market, "MARKET" },
            { SpotOrderType.LimitMaker, "LIMIT_MAKER" },
            { SpotOrderType.StopLoss, "STOP_LOSS" },
            { SpotOrderType.StopLossLimit, "STOP_LOSS_LIMIT" },
            { SpotOrderType.TakeProfit, "TAKE_PROFIT" },
            { SpotOrderType.TakeProfitLimit, "TAKE_PROFIT_LIMIT" }
        };

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(SpotOrderType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            return values.Single(v => v.Value == (string?)reader.Value).Key;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (quotes)
                writer.WriteValue(values[(SpotOrderType)value!]);
            else
                writer.WriteRawValue(values[(SpotOrderType)value!]);
        }
    }
}
