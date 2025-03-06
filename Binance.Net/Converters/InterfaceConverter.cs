using System.Text.Json;

namespace Binance.Net.Converters
{
    internal class InterfaceConverter<TImp, TInterface> : JsonConverter<TInterface> where TImp : TInterface
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(TInterface);

        public override TInterface? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return (TImp)JsonSerializer.Deserialize(ref reader, typeof(TImp), options)!;
        }

        public override void Write(Utf8JsonWriter writer, TInterface value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, typeof(TImp), options);
        }
    }
}
