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
    internal class BinanceUsdFuturesStreamConverter : SocketConverter
    {

        private static Dictionary<string, Type> _streamIdMapping = new Dictionary<string, Type>
        {
            { "!markPrice@arr", typeof(BinanceCombinedStream<IEnumerable<BinanceFuturesUsdtStreamMarkPrice>>) },
            { "!markPrice@arr@1s", typeof(BinanceCombinedStream<IEnumerable<BinanceFuturesUsdtStreamMarkPrice>>) },
            { "!miniTicker@arr", typeof(BinanceCombinedStream<IEnumerable<BinanceStreamMiniTick>>) },
            { "!ticker@arr", typeof(BinanceCombinedStream<IEnumerable<BinanceStreamTick>>) },
            { "!assetIndex@arr", typeof(BinanceCombinedStream<IEnumerable<BinanceFuturesStreamAssetIndexUpdate>>) },

        };

        private static Dictionary<string, Type> _eventTypeMapping = new Dictionary<string, Type>
        {
            { "trade", typeof(BinanceCombinedStream<BinanceStreamTrade>) },
            { "aggTrade", typeof(BinanceCombinedStream<BinanceStreamAggregatedTrade>) },
            { "markPriceUpdate", typeof(BinanceCombinedStream<BinanceFuturesUsdtStreamMarkPrice>) },
            { "kline", typeof(BinanceCombinedStream<BinanceStreamKlineData>) },
            { "continuous_kline", typeof(BinanceCombinedStream<BinanceStreamContinuousKlineData>) },
            { "24hrMiniTicker", typeof(BinanceCombinedStream<BinanceStreamMiniTick>) },
            { "24hrTicker", typeof(BinanceCombinedStream<BinanceStreamTick>) },
            { "bookTicker", typeof(BinanceCombinedStream<BinanceFuturesStreamBookPrice>) },
            { "forceOrder", typeof(BinanceCombinedStream<BinanceFuturesStreamLiquidationData>) },
            { "depthUpdate", typeof(BinanceCombinedStream<BinanceFuturesStreamOrderBookDepth>) },
            { "compositeIndex", typeof(BinanceCombinedStream<BinanceFuturesStreamCompositeIndex>) },
            { "contractInfo", typeof(BinanceCombinedStream<BinanceFuturesStreamSymbolUpdate>) },
            { "assetIndexUpdate", typeof(BinanceCombinedStream<BinanceFuturesStreamMarginUpdate>) },

            { "listenKeyExpired", typeof(BinanceCombinedStream<BinanceStreamEvent>) },
            { "MARGIN_CALL", typeof(BinanceCombinedStream<BinanceFuturesStreamMarginUpdate>) },
            { "ACCOUNT_UPDATE", typeof(BinanceCombinedStream<BinanceFuturesStreamAccountUpdate>) },
            { "ORDER_TRADE_UPDATE", typeof(BinanceCombinedStream<BinanceFuturesStreamOrderUpdate>) },
            { "ACCOUNT_CONFIG_UPDATE", typeof(BinanceCombinedStream<BinanceFuturesStreamConfigUpdate>) },
            { "STRATEGY_UPDATE", typeof(BinanceCombinedStream<BinanceStrategyUpdate>) },
            { "GRID_UPDATE", typeof(BinanceCombinedStream<BinanceGridUpdate>) },
            { "CONDITIONAL_ORDER_TRIGGER_REJECT", typeof(BinanceCombinedStream<BinanceConditionOrderTriggerRejectUpdate>) },
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

        public static PostInspectResult GetDeserializationTypeQueryResponse(IMessageAccessor accessor, Dictionary<string, Type> processors)
        {
            return new PostInspectResult { Type = typeof(BinanceSocketQueryResponse), Identifier = accessor.GetStringValue("id") };
        }

        public static PostInspectResult GetDeserializationTypeStreamEvent(IMessageAccessor accessor, Dictionary<string, Type> processors)
        {
            var streamId = accessor.GetStringValue("stream")!;
            if (_streamIdMapping.TryGetValue(streamId, out var streamIdMapping))
                return new PostInspectResult { Type = streamIdMapping, Identifier = streamId.ToLowerInvariant() };

            var eventType = accessor.GetStringValue("data:e")!;
            if (_eventTypeMapping.TryGetValue(eventType, out var eventTypeMapping))
                return new PostInspectResult { Type = eventTypeMapping, Identifier = streamId.ToLowerInvariant() };

            return new PostInspectResult();
        }
    }
}
