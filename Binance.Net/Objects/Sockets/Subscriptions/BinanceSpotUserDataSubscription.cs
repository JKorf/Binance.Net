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
        private readonly string _lk;

        private readonly Action<DataEvent<BinanceStreamOrderUpdate>>? _orderHandler;
        private readonly Action<DataEvent<BinanceStreamOrderList>>? _orderListHandler;
        private readonly Action<DataEvent<BinanceStreamPositionsUpdate>>? _positionHandler;
        private readonly Action<DataEvent<BinanceStreamBalanceUpdate>>? _balanceHandler;
        private readonly Action<DataEvent<BinanceStreamEvent>>? _listenKeyExpiredHandler;
        private readonly Action<DataEvent<BinanceStreamEvent>>? _streamTerminatedHandler;
        private readonly Action<DataEvent<BinanceStreamBalanceLockUpdate>>? _balanceLockHandler;

        /// <inheritdoc />
        public BinanceSpotUserDataSubscription(
            ILogger logger,
            string listenKey,
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
            _lk = listenKey;

            MessageMatcher = MessageMatcher.Create([
                new MessageHandlerLink<BinanceCombinedStream<BinanceStreamPositionsUpdate>>(_lk + "outboundAccountPosition", DoHandleMessage),
                new MessageHandlerLink<BinanceCombinedStream<BinanceStreamBalanceUpdate>>(_lk + "balanceUpdate", DoHandleMessage),
                new MessageHandlerLink<BinanceCombinedStream<BinanceStreamOrderUpdate>>(_lk + "executionReport", DoHandleMessage),
                new MessageHandlerLink<BinanceCombinedStream<BinanceStreamOrderList>>(_lk + "listStatus", DoHandleMessage),
                new MessageHandlerLink<BinanceCombinedStream<BinanceStreamEvent>>(_lk + "listenKeyExpired", DoHandleMessage),
                new MessageHandlerLink<BinanceCombinedStream<BinanceStreamEvent>>(_lk + "eventStreamTerminated", DoHandleMessage),
                new MessageHandlerLink<BinanceCombinedStream<BinanceStreamBalanceLockUpdate>>(_lk + "externalLockUpdate", DoHandleMessage),
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

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<BinanceCombinedStream<BinanceStreamPositionsUpdate>> message)
        {
            message.Data.Data.ListenKey = message.Data.Stream;
            _positionHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));
            
            return CallResult.SuccessResult;
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<BinanceCombinedStream<BinanceStreamBalanceUpdate>> message)
        {
            message.Data.Data.ListenKey = message.Data.Stream;
            _balanceHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));
            
            return CallResult.SuccessResult;
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<BinanceCombinedStream<BinanceStreamOrderUpdate>> message)
        {
            message.Data.Data.ListenKey = message.Data.Stream;
            _orderHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, message.Data.Data.Symbol, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));
            
            return CallResult.SuccessResult;
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<BinanceCombinedStream<BinanceStreamOrderList>> message)
        {
            message.Data.Data.ListenKey = message.Data.Stream;
            _orderListHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));
            
            return CallResult.SuccessResult;
        }


        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<BinanceCombinedStream<BinanceStreamEvent>> message)
        {
            if (message.Data.Data.Event.Equals("listenKeyExpired"))
                _listenKeyExpiredHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));
            else
                _streamTerminatedHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));
            
            return CallResult.SuccessResult;
        }


        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<BinanceCombinedStream<BinanceStreamBalanceLockUpdate>> message)
        {
            message.Data.Data.ListenKey = message.Data.Stream;
            _balanceLockHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));

            return CallResult.SuccessResult;
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<object> message)
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

            return CallResult.SuccessResult;
        }
    }
}
