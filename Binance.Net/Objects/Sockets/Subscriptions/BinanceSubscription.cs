using Binance.Net.Objects.Internal;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;

namespace Binance.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class BinanceSubscription<T> : Subscription<BinanceSocketQueryResponse, BinanceSocketQueryResponse>
    {
        private readonly Action<DataEvent<T>> _handler;
        private string[] _params;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="topics"></param>
        /// <param name="handler"></param>
        /// <param name="auth"></param>
        public BinanceSubscription(ILogger logger, List<string> topics, Action<DataEvent<T>> handler, bool auth) : base(logger, auth)
        {
            _handler = handler;
            _params = topics.ToArray();

            MessageMatcher = MessageMatcher.Create<T>(topics, DoHandleMessage);
        }

        /// <inheritdoc />
        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return new BinanceSystemQuery<BinanceSocketQueryResponse>(new BinanceSocketRequest
            {
                Method = "SUBSCRIBE",
                Params = _params,
                Id = ExchangeHelpers.NextId()
            }, false);
        }

        /// <inheritdoc />
        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return new BinanceSystemQuery<BinanceSocketQueryResponse>(new BinanceSocketRequest
            {
                Method = "UNSUBSCRIBE",
                Params = _params,
                Id = ExchangeHelpers.NextId()
            }, false);
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<T> message)
        {
            _handler.Invoke(message.As(message.Data!, null!, null, SocketUpdateType.Update));
            return CallResult.SuccessResult;
        }
    }
}
