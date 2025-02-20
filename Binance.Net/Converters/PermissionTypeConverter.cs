using Binance.Net.Enums;
using System.Text.Json;

namespace Binance.Net.Converters
{
    internal class PermissionTypeConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(PermissionType) 
                || typeToConvert == typeof(IEnumerable<PermissionType>)
                || typeToConvert == typeof(IEnumerable<IEnumerable<PermissionType>>);
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
                if (typeToConvert == typeof(PermissionType))
                {
                    return (T)(object)(ParseAccountType(ref reader) ?? PermissionType.TradeGroup002);
                }
                else if(typeToConvert == typeof(IEnumerable<PermissionType>))
                {
                    var  result = new List<PermissionType>();
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
                else if (typeToConvert == typeof(IEnumerable<IEnumerable<PermissionType>>))
                {
                    var result = new List<IEnumerable<PermissionType>>();
                    reader.Read(); // Start array
                    do
                    {
                        if (reader.TokenType == JsonTokenType.EndArray)
                            break;

                        var resultInner = new List<PermissionType>();
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
                if (value is PermissionType act)
                {
                    WriteAccountType(writer, act);
                }
                else if (value is IEnumerable<PermissionType> actList)
                {
                    writer.WriteStartArray();
                    foreach(var val in actList)
                        WriteAccountType(writer, val);
                    writer.WriteEndArray();
                }
                else if (value is IEnumerable<IEnumerable<PermissionType>> actListList)
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

            private void WriteAccountType(Utf8JsonWriter writer, PermissionType value)
            {
                if (value == PermissionType.Spot)
                    writer.WriteStringValue("SPOT");
                if (value == PermissionType.Margin)
                    writer.WriteStringValue("MARGIN");
                if (value == PermissionType.Leveraged)
                    writer.WriteStringValue("LEVERAGED");
                if (value == PermissionType.Futures)
                    writer.WriteStringValue("FUTURES");
                if (value == PermissionType.PreMarket)
                    writer.WriteStringValue("PRE_MARKET");

                writer.WriteStringValue("TRD_GRP_002");
            }

            private PermissionType? ParseAccountType(ref Utf8JsonReader reader)
            {
                var str = reader.GetString();
                if (str == null)
                    return PermissionType.TradeGroup002;

                if (str.StartsWith("TRD_GRP_"))
                {
                    var number = str.Substring(8);
                    if (Enum.TryParse<PermissionType>("TradeGroup" + number, out var value))
                        return value;

                    return null;
                }
                else
                {
                    if (str.Equals("SPOT", StringComparison.Ordinal))
                        return PermissionType.Spot;
                    if (str.Equals("MARGIN", StringComparison.Ordinal))
                        return PermissionType.Margin;
                    if (str.Equals("LEVERAGED", StringComparison.Ordinal))
                        return PermissionType.Leveraged;
                    if (str.Equals("FUTURES", StringComparison.Ordinal))
                        return PermissionType.Futures;
                    if (str.Equals("PRE_MARKET", StringComparison.Ordinal))
                        return PermissionType.PreMarket;
                }

                return null;
            }
        }
    }

    
}
