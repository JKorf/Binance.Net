using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures.Socket;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.Blvt;
using Binance.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Sockets.Converters
{
    internal class BinanceCoinFuturesStreamConverter : SocketConverter
    {
        private static Dictionary<string, Type> _streamIdMapping = new Dictionary<string, Type>
        {
            { "!markPrice@arr", typeof(BinanceCombinedStream<IEnumerable<BinanceFuturesCoinStreamMarkPrice>>) },
            { "!markPrice@arr@1s", typeof(BinanceCombinedStream<IEnumerable<BinanceFuturesCoinStreamMarkPrice>>) },
            { "!miniTicker@arr", typeof(BinanceCombinedStream<IEnumerable<BinanceStreamCoinMiniTick>>) },
            { "!ticker@arr", typeof(BinanceCombinedStream<IEnumerable<BinanceStreamCoinTick>>) },
        };

        private static Dictionary<string, Type> _eventTypeMapping = new Dictionary<string, Type>
        {
            { "kline", typeof(BinanceCombinedStream<BinanceFuturesStreamCoinKlineData>) },
            { "indexPriceUpdate", typeof(BinanceCombinedStream<BinanceFuturesStreamIndexPrice>) },
            { "markPriceUpdate", typeof(BinanceCombinedStream<BinanceFuturesCoinStreamMarkPrice>) },
            { "continuous_kline", typeof(BinanceCombinedStream<BinanceStreamKlineData>) },
            { "indexPrice_kline", typeof(BinanceCombinedStream<BinanceStreamIndexKlineData>) },
            { "markPrice_kline", typeof(BinanceCombinedStream<BinanceStreamIndexKlineData>) },
            { "24hrMiniTicker", typeof(BinanceCombinedStream<BinanceStreamCoinMiniTick>) },
            { "24hrTicker", typeof(BinanceCombinedStream<BinanceStreamCoinTick>) },
            { "aggTrade", typeof(BinanceCombinedStream<BinanceStreamAggregatedTrade>) },
            { "trade", typeof(BinanceCombinedStream<BinanceStreamTrade>) },
            { "bookTicker", typeof(BinanceCombinedStream<BinanceFuturesStreamBookPrice>) },
            { "forceOrder", typeof(BinanceCombinedStream<BinanceFuturesStreamLiquidationData>) },
            { "depthUpdate", typeof(BinanceCombinedStream<BinanceFuturesStreamOrderBookDepth>) },
            { "contractInfo", typeof(BinanceCombinedStream<BinanceFuturesStreamSymbolUpdate>) },

            { "MARGIN_CALL", typeof(BinanceCombinedStream<BinanceFuturesStreamMarginUpdate>) },
            { "ACCOUNT_UPDATE", typeof(BinanceCombinedStream<BinanceFuturesStreamAccountUpdate>) },
            { "ORDER_TRADE_UPDATE", typeof(BinanceCombinedStream<BinanceFuturesStreamOrderUpdate>) },
            { "ACCOUNT_CONFIG_UPDATE", typeof(BinanceCombinedStream<BinanceFuturesStreamConfigUpdate>) },
            { "STRATEGY_UPDATE", typeof(BinanceCombinedStream<BinanceStrategyUpdate>) },
            { "GRID_UPDATE", typeof(BinanceCombinedStream<BinanceGridUpdate>) }
        };

        public override MessageInterpreterPipeline InterpreterPipeline { get; } = new MessageInterpreterPipeline
        {
            //PostInspectCallbacks = new List<object>
            //{
            //    new PostInspectCallback
            //    {
            //        TypeFields = new List<TypeField> { new TypeField("id") },
            //        Callback = GetDeserializationTypeQueryResponse
            //    },
            //    new PostInspectCallback
            //    {
            //        TypeFields = new List<TypeField> { new TypeField("stream"), new TypeField("data:e") },
            //        Callback = GetDeserializationTypeStreamEvent
            //    }
            //}
        };

        public static PostInspectResult? GetDeserializationTypeStreamEvent(IMessageAccessor accessor, Dictionary<string, Type> processors)
        {
            var streamId = accessor.GetStringValue("stream")!;
            if (_streamIdMapping.TryGetValue(streamId, out var streamIdMapping))
                return new PostInspectResult { Type = streamIdMapping, Identifier = streamId.ToLowerInvariant() };

            var eventType = accessor.GetStringValue("data:e")!;
            if (_eventTypeMapping.TryGetValue(eventType, out var eventTypeMapping))
                return new PostInspectResult { Type = eventTypeMapping, Identifier = streamId.ToLowerInvariant() };

            return null;
        }

        public static PostInspectResult? GetDeserializationTypeQueryResponse(IMessageAccessor accessor, Dictionary<string, Type> processors)
        {
            return new PostInspectResult { Type = typeof(BinanceSocketQueryResponse), Identifier = accessor.GetStringValue("id") };
        }
    }
}
