using Binance.Net.Enums;
using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Sockets;
namespace Binance.Net
{
    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(int?))]
    [JsonSerializable(typeof(BinanceSourceGenerationAggregator))]
    [JsonSerializable(typeof(BinanceCombinedStream<BinanceEventOrderBook>))]

    internal partial class BinanceSourceGenerationContext : JsonSerializerContext
    {
    }
    internal partial class BinanceSourceGenerationAggregator
    { }
}