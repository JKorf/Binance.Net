using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Objects;
using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.Blvt;
using Binance.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Binance.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class BinanceSocketClientSpotApi : SocketApiClient//, IBinanceSocketClientSpotApi
    {
        #region fields
        internal BinanceSocketClientOptions ClientOptions { get; }
        #endregion

        public  BinanceSocketClientSpotApiExchangeData ExchangeData { get; }

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of BinanceSocketClientSpot with default options
        /// </summary>
        public BinanceSocketClientSpotApi(Log log, BinanceSocketClientOptions options) :
            base(log, options, options.SpotStreamsOptions)
        {
            ClientOptions = options;

            SetDataInterpreter((data) => string.Empty, null);
            RateLimitPerSocketPerSecond = 4;

            ExchangeData = new BinanceSocketClientSpotApiExchangeData(this);
        }
        #endregion

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new BinanceAuthenticationProvider((BinanceApiCredentials)credentials);

        internal Task<CallResult<UpdateSubscription>> SubscribeAsync<T>(string url, IEnumerable<string> topics, Action<DataEvent<T>> onData, CancellationToken ct)
        {
            var request = new BinanceSocketRequest
            {
                Method = "SUBSCRIBE",
                Params = topics.ToArray(),
                Id = NextId()
            };

            return SubscribeAsync(url.AppendPath("stream"), request, null, false, onData, ct);
        }

        internal Task<CallResult<T>> QueryAsync<T>(string url, string method, Dictionary<string, object> parameters)
        {
            var request = new BinanceSocketQuery
            {
                Method = method,
                Params = parameters,
                Id = NextId()
            };

            return QueryAsync<T>(url, request, false);
        }

        internal CallResult<T> DeserializeInternal<T>(JToken obj, JsonSerializer? serializer = null, int? requestId = null)
        {
            return base.Deserialize<T>(obj, serializer, requestId);
        }

        /// <inheritdoc />
        protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        {
            callResult = null;
            var bRequest = (BinanceSocketQuery)request;
            if (bRequest.Id != data["id"]?.Value<int>())
                return false;

            var status = data["status"]?.Value<int>();
            if (status != 200)
            {
                var error = data["error"]!;
                callResult = new CallResult<T>(new ServerError(error["code"]!.Value<int>(), error["msg"]!.Value<string>()!));
                return true;
            }

            var result = data["result"];
            callResult = Deserialize<T>(result!);
            return true;
        }

        /// <inheritdoc />
        protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object>? callResult)
        {
            callResult = null;
            if (message.Type != JTokenType.Object)
                return false;

            var id = message["id"];
            if (id == null)
                return false;

            var bRequest = (BinanceSocketRequest)request;
            if ((int)id != bRequest.Id)
                return false;

            var result = message["result"];
            if (result != null && result.Type == JTokenType.Null)
            {
                _log.Write(LogLevel.Trace, $"Socket {s.SocketId} Subscription completed");
                callResult = new CallResult<object>(new object());
                return true;
            }

            var error = message["error"];
            if (error == null)
            {
                callResult = new CallResult<object>(new ServerError("Unknown error: " + message));
                return true;
            }

            callResult = new CallResult<object>(new ServerError(error["code"]!.Value<int>(), error["msg"]!.ToString()));
            return true;
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(SocketConnection socketConnection, JToken message, object request)
        {
            if (message.Type != JTokenType.Object)
                return false;

            var bRequest = (BinanceSocketRequest)request;
            var stream = message["stream"];
            if (stream == null)
                return false;

            return bRequest.Params.Contains(stream.ToString());
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(SocketConnection socketConnection, JToken message, string identifier)
        {
            return true;
        }

        /// <inheritdoc />
        protected override Task<CallResult<bool>> AuthenticateSocketAsync(SocketConnection s)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override async Task<bool> UnsubscribeAsync(SocketConnection connection, SocketSubscription subscription)
        {
            var topics = ((BinanceSocketRequest)subscription.Request!).Params;
            var unsub = new BinanceSocketRequest { Method = "UNSUBSCRIBE", Params = topics, Id = NextId() };
            var result = false;

            if (!connection.Connected)
                return true;

            await connection.SendAndWaitAsync(unsub, Options.SocketResponseTimeout, null, data =>
            {
                if (data.Type != JTokenType.Object)
                    return false;

                var id = data["id"];
                if (id == null)
                    return false;

                if ((int)id != unsub.Id)
                    return false;

                var result = data["result"];
                if (result?.Type == JTokenType.Null)
                {
                    result = true;
                    return true;
                }

                return true;
            }).ConfigureAwait(false);
            return result;
        }
    }
}
