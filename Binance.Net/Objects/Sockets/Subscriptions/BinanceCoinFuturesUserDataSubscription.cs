using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures.Socket;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;

namespace Binance.Net.Objects.Sockets
{
    /// <inheritdoc />
    internal class BinanceCoinFuturesUserDataSubscription : Subscription
    {
        private readonly string _lk;

        private readonly Action<DataEvent<BinanceFuturesStreamOrderUpdate>>? _orderHandler;
        private readonly Action<DataEvent<BinanceFuturesStreamConfigUpdate>>? _configHandler;
        private readonly Action<DataEvent<BinanceFuturesStreamMarginUpdate>>? _marginHandler;
        private readonly Action<DataEvent<BinanceFuturesStreamAccountUpdate>>? _accountHandler;
        private readonly Action<DataEvent<BinanceStreamEvent>>? _listenkeyHandler;
        private readonly Action<DataEvent<BinanceStrategyUpdate>>? _strategyHandler;
        private readonly Action<DataEvent<BinanceGridUpdate>>? _gridHandler;

        /// <summary>
        /// ctor
        /// </summary>
        public BinanceCoinFuturesUserDataSubscription(
            ILogger logger,
            string listenKey,
            Action<DataEvent<BinanceFuturesStreamOrderUpdate>>? orderHandler,
            Action<DataEvent<BinanceFuturesStreamConfigUpdate>>? configHandler,
            Action<DataEvent<BinanceFuturesStreamMarginUpdate>>? marginHandler,
            Action<DataEvent<BinanceFuturesStreamAccountUpdate>>? accountHandler,
            Action<DataEvent<BinanceStreamEvent>>? listenkeyHandler,
            Action<DataEvent<BinanceStrategyUpdate>>? strategyHandler,
            Action<DataEvent<BinanceGridUpdate>>? gridHandler) : base(logger, false)
        {
            _orderHandler = orderHandler;
            _configHandler = configHandler;
            _marginHandler = marginHandler;
            _accountHandler = accountHandler;
            _listenkeyHandler = listenkeyHandler;
            _strategyHandler = strategyHandler;
            _gridHandler = gridHandler;

            _lk = listenKey;

            MessageRouter = MessageRouter.Create([
                MessageRoute<BinanceCombinedStream<BinanceFuturesStreamConfigUpdate>>.CreateWithTopicFilter("ACCOUNT_CONFIG_UPDATE", _lk, DoHandleMessage),
                MessageRoute<BinanceCombinedStream<BinanceFuturesStreamMarginUpdate>>.CreateWithTopicFilter("MARGIN_CALL", _lk, DoHandleMessage),
                MessageRoute<BinanceCombinedStream<BinanceFuturesStreamAccountUpdate>>.CreateWithTopicFilter("ACCOUNT_UPDATE", _lk, DoHandleMessage),
                MessageRoute<BinanceCombinedStream<BinanceFuturesStreamOrderUpdate>>.CreateWithTopicFilter("ORDER_TRADE_UPDATE", _lk, DoHandleMessage),
                MessageRoute<BinanceCombinedStream<BinanceStreamEvent>>.CreateWithTopicFilter("listenKeyExpired", _lk, DoHandleMessage),
                MessageRoute<BinanceCombinedStream<BinanceStrategyUpdate>>.CreateWithTopicFilter("STRATEGY_UPDATE", _lk, DoHandleMessage),
                MessageRoute<BinanceCombinedStream<BinanceGridUpdate>>.CreateWithTopicFilter("GRID_UPDATE", _lk, DoHandleMessage)
                ]);

            MessageMatcher = MessageMatcher.Create([
                new MessageHandlerLink<BinanceCombinedStream<BinanceFuturesStreamConfigUpdate>>(_lk + "ACCOUNT_CONFIG_UPDATE", DoHandleMessage),
                new MessageHandlerLink<BinanceCombinedStream<BinanceFuturesStreamMarginUpdate>>(_lk + "MARGIN_CALL", DoHandleMessage),
                new MessageHandlerLink<BinanceCombinedStream<BinanceFuturesStreamAccountUpdate>>(_lk + "ACCOUNT_UPDATE", DoHandleMessage),
                new MessageHandlerLink<BinanceCombinedStream<BinanceFuturesStreamOrderUpdate>>(_lk + "ORDER_TRADE_UPDATE", DoHandleMessage),
                new MessageHandlerLink<BinanceCombinedStream<BinanceStreamEvent>>(_lk + "listenKeyExpired", DoHandleMessage),
                new MessageHandlerLink<BinanceCombinedStream<BinanceStrategyUpdate>>(_lk + "STRATEGY_UPDATE", DoHandleMessage),
                new MessageHandlerLink<BinanceCombinedStream<BinanceGridUpdate>>(_lk + "GRID_UPDATE", DoHandleMessage)
                ]);
        }

        /// <inheritdoc />
        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return new BinanceSystemQuery<BinanceSocketQueryResponse>(new BinanceSocketRequest
            {
                Method = "SUBSCRIBE",
                Params = [_lk],
                Id = ExchangeHelpers.NextId()
            }, false);
        }

        /// <inheritdoc />
        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return new BinanceSystemQuery<BinanceSocketQueryResponse>(new BinanceSocketRequest
            {
                Method = "UNSUBSCRIBE",
                Params = [_lk],
                Id = ExchangeHelpers.NextId()
            }, false);
        }


        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceFuturesStreamConfigUpdate> message)
        {
            message.Data.ListenKey = message.Stream;
            _configHandler?.Invoke(
                new DataEvent<BinanceFuturesStreamConfigUpdate>(BinanceExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithDataTimestamp(message.Data.EventTime)
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceFuturesStreamMarginUpdate> message)
        {
            message.Data.ListenKey = message.Stream;
            _marginHandler?.Invoke(
                new DataEvent<BinanceFuturesStreamMarginUpdate>(BinanceExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithDataTimestamp(message.Data.EventTime)
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceFuturesStreamAccountUpdate> message)
        {
            message.Data.ListenKey = message.Stream;
            _accountHandler?.Invoke(
                new DataEvent<BinanceFuturesStreamAccountUpdate>(BinanceExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithDataTimestamp(message.Data.EventTime)
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceFuturesStreamOrderUpdate> message)
        {
            message.Data.ListenKey = message.Stream;
            _orderHandler?.Invoke(
                new DataEvent<BinanceFuturesStreamOrderUpdate>(BinanceExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithSymbol(message.Data.UpdateData.Symbol)
                    .WithDataTimestamp(message.Data.EventTime)
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceStreamEvent> message)
        {
            _listenkeyHandler?.Invoke(
                new DataEvent<BinanceStreamEvent>(BinanceExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithDataTimestamp(message.Data.EventTime)
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceStrategyUpdate> message)
        {
            _strategyHandler?.Invoke(
                new DataEvent<BinanceStrategyUpdate>(BinanceExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithDataTimestamp(message.Data.EventTime)
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceGridUpdate> message)
        {
            _gridHandler?.Invoke(
                new DataEvent<BinanceGridUpdate>(BinanceExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithDataTimestamp(message.Data.EventTime)
                );
            return CallResult.SuccessResult;
        }
    }
}
