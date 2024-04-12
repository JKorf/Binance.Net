﻿using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures.Socket;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;

namespace Binance.Net.Objects.Sockets
{
    /// <inheritdoc />
    internal class BinanceCoinFuturesUserDataSubscription : Subscription<BinanceSocketQueryResponse, BinanceSocketQueryResponse>
    {
        private static readonly MessagePath _ePath = MessagePath.Get().Property("data").Property("e");

        /// <inheritdoc />
        public override HashSet<string> ListenerIdentifiers { get; set; }

        private readonly Action<DataEvent<BinanceFuturesStreamOrderUpdate>>? _orderHandler;
        private readonly Action<DataEvent<BinanceFuturesStreamConfigUpdate>>? _configHandler;
        private readonly Action<DataEvent<BinanceFuturesStreamMarginUpdate>>? _marginHandler;
        private readonly Action<DataEvent<BinanceFuturesStreamAccountUpdate>>? _accountHandler;
        private readonly Action<DataEvent<BinanceStreamEvent>>? _listenkeyHandler;
        private readonly Action<DataEvent<BinanceStrategyUpdate>>? _strategyHandler;
        private readonly Action<DataEvent<BinanceGridUpdate>>? _gridHandler;

        /// <inheritdoc />
        public override Type? GetMessageType(IMessageAccessor message)
        {
            var identifier = message.GetValue<string>(_ePath);
            if (string.Equals(identifier, "ACCOUNT_CONFIG_UPDATE", StringComparison.Ordinal))
                return typeof(BinanceCombinedStream<BinanceFuturesStreamConfigUpdate>);
            if (string.Equals(identifier, "MARGIN_CALL", StringComparison.Ordinal))
                return typeof(BinanceCombinedStream<BinanceFuturesStreamMarginUpdate>);
            if (string.Equals(identifier, "ACCOUNT_UPDATE", StringComparison.Ordinal))
                return typeof(BinanceCombinedStream<BinanceFuturesStreamAccountUpdate>);
            if (string.Equals(identifier, "ORDER_TRADE_UPDATE", StringComparison.Ordinal))
                return typeof(BinanceCombinedStream<BinanceFuturesStreamOrderUpdate>);
            if (string.Equals(identifier, "listenKeyExpired", StringComparison.Ordinal))
                return typeof(BinanceCombinedStream<BinanceStreamEvent>);
            if (string.Equals(identifier, "STRATEGY_UPDATE", StringComparison.Ordinal))
                return typeof(BinanceCombinedStream<BinanceStrategyUpdate>);
            if (string.Equals(identifier, "GRID_UPDATE", StringComparison.Ordinal))
                return typeof(BinanceCombinedStream<BinanceGridUpdate>);

            return null;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="topics"></param>
        /// <param name="orderHandler"></param>
        /// <param name="configHandler"></param>
        /// <param name="marginHandler"></param>
        /// <param name="accountHandler"></param>
        /// <param name="listenkeyHandler"></param>
        /// <param name="strategyHandler"></param>
        /// <param name="gridHandler"></param>
        public BinanceCoinFuturesUserDataSubscription(
            ILogger logger,
            List<string> topics,
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
            ListenerIdentifiers = new HashSet<string>(topics);
        }

        /// <inheritdoc />
        public override Query? GetSubQuery(SocketConnection connection)
        {
            return new BinanceSystemQuery<BinanceSocketQueryResponse>(new BinanceSocketRequest
            {
                Method = "SUBSCRIBE",
                Params = ListenerIdentifiers.ToArray(),
                Id = ExchangeHelpers.NextId()
            }, false);
        }

        /// <inheritdoc />
        public override Query? GetUnsubQuery()
        {
            return new BinanceSystemQuery<BinanceSocketQueryResponse>(new BinanceSocketRequest
            {
                Method = "UNSUBSCRIBE",
                Params = ListenerIdentifiers.ToArray(),
                Id = ExchangeHelpers.NextId()
            }, false);
        }


        /// <inheritdoc />
        public override CallResult DoHandleMessage(SocketConnection connection, DataEvent<object> message)
        {
            if (message.Data is BinanceCombinedStream<BinanceFuturesStreamConfigUpdate> configUpdate)
            {
                configUpdate.Data.ListenKey = configUpdate.Stream;
                _configHandler?.Invoke(message.As(configUpdate.Data, configUpdate.Stream, SocketUpdateType.Update));
            }
            else if (message.Data is BinanceCombinedStream<BinanceFuturesStreamMarginUpdate> marginUpdate)
            {
                marginUpdate.Data.ListenKey = marginUpdate.Stream;
                _marginHandler?.Invoke(message.As(marginUpdate.Data, marginUpdate.Stream, SocketUpdateType.Update));
            }
            else if (message.Data is BinanceCombinedStream<BinanceFuturesStreamAccountUpdate> accountUpdate)
            {
                accountUpdate.Data.ListenKey = accountUpdate.Stream;
                _accountHandler?.Invoke(message.As(accountUpdate.Data, accountUpdate.Stream, SocketUpdateType.Update));
            }
            else if (message.Data is BinanceCombinedStream<BinanceFuturesStreamOrderUpdate> orderUpate)
            {
                orderUpate.Data.ListenKey = orderUpate.Stream;
                _orderHandler?.Invoke(message.As(orderUpate.Data, orderUpate.Stream, SocketUpdateType.Update));
            }
            else if (message.Data is BinanceCombinedStream<BinanceStreamEvent> listenKeyUpdate)
            {
                _listenkeyHandler?.Invoke(message.As(listenKeyUpdate.Data, listenKeyUpdate.Stream, SocketUpdateType.Update));
            }
            else if (message.Data is BinanceCombinedStream<BinanceStrategyUpdate> strategyUpdate)
            {
                _strategyHandler?.Invoke(message.As(strategyUpdate.Data, strategyUpdate.Stream, SocketUpdateType.Update));
            }
            else if (message.Data is BinanceCombinedStream<BinanceGridUpdate> gridUpdate)
            {
                _gridHandler?.Invoke(message.As(gridUpdate.Data, gridUpdate.Stream, SocketUpdateType.Update));
            }

            return new CallResult(null);
        }
    }
}
