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
using Binance.Net.Objects.Sockets;
using Binance.Net.Objects.Sockets.Converters;
using Binance.Net.Objects.Sockets.Subscriptions;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;

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

        /// <inheritdoc />
        public override SocketConverter StreamConverter => new BinanceStreamConverter();

        #region constructor/destructor

        internal BinanceSocketClientSpotApi(ILogger logger, BinanceSocketOptions options) :
            base(logger, options.Environment.SpotSocketStreamAddress, options, options.SpotOptions)
        {
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
            var subscription = new BinanceSubscription<T>(_logger, topics.ToList(), onData, false);
            return base.SubscribeAsync(url.AppendPath("stream"), subscription, ct);
        }

        internal Task<CallResult<UpdateSubscription>> SubscribeInternalAsync(string url, Subscription subscription, CancellationToken ct)
        {
            return base.SubscribeAsync(url.AppendPath("stream"), subscription, ct);
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

            var query = new BinanceSpotQuery<BinanceResponse<T>>(request, false, weight);
            return QueryAsync(url, query);
        }

        /// <inheritdoc />
        //protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        //{
        //    callResult = null!;
        //    var bRequest = (BinanceSocketQuery)request;
        //    if (bRequest.Id != data["id"]?.Value<int>())
        //        return false;

        //    var status = data["status"]?.Value<int>();
        //    if (status != 200)
        //    {
        //        var error = data["error"]!;

        //        if (status == 429 || status == 418)
        //        {
        //            DateTime? retryAfter = null;
        //            var retryAfterVal = error["data"]?["retryAfter"]?.ToString();
        //            if (long.TryParse(retryAfterVal, out var retryAfterMs))
        //                retryAfter = DateTimeConverter.ConvertFromMilliseconds(retryAfterMs);

        //            callResult = new CallResult<T>(new ServerRateLimitError(error["msg"]!.Value<string>()!)
        //            {
        //                RetryAfter = retryAfter
        //            });
        //        }
        //        else
        //            callResult = new CallResult<T>(new ServerError(error["code"]!.Value<int>(), error["msg"]!.Value<string>()!));
        //        return true;
        //    }
        //    callResult = Deserialize<T>(data!);
        //    return true;
        //}

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
