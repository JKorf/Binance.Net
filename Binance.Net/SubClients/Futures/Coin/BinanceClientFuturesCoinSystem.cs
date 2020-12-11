using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Interfaces.SubClients.Futures;
using Binance.Net.Objects.Futures.MarketData;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;

namespace Binance.Net.SubClients.Futures.Coin
{
    /// <summary>
    /// COIN-M futures system endpoints
    /// </summary>
    public class BinanceClientFuturesCoinSystem : BinanceClientFuturesSystem, IBinanceClientFuturesCoinSystem
    {
        private const string exchangeInfoEndpoint = "exchangeInfo";

        /// <summary>
        /// Api path
        /// </summary>
        protected override string Api { get; } = "dapi";

        private readonly BinanceClientFuturesCoin _futuresClient;

        internal BinanceClientFuturesCoinSystem(Log log, BinanceClient baseClient, BinanceClientFuturesCoin futuresClient) :
            base(log, baseClient, futuresClient)
        {
            _futuresClient = futuresClient;
        }

        #region Exchange Information

        /// <summary>
        /// Get's information about the exchange including rate limits and symbol list
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        public WebCallResult<BinanceFuturesCoinExchangeInfo> GetExchangeInfo(CancellationToken ct = default) => GetExchangeInfoAsync(ct).Result;

        /// <summary>
        /// Get's information about the exchange including rate limits and symbol list
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        public async Task<WebCallResult<BinanceFuturesCoinExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default)
        {
            var exchangeInfoResult = await _baseClient.SendRequestInternal<BinanceFuturesCoinExchangeInfo>(_futuresClient.GetUrl(exchangeInfoEndpoint, Api, publicVersion), HttpMethod.Get, ct).ConfigureAwait(false);
            if (!exchangeInfoResult)
                return exchangeInfoResult;

            _futuresClient.ExchangeInfo = exchangeInfoResult.Data;
            _futuresClient.LastExchangeInfoUpdate = DateTime.UtcNow;
            _log.Write(LogVerbosity.Info, "Trade rules updated");
            return exchangeInfoResult;
        }

        #endregion
    }
}
