using Binance.Net.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Binance.Net.Converters
{
    public class DepositStatusConverter: JsonConverter
    {
        private bool quotes;

        public DepositStatusConverter()
        {
            quotes = true;
        }

        public DepositStatusConverter(bool useQuotes)
        {
            quotes = useQuotes;
        }

        private Dictionary<DepositStatus, string> values = new Dictionary<DepositStatus, string>()
        {
            { DepositStatus.Pending, "0" },
            { DepositStatus.Success, "1" }
        };

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DepositStatus);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return values.Single(v => v.Value == reader.Value.ToString()).Key;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (quotes)
                writer.WriteValue(values[(DepositStatus)value]);
            else
                writer.WriteRawValue(values[(DepositStatus)value]);
        }
    }
}
