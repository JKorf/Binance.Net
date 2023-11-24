using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Binance.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    public class BinanceSpotUserDataSubscription : Subscription<BinanceSocketQueryResponse, BinanceCombinedStream<BinanceStreamEvent>>
    {
        /// <inheritdoc />
        public override List<string> Identifiers => _identifiers;

        private readonly Action<DataEvent<BinanceStreamOrderUpdate>>? _orderHandler;
        private readonly Action<DataEvent<BinanceStreamOrderList>>? _orderListHandler;
        private readonly Action<DataEvent<BinanceStreamPositionsUpdate>>? _positionHandler;
        private readonly Action<DataEvent<BinanceStreamBalanceUpdate>>? _balanceHandler;
        private readonly List<string> _identifiers;

        /// <inheritdoc />
        public BinanceSpotUserDataSubscription(
            ILogger logger,
            List<string> topics,
            Action<DataEvent<BinanceStreamOrderUpdate>>? orderHandler,
            Action<DataEvent<BinanceStreamOrderList>>? orderListHandler,
            Action<DataEvent<BinanceStreamPositionsUpdate>>? positionHandler,
            Action<DataEvent<BinanceStreamBalanceUpdate>>? balanceHandler,
            bool auth) : base(logger, auth)
        {
            _orderHandler = orderHandler;
            _orderListHandler = orderListHandler;
            _positionHandler = positionHandler;
            _balanceHandler = balanceHandler;
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
        public override Task<CallResult> HandleEventAsync(SocketConnection connection, DataEvent<ParsedMessage<BinanceCombinedStream<BinanceStreamEvent>>> message)
        {
            var data = message.Data.TypedData.Data;
            if (data is BinanceStreamOrderUpdate orderUpdate)
            {
                orderUpdate.ListenKey = message.Data.TypedData.Stream;
                _orderHandler?.Invoke(message.As(orderUpdate, message.Data.TypedData.Stream, SocketUpdateType.Update));
            }
            else if (data is BinanceStreamOrderList orderListUpdate)
            {
                orderListUpdate.ListenKey = message.Data.TypedData.Stream;
                _orderListHandler?.Invoke(message.As(orderListUpdate, message.Data.TypedData.Stream, SocketUpdateType.Update));
            }
            else if (data is BinanceStreamPositionsUpdate positionUpdate)
            {
                positionUpdate.ListenKey = message.Data.TypedData.Stream;
                _positionHandler?.Invoke(message.As(positionUpdate, message.Data.TypedData.Stream, SocketUpdateType.Update));
            }
            else if (data is BinanceStreamBalanceUpdate balanceUpdate)
            {
                balanceUpdate.ListenKey = message.Data.TypedData.Stream;
                _balanceHandler?.Invoke(message.As(balanceUpdate, message.Data.TypedData.Stream, SocketUpdateType.Update));
            }

            return Task.FromResult(new CallResult(null)); // TODO error not mapped
        }
    }
}
