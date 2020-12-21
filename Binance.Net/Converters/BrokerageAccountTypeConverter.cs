using Binance.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Binance.Net.Converters
{
    internal class BrokerageAccountTypeConverter : JsonConverter
    {
        private readonly bool quotes;

        public BrokerageAccountTypeConverter()
        {
            quotes = true;
        }

        public BrokerageAccountTypeConverter(bool useQuotes)
        {
            quotes = useQuotes;
        }

        private readonly Dictionary<BrokerageAccountType, string> values = new Dictionary<BrokerageAccountType, string>
                                                                           {
                                                                               {BrokerageAccountType.Spot, "SPOT"},
                                                                               {BrokerageAccountType.FuturesCoin, "COIN_FUTURE"},
                                                                               {BrokerageAccountType.FuturesUsdt, "USDT_FUTURE"},
                                                                           };

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(BrokerageAccountType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            return values.Single(v => v.Value == (string?)reader.Value).Key;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (quotes)
                writer.WriteValue(values[(BrokerageAccountType)value!]);
            else
                writer.WriteRawValue(values[(BrokerageAccountType)value!]);
        }
    }
}