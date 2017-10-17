using System;
using System.Collections.Generic;
using System.Linq;
using Binance.Net.Objects;
using Newtonsoft.Json;

namespace Binance.Net.Converters
{
    public class ExecutionTypeConverter: JsonConverter
    {
        private Dictionary<ExecutionType, string> values = new Dictionary<ExecutionType, string>()
        {
            { ExecutionType.New, "NEW" },
            { ExecutionType.Canceled, "CANCELED" },
            { ExecutionType.Replaced, "REPLACED" },
            { ExecutionType.Rejected, "REJECTED" },
            { ExecutionType.Trade, "TRADE" },
            { ExecutionType.Expired, "EXPIRED" },
        };

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ExecutionType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return values.Single(v => v.Value == (string)reader.Value).Key;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(values[(ExecutionType)value]);
        }
    }
}
