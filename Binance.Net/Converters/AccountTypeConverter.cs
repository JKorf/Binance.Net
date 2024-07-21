using Binance.Net.Enums;
using System.Text.Json;

namespace Binance.Net.Converters
{
    internal class AccountTypeConverter : JsonConverter<AccountType>
    {
        public override AccountType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var str = reader.GetString();
            if (str == null)
                return AccountType.TradeGroup002;

            if (str.StartsWith("TRD_GRP_"))
            {
                var number = str.Substring(8);
                if (Enum.TryParse<AccountType>("TradeGroup" + number, out var value))
                    return value;

                return AccountType.TradeGroup002;
            }
            else
            {
                if (str.Equals("SPOT", StringComparison.Ordinal))
                    return AccountType.Spot;
                if (str.Equals("MARGIN", StringComparison.Ordinal))
                    return AccountType.Margin;
                if (str.Equals("LEVERAGED", StringComparison.Ordinal))
                    return AccountType.Leveraged;
                if (str.Equals("FUTURES", StringComparison.Ordinal))
                    return AccountType.Futures;
            }

            return AccountType.TradeGroup002;
        }

        public override void Write(Utf8JsonWriter writer, AccountType value, JsonSerializerOptions options)
        {
            if (value == AccountType.Spot)
                writer.WriteStringValue("SPOT");
            if (value == AccountType.Margin)
                writer.WriteStringValue("MARGIN");
            if (value == AccountType.Leveraged)
                writer.WriteStringValue("LEVERAGED");
            if (value == AccountType.Futures)
                writer.WriteStringValue("FUTURES");

            writer.WriteStringValue("TRD_GRP_002");
        }
    }
}
