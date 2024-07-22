using Binance.Net.Enums;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text.Json;

namespace Binance.Net.Converters
{
    internal class AccountTypeConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(AccountType) 
                || typeToConvert == typeof(IEnumerable<AccountType>)
                || typeToConvert == typeof(IEnumerable<IEnumerable<AccountType>>);
        }

        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            Type converterType = typeof(AccountTypeConverterImp<>).MakeGenericType(typeToConvert);
            return (JsonConverter)Activator.CreateInstance(converterType);
        }

        internal class AccountTypeConverterImp<T> : JsonConverter<T>
        {
            public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (typeToConvert == typeof(AccountType))
                {
                    return (T)(object)(ParseAccountType(ref reader) ?? AccountType.TradeGroup002);
                }
                else if(typeToConvert == typeof(IEnumerable<AccountType>))
                {
                    var  result = new List<AccountType>();
                    while(reader.Read())
                    {
                        if (reader.TokenType == JsonTokenType.EndArray)
                            break;

                        if (reader.TokenType == JsonTokenType.StartArray)
                            continue;

                        var parseResult = ParseAccountType(ref reader);
                        if (parseResult != null)
                            result.Add(parseResult.Value);
                    }
                    return (T)(object)result;
                }
                else if (typeToConvert == typeof(IEnumerable<IEnumerable<AccountType>>))
                {
                    var result = new List<IEnumerable<AccountType>>();
                    reader.Read(); // Start array
                    do
                    {
                        if (reader.TokenType == JsonTokenType.EndArray)
                            break;

                        var resultInner = new List<AccountType>();
                        reader.Read(); // Start array
                        do
                        {
                            if (reader.TokenType == JsonTokenType.EndArray)
                                break;


                            var parseResult = ParseAccountType(ref reader);
                            if (parseResult != null)
                                resultInner.Add(parseResult.Value);
                        }
                        while (reader.Read());
                        result.Add(resultInner);
                    }
                    while (reader.Read());
                    return (T)(object)result;
                }

                throw new InvalidOperationException("Invalid type");
            }

            public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            {
                if (value is AccountType act)
                {
                    WriteAccountType(writer, act);
                }
                else if (value is IEnumerable<AccountType> actList)
                {
                    writer.WriteStartArray();
                    foreach(var val in actList)
                        WriteAccountType(writer, val);
                    writer.WriteEndArray();
                }
                else if (value is IEnumerable<IEnumerable<AccountType>> actListList)
                {
                    writer.WriteStartArray();
                    foreach (var valList in actListList)
                    {
                        writer.WriteStartArray();
                        foreach (var val in valList)
                            WriteAccountType(writer, val);
                        writer.WriteEndArray();
                    }
                    writer.WriteEndArray();
                }
            }

            private void WriteAccountType(Utf8JsonWriter writer, AccountType value)
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

            private AccountType? ParseAccountType(ref Utf8JsonReader reader)
            {
                var str = reader.GetString();
                if (str == null)
                    return AccountType.TradeGroup002;

                if (str.StartsWith("TRD_GRP_"))
                {
                    var number = str.Substring(8);
                    if (Enum.TryParse<AccountType>("TradeGroup" + number, out var value))
                        return value;

                    return null;
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

                return null;
            }
        }
    }

    
}
