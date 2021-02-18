using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces.SubClients;
using Binance.Net.Objects;
using Binance.Net.Objects.Spot.MarginData;
using Binance.Net.Objects.Spot.SpotData;
using Binance.Net.Objects.Spot.WalletData;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;

namespace Binance.Net.SubClients
{
    /// <summary>
    /// General endpoints
    /// </summary>
    public class BinanceClientGeneral : IBinanceClientGeneral
    {
        private const string accountInfoEndpoint = "account";
        private const string accountSnapshotEndpoint = "accountSnapshot";
        private const string accountStatusEndpoint = "accountStatus.html";
        private const string tradingStatusEndpoint = "apiTradingStatus.html";

        private const string dividendRecordsEndpoint = "asset/assetDividend";

        private const string userCoinsEndpoint = "capital/config/getall";

        private const string disableFastWithdrawSwitchEndpoint = "account/disableFastWithdrawSwitch";
        private const string enableFastWithdrawSwitchEndpoint = "account/enableFastWithdrawSwitch";

        private const string dustLogEndpoint = "userAssetDribbletLog.html";
        private const string dustTransferEndpoint = "asset/dust";

        private const string toggleBnbBurnEndpoint = "bnbBurn";
        private const string getBnbBurnEndpoint = "bnbBurn";


        private readonly BinanceClient _baseClient;

        internal BinanceClientGeneral(BinanceClient baseClient)
        {
            _baseClient = baseClient;
        }

        #region Daily snapshots
        /// <summary>
        /// Get a daily account snapshot (balances)
        /// </summary>
        /// <param name="startTime">The start time</param>
        /// <param name="endTime">The end time</param>
        /// <param name="limit">The amount of days to retrieve</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<BinanceSpotAccountSnapshot>> GetDailySpotAccountSnapshot(
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null,
            CancellationToken ct = default) =>
            GetDailySpotAccountSnapshotAsync(startTime, endTime, limit, receiveWindow, ct).Result;

