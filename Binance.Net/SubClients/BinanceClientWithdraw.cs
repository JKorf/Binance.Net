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
    /// Withdraw endpoints
    /// </summary>
    public class BinanceClientWithdraw: BinanceClientWithdrawDeposit, IBinanceClientWithdraw
    {
        // Withdrawing
        private const string withdrawEndpoint = "withdraw.html";
        private const string withdrawHistoryEndpoint = "withdrawHistory.html";

        private readonly BinanceClient _baseClient;

        internal BinanceClientWithdraw(BinanceClient baseClient): base(baseClient)
        {
            _baseClient = baseClient;
        }

        #region Withdraw

        /// <summary>
        /// Withdraw assets from Binance to an address
        /// </summary>
        /// <param name="asset">The asset to withdraw</param>
        /// <param name="address">The address to send the funds to</param>
        /// <param name="addressTag">Secondary address identifier for coins like XRP,XMR etc.</param>
        /// <param name="amount">The amount to withdraw</param>
        /// <param name="withdrawOrderId">Custom client order id</param>
        /// <param name="network">The network to use</param>
        /// <param name="name">Description of the address</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Withdrawal confirmation</returns>
        public WebCallResult<BinanceWithdrawalPlaced> Withdraw(string asset, string address, decimal amount, string? withdrawOrderId = null, string? network = null, string? addressTag = null, string? name = null, int? receiveWindow = null, CancellationToken ct = default) => WithdrawAsync(asset, address, amount, withdrawOrderId, network, addressTag, name, receiveWindow, ct).Result;

        /// <summary>
        /// Withdraw assets from Binance to an address
        /// </summary>
        /// <param name="asset">The asset to withdraw</param>
        /// <param name="address">The address to send the funds to</param>
        /// <param name="addressTag">Secondary address identifier for coins like XRP,XMR etc.</param>
        /// <param name="withdrawOrderId">Custom client order id</param>
        /// <param name="amount">The amount to withdraw</param>
        /// <param name="network">The network to use</param>
        /// <param name="name">Description of the address</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Withdrawal confirmation</returns>
        public async Task<WebCallResult<BinanceWithdrawalPlaced>> WithdrawAsync(string asset, string address, decimal amount, string? withdrawOrderId = null, string? network = null, string? addressTag = null, string? name = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            address.ValidateNotNull(nameof(address));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceWithdrawalPlaced>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "asset", asset },
                { "address", address },
                { "amount", amount.ToString(CultureInfo.InvariantCulture) },
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("name", name);
            parameters.AddOptionalParameter("withdrawOrderId", withdrawOrderId);
            parameters.AddOptionalParameter("network", network);
            parameters.AddOptionalParameter("addressTag", addressTag);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<BinanceWithdrawalPlaced>(_baseClient.GetUrl(false, withdrawEndpoint, "wapi", "3"), HttpMethod.Post, ct, parameters, true, true, PostParameters.InUri).ConfigureAwait(false);
            if (!result || result.Data == null)
                return result;

            if (!result.Data.Success)
                return new WebCallResult<BinanceWithdrawalPlaced>(result.ResponseStatusCode, result.ResponseHeaders, null, _baseClient.ParseErrorResponseInternal(result.Data.Message));

            return result;
        }

        #endregion
        
        #region Withdraw History

        /// <summary>
        /// Gets the withdrawal history
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter start time from</param>
        /// <param name="endTime">Filter end time till</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of withdrawals</returns>
        public WebCallResult<IEnumerable<BinanceWithdrawal>> GetWithdrawalHistory(string? asset = null, WithdrawalStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? receiveWindow = null, CancellationToken ct = default) => GetWithdrawalHistoryAsync(asset, status, startTime, endTime, receiveWindow, ct).Result;

        /// <summary>
        /// Gets the withdrawal history
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter start time from</param>
        /// <param name="endTime">Filter end time till</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of withdrawals</returns>
        public async Task<WebCallResult<IEnumerable<BinanceWithdrawal>>> GetWithdrawalHistoryAsync(string? asset = null, WithdrawalStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceWithdrawal>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };

            parameters.AddOptionalParameter("asset", asset);
            parameters.AddOptionalParameter("status", status != null ? JsonConvert.SerializeObject(status, new WithdrawalStatusConverter(false)) : null);
            parameters.AddOptionalParameter("startTime", startTime != null ? BinanceClient.ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? BinanceClient.ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<BinanceWithdrawalList>(_baseClient.GetUrl(false, withdrawHistoryEndpoint, "wapi", "3"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result || result.Data == null)
                return WebCallResult<IEnumerable<BinanceWithdrawal>>.CreateErrorResult(result.Error ?? new UnknownError("Unknown response"));

            if (!result.Data.Success)
                return new WebCallResult<IEnumerable<BinanceWithdrawal>>(result.ResponseStatusCode, result.ResponseHeaders, null, _baseClient.ParseErrorResponseInternal(result.Data.Message));

            return new WebCallResult<IEnumerable<BinanceWithdrawal>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.List, null);
        }

        #endregion


    }
}
