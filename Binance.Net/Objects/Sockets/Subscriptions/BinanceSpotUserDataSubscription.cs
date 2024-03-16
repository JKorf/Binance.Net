using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Binance.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class BinanceSpotUserDataSubscription : Subscription<BinanceSocketQueryResponse, BinanceSocketQueryResponse>
    {
        private static readonly MessagePath _ePath = MessagePath.Get().Property("data").Property("e");

        /// <inheritdoc />
        public override HashSet<string> ListenerIdentifiers { get; set; }

        private readonly Action<DataEvent<BinanceStreamOrderUpdate>>? _orderHandler;
        private readonly Action<DataEvent<BinanceStreamOrderList>>? _orderListHandler;
        private readonly Action<DataEvent<BinanceStreamPositionsUpdate>>? _positionHandler;
        private readonly Action<DataEvent<BinanceStreamBalanceUpdate>>? _balanceHandler;

        /// <inheritdoc />
        public override Type? GetMessageType(IMessageAccessor message)
        {
            var identifier = message.GetValue<string>(_ePath);
            if (identifier == "outboundAccountPosition")
                return typeof(BinanceCombinedStream<BinanceStreamPositionsUpdate>);
            if (identifier == "balanceUpdate")
                return typeof(BinanceCombinedStream<BinanceStreamBalanceUpdate>);
            if (identifier == "executionReport")
                return typeof(BinanceCombinedStream<BinanceStreamOrderUpdate>);
            if (identifier == "listStatus")
                return typeof(BinanceCombinedStream<BinanceStreamOrderList>);

            return null;
        }

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
            if (message.Data is BinanceCombinedStream<BinanceStreamPositionsUpdate> positionUpdate)
                _positionHandler?.Invoke(message.As(positionUpdate.Data, positionUpdate.Stream, SocketUpdateType.Update));
            else if (message.Data is BinanceCombinedStream<BinanceStreamBalanceUpdate> balanceUpdate)
                _balanceHandler?.Invoke(message.As(balanceUpdate.Data, balanceUpdate.Stream, SocketUpdateType.Update));
            else if (message.Data is BinanceCombinedStream<BinanceStreamOrderUpdate> orderUpdate)
                _orderHandler?.Invoke(message.As(orderUpdate.Data, orderUpdate.Stream, SocketUpdateType.Update));
            else if (message.Data is BinanceCombinedStream<BinanceStreamOrderList> orderListUpdate)
                _orderListHandler?.Invoke(message.As(orderListUpdate.Data, orderListUpdate.Stream, SocketUpdateType.Update));

            return new CallResult(null);
        }
    }
}
