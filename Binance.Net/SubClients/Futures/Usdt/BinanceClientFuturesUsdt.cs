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

namespace Binance.Net.SubClients.Futures.Usdt
{
    /// <summary>
    /// USDT-M futures endpoints
    /// </summary>
    public class BinanceClientFuturesUsdt : BinanceClientFutures, IBinanceClientFuturesUsdt
    {
        private const string positionInformationEndpoint = "positionRisk";
        /// <summary>
        /// Api path
        /// </summary>
        protected override string Api { get; } = "fapi";

        /// <summary>
        /// Futures market endpoints
        /// </summary>
        public IBinanceClientFuturesUsdtMarket Market { get; protected set; }
        
        /// <summary>
        /// Futures order endpoints
        /// </summary>
        public IBinanceClientFuturesUsdtOrder Order { get; protected set; }
        /// <summary>
        /// Futures account endpoints
        /// </summary>
        public IBinanceClientFuturesUsdtAccount Account { get; protected set; }

        /// <summary>
        /// System endpoints
        /// </summary>
        public override IBinanceClientFuturesSystem System { get; protected set; }
        /// <summary>
        /// User stream endpoints
        /// </summary>
        public override IBinanceClientUserStream UserStream { get; protected set; }

        internal BinanceClientFuturesUsdt(Log log, BinanceClient baseClient) : base(log, baseClient)
        {
            System = new BinanceClientFuturesUsdtSystem(log, baseClient, this);
            Account = new BinanceClientFuturesUsdtAccount(baseClient, this);
            Market = new BinanceClientFuturesUsdtMarket(BaseClient, this);
            Order = new BinanceClientFuturesUsdtOrder(log, BaseClient, this);
            UserStream = new BinanceClientFuturesUsdtUserStream(baseClient, this);
        }

        #region Position Information

        /// <summary>
        /// Gets account information
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of Positions</returns>
        public WebCallResult<IEnumerable<BinanceFuturesPosition>> GetPositionInformation(string? symbol = null, long? receiveWindow = null, CancellationToken ct = default) => GetPositionInformationAsync(symbol, receiveWindow, ct).Result;

        /// <summary>
        /// Gets account information
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of Positions</returns>
        public async Task<WebCallResult<IEnumerable<BinanceFuturesPosition>>> GetPositionInformationAsync(string? symbol = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await BaseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceFuturesPosition>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", BaseClient.GetTimestamp() }
            };

            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? BaseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await BaseClient.SendRequestInternal<IEnumerable<BinanceFuturesPosition>>(GetUrl(positionInformationEndpoint, Api, "2"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        internal override Uri GetUrl(string endpoint, string api, string? version = null)
            => BaseClient.GetUrlUsdtFutures(endpoint, api, version);
    }
}
