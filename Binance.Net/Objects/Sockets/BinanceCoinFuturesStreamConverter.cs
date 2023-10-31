using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures.Socket;
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
    internal class BinanceCoinFuturesStreamConverter : SocketConverter
    {
        public override string[] SubscriptionIdFields => new[] { "stream" }; 
        public override string[] TypeIdFields => new[] { "id", "data:e", "stream" };

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

            { "MARGIN_CALL", typeof(BinanceFuturesStreamMarginUpdate) },
            { "ACCOUNT_UPDATE", typeof(BinanceFuturesStreamAccountUpdate) },
            { "ORDER_TRADE_UPDATE", typeof(BinanceFuturesStreamOrderUpdate) },
            { "ACCOUNT_CONFIG_UPDATE", typeof(BinanceFuturesStreamConfigUpdate) },
            { "STRATEGY_UPDATE", typeof(BinanceStrategyUpdate) },
            { "GRID_UPDATE", typeof(BinanceGridUpdate) }
        };

        public override Type? GetDeserializationType(Dictionary<string, string?> idValues, List<MessageListener> listeners)
        {
            if (idValues["id"] != null)
                return typeof(BinanceSocketQueryResponse);
            
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
