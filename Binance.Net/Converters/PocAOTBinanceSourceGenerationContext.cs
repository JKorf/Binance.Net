using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Converters
{
    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(int?))]
    [JsonSerializable(typeof(Binance.Net.Objects.Models.Spot.BinanceOrderBook))]
    [JsonSerializable(typeof(Binance.Net.Objects.Sockets.BinanceSocketQueryResponse))]
    [JsonSerializable(typeof(Binance.Net.Objects.Models.BinanceCombinedStream<Binance.Net.Objects.Models.Spot.BinanceEventOrderBook>))]
    [JsonSerializable(typeof(Binance.Net.Objects.Internal.BinanceSocketRequest))]
    [JsonSerializable(typeof(Binance.Net.Objects.Models.Spot.BinanceExchangeInfo))]
    [JsonSerializable(typeof(Enums.SymbolFilterType))]
    [JsonSerializable(typeof(Binance.Net.Objects.Models.BinanceOrderBookEntry))]
    [JsonSerializable(typeof(Binance.Net.Objects.Models.Spot.BinanceOrderBook))]
    [JsonSerializable(typeof(Binance.Net.Objects.Models.Spot.BinanceEventOrderBook))]
    internal partial class PocAOTBinanceSourceGenerationContext : JsonSerializerContext
    {
        //static CryptoExchange.Net.Converters.SystemTextJson.EnumConverter.EnumConverterInner<Binance.Net.Enums.RateLimitInterval> enumConverterInner = new EnumConverter.EnumConverterInner<Enums.RateLimitInterval>();
    }
}
