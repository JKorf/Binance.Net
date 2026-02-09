using Binance.Net.Clients.CoinFuturesApi;
using Binance.Net.Clients.SpotApi;
using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures.Socket;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;

namespace Binance.Net.Objects.Sockets
{
    /// <inheritdoc />
    internal class BinanceCoinFuturesUserDataSubscription : Subscription
    {
        private readonly string _lk;
        private readonly BinanceSocketClientCoinFuturesApi _client;

        private readonly Action<DataEvent<BinanceFuturesStreamOrderUpdate>>? _orderHandler;
        private readonly Action<DataEvent<BinanceFuturesStreamConfigUpdate>>? _configHandler;
        private readonly Action<DataEvent<BinanceFuturesStreamMarginUpdate>>? _marginHandler;
        private readonly Action<DataEvent<BinanceFuturesStreamAccountUpdate>>? _accountHandler;
        private readonly Action<DataEvent<BinanceStreamEvent>>? _listenkeyHandler;
        private readonly Action<DataEvent<BinanceStrategyUpdate>>? _strategyHandler;
        private readonly Action<DataEvent<BinanceGridUpdate>>? _gridHandler;

        /// <summary>
        /// ctor
        /// </summary>
        public BinanceCoinFuturesUserDataSubscription(
            ILogger logger,
            BinanceSocketClientCoinFuturesApi client,
            string listenKey,
            Action<DataEvent<BinanceFuturesStreamOrderUpdate>>? orderHandler,
            Action<DataEvent<BinanceFuturesStreamConfigUpdate>>? configHandler,
            Action<DataEvent<BinanceFuturesStreamMarginUpdate>>? marginHandler,
            Action<DataEvent<BinanceFuturesStreamAccountUpdate>>? accountHandler,
            Action<DataEvent<BinanceStreamEvent>>? listenkeyHandler,
            Action<DataEvent<BinanceStrategyUpdate>>? strategyHandler,
            Action<DataEvent<BinanceGridUpdate>>? gridHandler) : base(logger, false)
        {
            _client = client;
            _orderHandler = orderHandler;
            _configHandler = configHandler;
            _marginHandler = marginHandler;
            _accountHandler = accountHandler;
            _listenkeyHandler = listenkeyHandler;
            _strategyHandler = strategyHandler;
            _gridHandler = gridHandler;

            _lk = listenKey;

            MessageRouter = MessageRouter.Create([
                MessageRoute<BinanceCombinedStream<BinanceFuturesStreamConfigUpdate>>.CreateWithoutTopicFilter("ACCOUNT_CONFIG_UPDATE", DoHandleMessage),
                MessageRoute<BinanceCombinedStream<BinanceFuturesStreamMarginUpdate>>.CreateWithoutTopicFilter("MARGIN_CALL", DoHandleMessage),
                MessageRoute<BinanceCombinedStream<BinanceFuturesStreamAccountUpdate>>.CreateWithoutTopicFilter("ACCOUNT_UPDATE", DoHandleMessage),
                MessageRoute<BinanceCombinedStream<BinanceFuturesStreamOrderUpdate>>.CreateWithoutTopicFilter("ORDER_TRADE_UPDATE", DoHandleMessage),
                MessageRoute<BinanceCombinedStream<BinanceStreamEvent>>.CreateWithoutTopicFilter("listenKeyExpired", DoHandleMessage),
                MessageRoute<BinanceCombinedStream<BinanceStrategyUpdate>>.CreateWithoutTopicFilter("STRATEGY_UPDATE", DoHandleMessage),
                MessageRoute<BinanceCombinedStream<BinanceGridUpdate>>.CreateWithoutTopicFilter("GRID_UPDATE", DoHandleMessage)
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


        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceFuturesStreamConfigUpdate> message)
        {
            _client.UpdateTimeOffset(message.Data.EventTime);

            message.Data.ListenKey = message.Stream;
            _configHandler?.Invoke(
                new DataEvent<BinanceFuturesStreamConfigUpdate>(BinanceExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithDataTimestamp(message.Data.EventTime, _client.GetTimeOffset())
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceFuturesStreamMarginUpdate> message)
        {
            _client.UpdateTimeOffset(message.Data.EventTime);

            message.Data.ListenKey = message.Stream;
            _marginHandler?.Invoke(
                new DataEvent<BinanceFuturesStreamMarginUpdate>(BinanceExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithDataTimestamp(message.Data.EventTime, _client.GetTimeOffset())
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceFuturesStreamAccountUpdate> message)
        {
            _client.UpdateTimeOffset(message.Data.EventTime);

            message.Data.ListenKey = message.Stream;
            _accountHandler?.Invoke(
                new DataEvent<BinanceFuturesStreamAccountUpdate>(BinanceExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithDataTimestamp(message.Data.EventTime, _client.GetTimeOffset())
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceFuturesStreamOrderUpdate> message)
        {
            _client.UpdateTimeOffset(message.Data.EventTime);

            message.Data.ListenKey = message.Stream;
            _orderHandler?.Invoke(
                new DataEvent<BinanceFuturesStreamOrderUpdate>(BinanceExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithSymbol(message.Data.UpdateData.Symbol)
                    .WithDataTimestamp(message.Data.EventTime, _client.GetTimeOffset())
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceStreamEvent> message)
        {
            _client.UpdateTimeOffset(message.Data.EventTime);

            _listenkeyHandler?.Invoke(
                new DataEvent<BinanceStreamEvent>(BinanceExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithDataTimestamp(message.Data.EventTime, _client.GetTimeOffset())
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceStrategyUpdate> message)
        {
            _client.UpdateTimeOffset(message.Data.EventTime);

            _strategyHandler?.Invoke(
                new DataEvent<BinanceStrategyUpdate>(BinanceExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithDataTimestamp(message.Data.EventTime, _client.GetTimeOffset())
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceGridUpdate> message)
        {
            _client.UpdateTimeOffset(message.Data.EventTime);

            _gridHandler?.Invoke(
                new DataEvent<BinanceGridUpdate>(BinanceExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithDataTimestamp(message.Data.EventTime, _client.GetTimeOffset())
                );
            return CallResult.SuccessResult;
        }
    }
}
