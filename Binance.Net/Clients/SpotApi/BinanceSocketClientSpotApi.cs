using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Objects;
using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Options;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Binance.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class BinanceSocketClientSpotApi : SocketApiClient, IBinanceSocketClientSpotApi
    {
        #region fields
        /// <inheritdoc />
        public new BinanceSocketOptions ClientOptions => (BinanceSocketOptions)base.ClientOptions;
        /// <inheritdoc />
        public new BinanceSocketApiOptions ApiOptions => (BinanceSocketApiOptions)base.ApiOptions;

        internal BinanceExchangeInfo? _exchangeInfo;
        internal DateTime? _lastExchangeInfoUpdate;
        internal readonly string _brokerId;

        #endregion

        /// <inheritdoc />
        public IBinanceSocketClientSpotApiAccount Account { get; }
        /// <inheritdoc />
        public IBinanceSocketClientSpotApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IBinanceSocketClientSpotApiTrading Trading { get; }

        #region constructor/destructor

        internal BinanceSocketClientSpotApi(ILogger logger, BinanceSocketOptions options) :
            base(logger, options.Environment.SpotSocketStreamAddress, options, options.SpotOptions)
        {
            SetDataInterpreter((data) => string.Empty, null);

            Account = new BinanceSocketClientSpotApiAccount(logger, this);
            ExchangeData = new BinanceSocketClientSpotApiExchangeData(logger, this);
            Trading = new BinanceSocketClientSpotApiTrading(logger, this);

            _brokerId = !string.IsNullOrEmpty(options.SpotOptions.BrokerId) ? options.SpotOptions.BrokerId! : "x-VICEW9VV";
        }
        #endregion

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new BinanceAuthenticationProvider(credentials);

        internal Task<CallResult<UpdateSubscription>> SubscribeAsync<T>(string url, IEnumerable<string> topics, Action<DataEvent<T>> onData, CancellationToken ct)
        {
            var request = new BinanceSocketRequest
            {
                Method = "SUBSCRIBE",
                Params = topics.ToArray(),
                Id = ExchangeHelpers.NextId()
            };

            return SubscribeAsync(url.AppendPath("stream"), request, null, false, onData, ct);
        }

        internal Task<CallResult<BinanceResponse<T>>> QueryAsync<T>(string url, string method, Dictionary<string, object> parameters, bool authenticated = false, bool sign = false, int weight = 1)
        {
            if (authenticated)
            {
                if (AuthenticationProvider == null)
                    throw new InvalidOperationException("No credentials provided for authenticated endpoint");

                var authProvider = (BinanceAuthenticationProvider)AuthenticationProvider;
                if (sign)
                {
                    parameters = authProvider.AuthenticateSocketParameters(parameters);
                }
                else
                {
                    parameters.Add("apiKey", authProvider.GetApiKey());
                }
            }

            var request = new BinanceSocketQuery
            {
                Method = method,
                Params = parameters,
                Id = ExchangeHelpers.NextId()
            };

            return QueryAsync<BinanceResponse<T>>(url, request, false, weight);
        }

        internal CallResult<T> DeserializeInternal<T>(JToken obj, JsonSerializer? serializer = null, int? requestId = null)
        {
            return base.Deserialize<T>(obj, serializer, requestId);
        }

        /// <inheritdoc />
        protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        {
            callResult = null!;
            var bRequest = (BinanceSocketQuery)request;
            if (bRequest.Id != data["id"]?.Value<int>())
                return false;

            var status = data["status"]?.Value<int>();
            if (status != 200)
            {
                var error = data["error"]!;

                if (status == 429 || status == 418)
                {
                    DateTime? retryAfter = null;
                    var retryAfterVal = error["data"]?["retryAfter"]?.ToString();
                    if (long.TryParse(retryAfterVal, out var retryAfterMs))
                        retryAfter = DateTimeConverter.ConvertFromMilliseconds(retryAfterMs);

                    callResult = new CallResult<T>(new ServerRateLimitError(error["msg"]!.Value<string>()!)
                    {
                        RetryAfter = retryAfter
                    });
                }
                else
                    callResult = new CallResult<T>(new ServerError(error["code"]!.Value<int>(), error["msg"]!.Value<string>()!));
                return true;
            }
            callResult = Deserialize<T>(data!);
            return true;
        }

        /// <inheritdoc />
        protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscriptionListener subscription, object request, JToken message, out CallResult<object>? callResult)
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
                _logger.Log(LogLevel.Trace, $"Socket {s.SocketId} Subscription completed");
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
        protected override async Task<bool> UnsubscribeAsync(SocketConnection connection, SocketSubscriptionListener subscription)
        {
            var topics = ((BinanceSocketRequest)subscription.Subscription!).Params;
            var topicsToUnsub = new List<string>();
            foreach(var topic in topics)
            {
                if (connection.Subscriptions.Where(s => s != subscription).Any(s => ((BinanceSocketRequest?)s.Subscription)?.Params.Contains(topic) == true))
                    continue;

                topicsToUnsub.Add(topic);
            }

            if (!topicsToUnsub.Any())
            {
                _logger.LogInformation("No topics need unsubscribing (still active on other subscriptions)");
                return true;
            }

            var unsub = new BinanceSocketRequest { Method = "UNSUBSCRIBE", Params = topicsToUnsub.ToArray(), Id = ExchangeHelpers.NextId() };
            var result = false;

            if (!connection.Connected)
                return true;

            await connection.SendAndWaitAsync(unsub, ClientOptions.RequestTimeout, null, 1, data =>
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

        internal async Task<BinanceTradeRuleResult> CheckTradeRules(string symbol, decimal? quantity, decimal? quoteQuantity, decimal? price, decimal? stopPrice, SpotOrderType? type)
        {
            if (ApiOptions.TradeRulesBehaviour == TradeRulesBehaviour.None)
                return BinanceTradeRuleResult.CreatePassed(quantity, quoteQuantity, price, stopPrice);

            if (_exchangeInfo == null || _lastExchangeInfoUpdate == null || (DateTime.UtcNow - _lastExchangeInfoUpdate.Value).TotalMinutes > ApiOptions.TradeRulesUpdateInterval.TotalMinutes)
                await ExchangeData.GetExchangeInfoAsync().ConfigureAwait(false);

            if (_exchangeInfo == null)
                return BinanceTradeRuleResult.CreateFailed("Unable to retrieve trading rules, validation failed");

            return BinanceHelpers.ValidateTradeRules(_logger, ApiOptions.TradeRulesBehaviour, _exchangeInfo, symbol, quantity, quoteQuantity, price, stopPrice, type);
        }

    }
}
