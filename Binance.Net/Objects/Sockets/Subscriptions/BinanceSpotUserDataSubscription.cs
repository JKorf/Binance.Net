using Binance.Net.Clients.SpotApi;
using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;

namespace Binance.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class BinanceSpotUserDataSubscription : Subscription
    {
        private readonly BinanceSocketClientSpotApi _client;
        private string? _subscriptionId;

        private readonly Action<DataEvent<BinanceStreamOrderUpdate>>? _orderHandler;
        private readonly Action<DataEvent<BinanceStreamOrderList>>? _orderListHandler;
        private readonly Action<DataEvent<BinanceStreamPositionsUpdate>>? _positionHandler;
        private readonly Action<DataEvent<BinanceStreamBalanceUpdate>>? _balanceHandler;
        private readonly Action<DataEvent<BinanceStreamEvent>>? _streamTerminatedHandler;
        private readonly Action<DataEvent<BinanceStreamBalanceLockUpdate>>? _balanceLockHandler;

        /// <inheritdoc />
        public BinanceSpotUserDataSubscription(
            ILogger logger,
            BinanceSocketClientSpotApi client,
            Action<DataEvent<BinanceStreamOrderUpdate>>? orderHandler,
            Action<DataEvent<BinanceStreamOrderList>>? orderListHandler,
            Action<DataEvent<BinanceStreamPositionsUpdate>>? positionHandler,
            Action<DataEvent<BinanceStreamBalanceUpdate>>? balanceHandler,
            Action<DataEvent<BinanceStreamEvent>>? streamTerminatedHandler,
            Action<DataEvent<BinanceStreamBalanceLockUpdate>>? lockHandler,
            bool auth) : base(logger, auth)
        {
            _client = client;
            _orderHandler = orderHandler;
            _orderListHandler = orderListHandler;
            _positionHandler = positionHandler;
            _balanceHandler = balanceHandler;
            _streamTerminatedHandler = streamTerminatedHandler;
            _balanceLockHandler = lockHandler;

            MessageRouter = MessageRouter.Create([]);
        }

        /// <inheritdoc />
        protected override Query? GetSubQuery(SocketConnection connection)
        {
            var signParameters = ((BinanceAuthenticationProvider)_client.AuthenticationProvider!).ProcessRequest(_client, new Dictionary<string, object>());
            return new BinanceSpotQuery<BinanceResponse<BinanceWebsocketApiWrapper>>(_client, new BinanceSocketQuery
            {
                Method = "userDataStream.subscribe.signature",
                Params = signParameters,
                Id = ExchangeHelpers.NextId()
            }, false);            
        }

        public override void HandleSubQueryResponse(SocketConnection connection, object? message)
        {
            if (message == null)
                return;

            var response = (BinanceResponse<BinanceWebsocketApiWrapper>)message;
            var id = response.Result.SubscriptionId.ToString();
            _subscriptionId = id;

            MessageRouter = MessageRouter.Create([
                MessageRoute<BinanceWebsocketApiWrapper<BinanceStreamPositionsUpdate>>.CreateWithTopicFilter("outboundAccountPosition", id, DoHandleMessage),
                MessageRoute<BinanceWebsocketApiWrapper<BinanceStreamBalanceUpdate>>.CreateWithTopicFilter("balanceUpdate", id, DoHandleMessage),
                MessageRoute<BinanceWebsocketApiWrapper<BinanceStreamOrderUpdate>>.CreateWithTopicFilter("executionReport", id, DoHandleMessage),
                MessageRoute<BinanceWebsocketApiWrapper<BinanceStreamOrderList>>.CreateWithTopicFilter("listStatus", id, DoHandleMessage),
                MessageRoute<BinanceWebsocketApiWrapper<BinanceStreamEvent>>.CreateWithTopicFilter("eventStreamTerminated", id, DoHandleMessage),
                MessageRoute<BinanceWebsocketApiWrapper<BinanceStreamBalanceLockUpdate>>.CreateWithTopicFilter("externalLockUpdate", id, DoHandleMessage),
                ]);
        }

        /// <inheritdoc />
        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return new BinanceSpotQuery<BinanceResponse>(_client, new BinanceSocketQuery
            {
                Method = "userDataStream.unsubscribe",
                Params = _subscriptionId != null ? new() { { "subscriptionId", _subscriptionId } } : [],
                Id = ExchangeHelpers.NextId()
            }, false);            
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceWebsocketApiWrapper<BinanceStreamPositionsUpdate> message)
        {
            _client.UpdateTimeOffset(message.Event.EventTime);

            message.Event.ApiKey = _client.AuthenticationProvider!.Key;
            _positionHandler?.Invoke(
                new DataEvent<BinanceStreamPositionsUpdate>(BinanceExchange.ExchangeName, message.Event, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(_client.AuthenticationProvider!.Key)
                    .WithDataTimestamp(message.Event.EventTime, _client.GetTimeOffset())                 
                );
            
            return CallResult.SuccessResult;
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceWebsocketApiWrapper<BinanceStreamBalanceUpdate> message)
        {
            _client.UpdateTimeOffset(message.Event.EventTime);

            message.Event.ApiKey = _client.AuthenticationProvider!.Key;
            _balanceHandler?.Invoke(
                new DataEvent<BinanceStreamBalanceUpdate>(BinanceExchange.ExchangeName, message.Event, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(_client.AuthenticationProvider!.Key)
                    .WithDataTimestamp(message.Event.EventTime, _client.GetTimeOffset())
                );
            
            return CallResult.SuccessResult;
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceWebsocketApiWrapper<BinanceStreamOrderUpdate> message)
        {
            _client.UpdateTimeOffset(message.Event.EventTime);

            message.Event.ApiKey = _client.AuthenticationProvider!.Key;
            _orderHandler?.Invoke(
                new DataEvent<BinanceStreamOrderUpdate>(BinanceExchange.ExchangeName, message.Event, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(_client.AuthenticationProvider!.Key)
                    .WithSymbol(message.Event.Symbol)
                    .WithDataTimestamp(message.Event.EventTime, _client.GetTimeOffset())
                );
            
            return CallResult.SuccessResult;
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceWebsocketApiWrapper<BinanceStreamOrderList> message)
        {
            _client.UpdateTimeOffset(message.Event.EventTime);

            message.Event.ApiKey = _client.AuthenticationProvider!.Key;
            _orderListHandler?.Invoke(
                new DataEvent<BinanceStreamOrderList>(BinanceExchange.ExchangeName, message.Event, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(_client.AuthenticationProvider!.Key)
                    .WithSymbol(message.Event.Symbol)
                    .WithDataTimestamp(message.Event.EventTime, _client.GetTimeOffset())
                );
            
            return CallResult.SuccessResult;
        }


        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceWebsocketApiWrapper<BinanceStreamEvent> message)
        {
            _client.UpdateTimeOffset(message.Event.EventTime);
                        
            _streamTerminatedHandler?.Invoke(
                new DataEvent<BinanceStreamEvent>(BinanceExchange.ExchangeName, message.Event, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(_client.AuthenticationProvider!.Key)
                    .WithDataTimestamp(message.Event.EventTime, _client.GetTimeOffset())
                );            
            
            return CallResult.SuccessResult;
        }


        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceWebsocketApiWrapper<BinanceStreamBalanceLockUpdate> message)
        {
            _client.UpdateTimeOffset(message.Event.EventTime);

            message.Event.ApiKey = _client.AuthenticationProvider!.Key;
            _balanceLockHandler?.Invoke(
                new DataEvent<BinanceStreamBalanceLockUpdate>(BinanceExchange.ExchangeName, message.Event, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(_client.AuthenticationProvider!.Key)
                    .WithDataTimestamp(message.Event.EventTime, _client.GetTimeOffset())
                );

            return CallResult.SuccessResult;
        }
    }
}
