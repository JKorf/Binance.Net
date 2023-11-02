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
    public class BinanceCoinFuturesUserDataSubscription : Subscription
    {
        /// <inheritdoc />
        public override List<string> Identifiers => _identifiers;

        private readonly Action<DataEvent<BinanceFuturesStreamOrderUpdate>>? _orderHandler;
        private readonly Action<DataEvent<BinanceFuturesStreamConfigUpdate>>? _configHandler;
        private readonly Action<DataEvent<BinanceFuturesStreamMarginUpdate>>? _marginHandler;
        private readonly Action<DataEvent<BinanceFuturesStreamAccountUpdate>>? _accountHandler;
        private readonly Action<DataEvent<BinanceStreamEvent>>? _listenkeyHandler;
        private readonly Action<DataEvent<BinanceStrategyUpdate>>? _strategyHandler;
        private readonly Action<DataEvent<BinanceGridUpdate>>? _gridHandler;
        private readonly List<string> _identifiers;
        private int _subId;
        private int _unsubId;

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
            _identifiers = topics;
        }

        /// <inheritdoc />
        public override object? GetSubRequest()
        {
            _subId = ExchangeHelpers.NextId();
            return new BinanceSocketRequest
            {
                Method = "SUBSCRIBE",
                Params = _identifiers.ToArray(),
                Id = _subId
            };
        }

        /// <inheritdoc />
        public override object? GetUnsubRequest()
        {
            _unsubId = ExchangeHelpers.NextId();
            return new BinanceSocketRequest
            {
                Method = "UNSUBSCRIBE",
                Params = _identifiers.ToArray(),
                Id = _unsubId
            };
        }
    
        /// <inheritdoc />
        public override bool MessageMatchesSubRequest(ParsedMessage message)
        {
            if (message.Data is not BinanceSocketQueryResponse response)
                return false;

            if (response.Id != _subId)
                return false;

            return true;
        }

        /// <inheritdoc />
        public override bool MessageMatchesUnsubRequest(ParsedMessage message)
        {
            if (message.Data is not BinanceSocketQueryResponse response)
                return false;

            if (response.Id != _unsubId)
                return false;

            return true;
        }

        /// <inheritdoc />
        public override Task HandleEventAsync(DataEvent<ParsedMessage> message)
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
            return Task.CompletedTask;
        }

        public override CallResult HandleSubResponse(ParsedMessage message) => new CallResult(null);
        public override CallResult HandleUnsubResponse(ParsedMessage message) => new CallResult(null);
    }
}
