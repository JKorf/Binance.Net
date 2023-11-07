using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.Blvt;
using Binance.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Binance.Net.Objects.Sockets.Converters
{
    internal class BinanceSpotStreamConverter : SocketConverter
    {

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
            { "outboundAccountPosition", typeof(BinanceCombinedStream<BinanceStreamPositionsUpdate>) },
            { "balanceUpdate", typeof(BinanceCombinedStream<BinanceStreamBalanceUpdate>) },
            { "executionReport", typeof(BinanceCombinedStream<BinanceStreamOrderUpdate>) },
            { "listStatus", typeof(BinanceCombinedStream<BinanceStreamOrderList>) },
        };
        public override MessageInterpreterPipeline InterpreterPipeline { get; } = new MessageInterpreterPipeline
        {
            PostInspectCallbacks = new List<PostInspectCallback>
            {
                new PostInspectCallback
                {
                    TypeFields = new List<string> { "id" },
                    Callback = GetDeserializationTypeQueryResponse
                },
                new PostInspectCallback
                {
                    TypeFields = new List<string> { "stream", "data:e" },
                    Callback = GetDeserializationTypeStreamEvent
                }
            }
        };

        private static PostInspectResult GetDeserializationTypeQueryResponse(Dictionary<string, string> idValues, IDictionary<string, IMessageProcessor> processors)
        {
            if(!processors.TryGetValue(idValues["id"], out var processor))
            {
                // Probably shouldn't be exception
                throw new Exception("Unknown update type");
            }

            return new PostInspectResult { Type = processor.ExpectedMessageType, Identifier = idValues["id"] };
        }

        private static PostInspectResult GetDeserializationTypeStreamEvent(Dictionary<string, string> idValues, IDictionary<string, IMessageProcessor> processors)
        {
            var streamId = idValues["stream"]!;
            if (_streamIdMapping.TryGetValue(streamId, out var streamIdMapping))
                return new PostInspectResult { Type = streamIdMapping, Identifier = idValues["stream"].ToLowerInvariant() };

            var eventType = idValues["data:e"]!;
            if (_eventTypeMapping.TryGetValue(eventType, out var eventTypeMapping))
                return new PostInspectResult { Type = eventTypeMapping, Identifier = idValues["stream"].ToLowerInvariant() };

            // These are single events but don't have an 'e' event identifier
            if (streamId.EndsWith("@bookTicker")) return new PostInspectResult { Type = typeof(BinanceCombinedStream<BinanceStreamBookPrice>), Identifier = idValues["stream"].ToLowerInvariant() };
            if (streamId.Contains("@depth")) return new PostInspectResult { Type = typeof(BinanceCombinedStream<BinanceOrderBook>), Identifier = idValues["stream"].ToLowerInvariant() };

            return new PostInspectResult();
        }
    }
}