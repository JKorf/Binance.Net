using Binance.Net.Objects.Models.Futures;
using Binance.Net.Objects.Models.Spot;

namespace Binance.Net.Converters
{
    //[JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(BinanceSourceGenerationAggregator))]
    [JsonSerializable(typeof(BinanceFuturesUsdtExchangeInfo))]
    [JsonSerializable(typeof(BinanceExchangeInfo))]
    [JsonSerializable(typeof(Binance24HPrice))]
    internal partial class BinanceSourceGenerationContext : JsonSerializerContext
    {
    }
}