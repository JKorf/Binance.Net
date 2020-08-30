using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Interfaces.SubClients;
using Binance.Net.Interfaces.SubClients.Futures;
using Binance.Net.Objects.Futures.FuturesData;
using CryptoExchange.Net;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;

namespace Binance.Net.SubClients.Futures.Coin
{
    /// <summary>
    /// Coin-M futures endpoints
    /// </summary>
    public class BinanceClientFuturesCoin: BinanceClientFutures, IBinanceClientFuturesCoin
    {
        private const string positionInformationEndpoint = "positionRisk";
        /// <summary>
        /// Api path
        /// </summary>
        protected override string Api { get; } = "dapi";

        /// <summary>
        /// Futures market endpoints
        /// </summary>
        public IBinanceClientFuturesCoinMarket Market { get; protected set; }
        /// <summary>
        /// Futures order endpoints
        /// </summary>
        public IBinanceClientFuturesCoinOrder Order { get; protected set; }
        /// <summary>
        /// Futures account endpoints
        /// </summary>
        public IBinanceClientFuturesCoinAccount Account { get; protected set; }
        /// <summary>
        /// Coin futures system endpoints
        /// </summary>
        public override IBinanceClientFuturesSystem System { get; protected set; }
        /// <summary>
        /// Coin futures user stream endpoints
        /// </summary>
        public override IBinanceClientUserStream UserStream { get; protected set; }

        internal BinanceClientFuturesCoin(Log log, BinanceClient baseClient) : base(log, baseClient)
        {
            System = new BinanceClientFuturesCoinSystem(log, baseClient, this);
            Account = new BinanceClientFuturesCoinAccount(baseClient, this);
            Market = new BinanceClientFuturesCoinMarket(BaseClient, this);
            Order = new BinanceClientFuturesCoinOrder(log, BaseClient, this);
            UserStream = new BinanceClientFuturesCoinUserStream(baseClient, this);
        }

        #region Position Information

        /// <summary>
        /// Gets account position information
        /// </summary>
        /// <param name="marginAsset">Filter by margin asset</param>
        /// <param name="pair">Filter by pair</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of Positions</returns>
        public WebCallResult<IEnumerable<BinanceFuturesPosition>> GetPositionInformation(string? marginAsset = null, string? pair = null, long? receiveWindow = null, CancellationToken ct = default) => GetPositionInformationAsync(marginAsset, pair, receiveWindow, ct).Result;

        /// <summary>
        /// Gets account position information
        /// </summary>
        /// <param name="marginAsset">Filter by margin asset</param>
        /// <param name="pair">Filter by pair</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of Positions</returns>
        public async Task<WebCallResult<IEnumerable<BinanceFuturesPosition>>> GetPositionInformationAsync(string? marginAsset = null, string? pair = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await BaseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceFuturesPosition>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", BaseClient.GetTimestamp() }
            };

            parameters.AddOptionalParameter("marginAsset", marginAsset);
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? BaseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await BaseClient.SendRequestInternal<IEnumerable<BinanceFuturesPosition>>(GetUrl(positionInformationEndpoint, Api, "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        internal override Uri GetUrl(string endpoint, string api, string? version = null)
            => BaseClient.GetUrlCoinFutures(endpoint, api, version);
    }
}
