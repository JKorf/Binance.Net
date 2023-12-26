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
    public class BinanceCoinFuturesUserDataSubscription : Subscription<BinanceSocketQueryResponse, BinanceCombinedStream<BinanceStreamEvent>>
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
        public override BaseQuery? GetSubQuery(SocketConnection connection)
        {
            return new BinanceSystemQuery<BinanceSocketQueryResponse>(new BinanceSocketRequest
            {
                Method = "SUBSCRIBE",
                Params = _identifiers.ToArray(),
                Id = ExchangeHelpers.NextId()
            }, false);
        }

        /// <inheritdoc />
        public override BaseQuery? GetUnsubQuery()
        {
            return new BinanceSystemQuery<BinanceSocketQueryResponse>(new BinanceSocketRequest
            {
                Method = "UNSUBSCRIBE",
                Params = _identifiers.ToArray(),
                Id = ExchangeHelpers.NextId()
            }, false);
        }
    
        /// <inheritdoc />
        public override Task<CallResult> DoHandleEventAsync(SocketConnection connection, DataEvent<ParsedMessage<BinanceCombinedStream<BinanceStreamEvent>>> message)
        {
            var data = message.Data.TypedData.Data;
            if (data is BinanceFuturesStreamConfigUpdate configUpdate)
            {
                configUpdate.ListenKey = message.Data.TypedData.Stream;
                _configHandler?.Invoke(message.As(configUpdate, message.Data.TypedData.Stream, SocketUpdateType.Update));
            }
            else if (data is BinanceFuturesStreamMarginUpdate marginUpdate)
            {
                marginUpdate.ListenKey = message.Data.TypedData.Stream;
                _marginHandler?.Invoke(message.As(marginUpdate, message.Data.TypedData.Stream, SocketUpdateType.Update));
            }
            else if (data is BinanceFuturesStreamAccountUpdate accountUpdate)
            {
                accountUpdate.ListenKey = message.Data.TypedData.Stream;
                _accountHandler?.Invoke(message.As(accountUpdate, message.Data.TypedData.Stream, SocketUpdateType.Update));
            }
            else if (data is BinanceFuturesStreamOrderUpdate orderUpate)
            {
                orderUpate.ListenKey = message.Data.TypedData.Stream;
                _orderHandler?.Invoke(message.As(orderUpate, message.Data.TypedData.Stream, SocketUpdateType.Update));
            }
            else if (data is BinanceStreamEvent listenKeyUpdate)
            {
                _listenkeyHandler?.Invoke(message.As(listenKeyUpdate, message.Data.TypedData.Stream, SocketUpdateType.Update));
            }
            else if (data is BinanceStrategyUpdate strategyUpdate)
            {
                _strategyHandler?.Invoke(message.As(strategyUpdate, message.Data.TypedData.Stream, SocketUpdateType.Update));
            }
            else if (data is BinanceGridUpdate gridUpdate)
            {
                _gridHandler?.Invoke(message.As(gridUpdate, message.Data.TypedData.Stream, SocketUpdateType.Update));
            }
            return Task.FromResult(new CallResult(null)); // TODO error not mapped
        }
    }
}
