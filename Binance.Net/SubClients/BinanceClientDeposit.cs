using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces.SubClients;
using Binance.Net.Objects.Spot.WalletData;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;

namespace Binance.Net.SubClients
{
    /// <summary>
    /// Deposit endpoints
    /// </summary>
    public class BinanceClientDeposit: BinanceClientWithdrawDeposit, IBinanceClientDeposit
    {
        // Withdrawing
        private const string depositHistoryEndpoint = "capital/deposit/hisrec";
        private const string depositAddressEndpoint = "capital/deposit/address";

        private readonly BinanceClient _baseClient;

        internal BinanceClientDeposit(BinanceClient baseClient): base(baseClient)
        {
            _baseClient = baseClient;
        }
        
        /// <summary>
        /// Gets the deposit history
        /// </summary>
        /// <param name="coin">Filter by asset</param>
        /// <param name="status">Filter by status</param>
        /// <param name="limit">Amount of results</param>
        /// <param name="offset">Offset the results</param>
        /// <param name="startTime">Filter start time from</param>
        /// <param name="endTime">Filter end time till</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of deposits</returns>
        public WebCallResult<IEnumerable<BinanceDeposit>> GetDepositHistory(string? coin = null, DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default) => GetDepositHistoryAsync(coin, status, startTime, endTime, offset, limit, receiveWindow, ct).Result;

        /// <summary>
        /// Gets the deposit history
        /// </summary>
        /// <param name="coin">Filter by asset</param>
        /// <param name="status">Filter by status</param>
        /// <param name="limit">Amount of results</param>
        /// <param name="offset">Offset the results</param>
        /// <param name="startTime">Filter start time from</param>
        /// <param name="endTime">Filter end time till</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of deposits</returns>
        public async Task<WebCallResult<IEnumerable<BinanceDeposit>>> GetDepositHistoryAsync(string? coin = null, DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceDeposit>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("coin", coin);
            parameters.AddOptionalParameter("offset", offset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("status", status != null ? JsonConvert.SerializeObject(status, new DepositStatusConverter(false)) : null);
            parameters.AddOptionalParameter("startTime", startTime != null ? BinanceClient.ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? BinanceClient.ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceDeposit>>(
                    _baseClient.GetUrl(false, depositHistoryEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true)
                .ConfigureAwait(false);
        }
        
        /// <summary>
        /// Gets the deposit address for an asset
        /// </summary>
        /// <param name="coin">Asset to get address for</param>
        /// <param name="network">Network</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Deposit address</returns>
        public WebCallResult<BinanceDepositAddress> GetDepositAddress(string coin, string? network = null, int? receiveWindow = null, CancellationToken ct = default) => GetDepositAddressAsync(coin, network, receiveWindow, ct).Result;

        /// <summary>
        /// Gets the deposit address for an asset
        /// </summary>
        /// <param name="coin">Asset to get address for</param>
        /// <param name="network">Network</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Deposit address</returns>
        public async Task<WebCallResult<BinanceDepositAddress>> GetDepositAddressAsync(string coin, string? network = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            coin.ValidateNotNull(nameof(coin));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceDepositAddress>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "coin", coin },
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("network", network);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceDepositAddress>(_baseClient.GetUrl(false, depositAddressEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
    }
}
