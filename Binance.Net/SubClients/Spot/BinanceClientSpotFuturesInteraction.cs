using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces.SubClients.Spot;
using Binance.Net.Objects;
using Binance.Net.Objects.Spot.Futures;
using Binance.Net.Objects.Spot.MarginData;
using Binance.Net.Objects.Spot.MarketData;
using Binance.Net.Objects.Spot.WalletData;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;

namespace Binance.Net.SubClients.Spot
{
    /// <summary>
    /// Spot/futures endpoints
    /// </summary>
    public class BinanceClientSpotFuturesInteraction : IBinanceClientSpotFuturesInteraction
    {
        private readonly BinanceClient _baseClient;

        private const string futuresTransferEndpoint = "futures/transfer";
        private const string futuresTransferHistoryEndpoint = "futures/transfer";
        private const string futuresBorrowEndpoint = "futures/loan/borrow";
        private const string futuresBorrowHistoryEndpoint = "futures/loan/borrow/history";
        private const string futuresRepayEndpoint = "futures/loan/repay";
        private const string futuresRepayHistoryEndpoint = "futures/loan/repay/history";
        private const string futuresWalletEndpoint = "futures/loan/wallet";
        private const string futuresInformationEndpoint = "futures/loan/configs";
        private const string futuresCalculateAdjustLevelEndpoint = "futures/loan/calcAdjustLevel";
        private const string futuresCalculateMaxAdjustAmountEndpoint = "futures/loan/calcMaxAdjustAmount";
        private const string futuresAdjustCrossCollateralEndpoint = "futures/loan/adjustCollateral";
        private const string futuresAdjustCrossCollateralHistoryEndpoint = "futures/loan/adjustCollateral/history";
        private const string futuresCrossCollateralLiquidationHistoryEndpoint = "futures/loan/liquidationHistory";

        private const string api = "sapi";
        private const string publicVersion = "1";

        internal BinanceClientSpotFuturesInteraction(BinanceClient baseClient)
        {
            _baseClient = baseClient;
        }

        #region New Future Account Transfer

        /// <summary>
        /// Execute a transfer between the spot account and a futures account
        /// </summary>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">Amount to transfer</param>
        /// <param name="transferType">The transfer direction</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The transaction id</returns>
        public WebCallResult<BinanceTransaction> TransferFuturesAccount(string asset, decimal amount, FuturesTransferType transferType, long? receiveWindow = null, CancellationToken ct = default) => TransferFuturesAccountAsync(asset, amount, transferType, receiveWindow, ct).Result;

