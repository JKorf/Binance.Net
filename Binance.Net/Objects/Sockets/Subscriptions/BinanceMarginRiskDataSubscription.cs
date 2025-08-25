using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot.Margin;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;

namespace Binance.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class BinanceMarginRiskDataSubscription : Subscription<BinanceSocketQueryResponse, BinanceSocketQueryResponse>
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

            MessageMatcher = MessageMatcher.Create([
                new MessageHandlerLink<BinanceCombinedStream<BinanceMarginCallUpdate>>(_lk + "MARGIN_LEVEL_STATUS_CHANGE", DoHandleMessage),
                new MessageHandlerLink<BinanceCombinedStream<BinanceLiabilityUpdate>>(_lk + "USER_LIABILITY_CHANGE", DoHandleMessage)
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
        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<BinanceCombinedStream<BinanceMarginCallUpdate>> message)
        {
            message.Data.Data.ListenKey = message.Data.Stream;
            _marginCallHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));
            
            return CallResult.SuccessResult;
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<BinanceCombinedStream<BinanceLiabilityUpdate>> message)
        {
            message.Data.Data.ListenKey = message.Data.Stream;
            _liabilityHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));
            
            return CallResult.SuccessResult;
        }
    }
}
