using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.Json;

namespace Binance.Net.Converters
{
    [AttributeUsage(AttributeTargets.Property)]
    public class JsonConverterCtorAttribute<T> : JsonConverterAttribute where T: JsonConverter
    {
        private readonly object[] _parameters;

        public JsonConverterCtorAttribute(params object[] parameters) => _parameters = parameters;

        public override JsonConverter CreateConverter(Type typeToConvert)
        {
            return (T)Activator.CreateInstance(typeof(T), _parameters);
        }
    }

    internal class ReplaceConverter : JsonConverter<string>
    {
        private string _valueToRepace;
        private string _valueReplaceWith;

        public ReplaceConverter(string valueToReplace, string valueToReplaceWith)
        {
            _valueToRepace = valueToReplace;
            _valueReplaceWith = valueToReplaceWith;
        }

        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (value != null)
                value.Replace(_valueToRepace, _valueReplaceWith);
            return value;
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options) => writer.WriteStringValue(value);
    }
}
