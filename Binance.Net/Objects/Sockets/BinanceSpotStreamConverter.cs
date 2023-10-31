using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.Blvt;
using Binance.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects.Sockets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Sockets
{
    internal class BinanceSpotStreamConverter : SocketConverter
    {
        public override string[] SubscriptionIdFields => new[] { "stream" }; 
        public override string[] TypeIdFields => new[] { "id", "data:e", "stream" };

        private static Dictionary<string, Type> _streamIdMapping = new Dictionary<string, Type>
        {
            { "!miniTicker@arr", typeof(BinanceCombinedStream<IEnumerable<BinanceStreamMiniTick>>) },
            { "!ticker@arr", typeof(BinanceCombinedStream<IEnumerable<BinanceStreamTick>>) },
            { "!ticker_1h@arr", typeof(BinanceCombinedStream<IEnumerable<BinanceStreamRollingWindowTick>>) },
            { "!ticker_4h@arr", typeof(BinanceCombinedStream<IEnumerable<BinanceStreamRollingWindowTick>>) },
            { "!ticker_1d@arr", typeof(BinanceCombinedStream<IEnumerable<BinanceStreamRollingWindowTick>>) },
        };

        private static Dictionary<string, Type> _eventTypeMapping = new Dictionary<string, Type>
        {
            { "trade", typeof(BinanceCombinedStream<BinanceStreamTrade>) },
            { "kline", typeof(BinanceCombinedStream<BinanceStreamKlineData>) },
            { "aggTrade", typeof(BinanceCombinedStream<BinanceStreamAggregatedTrade>) },
            { "24hrMiniTicker", typeof(BinanceCombinedStream<BinanceStreamMiniTick>) },
            { "24hrTicker", typeof(BinanceCombinedStream<BinanceStreamTick>) },
            { "1hTicker", typeof(BinanceCombinedStream<BinanceStreamRollingWindowTick>) },
            { "4hTicker", typeof(BinanceCombinedStream<BinanceStreamRollingWindowTick>) },
            { "1dTicker", typeof(BinanceCombinedStream<BinanceStreamRollingWindowTick>) },
            { "depthUpdate", typeof(BinanceCombinedStream<BinanceEventOrderBook>) },
            { "nav", typeof(BinanceCombinedStream<BinanceBlvtInfoUpdate>) },
            { "outboundAccountPosition", typeof(BinanceStreamPositionsUpdate) },
            { "balanceUpdate", typeof(BinanceStreamBalanceUpdate) },
            { "executionReport", typeof(BinanceStreamOrderUpdate) },
            { "listStatus", typeof(BinanceStreamOrderList) },
        };

        public override Type? GetDeserializationType(Dictionary<string, string?> idValues, List<MessageListener> listeners)
        {
            if (idValues["id"] != null)
                return typeof(BinanceSocketQueryResponse);
            
            var streamId = idValues["stream"]!;
            if (_streamIdMapping.TryGetValue(streamId, out var streamIdMapping))
                return streamIdMapping;

            var eventType = idValues["data:e"];
            if (_eventTypeMapping.TryGetValue(streamId, out var eventTypeMapping))
                return eventTypeMapping;

            // These are single events but don't have an 'e' event identifier
            if (streamId.EndsWith("@bookTicker")) return typeof(BinanceCombinedStream<BinanceStreamBookPrice>);
            if (streamId.Contains("@depth")) return typeof(BinanceCombinedStream<BinanceOrderBook>);

            return null;
        }
    }
}
