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
        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return new BinanceSystemQuery<BinanceSocketQueryResponse>(new BinanceSocketRequest
            {
                Method = "SUBSCRIBE",
                Params = [_lk],
                Id = ExchangeHelpers.NextId()
            }, false);
        }

        /// <inheritdoc />
        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return new BinanceSystemQuery<BinanceSocketQueryResponse>(new BinanceSocketRequest
            {
                Method = "UNSUBSCRIBE",
                Params = [_lk],
                Id = ExchangeHelpers.NextId()
            }, false);
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceStreamPositionsUpdate> message)
        {
            message.Data.ListenKey = message.Stream;
            _positionHandler?.Invoke(
                new DataEvent<BinanceStreamPositionsUpdate>(message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithDataTimestamp(message.Data.EventTime)                 
                );
            
            return CallResult.SuccessResult;
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceStreamBalanceUpdate> message)
        {
            message.Data.ListenKey = message.Stream;
            _balanceHandler?.Invoke(
                new DataEvent<BinanceStreamBalanceUpdate>(message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithDataTimestamp(message.Data.EventTime)
                );
            
            return CallResult.SuccessResult;
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceStreamOrderUpdate> message)
        {
            message.Data.ListenKey = message.Stream;
            _orderHandler?.Invoke(
                new DataEvent<BinanceStreamOrderUpdate>(message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithSymbol(message.Data.Symbol)
                    .WithDataTimestamp(message.Data.EventTime)
                );
            
            return CallResult.SuccessResult;
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceStreamOrderList> message)
        {
            message.Data.ListenKey = message.Stream;
            _orderListHandler?.Invoke(
                new DataEvent<BinanceStreamOrderList>(message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithSymbol(message.Data.Symbol)
                    .WithDataTimestamp(message.Data.EventTime)
                );
            
            return CallResult.SuccessResult;
        }


        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceStreamEvent> message)
        {
            if (message.Data.Event.Equals("listenKeyExpired"))
            {
                _listenKeyExpiredHandler?.Invoke(
                    new DataEvent<BinanceStreamEvent>(message.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(message.Stream)
                        .WithDataTimestamp(message.Data.EventTime)
                    );
            }
            else
            {
                _listenKeyExpiredHandler?.Invoke(
                    new DataEvent<BinanceStreamEvent>(message.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(message.Stream)
                        .WithDataTimestamp(message.Data.EventTime)
                    );
            }
            
            return CallResult.SuccessResult;
        }


        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceStreamBalanceLockUpdate> message)
        {
            message.Data.ListenKey = message.Stream;
            _balanceLockHandler?.Invoke(
                new DataEvent<BinanceStreamBalanceLockUpdate>(message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithDataTimestamp(message.Data.EventTime)
                );

            return CallResult.SuccessResult;
        }
    }
}