        /// <summary>
        /// Get a daily account snapshot (balances)
        /// </summary>
        /// <param name="startTime">The start time</param>
        /// <param name="endTime">The end time</param>
        /// <param name="limit">The amount of days to retrieve</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<BinanceSpotAccountSnapshot>>> GetDailySpotAccountSnapshotAsync(
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null,
            CancellationToken ct = default) =>
            await GetDailyAccountSnapshot<IEnumerable<BinanceSpotAccountSnapshot>>(AccountType.Spot, startTime, endTime, limit, receiveWindow, ct).ConfigureAwait(false);

        /// <summary>
        /// Get a daily account snapshot (assets)
        /// </summary>
        /// <param name="startTime">The start time</param>
        /// <param name="endTime">The end time</param>
        /// <param name="limit">The amount of days to retrieve</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<BinanceMarginAccountSnapshot>> GetDailyMarginAccountSnapshot(
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null,
            CancellationToken ct = default) =>
            GetDailyMarginAccountSnapshotAsync(startTime, endTime, limit, receiveWindow, ct).Result;

        /// <summary>
        /// Get a daily account snapshot (assets)
        /// </summary>
        /// <param name="startTime">The start time</param>
        /// <param name="endTime">The end time</param>
        /// <param name="limit">The amount of days to retrieve</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<BinanceMarginAccountSnapshot>>> GetDailyMarginAccountSnapshotAsync(
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null,
            CancellationToken ct = default) =>
            await GetDailyAccountSnapshot<IEnumerable<BinanceMarginAccountSnapshot>>(AccountType.Spot, startTime, endTime, limit, receiveWindow, ct).ConfigureAwait(false);

        /// <summary>
        /// Get a daily account snapshot (assets and positions)
        /// </summary>
        /// <param name="startTime">The start time</param>
        /// <param name="endTime">The end time</param>
        /// <param name="limit">The amount of days to retrieve</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<BinanceFuturesAccountSnapshot>> GetDailyFutureAccountSnapshot(
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null,
            CancellationToken ct = default) =>
            GetDailyFutureAccountSnapshotAsync(startTime, endTime, limit, receiveWindow, ct).Result;

        /// <summary>
        /// Get a daily account snapshot (assets and positions)
        /// </summary>
        /// <param name="startTime">The start time</param>
        /// <param name="endTime">The end time</param>
        /// <param name="limit">The amount of days to retrieve</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<BinanceFuturesAccountSnapshot>>> GetDailyFutureAccountSnapshotAsync(
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null,
            CancellationToken ct = default) =>
            await GetDailyAccountSnapshot<IEnumerable<BinanceFuturesAccountSnapshot>>(AccountType.Futures, startTime, endTime, limit, receiveWindow, ct).ConfigureAwait(false);


        private async Task<WebCallResult<T>> GetDailyAccountSnapshot<T>(AccountType accountType, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null,
            CancellationToken ct = default) where T : class
        {
            limit?.ValidateIntBetween(nameof(limit), 5, 30);

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<T>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "type", JsonConvert.SerializeObject(accountType, new AccountTypeConverter(false)) },
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", startTime.HasValue ? JsonConvert.SerializeObject(startTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endTime.HasValue ? JsonConvert.SerializeObject(endTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<BinanceSnapshotWrapper<T>>(_baseClient.GetUrlSpot(accountSnapshotEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success)
                return WebCallResult<T>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error!);

            if (result.Data.Code != 200)
                return WebCallResult<T>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.Code, result.Data.Message));

            return new WebCallResult<T>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.SnapshotData, null);
        }
        #endregion

        #region Account status
        /// <summary>
        /// Gets the status of the account associated with the api key/secret
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Account status</returns>
        public WebCallResult<BinanceAccountStatus> GetAccountStatus(int? receiveWindow = null, CancellationToken ct = default) => GetAccountStatusAsync(receiveWindow, ct).Result;

        /// <summary>
        /// Gets the status of the account associated with the api key/secret
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Account status</returns>
        public async Task<WebCallResult<BinanceAccountStatus>> GetAccountStatusAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceAccountStatus>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<BinanceAccountStatus>(_baseClient.GetUrlSpot(accountStatusEndpoint, "wapi", "3"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result)
                return new WebCallResult<BinanceAccountStatus>(result.ResponseStatusCode, result.ResponseHeaders, null, result.Error);

            return !result.Data.Success ? new WebCallResult<BinanceAccountStatus>(result.ResponseStatusCode, result.ResponseHeaders, null, _baseClient.ParseErrorResponseInternal(result.Data.Message)) : new WebCallResult<BinanceAccountStatus>(result.ResponseStatusCode, result.ResponseHeaders, result.Data, null);
        }
        #endregion

        #region Account info
        /// <summary>
        /// Gets account information, including balances
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The account information</returns>
        public WebCallResult<BinanceAccountInfo> GetAccountInfo(long? receiveWindow = null, CancellationToken ct = default) => GetAccountInfoAsync(receiveWindow, ct).Result;

        /// <summary>
        /// Gets account information, including balances
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The account information</returns>
        public async Task<WebCallResult<BinanceAccountInfo>> GetAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceAccountInfo>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceAccountInfo>(_baseClient.GetUrlSpot(accountInfoEndpoint, "api", "3"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Trading status
        /// <summary>
        /// Gets the trading status for the current account
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The trading status of the account</returns>
        public WebCallResult<BinanceTradingStatus> GetTradingStatus(int? receiveWindow = null, CancellationToken ct = default) => GetTradingStatusAsync(receiveWindow, ct).Result;

        /// <summary>
        /// Gets the trading status for the current account
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The trading status of the account</returns>
        public async Task<WebCallResult<BinanceTradingStatus>> GetTradingStatusAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceTradingStatus>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() },
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<BinanceTradingStatusWrapper>(_baseClient.GetUrlSpot(tradingStatusEndpoint, "wapi", "3"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result)
                return new WebCallResult<BinanceTradingStatus>(result.ResponseStatusCode, result.ResponseHeaders, null, result.Error);

            return !result.Data.Success ? new WebCallResult<BinanceTradingStatus>(result.ResponseStatusCode, result.ResponseHeaders, null, new ServerError(result.Data.Message!)) : new WebCallResult<BinanceTradingStatus>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Status, null);
        }
        #endregion

        #region User coins
        /// <summary>
        /// Gets information of coins for a user
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Coins info</returns>
        public WebCallResult<IEnumerable<BinanceUserCoin>> GetUserCoins(int? receiveWindow = null, CancellationToken ct = default) => GetUserCoinsAsync(receiveWindow, ct).Result;

        /// <summary>
        /// Gets information of coins for a user
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Coins info</returns>
        public async Task<WebCallResult<IEnumerable<BinanceUserCoin>>> GetUserCoinsAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));


            return await _baseClient.SendRequestInternal<IEnumerable<BinanceUserCoin>>(_baseClient.GetUrlSpot(userCoinsEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Asset Dividend Records
        /// <summary>
        /// Get asset dividend records
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="startTime">Filter by start time from</param>
        /// <param name="endTime">Filter by end time till</param>
        /// <param name="limit">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Dividend records</returns>
        public WebCallResult<BinanceQueryRecords<BinanceDividendRecord>> GetAssetDividendRecords(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default) => GetAssetDividendRecordsAsync(asset, startTime, endTime, limit, receiveWindow, ct).Result;

        /// <summary>
        /// Get asset dividend records
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// /// <param name="startTime">Filter by start time from</param>
        /// <param name="endTime">Filter by end time till</param>
        /// <param name="limit">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Dividend records</returns>
        public async Task<WebCallResult<BinanceQueryRecords<BinanceDividendRecord>>> GetAssetDividendRecordsAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceQueryRecords<BinanceDividendRecord>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("asset", asset);
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("startTime", startTime != null ? BinanceClient.ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? BinanceClient.ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceQueryRecords<BinanceDividendRecord>>(_baseClient.GetUrlSpot(dividendRecordsEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Disable Fast Withdraw Switch

        /// <summary>
        /// This request will disable fastwithdraw switch under your account.
        /// You need to enable "trade" option for the api key which requests this endpoint.
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public WebCallResult<object> DisableFastWithdrawSwitch(int? receiveWindow = null, CancellationToken ct = default) => DisableFastWithdrawSwitchAsync(receiveWindow, ct).Result;

        /// <summary>
        /// This request will disable fastwithdraw switch under your account.
        /// You need to enable "trade" option for the api key which requests this endpoint.
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<object>> DisableFastWithdrawSwitchAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));


            return await _baseClient.SendRequestInternal<object>(_baseClient.GetUrlSpot(disableFastWithdrawSwitchEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Enable Fast Withdraw Switch
        /// <summary>
        /// This request will enable fastwithdraw switch under your account.
        /// You need to enable "trade" option for the api key which requests this endpoint.
        ///
        /// When Fast Withdraw Switch is on, transferring funds to a Binance account will be done instantly.
        /// There is no on-chain transaction, no transaction ID and no withdrawal fee.
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public WebCallResult<object> EnableFastWithdrawSwitch(int? receiveWindow = null, CancellationToken ct = default) => EnableFastWithdrawSwitchAsync(receiveWindow, ct).Result;

        /// <summary>
        /// This request will enable fastwithdraw switch under your account.
        /// You need to enable "trade" option for the api key which requests this endpoint.
        ///
        /// When Fast Withdraw Switch is on, transferring funds to a Binance account will be done instantly.
        /// There is no on-chain transaction, no transaction ID and no withdrawal fee.
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<object>> EnableFastWithdrawSwitchAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));


            return await _baseClient.SendRequestInternal<object>(_baseClient.GetUrlSpot(enableFastWithdrawSwitchEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region DustLog

        /// <summary>
        /// Gets the history of dust conversions
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The history of dust conversions</returns>
        public WebCallResult<IEnumerable<BinanceDustLog>> GetDustLog(int? receiveWindow = null, CancellationToken ct = default) => GetDustLogAsync(receiveWindow, ct).Result;

        /// <summary>
        /// Gets the history of dust conversions
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The history of dust conversions</returns>
        public async Task<WebCallResult<IEnumerable<BinanceDustLog>>> GetDustLogAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceDustLog>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<BinanceDustLogListWrapper>(_baseClient.GetUrlSpot(dustLogEndpoint, "wapi", "3"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result)
                return new WebCallResult<IEnumerable<BinanceDustLog>>(result.ResponseStatusCode, result.ResponseHeaders, null, result.Error);

            return !result.Data.Success ? new WebCallResult<IEnumerable<BinanceDustLog>>(result.ResponseStatusCode, result.ResponseHeaders, null, new ServerError("Unknown server error while requesting dust log")) : new WebCallResult<IEnumerable<BinanceDustLog>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Results!.Rows, null);
        }

        #endregion

        #region Dust Transfer

        /// <summary>
        /// Converts dust (small amounts of) assets to BNB 
        /// </summary>
        /// <param name="assets">The assets to convert to BNB</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Dust transfer result</returns>
        public WebCallResult<BinanceDustTransferResult> DustTransfer(IEnumerable<string> assets, int? receiveWindow = null, CancellationToken ct = default) => DustTransferAsync(assets, receiveWindow, ct).Result;

        /// <summary>
        /// Converts dust (small amounts of) assets to BNB 
        /// </summary>
        /// <param name="assets">The assets to convert to BNB</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Dust transfer result</returns>
        public async Task<WebCallResult<BinanceDustTransferResult>> DustTransferAsync(IEnumerable<string> assets, int? receiveWindow = null, CancellationToken ct = default)
        {
            var assetsArray = assets.ToArray();

            assetsArray.ValidateNotNull(nameof(assets));
            foreach (var asset in assetsArray)
                asset.ValidateNotNull(nameof(asset));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceDustTransferResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "asset", assetsArray },
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceDustTransferResult>(_baseClient.GetUrlSpot(dustTransferEndpoint, "sapi", "1"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get BNB Burn Status
        /// <summary>
        /// Gets the status of the BNB burn switch for spot trading and margin interest
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public WebCallResult<BinanceBnbBurnStatus> GetBnbBurnStatus(int? receiveWindow = null, CancellationToken ct = default) => GetBnbBurnStatusAsync(receiveWindow, ct).Result;

        /// <summary>
        /// Gets the status of the BNB burn switch for spot trading and margin interest
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<BinanceBnbBurnStatus>> GetBnbBurnStatusAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBnbBurnStatus>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBnbBurnStatus>(_baseClient.GetUrlSpot(getBnbBurnEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Set BNB Burn Status
        /// <summary>
        /// Sets the status of the BNB burn switch for spot trading and margin interest
        /// </summary>
        /// <param name="spotTrading">If BNB burning should be enabled for spot trading</param>
        /// <param name="marginInterest">If BNB burning should be enabled for margin interest</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public WebCallResult<BinanceBnbBurnStatus> SetBnbBurnStatus(bool? spotTrading = null, bool? marginInterest = null, int? receiveWindow = null, CancellationToken ct = default) => SetBnbBurnStatusAsync(spotTrading, marginInterest, receiveWindow, ct).Result;

        /// <summary>
        /// Sets the status of the BNB burn switch for spot trading and margin interest
        /// </summary>
        /// <param name="spotTrading">If BNB burning should be enabled for spot trading</param>
        /// <param name="marginInterest">If BNB burning should be enabled for margin interest</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<BinanceBnbBurnStatus>> SetBnbBurnStatusAsync(bool? spotTrading = null, bool? marginInterest = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            if(spotTrading == null && marginInterest == null)
                throw new ArgumentException("SpotTrading or MarginInterest should be provided");

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBnbBurnStatus>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("spotBNBBurn", spotTrading);
            parameters.AddOptionalParameter("interestBNBBurn", marginInterest);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBnbBurnStatus>(_baseClient.GetUrlSpot(toggleBnbBurnEndpoint, "sapi", "1"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion
    }
}
