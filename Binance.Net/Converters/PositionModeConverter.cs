using Binance.Net.Enums;
using System.Text.Json;

namespace Binance.Net.Converters
{
    internal class PositionModeConverter : JsonConverter<PositionMode>
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool);
        }

        public override PositionMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetBoolean() ? PositionMode.Hedge : PositionMode.OneWay;
        }

        public override void Write(Utf8JsonWriter writer, PositionMode value, JsonSerializerOptions options)
        {
            writer.WriteBooleanValue(value.ToString() == PositionMode.Hedge.ToString());
        }
    }
}
