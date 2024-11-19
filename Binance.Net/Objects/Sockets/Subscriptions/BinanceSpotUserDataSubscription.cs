using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;

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
        private readonly Action<DataEvent<BinanceStreamEvent>>? _listenKeyExpiredHandler;

        /// <inheritdoc />
        public override Type? GetMessageType(IMessageAccessor message)
        {
            var identifier = message.GetValue<string>(_ePath);
            if (string.Equals(identifier, "outboundAccountPosition", StringComparison.Ordinal))
                return typeof(BinanceCombinedStream<BinanceStreamPositionsUpdate>);
            if (string.Equals(identifier, "balanceUpdate", StringComparison.Ordinal))
                return typeof(BinanceCombinedStream<BinanceStreamBalanceUpdate>);
            if (string.Equals(identifier, "executionReport", StringComparison.Ordinal))
                return typeof(BinanceCombinedStream<BinanceStreamOrderUpdate>);
            if (string.Equals(identifier, "listStatus", StringComparison.Ordinal))
                return typeof(BinanceCombinedStream<BinanceStreamOrderList>);
            if (string.Equals(identifier, "listenKeyExpired", StringComparison.Ordinal))
                return typeof(BinanceCombinedStream<BinanceStreamEvent>);

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
            Action<DataEvent<BinanceStreamEvent>>? listenKeyExpiredHandler,
            bool auth) : base(logger, auth)
        {
            _orderHandler = orderHandler;
            _orderListHandler = orderListHandler;
            _positionHandler = positionHandler;
            _balanceHandler = balanceHandler;
            _listenKeyExpiredHandler = listenKeyExpiredHandler;
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
            {
                positionUpdate.Data.ListenKey = positionUpdate.Stream;
                _positionHandler?.Invoke(message.As(positionUpdate.Data, positionUpdate.Stream, null, SocketUpdateType.Update));
            }
            else if (message.Data is BinanceCombinedStream<BinanceStreamBalanceUpdate> balanceUpdate)
            {
                balanceUpdate.Data.ListenKey = balanceUpdate.Stream;
                _balanceHandler?.Invoke(message.As(balanceUpdate.Data, balanceUpdate.Stream, null, SocketUpdateType.Update));
            }
            else if (message.Data is BinanceCombinedStream<BinanceStreamOrderUpdate> orderUpdate)
            {
                orderUpdate.Data.ListenKey = orderUpdate.Stream;
                _orderHandler?.Invoke(message.As(orderUpdate.Data, orderUpdate.Stream, orderUpdate.Data.Symbol, SocketUpdateType.Update));
            }
            else if (message.Data is BinanceCombinedStream<BinanceStreamOrderList> orderListUpdate)
            {
                orderListUpdate.Data.ListenKey = orderListUpdate.Stream;
                _orderListHandler?.Invoke(message.As(orderListUpdate.Data, orderListUpdate.Stream, null, SocketUpdateType.Update));
            }
            else if (message.Data is BinanceCombinedStream<BinanceStreamEvent> listenKeyExpired)
            {
                _listenKeyExpiredHandler?.Invoke(message.As(listenKeyExpired.Data, listenKeyExpired.Stream, null, SocketUpdateType.Update));
            }

            return new CallResult(null);
        }
    }
}
