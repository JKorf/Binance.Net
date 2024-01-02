using Binance.Net.Objects.Internal;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Binance.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    public class BinanceSubscription<T> : Subscription<BinanceSocketQueryResponse>
    {
        /// <inheritdoc />
        public override List<string> StreamIdentifiers { get; set; }

        public override Dictionary<string, Type> TypeMapping { get; } = new Dictionary<string, Type>
        {
            { "", typeof(T) }
        };

        private readonly Action<DataEvent<T>> _handler;

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
            StreamIdentifiers = topics;
        }

        /// <inheritdoc />
        public override BaseQuery? GetSubQuery(SocketConnection connection)
        {
            return new BinanceSystemQuery<BinanceSocketQueryResponse>(new BinanceSocketRequest
            {
                Method = "SUBSCRIBE",
                Params = StreamIdentifiers.ToArray(),
                Id = ExchangeHelpers.NextId()
            }, false);
        }

        /// <inheritdoc />
        public override BaseQuery? GetUnsubQuery()
        {
            return new BinanceSystemQuery<BinanceSocketQueryResponse>(new BinanceSocketRequest
            {
                Method = "UNSUBSCRIBE",
                Params = StreamIdentifiers.ToArray(),
                Id = ExchangeHelpers.NextId()
            }, false);
        }

        /// <inheritdoc />
        public override Task<CallResult> DoHandleMessageAsync(SocketConnection connection, DataEvent<BaseParsedMessage> message)
        {
            _handler.Invoke(message.As((T)message.Data.Data!, null, SocketUpdateType.Update));
            return Task.FromResult(new CallResult(null));
        }
    }
}
