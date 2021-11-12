using Binance.Net.Enums;
using Binance.Net.Objects;
using CryptoExchange.Net;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Objects;
using System;
using System.Globalization;

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

        protected static KlineInterval GetKlineIntervalFromTimespan(TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.FromMinutes(1)) return KlineInterval.OneMinute;
            if (timeSpan == TimeSpan.FromMinutes(3)) return KlineInterval.ThreeMinutes;
            if (timeSpan == TimeSpan.FromMinutes(5)) return KlineInterval.FiveMinutes;
            if (timeSpan == TimeSpan.FromMinutes(15)) return KlineInterval.FifteenMinutes;
            if (timeSpan == TimeSpan.FromMinutes(30)) return KlineInterval.ThirtyMinutes;
            if (timeSpan == TimeSpan.FromHours(1)) return KlineInterval.OneHour;
            if (timeSpan == TimeSpan.FromHours(2)) return KlineInterval.TwoHour;
            if (timeSpan == TimeSpan.FromHours(4)) return KlineInterval.FourHour;
            if (timeSpan == TimeSpan.FromHours(6)) return KlineInterval.SixHour;
            if (timeSpan == TimeSpan.FromHours(8)) return KlineInterval.EightHour;
            if (timeSpan == TimeSpan.FromHours(12)) return KlineInterval.TwelveHour;
            if (timeSpan == TimeSpan.FromDays(1)) return KlineInterval.OneDay;
            if (timeSpan == TimeSpan.FromDays(3)) return KlineInterval.ThreeDay;
            if (timeSpan == TimeSpan.FromDays(7)) return KlineInterval.OneWeek;
            if (timeSpan == TimeSpan.FromDays(30) || timeSpan == TimeSpan.FromDays(31)) return KlineInterval.OneMonth;

            throw new ArgumentException("Unsupported timespan for Binance Klines, check supported intervals using Binance.Net.Enums.KlineInterval");
        }

        /// <inheritdoc />
        public string GetSymbolName(string baseAsset, string quoteAsset) =>
            (baseAsset + quoteAsset).ToUpper(CultureInfo.InvariantCulture);

        protected static OrderSide GetOrderSide(IExchangeClient.OrderSide side)
        {
            if (side == IExchangeClient.OrderSide.Sell) return OrderSide.Sell;
            if (side == IExchangeClient.OrderSide.Buy) return OrderSide.Buy;

            throw new ArgumentException("Unsupported order side for Binance order: " + side);
        }

        protected static OrderType GetOrderType(IExchangeClient.OrderType type)
        {
            if (type == IExchangeClient.OrderType.Limit) return OrderType.Limit;
            if (type == IExchangeClient.OrderType.Market) return OrderType.Market;

            throw new ArgumentException("Unsupported order type for Binance order: " + type);
        }

        #endregion
    }
}
