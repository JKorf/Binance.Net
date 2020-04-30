using Binance.Net.Interfaces;
using Binance.Net.Objects.Brokerage;
using Binance.Net.Objects.Brokerage.SubAccountData;
using Binance.Net.Objects.Spot.MarketData;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Binance.Net
{
    public class BinanceBrokerageClient : RestClient, IBinanceBrokerageClient
    {
        private static BinanceBrokerageClientOptions defaultOptions = new BinanceBrokerageClientOptions();
        private static BinanceBrokerageClientOptions DefaultOptions => defaultOptions.Copy();
        
        private readonly bool autoTimestamp;
        private readonly TimeSpan autoTimestampRecalculationInterval;
        private readonly TimeSpan timestampOffset;
        private readonly TimeSpan defaultReceiveWindow;
        
        private double calculatedTimeOffset;
        private bool timeSynced;
        private DateTime lastTimeSync;
        
        private const string Api = "api";
        private const string BrokerageApi = "sapi";
        
        private const string PublicVersion = "3";
        private const string BrokerageVersion = "1";
        
        private const string CheckTimeEndpoint = "time";
        
        private const string SubAccountEndpoint = "broker/subAccount";
        private const string BrokerAccountInfoEndpoint = "broker/info";
        private const string EnableMarginForSubAccountEndpoint = "broker/subAccount/margin";
        private const string EnableFuturesForSubAccountEndpoint = "broker/subAccount/futures";
        private const string ApiKeySubAccountEndpoint = "broker/subAccountApi";
        private const string ApiKeySubAccountPermissionEndpoint = "broker/subAccountApi/permission";

        public BinanceBrokerageClient(ApiCredentials credentials)
            : base(DefaultOptions, new BinanceAuthenticationProvider(credentials, ArrayParametersSerialization.MultipleValues))
        {
            postParametersPosition = PostParameters.InUri;
        }

        public BinanceBrokerageClient(BinanceBrokerageClientOptions options) 
            : base(options, options.ApiCredentials == null ? null : new BinanceAuthenticationProvider(options.ApiCredentials, ArrayParametersSerialization.MultipleValues))
        {
            autoTimestamp = options.AutoTimestamp;
            autoTimestampRecalculationInterval = options.AutoTimestampRecalculationInterval;
            timestampOffset = options.TimestampOffset;
            defaultReceiveWindow = options.ReceiveWindow;
            postParametersPosition = PostParameters.InUri;
        }
        
        /// <summary>
        /// Generate a sub account under your brokerage master account
        /// <para>You need to enable "trade" option for the api key which requests this endpoint</para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Created sub-account id</returns>
        public async Task<WebCallResult<BinanceBrokerageSubAccountCreateResult>> CreateSubAccountAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageSubAccountCreateResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"timestamp", GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<BinanceBrokerageSubAccountCreateResult>(GetUrl(SubAccountEndpoint, BrokerageApi, BrokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Enable Margin for Sub Account
        /// </summary>
        /// <param name="id">Sub account id</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Enable Margin result</returns>
        public async Task<WebCallResult<BinanceBrokerageEnableMarginResult>> EnableMarginForSubAccountAsync(string id, int? receiveWindow = null, CancellationToken ct = default)
        {
            id.ValidateNotNull(nameof(id));
            
            var timestampResult = await CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageEnableMarginResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", id},
                                 {"margin", true},  // only true for now
                                 {"timestamp", GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<BinanceBrokerageEnableMarginResult>(GetUrl(EnableMarginForSubAccountEndpoint, BrokerageApi, BrokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Enable Futures for Sub Account
        /// </summary>
        /// <param name="id">Sub account id</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Enable Futures result</returns>
        public async Task<WebCallResult<BinanceBrokerageEnableFuturesResult>> EnableFuturesForSubAccountAsync(string id, int? receiveWindow = null, CancellationToken ct = default)
        {
            id.ValidateNotNull(nameof(id));
            
            var timestampResult = await CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageEnableFuturesResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", id},
                                 {"futures", true},  // only true for now
                                 {"timestamp", GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<BinanceBrokerageEnableFuturesResult>(GetUrl(EnableFuturesForSubAccountEndpoint, BrokerageApi, BrokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Create Api Key for Sub Account
        /// <para>Sub account should be enable margin before its api-key's marginTrade being enabled</para>
        /// <para>Sub account should be enable futures before its api-key's futuresTrade being enabled</para>
        /// </summary>
        /// <param name="id">Sub account id</param>
        /// <param name="isSpotTradingEnabled">Is spot trading enabled</param>
        /// <param name="isMarginTradingEnabled">Is margin trading enabled</param>
        /// <param name="isFuturesTradingEnabled">Is futures trading enabled</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Api key result</returns>
        public async Task<WebCallResult<BinanceBrokerageApiKeyCreateResult>> CreateApiKeyForSubAccountAsync(string id, bool isSpotTradingEnabled, bool? isMarginTradingEnabled = null, 
            bool? isFuturesTradingEnabled = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            id.ValidateNotNull(nameof(id));
            
            var timestampResult = await CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageApiKeyCreateResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", id},
                                 {"canTrade", isSpotTradingEnabled},
                                 {"timestamp", GetTimestamp()}
                             };
            parameters.AddOptionalParameter("marginTrade", isMarginTradingEnabled);
            parameters.AddOptionalParameter("futuresTrade", isFuturesTradingEnabled);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<BinanceBrokerageApiKeyCreateResult>(GetUrl(ApiKeySubAccountEndpoint, BrokerageApi, BrokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Delete Sub Account Api Key
        /// <para>You need to enable "trade" option for the api key which requests this endpoint</para>
        /// </summary>
        /// <param name="id">Sub account id</param>
        /// <param name="apiKey"></param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        public async Task<object> DeleteSubAccountApiKeyAsync(string id, string apiKey, int? receiveWindow = null, CancellationToken ct = default)
        {
            id.ValidateNotNull(nameof(id));
            id.ValidateNotNull(nameof(apiKey));
            
            var timestampResult = await CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<object>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", id},
                                 {"subAccountApiKey", apiKey},
                                 {"timestamp", GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<object>(GetUrl(ApiKeySubAccountEndpoint, BrokerageApi, BrokerageVersion), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Query Sub Account Api Key
        /// </summary>
        /// <param name="id">Sub account id</param>
        /// <param name="apiKey">Api key</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Api key result</returns>
        public async Task<WebCallResult<BinanceBrokerageSubAccountApiKey>> QuerySubAccountApiKeyAsync(string id, string apiKey = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            id.ValidateNotNull(nameof(id));
            
            var timestampResult = await CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageSubAccountApiKey>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", id},
                                 {"timestamp", GetTimestamp()}
                             };
            parameters.AddOptionalParameter("subAccountApiKey", apiKey);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<BinanceBrokerageSubAccountApiKey>(GetUrl(ApiKeySubAccountEndpoint, BrokerageApi, BrokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Change Sub Account Api Permission
        /// <para>You need to enable "trade" option for the api key which requests this endpoint</para>
        /// <para>Sub account should be enable margin before its api-key's marginTrade being enabled</para>
        /// <para>Sub account should be enable futures before its api-key's futuresTrade being enabled</para>
        /// </summary>
        /// <param name="id">Sub account id</param>
        /// <param name="apiKey">Api key</param>
        /// <param name="isSpotTradingEnabled">Is spot trading enabled</param>
        /// <param name="isMarginTradingEnabled">Is margin trading enabled</param>
        /// <param name="isFuturesTradingEnabled">Is futures trading enabled</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Api key result</returns>
        public async Task<WebCallResult<BinanceBrokerageSubAccountApiKey>> ChangeSubAccountApiPermissionAsync(string id, string apiKey, 
            bool isSpotTradingEnabled, bool isMarginTradingEnabled, bool isFuturesTradingEnabled, int? receiveWindow = null, CancellationToken ct = default)
        {
            id.ValidateNotNull(nameof(id));
            id.ValidateNotNull(nameof(apiKey));
            
            var timestampResult = await CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageSubAccountApiKey>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", id},
                                 {"subAccountApiKey", apiKey},
                                 {"canTrade", isSpotTradingEnabled},
                                 {"marginTrade", isMarginTradingEnabled},
                                 {"futuresTrade", isFuturesTradingEnabled},
                                 {"timestamp", GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<BinanceBrokerageSubAccountApiKey>(GetUrl(ApiKeySubAccountPermissionEndpoint, BrokerageApi, BrokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Query Sub Account
        /// </summary>
        /// <param name="id">Sub account id</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Sub accounts</returns>
        public async Task<WebCallResult<IEnumerable<BinanceBrokerageSubAccount>>> GetSubAccountsAsync(string id = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceBrokerageSubAccount>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"timestamp", GetTimestamp()}
                             };
            parameters.AddOptionalParameter("subAccountId", id);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<IEnumerable<BinanceBrokerageSubAccount>>(GetUrl(SubAccountEndpoint, BrokerageApi, BrokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        
        public async Task<WebCallResult<BinanceBrokerageAccountInfo>> GetBrokerAccountInfoAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageAccountInfo>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"timestamp", GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<BinanceBrokerageAccountInfo>(GetUrl(BrokerAccountInfoEndpoint, BrokerageApi, BrokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        
        private async Task<WebCallResult<DateTime>> GetServerTimeAsync(bool resetAutoTimestamp = false, CancellationToken ct = default)
        {
            var url = GetUrl(CheckTimeEndpoint, Api, PublicVersion);
            if (!autoTimestamp)
            {
                var result = await SendRequest<BinanceCheckTime>(url, HttpMethod.Get, ct).ConfigureAwait(false);
                return new WebCallResult<DateTime>(result.ResponseStatusCode, result.ResponseHeaders, result.Data?.ServerTime ?? default, result.Error);
            }
            else
            {
                var localTime = DateTime.UtcNow;
                var result = await SendRequest<BinanceCheckTime>(url, HttpMethod.Get, ct).ConfigureAwait(false);
                if (!result)
                    return new WebCallResult<DateTime>(result.ResponseStatusCode, result.ResponseHeaders, default, result.Error);

                if (timeSynced && !resetAutoTimestamp)
                    return new WebCallResult<DateTime>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.ServerTime, result.Error);

                if (TotalRequestsMade == 1)
                {
                    // If this was the first request make another one to calculate the offset since the first one can be slower
                    localTime = DateTime.UtcNow;
                    result = await SendRequest<BinanceCheckTime>(url, HttpMethod.Get, ct).ConfigureAwait(false);
                    if (!result)
                        return new WebCallResult<DateTime>(result.ResponseStatusCode, result.ResponseHeaders, default, result.Error);
                }

                // Calculate time offset between local and server
                var offset = (result.Data.ServerTime - localTime).TotalMilliseconds;
                if (offset >= 0 && offset < 500)
                {
                    // Small offset, probably mainly due to ping. Don't adjust time
                    calculatedTimeOffset = 0;
                    timeSynced = true;
                    lastTimeSync = DateTime.UtcNow;
                    log.Write(LogVerbosity.Info, $"Time offset between 0 and 500ms ({offset}ms), no adjustment needed");
                    return new WebCallResult<DateTime>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.ServerTime, result.Error);
                }

                calculatedTimeOffset = (result.Data.ServerTime - localTime).TotalMilliseconds;
                timeSynced = true;
                lastTimeSync = DateTime.UtcNow;
                log.Write(LogVerbosity.Info, $"Time offset set to {calculatedTimeOffset}ms");
                return new WebCallResult<DateTime>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.ServerTime, result.Error);
            }
        }
        
        private Uri GetUrl(string endpoint, string api, string version)
        {
            var result = $"{BaseAddress}/{api}/v{version}/{endpoint}";
            return new Uri(result);
        }
        
        private async Task<WebCallResult<DateTime>> CheckAutoTimestamp(CancellationToken ct)
        {
            if (autoTimestamp && (!timeSynced || DateTime.UtcNow - lastTimeSync > autoTimestampRecalculationInterval))
                return await GetServerTimeAsync(timeSynced, ct).ConfigureAwait(false);

            return new WebCallResult<DateTime>(null, null, default, null);
        }
        
        private static long ToUnixTimestamp(DateTime time) => (long)(time - new DateTime(1970, 1, 1)).TotalMilliseconds;

        private string GetTimestamp()
        {
            var offset = autoTimestamp ? calculatedTimeOffset : 0;
            offset += timestampOffset.TotalMilliseconds;
            return ToUnixTimestamp(DateTime.UtcNow.AddMilliseconds(offset)).ToString();
        }     
    }
}