using Binance.Net.Objects.Internal;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Binance.Net.Objects.Sockets
{
    public class BinanceSpotSubscription<T> : Subscription
    {
        public override List<string> Identifiers => _identifiers;

        private readonly Action<DataEvent<T>> _handler;
        private readonly List<string> _identifiers;
        private int _subId;
        private int _unsubId;

        public BinanceSpotSubscription(ILogger logger, ISocketApiClient client, List<string> topics, Action<DataEvent<T>> handler, bool auth) : base(logger, client, auth)
        {
            _handler = handler;
            _identifiers = topics;
        }

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

        public override object? GetUnsubRequest()
        {
            _unsubId = ExchangeHelpers.NextId();
            return new BinanceSocketRequest
            {
                Method = "UNSUBSCRIBE",
                Params = _identifiers.ToArray(),
                Id = _subId
            };
        }
    

        public override (bool, CallResult?) MessageMatchesSubRequest(ParsedMessage message)
        {
            if (message.Data is not BinanceSocketQueryResponse response)
                return (false, null);

            if (response.Id != _subId)
                return (false, null);

            return (true, new CallResult(null));
        }

        public override (bool, CallResult?) MessageMatchesUnsubRequest(ParsedMessage message)
        {
            if (message.Data is not BinanceSocketQueryResponse response)
                return (false, null);

            if (response.Id != _unsubId)
                return (false, null);

            return (true, new CallResult(null));
        }

        public override Task HandleEventAsync(DataEvent<ParsedMessage> message)
        {
            var data = (T)message.Data.Data;
            _handler.Invoke(message.As(data, null, SocketUpdateType.Update));
            return Task.CompletedTask;
        }
    }
}
