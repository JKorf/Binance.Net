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
        private readonly Action<DataEvent<BinanceStreamEvent>>? _streamTerminatedHandler;
        private readonly Action<DataEvent<BinanceStreamBalanceLockUpdate>>? _balanceLockHandler;

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
            if (string.Equals(identifier, "eventStreamTerminated", StringComparison.Ordinal))
                return typeof(BinanceCombinedStream<BinanceStreamEvent>);
            if (string.Equals(identifier, "externalLockUpdate", StringComparison.Ordinal))
                return typeof(BinanceCombinedStream<BinanceStreamBalanceLockUpdate>);

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
            Action<DataEvent<BinanceStreamEvent>>? streamTerminatedHandler,
            Action<DataEvent<BinanceStreamBalanceLockUpdate>>? lockHandler,
            bool auth) : base(logger, auth)
        {
            _orderHandler = orderHandler;
            _orderListHandler = orderListHandler;
            _positionHandler = positionHandler;
            _balanceHandler = balanceHandler;
            _listenKeyExpiredHandler = listenKeyExpiredHandler;
            _streamTerminatedHandler = streamTerminatedHandler;
            _balanceLockHandler = lockHandler;
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
                _positionHandler?.Invoke(message.As(positionUpdate.Data, positionUpdate.Stream, null, SocketUpdateType.Update).WithDataTimestamp(positionUpdate.Data.EventTime));
            }
            else if (message.Data is BinanceCombinedStream<BinanceStreamBalanceUpdate> balanceUpdate)
            {
                balanceUpdate.Data.ListenKey = balanceUpdate.Stream;
                _balanceHandler?.Invoke(message.As(balanceUpdate.Data, balanceUpdate.Stream, null, SocketUpdateType.Update).WithDataTimestamp(balanceUpdate.Data.EventTime));
            }
            else if (message.Data is BinanceCombinedStream<BinanceStreamOrderUpdate> orderUpdate)
            {
                orderUpdate.Data.ListenKey = orderUpdate.Stream;
                _orderHandler?.Invoke(message.As(orderUpdate.Data, orderUpdate.Stream, orderUpdate.Data.Symbol, SocketUpdateType.Update).WithDataTimestamp(orderUpdate.Data.EventTime));
            }
            else if (message.Data is BinanceCombinedStream<BinanceStreamOrderList> orderListUpdate)
            {
                orderListUpdate.Data.ListenKey = orderListUpdate.Stream;
                _orderListHandler?.Invoke(message.As(orderListUpdate.Data, orderListUpdate.Stream, null, SocketUpdateType.Update).WithDataTimestamp(orderListUpdate.Data.EventTime));
            }
            else if (message.Data is BinanceCombinedStream<BinanceStreamEvent> streamEvent)
            {
                if (streamEvent.Data.Event.Equals("listenKeyExpired"))
                    _listenKeyExpiredHandler?.Invoke(message.As(streamEvent.Data, streamEvent.Stream, null, SocketUpdateType.Update).WithDataTimestamp(streamEvent.Data.EventTime));
                else
                    _streamTerminatedHandler?.Invoke(message.As(streamEvent.Data, streamEvent.Stream, null, SocketUpdateType.Update).WithDataTimestamp(streamEvent.Data.EventTime));
            }
            else if (message.Data is BinanceCombinedStream<BinanceStreamBalanceLockUpdate> lockUpdate)
            {
                lockUpdate.Data.ListenKey = lockUpdate.Stream;
                _balanceLockHandler?.Invoke(message.As(lockUpdate.Data, lockUpdate.Stream, null, SocketUpdateType.Update).WithDataTimestamp(lockUpdate.Data.EventTime));
            }

            return new CallResult(null);
        }
    }
}
