using Binance.Net.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Binance.Net.Converters
{
    public class WithdrawalStatusConverter : JsonConverter
    {
        private Dictionary<WithdrawalStatus, string> values = new Dictionary<WithdrawalStatus, string>()
        {
            { WithdrawalStatus.Completed, "6" },
            { WithdrawalStatus.Proccessing, "4" },
        };

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(WithdrawalStatus);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if(values.ContainsValue(reader.Value.ToString()))
                return values.SingleOrDefault(v => v.Value == reader.Value.ToString()).Key;
            return WithdrawalStatus.Unknown;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(values[(WithdrawalStatus)value]);
        }
    }
}
