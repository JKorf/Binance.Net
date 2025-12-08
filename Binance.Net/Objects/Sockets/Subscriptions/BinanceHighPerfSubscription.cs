using Binance.Net.Objects.Internal;
using CryptoExchange.Net.Sockets.HighPerf;

namespace Binance.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class BinanceHighPerfSubscription<T> : HighPerfSubscription<T>
    {
        private string[] _params;

        /// <summary>
        /// ctor
        /// </summary>
        public BinanceHighPerfSubscription(string[] topics, Action<T> handler) : base(handler)
        {
            _params = topics;
        }

        /// <inheritdoc />
        protected override object? GetSubQuery(HighPerfSocketConnection connection)
        {
            return new BinanceSocketRequest
            {
                Method = "SUBSCRIBE",
                Params = _params,
                Id = ExchangeHelpers.NextId()
            };
        }

        /// <inheritdoc />
        protected override object? GetUnsubQuery(HighPerfSocketConnection connection)
        {
            return new BinanceSocketRequest
            {
                Method = "UNSUBSCRIBE",
                Params = _params,
                Id = ExchangeHelpers.NextId()
            };
        }
    }
}
