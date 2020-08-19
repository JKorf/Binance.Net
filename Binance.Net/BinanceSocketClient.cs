﻿using System;
using System.Threading.Tasks;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.SocketSubClient;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Newtonsoft.Json.Linq;
using Binance.Net.Objects.Spot;
using Binance.Net.SocketSubClients;

namespace Binance.Net
{
    /// <summary>
    /// Client providing access to the Binance websocket Api
    /// </summary>
    public class BinanceSocketClient : SocketClient, IBinanceSocketClient
    {
        #region fields
        private static BinanceSocketClientOptions _defaultOptions = new BinanceSocketClientOptions();
        private static BinanceSocketClientOptions DefaultOptions => _defaultOptions.Copy();
        
        /// <summary>
        /// Spot subscriptions
        /// </summary>
        public IBinanceSocketClientSpot Spot { get; set; }
        /// <summary>
        /// Futures subscriptions
        /// </summary>
        public IBinanceSocketClientFutures Futures { get; set; }
        #endregion

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of BinanceSocketClient with default options
        /// </summary>
        public BinanceSocketClient() : this(DefaultOptions)
        {
        }

        /// <summary>
        /// Create a new instance of BinanceSocketClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public BinanceSocketClient(BinanceSocketClientOptions options) : base(options, options.ApiCredentials == null ? null : new BinanceAuthenticationProvider(options.ApiCredentials))
        {
            Spot = new BinanceSocketClientSpot(log, this, options);
            Futures = new BinanceSocketClientFutures(log, this, options);
        }
        #endregion 

        #region methods

        /// <summary>
        /// Set the default options to be used when creating new socket clients
        /// </summary>
        /// <param name="options"></param>
        public static void SetDefaultOptions(BinanceSocketClientOptions options)
        {
            _defaultOptions = options;
        }

        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        public void SetApiCredentials(string apiKey, string apiSecret)
        {
            SetAuthenticationProvider(new BinanceAuthenticationProvider(new ApiCredentials(apiKey, apiSecret)));
        }

        internal CallResult<T> DeserializeInternal<T>(JToken data, bool checkObject = true)
        {
            return Deserialize<T>(data, checkObject);   
        }

        internal Task<CallResult<UpdateSubscription>> SubscribeInternal<T>(string url, Action<T> onData)
        {
            return Subscribe(url, null, url + NextId(), false, onData);
        }


        /// <inheritdoc />
        protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T>? callResult)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object>? callResult)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(JToken message, object request)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(JToken message, string identifier)
        {
            return true;
        }

        /// <inheritdoc />
        protected override Task<CallResult<bool>> AuthenticateSocket(SocketConnection s)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override Task<bool> Unsubscribe(SocketConnection connection, SocketSubscription s)
        {
            return Task.FromResult(true);
        }
        #endregion
    }
}
