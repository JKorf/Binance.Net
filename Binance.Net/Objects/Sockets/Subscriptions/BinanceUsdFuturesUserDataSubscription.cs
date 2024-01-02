using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures.Socket;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Binance.Net.Objects.Sockets
{
    /// <inheritdoc />
    public class BinanceUsdFuturesUserDataSubscription : Subscription<BinanceSocketQueryResponse, BinanceCombinedStream<BinanceStreamEvent>>
    {
        /// <inheritdoc />
        public override List<string> StreamIdentifiers { get; set; }

        private readonly Action<DataEvent<BinanceFuturesStreamOrderUpdate>>? _orderHandler;
        private readonly Action<DataEvent<BinanceFuturesStreamConfigUpdate>>? _configHandler;
        private readonly Action<DataEvent<BinanceFuturesStreamMarginUpdate>>? _marginHandler;
        private readonly Action<DataEvent<BinanceFuturesStreamAccountUpdate>>? _accountHandler;
        private readonly Action<DataEvent<BinanceStreamEvent>>? _listenkeyHandler;
        private readonly Action<DataEvent<BinanceStrategyUpdate>>? _strategyHandler;
        private readonly Action<DataEvent<BinanceGridUpdate>>? _gridHandler;
        private readonly Action<DataEvent<BinanceConditionOrderTriggerRejectUpdate>>? _condOrderHandler;

        /// <inheritdoc />
        public override Dictionary<string, Type> TypeMapping { get; } = new Dictionary<string, Type>
        {
            { "listenKeyExpired", typeof(BinanceCombinedStream<BinanceStreamEvent>) },
            { "MARGIN_CALL", typeof(BinanceCombinedStream<BinanceFuturesStreamMarginUpdate>) },
            { "ACCOUNT_UPDATE", typeof(BinanceCombinedStream<BinanceFuturesStreamAccountUpdate>) },
            { "ORDER_TRADE_UPDATE", typeof(BinanceCombinedStream<BinanceFuturesStreamOrderUpdate>) },
            { "ACCOUNT_CONFIG_UPDATE", typeof(BinanceCombinedStream<BinanceFuturesStreamConfigUpdate>) },
            { "STRATEGY_UPDATE", typeof(BinanceCombinedStream<BinanceStrategyUpdate>) },
            { "GRID_UPDATE", typeof(BinanceCombinedStream<BinanceGridUpdate>) },
            { "CONDITIONAL_ORDER_TRIGGER_REJECT", typeof(BinanceCombinedStream<BinanceConditionOrderTriggerRejectUpdate>) },
        };

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
        /// <param name="condOrderHandler"></param>
        public BinanceUsdFuturesUserDataSubscription(
            ILogger logger,
            List<string> topics,
            Action<DataEvent<BinanceFuturesStreamOrderUpdate>>? orderHandler,
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
            StreamIdentifiers = topics;
        }

        /// <inheritdoc />
        public override BaseQuery? GetSubQuery(SocketConnection connection)
        {
            return new BinanceSystemQuery<BinanceSocketQueryResponse>(new BinanceSocketRequest
            {
                Method = "SUBSCRIBE",
                Params = StreamIdentifiers.ToArray(),
                Id = ExchangeHelpers.NextId()
            }, false);
        }

        /// <inheritdoc />
        public override BaseQuery? GetUnsubQuery()
        {
            return new BinanceSystemQuery<BinanceSocketQueryResponse>(new BinanceSocketRequest
            {
                Method = "UNSUBSCRIBE",
                Params = StreamIdentifiers.ToArray(),
                Id = ExchangeHelpers.NextId()
            }, false);
        }

        /// <inheritdoc />
        public override Task<CallResult> DoHandleMessageAsync(SocketConnection connection, DataEvent<BaseParsedMessage> message)
        {
            var data = message.Data.Data;
            if (data is BinanceCombinedStream<BinanceFuturesStreamConfigUpdate> configUpdate)
            {
                configUpdate.Data.ListenKey = configUpdate.Stream;
                _configHandler?.Invoke(message.As(configUpdate.Data, configUpdate.Stream, SocketUpdateType.Update));
            }
            else if (data is BinanceCombinedStream<BinanceFuturesStreamMarginUpdate> marginUpdate)
            {
                marginUpdate.Data.ListenKey = marginUpdate.Stream;
                _marginHandler?.Invoke(message.As(marginUpdate.Data, marginUpdate.Stream, SocketUpdateType.Update));
            }
            else if (data is BinanceCombinedStream<BinanceFuturesStreamAccountUpdate> accountUpdate)
            {
                accountUpdate.Data.ListenKey = accountUpdate.Stream;
                _accountHandler?.Invoke(message.As(accountUpdate.Data, accountUpdate.Stream, SocketUpdateType.Update));
            }
            else if (data is BinanceCombinedStream<BinanceFuturesStreamOrderUpdate> orderUpate)
            {
                orderUpate.Data.ListenKey = orderUpate.Stream;
                _orderHandler?.Invoke(message.As(orderUpate.Data, orderUpate.Stream, SocketUpdateType.Update));
            }
            else if (data is BinanceCombinedStream<BinanceStreamEvent> listenKeyUpdate)
            {
                _listenkeyHandler?.Invoke(message.As(listenKeyUpdate.Data, listenKeyUpdate.Stream, SocketUpdateType.Update));
            }
            else if (data is BinanceCombinedStream<BinanceStrategyUpdate> strategyUpdate)
            {
                _strategyHandler?.Invoke(message.As(strategyUpdate.Data, strategyUpdate.Stream, SocketUpdateType.Update));
            }
            else if (data is BinanceCombinedStream<BinanceGridUpdate> gridUpdate)
            {
                _gridHandler?.Invoke(message.As(gridUpdate.Data, gridUpdate.Stream, SocketUpdateType.Update));
            }
            else if (data is BinanceCombinedStream<BinanceConditionOrderTriggerRejectUpdate> condUpdate)
            {
                _condOrderHandler?.Invoke(message.As(condUpdate.Data, condUpdate.Stream, SocketUpdateType.Update));
            }
            return Task.FromResult(new CallResult(null));
        }
    }
}
