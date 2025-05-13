using System.Text.Json;
using System.Diagnostics.CodeAnalysis;

namespace Binance.Net.Converters
{

#if NET5_0_OR_GREATER
        [UnconditionalSuppressMessage("AssemblyLoadTrimming", "IL3050:RequiresUnreferencedCode", Justification = "JsonSerializerOptions provided here has TypeInfoResolver set")]
        [UnconditionalSuppressMessage("AssemblyLoadTrimming", "IL2026:RequiresUnreferencedCode", Justification = "JsonSerializerOptions provided here has TypeInfoResolver set")]
#endif
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
