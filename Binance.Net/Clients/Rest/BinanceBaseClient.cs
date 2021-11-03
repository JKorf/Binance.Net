using Binance.Net.Objects;
using CryptoExchange.Net;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Objects;
using System;
namespace Binance.Net.Clients.Rest
{
    /// <summary>
    /// Client for the Binance API
    /// </summary>
    public abstract class BinanceBaseClient: RestClient
    {
        #region fields 
        internal readonly bool AutoTimestamp;
        internal readonly TimeSpan AutoTimestampRecalculationInterval;
        internal readonly TimeSpan TimestampOffset;
        internal readonly TimeSpan TradeRulesUpdateInterval;
        internal readonly TimeSpan DefaultReceiveWindow;
        #endregion

        /// <summary>
        /// Event triggered when an order is placed via this client. Only available for Spot orders
        /// </summary>
        public event Action<ICommonOrderId>? OnOrderPlaced;
        /// <summary>
        /// Event triggered when an order is canceled via this client. Note that this does not trigger when using CancelAllOrdersAsync. Only available for Spot orders
        /// </summary>
        public event Action<ICommonOrderId>? OnOrderCanceled;

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of BinanceBaseClient using provided options
        /// </summary>
        /// <param name="name">The name of the exchange this client is for</param>
        /// <param name="options">The options to use for this client</param>
        /// <param name="authenticationProvider">The authentication provider for this client (can be null if no credentials are provided)</param>
        internal BinanceBaseClient(string name, BinanceClientOptionsBase options,  BinanceAuthenticationProvider? authenticationProvider): base(name, options, authenticationProvider)
        {
            AutoTimestamp = options.AutoTimestamp;
            arraySerialization = ArrayParametersSerialization.MultipleValues;
            requestBodyFormat = RequestBodyFormat.FormData;
            requestBodyEmptyContent = string.Empty;

            TradeRulesUpdateInterval = options.TradeRulesUpdateInterval;
            AutoTimestampRecalculationInterval = options.AutoTimestampRecalculationInterval;
            TimestampOffset = options.TimestampOffset;
            DefaultReceiveWindow = options.ReceiveWindow;
        }
        #endregion

        #region helpers

        internal static long ToUnixTimestamp(DateTime time)
        {
            return (long)(time - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        internal void InvokeOrderPlaced(ICommonOrderId id)
        {
            OnOrderPlaced?.Invoke(id);
        }

        internal void InvokeOrderCanceled(ICommonOrderId id)
        {
            OnOrderCanceled?.Invoke(id);
        }

        #endregion
    }
}
