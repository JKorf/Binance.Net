using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures.Socket;
using Binance.Net.Objects.Models.Spot.Margin;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;

namespace Binance.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class BinanceMarginRiskDataSubscription : Subscription
    {
        private readonly string _lk;

        private readonly Action<DataEvent<BinanceMarginCallUpdate>>? _marginCallHandler;
        private readonly Action<DataEvent<BinanceLiabilityUpdate>>? _liabilityHandler;

        /// <inheritdoc />
        public BinanceMarginRiskDataSubscription(
            ILogger logger,
            string listenKey,
            Action<DataEvent<BinanceMarginCallUpdate>>? marginCallHandler,
            Action<DataEvent<BinanceLiabilityUpdate>>? liabilityHandler,
            bool auth) : base(logger, auth)
        {
            _marginCallHandler = marginCallHandler;
            _liabilityHandler = liabilityHandler;
            _lk = listenKey;

            MessageRouter = MessageRouter.Create([
                MessageRoute<BinanceCombinedStream<BinanceMarginCallUpdate>>.CreateWithTopicFilter("MARGIN_LEVEL_STATUS_CHANGE", _lk, DoHandleMessage),
                MessageRoute<BinanceCombinedStream<BinanceLiabilityUpdate>>.CreateWithTopicFilter("USER_LIABILITY_CHANGE", _lk, DoHandleMessage)
                ]);

            MessageMatcher = MessageMatcher.Create([
                new MessageHandlerLink<BinanceCombinedStream<BinanceMarginCallUpdate>>(_lk + "MARGIN_LEVEL_STATUS_CHANGE", DoHandleMessage),
                new MessageHandlerLink<BinanceCombinedStream<BinanceLiabilityUpdate>>(_lk + "USER_LIABILITY_CHANGE", DoHandleMessage)
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
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceMarginCallUpdate> message)
        {
            message.Data.ListenKey = message.Stream;

            _marginCallHandler?.Invoke(
                new DataEvent<BinanceMarginCallUpdate>(message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithDataTimestamp(message.Data.EventTime)
                );
            
            return CallResult.SuccessResult;
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceCombinedStream<BinanceLiabilityUpdate> message)
        {
            message.Data.ListenKey = message.Stream;
            _liabilityHandler?.Invoke(
                new DataEvent<BinanceLiabilityUpdate>(message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithDataTimestamp(message.Data.EventTime)
                );
            
            return CallResult.SuccessResult;
        }
    }
}
