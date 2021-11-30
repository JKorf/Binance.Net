using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Objects;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Interfaces.Clients.Rest.Spot;
using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.Converters;
using Binance.Net.Interfaces.Clients.General;
using Binance.Net.Interfaces.Clients.Rest.UsdFutures;
using Binance.Net.Interfaces.Clients.Rest.CoinFutures;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Clients.Rest.UsdFutures;
using Binance.Net.Clients.Rest.CoinFutures;

namespace Binance.Net.Clients.Rest.Spot
{
    /// <inheritdoc cref="IBinanceClientSpot" />
    public class BinanceClient: RestClient, IBinanceClient
    {
        #region Subclients
        public IBinanceClientGeneral General { get; }
        public IBinanceClientSpotMarket SpotMarket { get; }
        public IBinanceClientUsdFuturesMarket UsdFuturesMarket { get; }
        public IBinanceClientCoinFuturesMarket CoinFuturesMarket { get; }
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
            General = new BinanceClientGeneral(this, options);
            SpotMarket = new BinanceClientSpotMarket(log, this, options);
            UsdFuturesMarket = new BinanceClientUsdFuturesMarket(log, this, options);
            CoinFuturesMarket = new BinanceClientCoinFuturesMarket(log, this, options);
        }
        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="options"></param>
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

        internal Task<WebCallResult<T>> SendRequestInternal<T>(RestSubClient subClient, Uri uri, HttpMethod method, CancellationToken cancellationToken,
            Dictionary<string, object>? parameters = null, bool signed = false, HttpMethodParameterPosition? postPosition = null, 
            ArrayParametersSerialization? arraySerialization = null, int weight = 1) where T : class
        {
            return base.SendRequestAsync<T>(subClient, uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, requestWeight: weight);
        }

        public override void Dispose()
        {
            General.Dispose();
            SpotMarket.Dispose();
            UsdFuturesMarket.Dispose();
            CoinFuturesMarket.Dispose();
            base.Dispose();
        }
    }
}
