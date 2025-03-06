using Binance.Net.Objects.Models.Futures;
using Binance.Net.Objects.Models.Spot;

namespace Binance.Net.Converters
{
    // Standard types
    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(int?))]
    [JsonSerializable(typeof(int))]
    [JsonSerializable(typeof(long?))]
    [JsonSerializable(typeof(long))]
    [JsonSerializable(typeof(decimal))]
    [JsonSerializable(typeof(decimal?))]
    [JsonSerializable(typeof(DateTime))]
    [JsonSerializable(typeof(DateTime?))]

    [JsonSerializable(typeof(BinanceFuturesUsdtExchangeInfo))]
    [JsonSerializable(typeof(BinanceExchangeInfo))]
    [JsonSerializable(typeof(Binance24HPrice[]))]
    [JsonSerializable(typeof(BinanceAccountInfo))]
    [JsonSerializable(typeof(BinanceFuturesCoin24HPrice))]
    [JsonSerializable(typeof(BinanceCheckTime))]
    [JsonSerializable(typeof(BinanceFuturesCoin24HPrice[]))]
    [JsonSerializable(typeof(BinanceFuturesCoinMarkPrice[]))]
    [JsonSerializable(typeof(BinanceFuturesMarkPrice[]))]
    [JsonSerializable(typeof(BinanceFuturesSymbolBracket[]))]
    internal partial class BinanceSourceGenerationContext : JsonSerializerContext
    {
    }
}