        /// <summary>
        /// Execute a transfer between the spot account and a futures account
        /// </summary>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">Amount to transfer</param>
        /// <param name="transferType">The transfer direction</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The transaction id</returns>
        public async Task<WebCallResult<BinanceTransaction>> TransferFuturesAccountAsync(string asset, decimal amount, FuturesTransferType transferType, long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceTransaction>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "asset", asset }, 
                { "amount", amount.ToString(CultureInfo.InvariantCulture) },
                { "type", JsonConvert.SerializeObject(transferType, new FuturesTransferTypeConverter(false)) },
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceTransaction>(_baseClient.GetUrlSpot(futuresTransferEndpoint, api, publicVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get future account transaction history

        /// <summary>
        /// Get history of transfers between spot and futures account
        /// </summary>
        /// <param name="asset">The asset to get history for</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">The page to return</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>History</returns>
        public WebCallResult<BinanceQueryRecords<BinanceSpotFuturesTransfer>> GetFuturesTransferHistory(string asset, DateTime startTime, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default) => GetFuturesTransferHistoryAsync(asset, startTime, endTime, page, limit, receiveWindow, ct).Result;

        /// <summary>
        /// Get history of transfers between spot and futures account
        /// </summary>
        /// <param name="asset">The asset to get history for</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">The page to return</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>History</returns>
        public async Task<WebCallResult<BinanceQueryRecords<BinanceSpotFuturesTransfer>>> GetFuturesTransferHistoryAsync(string asset, DateTime startTime, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceQueryRecords<BinanceSpotFuturesTransfer>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);
            
            var parameters = new Dictionary<string, object>
            {
                { "asset", asset },
                { "startTime", JsonConvert.SerializeObject(startTime, new TimestampConverter()) },
                { "timestamp", _baseClient.GetTimestamp() }
            };

            parameters.AddOptionalParameter("endTime", endTime.HasValue ? JsonConvert.SerializeObject(endTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("current", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceQueryRecords<BinanceSpotFuturesTransfer>>(_baseClient.GetUrlSpot(futuresTransferHistoryEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Borrow for cross-collateral

        /// <summary>
        /// Borrow for cross-collateral
        /// </summary>
        /// <param name="coin">The coin to borrow</param>
        /// <param name="amount">The amount to borrow</param>
        /// <param name="collateralCoin">The coin to use as collateral</param>
        /// <param name="collateralAmount">The amount of collateral coin to use</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Borrow result</returns>
        public WebCallResult<BinanceCrossCollateralBorrowResult> BorrowForCrossCollateral(string coin, string collateralCoin, decimal? amount = null, decimal? collateralAmount = null, long? receiveWindow = null, CancellationToken ct = default) => BorrowForCrossCollateralAsync(coin, collateralCoin, amount, collateralAmount, receiveWindow, ct).Result;

        /// <summary>
        /// Borrow for cross-collateral
        /// </summary>
        /// <param name="coin">The coin to borrow</param>
        /// <param name="amount">The amount to borrow</param>
        /// <param name="collateralCoin">The coin to use as collateral</param>
        /// <param name="collateralAmount">The amount of collateral coin to use</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Borrow result</returns>
        public async Task<WebCallResult<BinanceCrossCollateralBorrowResult>> BorrowForCrossCollateralAsync(string coin, string collateralCoin, decimal? amount = null, decimal? collateralAmount = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if(amount.HasValue == collateralAmount.HasValue)
                throw new ArgumentException("Either amount or collateralAmount should be specified", nameof(amount));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceCrossCollateralBorrowResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "coin", coin },
                { "collateralCoin", collateralCoin },
                { "timestamp", _baseClient.GetTimestamp() }
            };

            parameters.AddOptionalParameter("amount", amount?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("collateralAmount", collateralAmount?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceCrossCollateralBorrowResult>(_baseClient.GetUrlSpot(futuresBorrowEndpoint, api, publicVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get cross collateral borrow history

        /// <summary>
        /// Get cross collateral borrow history
        /// </summary>
        /// <param name="coin">The coin to get history for</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>History</returns>
        public WebCallResult<BinanceQueryRecords<BinanceCrossCollateralBorrowHistory>> GetCrossCollateralBorrowHistory(string? coin = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default) => GetCrossCollateralBorrowHistoryAsync(coin, startTime, endTime, limit, receiveWindow, ct).Result;

        /// <summary>
        /// Get cross collateral borrow history
        /// </summary>
        /// <param name="coin">The coin to get history for</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>History</returns>
        public async Task<WebCallResult<BinanceQueryRecords<BinanceCrossCollateralBorrowHistory>>> GetCrossCollateralBorrowHistoryAsync(string? coin = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceQueryRecords<BinanceCrossCollateralBorrowHistory>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };

            parameters.AddOptionalParameter("coin", coin);
            parameters.AddOptionalParameter("startTime", startTime.HasValue ? JsonConvert.SerializeObject(startTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endTime.HasValue ? JsonConvert.SerializeObject(endTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("size", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceQueryRecords<BinanceCrossCollateralBorrowHistory>>(_baseClient.GetUrlSpot(futuresBorrowHistoryEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Repay for cross-collateral

        /// <summary>
        /// Repay for cross-collateral
        /// </summary>
        /// <param name="coin">The coin</param>
        /// <param name="amount">The amount to repay</param>
        /// <param name="collateralCoin">The collateral coin to repay</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Repay result</returns>
        public WebCallResult<BinanceCrossCollateralRepayResult> RepayForCrossCollateral(string coin, string collateralCoin, decimal amount, long? receiveWindow = null, CancellationToken ct = default) => RepayForCrossCollateralAsync(coin, collateralCoin, amount, receiveWindow, ct).Result;

        /// <summary>
        /// Repay for cross-collateral
        /// </summary>
        /// <param name="coin">The coin</param>
        /// <param name="amount">The amount to repay</param>
        /// <param name="collateralCoin">The collateral coin to repay</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Repay result</returns>
        public async Task<WebCallResult<BinanceCrossCollateralRepayResult>> RepayForCrossCollateralAsync(string coin, string collateralCoin, decimal amount, long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceCrossCollateralRepayResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "coin", coin },
                { "collateralCoin", collateralCoin },
                { "amount", amount.ToString(CultureInfo.InvariantCulture) },
                { "timestamp", _baseClient.GetTimestamp() }
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceCrossCollateralRepayResult>(_baseClient.GetUrlSpot(futuresRepayEndpoint, api, publicVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get cross collateral repay history

        /// <summary>
        /// Get cross collateral repay history
        /// </summary>
        /// <param name="coin">The coin to get history for</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>History</returns>
        public WebCallResult<BinanceQueryRecords<BinanceCrossCollateralRepayHistory>> GetCrossCollateralRepayHistory(string? coin = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default) => GetCrossCollateralRepayHistoryAsync(coin, startTime, endTime, limit, receiveWindow, ct).Result;

        /// <summary>
        /// Get cross collateral borrow history
        /// </summary>
        /// <param name="coin">The coin to get history for</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>History</returns>
        public async Task<WebCallResult<BinanceQueryRecords<BinanceCrossCollateralRepayHistory>>> GetCrossCollateralRepayHistoryAsync(string? coin = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceQueryRecords<BinanceCrossCollateralRepayHistory>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };

            parameters.AddOptionalParameter("coin", coin);
            parameters.AddOptionalParameter("startTime", startTime.HasValue ? JsonConvert.SerializeObject(startTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endTime.HasValue ? JsonConvert.SerializeObject(endTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("size", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceQueryRecords<BinanceCrossCollateralRepayHistory>>(_baseClient.GetUrlSpot(futuresRepayHistoryEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Cross collateral wallet

        /// <summary>
        /// Get cross-collateral wallet info
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Wallet</returns>
        public WebCallResult<BinanceCrossCollateralWallet> GetCrossCollateralWallet(long? receiveWindow = null, CancellationToken ct = default) => GetCrossCollateralWalletAsync(receiveWindow, ct).Result;

        /// <summary>
        /// Get cross-collateral wallet info
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Wallet</returns>
        public async Task<WebCallResult<BinanceCrossCollateralWallet>> GetCrossCollateralWalletAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceCrossCollateralWallet>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceCrossCollateralWallet>(_baseClient.GetUrlSpot(futuresWalletEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Cross collateral information

        /// <summary>
        /// Get cross-collateral info
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Info</returns>
        public WebCallResult<IEnumerable<BinanceCrossCollateralInformation>> GetCrossCollateralInformation(long? receiveWindow = null, CancellationToken ct = default) => GetCrossCollateralInformationAsync(receiveWindow, ct).Result;

        /// <summary>
        /// Get cross-collateral info
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Info</returns>
        public async Task<WebCallResult<IEnumerable<BinanceCrossCollateralInformation>>> GetCrossCollateralInformationAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceCrossCollateralInformation>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceCrossCollateralInformation>>(_baseClient.GetUrlSpot(futuresInformationEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Calculate Rate After Adjust Cross-Collateral LTV

        /// <summary>
        /// Calculate rate after adjust cross-collateral loan to value
        /// </summary>
        /// <param name="collateralCoin">The collateral coin</param>
        /// <param name="amount">The amount</param>
        /// <param name="direction">The direction</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>After collateral rate</returns>
        public WebCallResult<BinanceCrossCollateralAfterAdjust> GetRateAfterAdjustLoanToValue(string collateralCoin, decimal amount, AdjustRateDirection direction, long? receiveWindow = null, CancellationToken ct = default) => GetRateAfterAdjustLoanToValueAsync(collateralCoin, amount, direction, receiveWindow, ct).Result;

        /// <summary>
        /// Calculate rate after adjust cross-collateral loan to value
        /// </summary>
        /// <param name="collateralCoin">The collateral coin</param>
        /// <param name="amount">The amount</param>
        /// <param name="direction">The direction</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>After collateral rate</returns>
        public async Task<WebCallResult<BinanceCrossCollateralAfterAdjust>> GetRateAfterAdjustLoanToValueAsync(string collateralCoin, decimal amount, AdjustRateDirection direction, long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceCrossCollateralAfterAdjust>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() },
                { "collateralCoin", collateralCoin },
                { "amount", amount.ToString(CultureInfo.InvariantCulture) },
                { "direction", JsonConvert.SerializeObject(direction, new AdjustRateDirectionConverter(false)) },
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceCrossCollateralAfterAdjust>(_baseClient.GetUrlSpot(futuresCalculateAdjustLevelEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Max Amount for Adjust Cross-Collateral LTV

        /// <summary>
        /// Get max amount for adjust cross-collateral LTV
        /// </summary>
        /// <param name="collateralCoin">The collateral coin</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Max amounts</returns>
        public WebCallResult<BinanceCrossCollateralAdjustMaxAmounts> GetMaxAmountForAdjustCrossCollateralLoanToValue(string collateralCoin, long? receiveWindow = null, CancellationToken ct = default) => GetMaxAmountForAdjustCrossCollateralLoanToValueAsync(collateralCoin, receiveWindow, ct).Result;

        /// <summary>
        /// Get max amount for adjust cross-collateral LTV
        /// </summary>
        /// <param name="collateralCoin">The collateral coin</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Max amounts</returns>
        public async Task<WebCallResult<BinanceCrossCollateralAdjustMaxAmounts>> GetMaxAmountForAdjustCrossCollateralLoanToValueAsync(string collateralCoin, long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceCrossCollateralAdjustMaxAmounts>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() },
                { "collateralCoin", collateralCoin },
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceCrossCollateralAdjustMaxAmounts>(_baseClient.GetUrlSpot(futuresCalculateMaxAdjustAmountEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Adjust cross collateral LTV

        /// <summary>
        /// Adjust cross collateral LTV
        /// </summary>
        /// <param name="collateralCoin">The collateral coin</param>
        /// <param name="amount">The amount</param>
        /// <param name="direction">The direction</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Adjust result</returns>
        public WebCallResult<BinanceCrossCollateralAdjustLtvResult> AdjustCrossCollateralLoanToValue(string collateralCoin, decimal amount, AdjustRateDirection direction, long? receiveWindow = null, CancellationToken ct = default) => AdjustCrossCollateralLoanToValueAsync(collateralCoin, amount, direction, receiveWindow, ct).Result;

        /// <summary>
        /// Adjust cross collateral LTV
        /// </summary>
        /// <param name="collateralCoin">The collateral coin</param>
        /// <param name="amount">The amount</param>
        /// <param name="direction">The direction</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Adjust result</returns>
        public async Task<WebCallResult<BinanceCrossCollateralAdjustLtvResult>> AdjustCrossCollateralLoanToValueAsync(string collateralCoin, decimal amount, AdjustRateDirection direction, long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceCrossCollateralAdjustLtvResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() },
                { "collateralCoin", collateralCoin },
                { "amount", amount.ToString(CultureInfo.InvariantCulture) },
                { "direction", JsonConvert.SerializeObject(direction, new AdjustRateDirectionConverter(false)) },
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceCrossCollateralAdjustLtvResult>(_baseClient.GetUrlSpot(futuresAdjustCrossCollateralEndpoint, api, publicVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Adjust Cross-Collateral LTV History

        /// <summary>
        /// Get cross collateral LTV adjustment history
        /// </summary>
        /// <param name="collateralCoin">The collateral coin</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Adjustment history</returns>
        public WebCallResult<BinanceQueryRecords<BinanceCrossCollateralAdjustLtvHistory>> GetAdjustCrossCollateralLoanToValueHistory(string collateralCoin, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default) => GetAdjustCrossCollateralLoanToValueHistoryAsync(collateralCoin, startTime, endTime, limit, receiveWindow, ct).Result;

        /// <summary>
        /// Get cross collateral LTV adjustment history
        /// </summary>
        /// <param name="collateralCoin">The collateral coin</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Adjustment history</returns>
        public async Task<WebCallResult<BinanceQueryRecords<BinanceCrossCollateralAdjustLtvHistory>>> GetAdjustCrossCollateralLoanToValueHistoryAsync(string collateralCoin, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceQueryRecords<BinanceCrossCollateralAdjustLtvHistory>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() },
                { "collateralCoin", collateralCoin }
            };

            parameters.AddOptionalParameter("startTime", startTime.HasValue ? JsonConvert.SerializeObject(startTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endTime.HasValue ? JsonConvert.SerializeObject(endTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("size", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceQueryRecords<BinanceCrossCollateralAdjustLtvHistory>>(_baseClient.GetUrlSpot(futuresAdjustCrossCollateralHistoryEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Cross-Collateral Liquidation History

        /// <summary>
        /// Get cross collateral liquidation history
        /// </summary>
        /// <param name="collateralCoin">The collateral coin</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Liquidation history</returns>
        public WebCallResult<BinanceQueryRecords<BinanceCrossCollateralLiquidationHistory>> GetCrossCollateralLiquidationHistory(string? collateralCoin = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default) => GetCrossCollateralLiquidationHistoryAsync(collateralCoin, startTime, endTime, limit, receiveWindow, ct).Result;

        /// <summary>
        /// Get cross collateral liquidation history
        /// </summary>
        /// <param name="collateralCoin">The collateral coin</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Liquidation history</returns>
        public async Task<WebCallResult<BinanceQueryRecords<BinanceCrossCollateralLiquidationHistory>>> GetCrossCollateralLiquidationHistoryAsync(string? collateralCoin = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceQueryRecords<BinanceCrossCollateralLiquidationHistory>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };

            parameters.AddOptionalParameter("collateralCoin", collateralCoin);
            parameters.AddOptionalParameter("startTime", startTime.HasValue ? JsonConvert.SerializeObject(startTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endTime.HasValue ? JsonConvert.SerializeObject(endTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("size", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceQueryRecords<BinanceCrossCollateralLiquidationHistory>>(_baseClient.GetUrlSpot(futuresCrossCollateralLiquidationHistoryEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion
    }
}
