using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures.Socket;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.Blvt;
using Binance.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
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

        public static PostInspectResult? GetDeserializationTypeStreamEvent(Dictionary<string, string> idValues, IDictionary<string, IMessageProcessor> processors)
        {
            var streamId = idValues["stream"]!;
            if (_streamIdMapping.TryGetValue(streamId, out var streamIdMapping))
                return new PostInspectResult { Type = streamIdMapping, Identifier = idValues["stream"].ToLowerInvariant() };

            var eventType = idValues["data:e"]!;
            if (_eventTypeMapping.TryGetValue(eventType, out var eventTypeMapping))
                return new PostInspectResult { Type = eventTypeMapping, Identifier = idValues["stream"].ToLowerInvariant() };

            return null;
        }

        public static PostInspectResult? GetDeserializationTypeQueryResponse(Dictionary<string, string> idValues, IDictionary<string, IMessageProcessor> processors)
        {
            return new PostInspectResult { Type = typeof(BinanceSocketQueryResponse), Identifier = idValues["id"] };
        }
    }
}
