using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Converters;
using Binance.Net.Enums;
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
    public class BinanceClientSpotFuturesInteraction
    {
        private readonly BinanceClient _baseClient;

        private const string futuresTransferEndpoint = "futures/transfer";
        private const string futuresTransferHistoryEndpoint = "futures/transfer";
        private const string futuresBorrowEndpoint = "futures/loan/borrow";
        private const string futuresBorrowHistoryEndpoint = "futures/loan/borrow/history";
        private const string futuresRepayEndpoint = "futures/loan/repay";
        private const string futuresRepayHistoryEndpoint = "futures/loan/repay/history";

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
                { "amount", amount },
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
    }
}
