﻿using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures.Socket;
using CryptoExchange.Net.Converters.MessageParsing;
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
            Action<DataEvent<BinanceConditionOrderTriggerRejectUpdate>>? condOrderHandler) : base(logger, false)
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
                ]);
        }

        /// <inheritdoc />
        public override Query? GetSubQuery(SocketConnection connection)
        {
            return new BinanceSystemQuery<BinanceSocketQueryResponse>(new BinanceSocketRequest
            {
                Method = "SUBSCRIBE",
                Params = [_lk],
                Id = ExchangeHelpers.NextId()
            }, false);
        }

        /// <inheritdoc />
        public override Query? GetUnsubQuery()
        {
            return new BinanceSystemQuery<BinanceSocketQueryResponse>(new BinanceSocketRequest
            {
                Method = "UNSUBSCRIBE",
                Params = [_lk],
                Id = ExchangeHelpers.NextId()
            }, false);
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<BinanceCombinedStream<BinanceFuturesStreamConfigUpdate>> message)
        {
            message.Data.Data.ListenKey = message.Data.Stream;
            _configHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<BinanceCombinedStream<BinanceFuturesStreamMarginUpdate>> message)
        {
            message.Data.Data.ListenKey = message.Data.Stream;
            _marginHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<BinanceCombinedStream<BinanceFuturesStreamAccountUpdate>> message)
        {
            message.Data.Data.ListenKey = message.Data.Stream;
            _accountHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<BinanceCombinedStream<BinanceFuturesStreamOrderUpdate>> message)
        {
            message.Data.Data.ListenKey = message.Data.Stream;
            _orderHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, message.Data.Data.UpdateData.Symbol, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<BinanceCombinedStream<BinanceStreamEvent>> message)
        {
            _listenkeyHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<BinanceCombinedStream<BinanceStrategyUpdate>> message)
        {
            _strategyHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<BinanceCombinedStream<BinanceGridUpdate>> message)
        {
            _gridHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<BinanceCombinedStream<BinanceConditionOrderTriggerRejectUpdate>> message)
        {
            _condOrderHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<BinanceCombinedStream<BinanceFuturesStreamTradeUpdate>> message)
        {
            _tradeHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, message.Data.Data.Symbol, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));
            return CallResult.SuccessResult;
        }
    }
}
