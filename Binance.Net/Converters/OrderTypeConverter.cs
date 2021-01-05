using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Binance.Net.Enums;

namespace Binance.Net.Converters
{
    internal class OrderTypeConverter : JsonConverter
    {
        private readonly bool quotes;

        public OrderTypeConverter()
        {
            quotes = true;
        }

        public OrderTypeConverter(bool useQuotes)
        {
            quotes = useQuotes;
        }

        private readonly Dictionary<OrderType, string> values = new Dictionary<OrderType, string>
        {
            { OrderType.Limit, "LIMIT" },
            { OrderType.Market, "MARKET" },
            { OrderType.LimitMaker, "LIMIT_MAKER" },
            { OrderType.StopLoss, "STOP_LOSS" },
            { OrderType.StopLossLimit, "STOP_LOSS_LIMIT" },
            { OrderType.Stop, "STOP" },
            { OrderType.StopMarket, "STOP_MARKET" },
            { OrderType.TakeProfit, "TAKE_PROFIT" },
            { OrderType.TakeProfitMarket, "TAKE_PROFIT_MARKET" },
            { OrderType.TakeProfitLimit, "TAKE_PROFIT_LIMIT" },
            { OrderType.TrailingStopMarket, "TRAILING_STOP_MARKET" },
            { OrderType.Liquidation, "LIQUIDATION" }
        };

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(OrderType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            return values.Single(v => v.Value == (string?)reader.Value).Key;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (quotes)
                writer.WriteValue(values[(OrderType)value!]);
            else
                writer.WriteRawValue(values[(OrderType)value!]);
        }
    }
}
