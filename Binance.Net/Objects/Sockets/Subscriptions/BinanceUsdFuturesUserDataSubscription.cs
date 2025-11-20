using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures.Socket;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;

namespace Binance.Net.Objects.Sockets
{
    /// <inheritdoc />
    internal class BinanceUsdFuturesUserDataSubscription : Subscription<BinanceSocketQueryResponse, BinanceSocketQueryResponse>
    {
        private readonly string _lk;

        private readonly Action<DataEvent<BinanceFuturesStreamOrderUpdate>>? _orderHandler;
        private readonly Action<DataEvent<BinanceFuturesStreamTradeUpdate>>? _tradeHandler;
        private readonly Action<DataEvent<BinanceFuturesStreamConfigUpdate>>? _configHandler;
        private readonly Action<DataEvent<BinanceFuturesStreamMarginUpdate>>? _marginHandler;
        private readonly Action<DataEvent<BinanceFuturesStreamAccountUpdate>>? _accountHandler;
        private readonly Action<DataEvent<BinanceStreamEvent>>? _listenkeyHandler;
        private readonly Action<DataEvent<BinanceStrategyUpdate>>? _strategyHandler;
        private readonly Action<DataEvent<BinanceGridUpdate>>? _gridHandler;
        private readonly Action<DataEvent<BinanceConditionOrderTriggerRejectUpdate>>? _condOrderHandler;
        private readonly Action<DataEvent<BinanceAlgoOrderUpdate>>? _algoOrderHandler;

        /// <summary>
        /// ctor
        /// </summary>
        public BinanceUsdFuturesUserDataSubscription(
            ILogger logger,
            string listenKey,
            Action<DataEvent<BinanceFuturesStreamOrderUpdate>>? orderHandler,
            Action<DataEvent<BinanceFuturesStreamTradeUpdate>>? tradeHandler,
            Action<DataEvent<BinanceFuturesStreamConfigUpdate>>? configHandler,
            Action<DataEvent<BinanceFuturesStreamMarginUpdate>>? marginHandler,
            Action<DataEvent<BinanceFuturesStreamAccountUpdate>>? accountHandler,
            Action<DataEvent<BinanceStreamEvent>>? listenkeyHandler,
            Action<DataEvent<BinanceStrategyUpdate>>? strategyHandler,
            Action<DataEvent<BinanceGridUpdate>>? gridHandler,
            Action<DataEvent<BinanceConditionOrderTriggerRejectUpdate>>? condOrderHandler,
            Action<DataEvent<BinanceAlgoOrderUpdate>>? onAlgoOrderUpdate) : base(logger, false)
        {
            _orderHandler = orderHandler;
            _configHandler = configHandler;
            _marginHandler = marginHandler;
            _accountHandler = accountHandler;
            _listenkeyHandler = listenkeyHandler;
            _strategyHandler = strategyHandler;
            _gridHandler = gridHandler;
            _condOrderHandler = condOrderHandler;
            _tradeHandler = tradeHandler;
            _algoOrderHandler = onAlgoOrderUpdate;

            _lk = listenKey;

            MessageMatcher = MessageMatcher.Create([
                new MessageHandlerLink<BinanceCombinedStream<BinanceFuturesStreamConfigUpdate>>(_lk + "ACCOUNT_CONFIG_UPDATE", DoHandleMessage),
                new MessageHandlerLink<BinanceCombinedStream<BinanceFuturesStreamMarginUpdate>>(_lk + "MARGIN_CALL", DoHandleMessage),
                new MessageHandlerLink<BinanceCombinedStream<BinanceFuturesStreamAccountUpdate>>(_lk + "ACCOUNT_UPDATE", DoHandleMessage),
                new MessageHandlerLink<BinanceCombinedStream<BinanceFuturesStreamOrderUpdate>>(_lk + "ORDER_TRADE_UPDATE", DoHandleMessage),
                new MessageHandlerLink<BinanceCombinedStream<BinanceFuturesStreamTradeUpdate>>(_lk + "TRADE_LITE", DoHandleMessage),
                new MessageHandlerLink<BinanceCombinedStream<BinanceStreamEvent>>(_lk + "listenKeyExpired", DoHandleMessage),
                new MessageHandlerLink<BinanceCombinedStream<BinanceStrategyUpdate>>(_lk + "STRATEGY_UPDATE", DoHandleMessage),
                new MessageHandlerLink<BinanceCombinedStream<BinanceGridUpdate>>(_lk + "GRID_UPDATE", DoHandleMessage),
                new MessageHandlerLink<BinanceCombinedStream<BinanceConditionOrderTriggerRejectUpdate>>(_lk + "CONDITIONAL_ORDER_TRIGGER_REJECT", DoHandleMessage),
                new MessageHandlerLink<BinanceCombinedStream<BinanceAlgoOrderUpdate>>(_lk + "ALGO_UPDATE", DoHandleMessage),
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
                new DataEvent<BinanceFuturesStreamConfigUpdate>(message.Data, receiveTime, originalData)
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
                new DataEvent<BinanceFuturesStreamMarginUpdate>(message.Data, receiveTime, originalData)
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
                new DataEvent<BinanceFuturesStreamAccountUpdate>(message.Data, receiveTime, originalData)
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
                new DataEvent<BinanceFuturesStreamOrderUpdate>(message.Data, receiveTime, originalData)
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
                new DataEvent<BinanceStreamEvent>(message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithDataTimestamp(message.Data.EventTime)
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceStrategyUpdate> message)
        {
            _strategyHandler?.Invoke(
                new DataEvent<BinanceStrategyUpdate>(message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithDataTimestamp(message.Data.EventTime)
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceGridUpdate> message)
        {
            _gridHandler?.Invoke(
                new DataEvent<BinanceGridUpdate>(message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithDataTimestamp(message.Data.EventTime)
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceConditionOrderTriggerRejectUpdate> message)
        {
            _condOrderHandler?.Invoke(
                new DataEvent<BinanceConditionOrderTriggerRejectUpdate>(message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithDataTimestamp(message.Data.EventTime)
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceFuturesStreamTradeUpdate> message)
        {
            _tradeHandler?.Invoke(
                new DataEvent<BinanceFuturesStreamTradeUpdate>(message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithSymbol(message.Data.Symbol)
                    .WithDataTimestamp(message.Data.EventTime)
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceAlgoOrderUpdate> message)
        {
            _algoOrderHandler?.Invoke(
                new DataEvent<BinanceAlgoOrderUpdate>(message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithSymbol(message.Data.Order.Symbol)
                    .WithDataTimestamp(message.Data.EventTime)
                );
            return CallResult.SuccessResult;
        }
    }
}
