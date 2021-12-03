using Binance.Net.Objects;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Interfaces.Clients.GeneralApi;
using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using Binance.Net.Clients.GeneralApi;
using Binance.Net.Clients.SpotApi;
using Binance.Net.Clients.UsdFuturesApi;
using Binance.Net.Clients.CoinFuturesApi;

namespace Binance.Net.Clients
{
    /// <inheritdoc cref="IBinanceClient" />
    public class BinanceClient : BaseRestClient, IBinanceClient
    {
        #region Api clients

        /// <inheritdoc />
        public IBinanceClientGeneralApi GeneralApi { get; }
        /// <inheritdoc />
        public IBinanceClientSpotApi SpotApi { get; }
        /// <inheritdoc />
        public IBinanceClientUsdFuturesApi UsdFuturesApi { get; }
        /// <inheritdoc />
        public IBinanceClientCoinFuturesApi CoinFuturesApi { get; }

        #endregion

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of BinanceClient using the default options
        /// </summary>
        public BinanceClient() : this(BinanceClientOptions.Default)
        {
        }

        /// <summary>
        /// Create a new instance of BinanceClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public BinanceClient(BinanceClientOptions options) : base("Binance", options)
        {
            GeneralApi = new BinanceClientGeneralApi(this, options);
            SpotApi = new BinanceClientSpotApi(log, this, options);
            UsdFuturesApi = new BinanceClientUsdFuturesApi(log, this, options);
            CoinFuturesApi = new BinanceClientCoinFuturesApi(log, this, options);
        }
        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="options">Options to use as default</param>
        public static void SetDefaultOptions(BinanceClientOptions options)
        {
            BinanceClientOptions.Default = options;
        }

        /// <inheritdoc />
        protected override Error ParseErrorResponse(JToken error)
        {
            if (!error.HasValues)
                return new ServerError(error.ToString());

            if (error["msg"] == null && error["code"] == null)
                return new ServerError(error.ToString());

            if (error["msg"] != null && error["code"] == null)
                return new ServerError((string)error["msg"]!);

            return new ServerError((int)error["code"]!, (string)error["msg"]!);
        }

        internal Task<WebCallResult<T>> SendRequestInternal<T>(RestApiClient apiClient, Uri uri, HttpMethod method, CancellationToken cancellationToken,
            Dictionary<string, object>? parameters = null, bool signed = false, HttpMethodParameterPosition? postPosition = null,
            ArrayParametersSerialization? arraySerialization = null, int weight = 1) where T : class
        {
            return base.SendRequestAsync<T>(apiClient, uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, requestWeight: weight);
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            GeneralApi.Dispose();
            SpotApi.Dispose();
            UsdFuturesApi.Dispose();
            CoinFuturesApi.Dispose();
            base.Dispose();
        }
    }
}
