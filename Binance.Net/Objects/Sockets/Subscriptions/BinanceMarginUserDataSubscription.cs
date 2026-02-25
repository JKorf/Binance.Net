using Binance.Net.Clients.SpotApi;
using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;

namespace Binance.Net.Objects.Sockets.Subscriptions
{
    internal class BinanceMarginUserDataSubscription : Subscription
    {
        private readonly BinanceSocketClientSpotApi _client;
        private string _listenToken;  // not readonly - updated on renewal

        private readonly Action<DataEvent<BinanceStreamOrderUpdate>>? _orderHandler;
        private readonly Action<DataEvent<BinanceStreamOrderList>>? _orderListHandler;
        private readonly Action<DataEvent<BinanceStreamPositionsUpdate>>? _positionHandler;
        private readonly Action<DataEvent<BinanceStreamBalanceUpdate>>? _balanceHandler;
        private readonly Action<DataEvent<BinanceStreamEvent>>? _streamTerminatedHandler;

        public BinanceMarginUserDataSubscription(
            ILogger logger,
            BinanceSocketClientSpotApi client,
            string listenToken,
            Action<DataEvent<BinanceStreamOrderUpdate>>? orderHandler,
            Action<DataEvent<BinanceStreamOrderList>>? orderListHandler,
            Action<DataEvent<BinanceStreamPositionsUpdate>>? positionHandler,
            Action<DataEvent<BinanceStreamBalanceUpdate>>? balanceHandler,
            Action<DataEvent<BinanceStreamEvent>>? streamTerminatedHandler) : base(logger, false)
        {
            _client = client;
            _listenToken = listenToken;
            _orderHandler = orderHandler;
            _orderListHandler = orderListHandler;
            _positionHandler = positionHandler;
            _balanceHandler = balanceHandler;
            _streamTerminatedHandler = streamTerminatedHandler;

            MessageRouter = MessageRouter.Create([
                MessageRoute<BinanceWebsocketApiWrapper<BinanceStreamPositionsUpdate>>.CreateWithoutTopicFilter("outboundAccountPosition", DoHandleMessage),
                MessageRoute<BinanceWebsocketApiWrapper<BinanceStreamBalanceUpdate>>.CreateWithoutTopicFilter("balanceUpdate", DoHandleMessage),
                MessageRoute<BinanceWebsocketApiWrapper<BinanceStreamOrderUpdate>>.CreateWithoutTopicFilter("executionReport", DoHandleMessage),
                MessageRoute<BinanceWebsocketApiWrapper<BinanceStreamOrderList>>.CreateWithoutTopicFilter("listStatus", DoHandleMessage),
                MessageRoute<BinanceWebsocketApiWrapper<BinanceStreamEvent>>.CreateWithoutTopicFilter("eventStreamTerminated", DoHandleMessage),
            ]);
        }

        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return new BinanceSpotQuery<BinanceResponse>(_client, new BinanceSocketQuery
            {
                Method = "userDataStream.subscribe.listenToken",
                Params = new Dictionary<string, object>
                {
                    { "listenToken", _listenToken }
                },
                Id = ExchangeHelpers.NextId()
            }, false);
        }

        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return new BinanceSpotQuery<BinanceResponse>(_client, new BinanceSocketQuery
            {
                Method = "userDataStream.unsubscribe",
                Params = [],
                Id = ExchangeHelpers.NextId()
            }, false);
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceWebsocketApiWrapper<BinanceStreamPositionsUpdate> message)
        {
            _client.UpdateTimeOffset(message.Event.EventTime);
            _positionHandler?.Invoke(
                new DataEvent<BinanceStreamPositionsUpdate>(BinanceExchange.ExchangeName, message.Event, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Event.EventTime, _client.GetTimeOffset())
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceWebsocketApiWrapper<BinanceStreamBalanceUpdate> message)
        {
            _client.UpdateTimeOffset(message.Event.EventTime);
            _balanceHandler?.Invoke(
                new DataEvent<BinanceStreamBalanceUpdate>(BinanceExchange.ExchangeName, message.Event, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Event.EventTime, _client.GetTimeOffset())
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceWebsocketApiWrapper<BinanceStreamOrderUpdate> message)
        {
            _client.UpdateTimeOffset(message.Event.EventTime);
            _orderHandler?.Invoke(
                new DataEvent<BinanceStreamOrderUpdate>(BinanceExchange.ExchangeName, message.Event, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithSymbol(message.Event.Symbol)
                    .WithDataTimestamp(message.Event.EventTime, _client.GetTimeOffset())
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceWebsocketApiWrapper<BinanceStreamOrderList> message)
        {
            _client.UpdateTimeOffset(message.Event.EventTime);
            _orderListHandler?.Invoke(
                new DataEvent<BinanceStreamOrderList>(BinanceExchange.ExchangeName, message.Event, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithSymbol(message.Event.Symbol)
                    .WithDataTimestamp(message.Event.EventTime, _client.GetTimeOffset())
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceWebsocketApiWrapper<BinanceStreamEvent> message)
        {
            _client.UpdateTimeOffset(message.Event.EventTime);
            _streamTerminatedHandler?.Invoke(
                new DataEvent<BinanceStreamEvent>(BinanceExchange.ExchangeName, message.Event, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Event.EventTime, _client.GetTimeOffset())
                );
            return CallResult.SuccessResult;
        }

        /// <summary>
        /// Seamlessly renew the listen token on the existing connection without
        /// disconnecting. Also updates the stored token so reconnects use the new value.
        /// </summary>
        internal async Task<CallResult> RenewTokenAsync(string newListenToken, CancellationToken ct = default)
        {
            var result = await _client.QueryAsync<object>(
                _client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"),
                "userDataStream.subscribe.listenToken",
                new Dictionary<string, object> { { "listenToken", newListenToken } },
                authenticated: false,
                ct: ct).ConfigureAwait(false);

            if (result.Success)
                _listenToken = newListenToken;

            return result;
        }
    }
}