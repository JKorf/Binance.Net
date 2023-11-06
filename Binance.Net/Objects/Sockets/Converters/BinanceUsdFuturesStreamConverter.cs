using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures.Socket;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.Blvt;
using Binance.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Converters;
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

        public override List<StreamMessageParseCallback> InterpreterPipeline { get; } = new List<StreamMessageParseCallback>
        {
            new StreamMessageParseCallback
            {
                TypeFields = new List<string> { "id" },
                IdFields = new List<string> { "id" },
                Callback = GetDeserializationTypeQueryResponse
            },
            new StreamMessageParseCallback
            {
                IdFields = new List<string> { "stream" },
                TypeFields = new List<string> { "stream", "data:e" },
                Callback = GetDeserializationTypeStreamEvent
            }
        };

        public static Type? GetDeserializationTypeQueryResponse(Dictionary<string, string> idValues, IEnumerable<BasePendingRequest> pendingRequests, IEnumerable<Subscription> listeners)
        {
            return typeof(BinanceSocketQueryResponse);
        }

        public static Type? GetDeserializationTypeStreamEvent(Dictionary<string, string> idValues, IEnumerable<BasePendingRequest> pendingRequests, IEnumerable<Subscription> listeners)
        {
            var streamId = idValues["stream"]!;
            if (_streamIdMapping.TryGetValue(streamId, out var streamIdMapping))
                return streamIdMapping;

            var eventType = idValues["data:e"]!;
            if (_eventTypeMapping.TryGetValue(eventType, out var eventTypeMapping))
                return eventTypeMapping;

            return null;
        }
    }
}
