using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Interfaces.SubClients.Futures;
using Binance.Net.Objects.Futures.MarketData;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;

namespace Binance.Net.SubClients.Futures.Usdt
{
    /// <summary>
    /// USDT-M futures system endpoints
    /// </summary>
    public class BinanceClientFuturesUsdtSystem : BinanceClientFuturesSystem, IBinanceClientFuturesUsdtSystem
    {
        private const string exchangeInfoEndpoint = "exchangeInfo";

        /// <summary>
        /// Api path
        /// </summary>
        protected override string Api { get; } = "fapi";

        private readonly BinanceClientFuturesUsdt _futuresClient;

        internal BinanceClientFuturesUsdtSystem(Log log, BinanceClient baseClient, BinanceClientFuturesUsdt futuresClient) :
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
        public WebCallResult<BinanceFuturesUsdtExchangeInfo> GetExchangeInfo(CancellationToken ct = default) => GetExchangeInfoAsync(ct).Result;

        /// <summary>
        /// Get's information about the exchange including rate limits and symbol list
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        public async Task<WebCallResult<BinanceFuturesUsdtExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default)
        {
            var exchangeInfoResult = await _baseClient.SendRequestInternal<BinanceFuturesUsdtExchangeInfo>(_futuresClient.GetUrl(exchangeInfoEndpoint, Api, publicVersion), HttpMethod.Get, ct).ConfigureAwait(false);
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
