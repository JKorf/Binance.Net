using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Interfaces.SubClients.Futures;
using Binance.Net.Objects.Futures.MarketData;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;

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

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesCoinExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default)
        {
            var exchangeInfoResult = await _baseClient.SendRequestInternal<BinanceFuturesCoinExchangeInfo>(_futuresClient.GetUrl(exchangeInfoEndpoint, Api, publicVersion), HttpMethod.Get, ct).ConfigureAwait(false);
            if (!exchangeInfoResult)
                return exchangeInfoResult;

            _futuresClient.ExchangeInfo = exchangeInfoResult.Data;
            _futuresClient.LastExchangeInfoUpdate = DateTime.UtcNow;
            _log.Write(LogLevel.Information, "Trade rules updated");
            return exchangeInfoResult;
        }

        #endregion
    }
}
