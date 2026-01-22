using Binance.Net.Clients.SpotApi;
using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;

namespace Binance.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class BinanceSpotUserDataSubscription : Subscription
    {
        private readonly string _lk;
        private readonly BinanceSocketClientSpotApi _client;

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
            BinanceSocketClientSpotApi client,
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
            _client = client;
            _orderHandler = orderHandler;
            _orderListHandler = orderListHandler;
            _positionHandler = positionHandler;
            _balanceHandler = balanceHandler;
            _listenKeyExpiredHandler = listenKeyExpiredHandler;
            _streamTerminatedHandler = streamTerminatedHandler;
            _balanceLockHandler = lockHandler;
            _lk = listenKey;

            MessageRouter = MessageRouter.Create([
                MessageRoute<BinanceCombinedStream<BinanceStreamPositionsUpdate>>.CreateWithTopicFilter("outboundAccountPosition", _lk, DoHandleMessage),
                MessageRoute<BinanceCombinedStream<BinanceStreamBalanceUpdate>>.CreateWithTopicFilter("balanceUpdate", _lk, DoHandleMessage),
                MessageRoute<BinanceCombinedStream<BinanceStreamOrderUpdate>>.CreateWithTopicFilter("executionReport", _lk, DoHandleMessage),
                MessageRoute<BinanceCombinedStream<BinanceStreamOrderList>>.CreateWithTopicFilter("listStatus", _lk, DoHandleMessage),
                MessageRoute<BinanceCombinedStream<BinanceStreamEvent>>.CreateWithTopicFilter("listenKeyExpired", _lk, DoHandleMessage),
                MessageRoute<BinanceCombinedStream<BinanceStreamEvent>>.CreateWithTopicFilter("eventStreamTerminated", _lk, DoHandleMessage),
                MessageRoute<BinanceCombinedStream<BinanceStreamBalanceLockUpdate>>.CreateWithTopicFilter("externalLockUpdate", _lk, DoHandleMessage),
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
            _client.UpdateTimeOffset(message.Data.EventTime);

            message.Data.ListenKey = message.Stream;
            _positionHandler?.Invoke(
                new DataEvent<BinanceStreamPositionsUpdate>(BinanceExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithDataTimestamp(message.Data.EventTime, _client.GetTimeOffset())                 
                );
            
            return CallResult.SuccessResult;
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceStreamBalanceUpdate> message)
        {
            _client.UpdateTimeOffset(message.Data.EventTime);

            message.Data.ListenKey = message.Stream;
            _balanceHandler?.Invoke(
                new DataEvent<BinanceStreamBalanceUpdate>(BinanceExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithDataTimestamp(message.Data.EventTime, _client.GetTimeOffset())
                );
            
            return CallResult.SuccessResult;
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceStreamOrderUpdate> message)
        {
            _client.UpdateTimeOffset(message.Data.EventTime);

            message.Data.ListenKey = message.Stream;
            _orderHandler?.Invoke(
                new DataEvent<BinanceStreamOrderUpdate>(BinanceExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithSymbol(message.Data.Symbol)
                    .WithDataTimestamp(message.Data.EventTime, _client.GetTimeOffset())
                );
            
            return CallResult.SuccessResult;
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceStreamOrderList> message)
        {
            _client.UpdateTimeOffset(message.Data.EventTime);

            message.Data.ListenKey = message.Stream;
            _orderListHandler?.Invoke(
                new DataEvent<BinanceStreamOrderList>(BinanceExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithSymbol(message.Data.Symbol)
                    .WithDataTimestamp(message.Data.EventTime, _client.GetTimeOffset())
                );
            
            return CallResult.SuccessResult;
        }


        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceStreamEvent> message)
        {
            _client.UpdateTimeOffset(message.Data.EventTime);

            if (message.Data.Event.Equals("listenKeyExpired"))
            {
                _listenKeyExpiredHandler?.Invoke(
                    new DataEvent<BinanceStreamEvent>(BinanceExchange.ExchangeName, message.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(message.Stream)
                        .WithDataTimestamp(message.Data.EventTime, _client.GetTimeOffset())
                    );
            }
            else
            {
                _listenKeyExpiredHandler?.Invoke(
                    new DataEvent<BinanceStreamEvent>(BinanceExchange.ExchangeName, message.Data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(message.Stream)
                        .WithDataTimestamp(message.Data.EventTime, _client.GetTimeOffset())
                    );
            }
            
            return CallResult.SuccessResult;
        }


        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceStreamBalanceLockUpdate> message)
        {
            _client.UpdateTimeOffset(message.Data.EventTime);

            message.Data.ListenKey = message.Stream;
            _balanceLockHandler?.Invoke(
                new DataEvent<BinanceStreamBalanceLockUpdate>(BinanceExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithDataTimestamp(message.Data.EventTime, _client.GetTimeOffset())
                );

            return CallResult.SuccessResult;
        }
    }
}
