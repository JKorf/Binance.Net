using Binance.Net.Enums;
using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Sockets;

[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(int?))]
[JsonSerializable(typeof(BinanceExchangeInfo))]
[JsonSerializable(typeof(BinanceFuturesUsdtExchangeInfo))]
[JsonSerializable(typeof(BinanceFuturesCoinExchangeInfo))]
[JsonSerializable(typeof(SymbolFilterType))]
[JsonSerializable(typeof(BinanceCombinedStream<BinanceEventOrderBook>))]
[JsonSerializable(typeof(BinanceOrderBookEntry))]
[JsonSerializable(typeof(BinanceEventOrderBook))]
[JsonSerializable(typeof(BinanceOrderBook))]
[JsonSerializable(typeof(BinanceSocketQueryResponse))]
[JsonSerializable(typeof(BinanceSocketRequest))]

internal partial class BinanceSourceGenerationContext : JsonSerializerContext
{
}
