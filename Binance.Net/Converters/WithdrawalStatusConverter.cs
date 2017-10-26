using Binance.Net.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Binance.Net.Converters
{
    public class WithdrawalStatusConverter : JsonConverter
    {
        private readonly bool quotes;

        public WithdrawalStatusConverter()
        {
            quotes = true;
        }

        public WithdrawalStatusConverter(bool useQuotes)
        {
            quotes = useQuotes;
        }

        private readonly Dictionary<WithdrawalStatus, string> values = new Dictionary<WithdrawalStatus, string>()
        {
            { WithdrawalStatus.EmailSend, "0" },
            { WithdrawalStatus.Canceled, "1" },
            { WithdrawalStatus.AwaitingApproval, "2" },
            { WithdrawalStatus.Rejected, "3" },
            { WithdrawalStatus.Proccessing, "4" },
            { WithdrawalStatus.Failure, "5" },
            { WithdrawalStatus.Completed, "6" },
        };

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(WithdrawalStatus);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return values.Single(v => v.Value == reader.Value.ToString()).Key;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (quotes)
                writer.WriteValue(values[(WithdrawalStatus)value]);
            else
                writer.WriteRawValue(values[(WithdrawalStatus)value]);
        }
    }
}
