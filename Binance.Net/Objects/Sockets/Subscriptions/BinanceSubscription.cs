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
    public class BinanceSubscription<T> : Subscription
    {
        /// <inheritdoc />
        public override List<string> Identifiers => _identifiers;

        private readonly Action<DataEvent<T>> _handler;
        private readonly List<string> _identifiers;
        private int _subId;
        private int _unsubId;

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
            _identifiers = topics;
        }

        /// <inheritdoc />
        public override object? GetSubRequest()
        {
            _subId = ExchangeHelpers.NextId();
            return new BinanceSocketRequest
            {
                Method = "SUBSCRIBE",
                Params = _identifiers.ToArray(),
                Id = _subId
            };
        }

        /// <inheritdoc />
        public override object? GetUnsubRequest()
        {
            _unsubId = ExchangeHelpers.NextId();
            return new BinanceSocketRequest
            {
                Method = "UNSUBSCRIBE",
                Params = _identifiers.ToArray(),
                Id = _unsubId
            };
        }

        /// <inheritdoc />
        public override bool MessageMatchesSubRequest(ParsedMessage message)
        {
            if (message.Data is not BinanceSocketQueryResponse response)
                return false;

            if (response.Id != _subId)
                return false;

            return true;
        }

        /// <inheritdoc />
        public override bool MessageMatchesUnsubRequest(ParsedMessage message)
        {
            if (message.Data is not BinanceSocketQueryResponse response)
                return false;

            if (response.Id != _unsubId)
                return false;

            return true;
        }

        /// <inheritdoc />
        public override Task HandleEventAsync(DataEvent<ParsedMessage> message)
        {
            var data = (T)message.Data.Data!;
            _handler.Invoke(message.As(data, null, SocketUpdateType.Update));
            return Task.CompletedTask;
        }

        public override CallResult HandleSubResponse(ParsedMessage message) => new CallResult(null);
        public override CallResult HandleUnsubResponse(ParsedMessage message) => new CallResult(null);
    }
}
