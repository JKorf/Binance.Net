using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Interfaces.SubClients.Futures;
using Binance.Net.Objects.Futures.FuturesData;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;

namespace Binance.Net.SubClients.Futures.Coin
{
    /// <summary>
    /// COIN-M futures order endpoints
    /// </summary>
    public class BinanceClientFuturesCoinOrder: BinanceClientFuturesOrder, IBinanceClientFuturesCoinOrder
    {

        private const string myFuturesTradesEndpoint = "userTrades";
        /// <summary>
        /// Api path
        /// </summary>
        protected override string Api { get; } = "dapi";

        internal BinanceClientFuturesCoinOrder(Log log, BinanceClient baseClient, BinanceClientFutures futuresClient) : base(log, baseClient, futuresClient) { }

        #region Account Trade List

        /// <summary>
        /// Gets all user trades for provided symbol
        /// </summary>
        /// <param name="symbol">Symbol to get trades for</param>
        /// <param name="pair">Symbol to get trades for</param>
        /// <param name="limit">The max number of results</param>
        /// <param name="fromId">TradeId to fetch from. Default gets most recent trades</param>
        /// <param name="startTime">Orders newer than this date will be retrieved</param>
        /// <param name="endTime">Orders older than this date will be retrieved</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of trades</returns>
        public async Task<WebCallResult<IEnumerable<BinanceFuturesCoinTrade>>> GetMyTradesAsync(string? symbol = null, string? pair = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? fromId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var timestampResult = await BaseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceFuturesCoinTrade>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", BaseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", startTime.HasValue ? JsonConvert.SerializeObject(startTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endTime.HasValue ? JsonConvert.SerializeObject(endTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? BaseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await BaseClient.SendRequestInternal<IEnumerable<BinanceFuturesCoinTrade>>(FuturesClient.GetUrl(myFuturesTradesEndpoint, Api, SignedVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion
    }
}
