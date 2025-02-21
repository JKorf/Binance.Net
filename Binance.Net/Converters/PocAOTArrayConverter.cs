using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Binance.Net.Converters
{
    // Based on ArrayConverterInner
#if NET5_0_OR_GREATER
    class PocAOTArrayConverter<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]T> : JsonConverter<T> where T : new()
# else
    class PocAOTArrayConverter<T> : JsonConverter<T> where T : new()
#endif
    {
        private static readonly ConcurrentDictionary<Type, List<ArrayPropertyInfo>> _typeAttributesCache = new ConcurrentDictionary<Type, List<ArrayPropertyInfo>>();
        private static readonly ConcurrentDictionary<Type, JsonSerializerOptions> _converterOptionsCache = new ConcurrentDictionary<Type, JsonSerializerOptions>();

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStartArray();

            var valueType = value.GetType();
            if (!_typeAttributesCache.TryGetValue(valueType, out var typeAttributes))
                typeAttributes = CacheTypeAttributes(valueType);

            var ordered = typeAttributes.Where(x => x.ArrayProperty != null).OrderBy(p => p.ArrayProperty.Index);
            var last = -1;
            foreach (var prop in ordered)
            {
                if (prop.ArrayProperty.Index == last)
                    continue;

                while (prop.ArrayProperty.Index != last + 1)
                {
                    writer.WriteNullValue();
                    last += 1;
                }

                last = prop.ArrayProperty.Index;

                var objValue = prop.PropertyInfo.GetValue(value);
                if (objValue == null)
                {
                    writer.WriteNullValue();
                    continue;
                }

                JsonSerializerOptions? typeOptions = null;
                if (prop.JsonConverterType != null)
                {
                    var converter = (JsonConverter)Activator.CreateInstance(prop.JsonConverterType)!;
                    typeOptions = new JsonSerializerOptions();
                    typeOptions.Converters.Clear();
                    typeOptions.Converters.Add(converter);
                }

                if (prop.JsonConverterType == null && IsSimple(prop.PropertyInfo.PropertyType))
                {
                    if (prop.TargetType == typeof(string))
                        writer.WriteStringValue(Convert.ToString(objValue, CultureInfo.InvariantCulture));
                    else if (prop.TargetType.IsEnum)
                        writer.WriteStringValue(EnumConverter.GetString(objValue));
                    else if (prop.TargetType == typeof(bool))
                        writer.WriteBooleanValue((bool)objValue);
                    else
                        writer.WriteRawValue(Convert.ToString(objValue, CultureInfo.InvariantCulture)!);
                }
                else
                {
                    JsonSerializer.Serialize(writer, objValue, typeOptions ?? options);
                }
            }

            writer.WriteEndArray();
        }

        /// <inheritdoc />
        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return default;

            var result = Activator.CreateInstance(typeToConvert)!;
            return (T)ParseObject(ref reader, result, typeToConvert, options);
        }

        private static bool IsSimple(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // nullable type, check if the nested type is simple.
                return IsSimple(type.GetGenericArguments()[0]);
            }
            return type.IsPrimitive
              || type.IsEnum
              || type == typeof(string)
              || type == typeof(decimal);
        }

            private static List<ArrayPropertyInfo> CacheTypeAttributes(Type type)
        {
            var attributes = new List<ArrayPropertyInfo>();
                var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var att = property.GetCustomAttribute<CryptoExchange.Net.Converters.ArrayPropertyAttribute>();
                if (att == null)
                    continue;

                attributes.Add(new ArrayPropertyInfo
                {
                    ArrayProperty = att,
                    PropertyInfo = property,
                    DefaultDeserialization = property.GetCustomAttribute<CryptoExchange.Net.Attributes.JsonConversionAttribute>() != null,
                    JsonConverterType = property.GetCustomAttribute<JsonConverterAttribute>()?.ConverterType ?? property.PropertyType.GetCustomAttribute<JsonConverterAttribute>()?.ConverterType,
                    TargetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType
                });
            }

            _typeAttributesCache.TryAdd(type, attributes);
            return attributes;
        }

            private static object ParseObject(ref Utf8JsonReader reader, object result, Type objectType, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
                throw new Exception("Not an array");

            if (!_typeAttributesCache.TryGetValue(objectType, out var attributes))
                attributes = CacheTypeAttributes(objectType);

            int index = 0;
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                    break;

                var indexAttributes = attributes.Where(a => a.ArrayProperty.Index == index);
                if (!indexAttributes.Any())
                {
                    index++;
                    continue;
                }

                foreach (var attribute in indexAttributes)
                {
                    var targetType = attribute.TargetType;
                    object? value = null;
                    if (attribute.JsonConverterType != null)
                    {
                        if (!_converterOptionsCache.TryGetValue(attribute.JsonConverterType, out var newOptions))
                        {
                            var converter = (JsonConverter)Activator.CreateInstance(attribute.JsonConverterType)!;
                            newOptions = new JsonSerializerOptions
                            {
                                NumberHandling = SerializerOptions.WithConverters.NumberHandling,
                                PropertyNameCaseInsensitive = SerializerOptions.WithConverters.PropertyNameCaseInsensitive,
                                Converters = { converter },
                                TypeInfoResolver = PocAOTBinanceSerializerOptions.WithConverters.TypeInfoResolver,
                            };
                            _converterOptionsCache.TryAdd(attribute.JsonConverterType, newOptions);
                        }

                        value = JsonDocument.ParseValue(ref reader).Deserialize(attribute.PropertyInfo.PropertyType, newOptions);
                    }
                    else if (attribute.DefaultDeserialization)
                    {
                        // Use default deserialization
                        value = JsonDocument.ParseValue(ref reader).Deserialize(attribute.PropertyInfo.PropertyType, SerializerOptions.WithConverters);
                    }
                    else
                    {
                        value = reader.TokenType switch
                        {
                            JsonTokenType.Null => null,
                            JsonTokenType.False => false,
                            JsonTokenType.True => true,
                            JsonTokenType.String => reader.GetString(),
                            JsonTokenType.Number => reader.GetDecimal(),
                            JsonTokenType.StartObject => JsonSerializer.Deserialize(ref reader, attribute.TargetType, options),
                            _ => throw new NotImplementedException($"Array deserialization of type {reader.TokenType} not supported"),
                        };
                    }

                    if (targetType.IsAssignableFrom(value?.GetType()))
                        attribute.PropertyInfo.SetValue(result, value);
                    else
                        attribute.PropertyInfo.SetValue(result, value == null ? null : Convert.ChangeType(value, targetType, CultureInfo.InvariantCulture));
                }

                index++;
            }

            return result;
        }
        private class ArrayPropertyInfo
        {
            public PropertyInfo PropertyInfo { get; set; } = null!;
            public CryptoExchange.Net.Converters.ArrayPropertyAttribute ArrayProperty { get; set; } = null!;
            public Type? JsonConverterType { get; set; }
            public bool DefaultDeserialization { get; set; }
            public Type TargetType { get; set; } = null!;
        }
    }
}
