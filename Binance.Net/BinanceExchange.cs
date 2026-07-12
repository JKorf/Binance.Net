using Binance.Net.Clients;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Objects.Models.Futures;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.Caching;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.RateLimiting;
using CryptoExchange.Net.RateLimiting.Filters;
using CryptoExchange.Net.RateLimiting.Guards;
using CryptoExchange.Net.RateLimiting.Interfaces;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.SharedApis.Models;

namespace Binance.Net
{
    /// <summary>
    /// Binance exchange information and configuration
    /// </summary>
    public static class BinanceExchange
    {
        /// <summary>
        /// Platform metadata
        /// </summary>
        public static PlatformInfo Metadata { get; } = new PlatformInfo(
                "Binance",
                "Binance",
                "https://raw.githubusercontent.com/JKorf/Binance.Net/master/Binance.Net/Icon/icon.png",
                "https://www.binance.com",
                ["https://binance-docs.github.io/apidocs/spot/en/#change-log"],
                PlatformType.CryptoCurrencyExchange,
                CentralizationType.Centralized,
                BinanceEnvironment.All
                );

        /// <summary>
        /// Exchange name
        /// </summary>
        public static string ExchangeName => "Binance";

        /// <summary>
        /// Exchange name
        /// </summary>
        public static string DisplayName => "Binance";

        /// <summary>
        /// Url to exchange image
        /// </summary>
        public static string ImageUrl { get; } = "https://raw.githubusercontent.com/JKorf/Binance.Net/master/Binance.Net/Icon/icon.png";

        /// <summary>
        /// Url to the main website
        /// </summary>
        public static string Url { get; } = "https://www.binance.com";

        /// <summary>
        /// Urls to the API documentation
        /// </summary>
        public static string[] ApiDocsUrl { get; } = new[] {
            "https://binance-docs.github.io/apidocs/spot/en/#change-log"
            };

        /// <summary>
        /// Type of exchange
        /// </summary>
        public static ExchangeType Type { get; } = ExchangeType.CEX;

        internal static JsonSerializerContext _serializerContext = JsonSerializerContextCache.GetOrCreate<BinanceSourceGenerationContext>();
        internal static readonly ParameterSerializationSettings _socketParameterSignSettings = new ParameterSerializationSettings()
        {
            Sort = true
        };
        internal static readonly ParameterSerializationSettings _parameterSerializationSettings = new ParameterSerializationSettings()
        {
            Decimal = DecimalSerialization.String,
            Array = ArrayParametersSerialization.MultipleValues,
            Sort = false
        };


        /// <summary>
        /// Aliases for Binance assets
        /// </summary>
        public static AssetAliasConfiguration AssetAliases { get; } = new AssetAliasConfiguration
        {
            Aliases =
            [
                new AssetAlias("USDT", SharedSymbol.UsdOrStable.ToUpperInvariant(), AliasType.OnlyToExchange)
            ]
        };

        /// <summary>
        /// Format a base and quote asset to a Binance recognized symbol 
        /// </summary>
        /// <param name="baseAsset">Base asset</param>
        /// <param name="quoteAsset">Quote asset</param>
        /// <param name="tradingMode">Trading mode</param>
        /// <param name="deliverTime">Delivery time for delivery futures</param>
        /// <returns></returns>
        public static string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
        {
            baseAsset = AssetAliases.CommonToExchangeName(baseAsset.ToUpperInvariant());
            quoteAsset = AssetAliases.CommonToExchangeName(quoteAsset.ToUpperInvariant());

            if (tradingMode == TradingMode.Spot)
                return baseAsset + quoteAsset;

            if (tradingMode.IsLinear())
                return baseAsset + quoteAsset + (deliverTime == null ? string.Empty : "_" + deliverTime.Value.ToString("yyMMdd"));

            return baseAsset + quoteAsset + (deliverTime == null ? "_PERP" : "_" + deliverTime.Value.ToString("yyMMdd"));
        }

        /// <summary>
        /// Rate limiter configuration for the Binance API
        /// </summary>
        public static BinanceRateLimiters RateLimiter { get; set; } = new BinanceRateLimiters();

        private static HashSet<string> _exchangeSupportedFiatCurrencies = ["USD", "EUR"];

#warning these should be in the shared interfaces
        public static BinanceExchangeInfo? _spotExchangeInfo;
        public static BinanceFuturesUsdtExchangeInfo? _usdtFuturesExchangeInfo;
        public static BinanceProduct[]? _products;
        public static ExchangeCache Cache { get; } = new ExchangeCache(
            new CacheItemDefinition<SharedExchangeInfo>
            {
                Key = "Binance.Spot.live.ExchangeInfo",
                Ttl = TimeSpan.FromMinutes(5),
                ValueFactory = async () =>
                {
                    SharedAssetInfo MapAsset(string name, BinanceProduct[] products)
                    {
                        var product = products.FirstOrDefault(p => p.BaseAsset == name);
                        if (product?.Tags.Contains("bStocks", StringComparer.InvariantCultureIgnoreCase) == true)
                            return new SharedAssetInfo(name, SharedAssetType.Rwa, SharedAssetSubType.Stock);

                        if (product?.Tags.Contains("tCommodities", StringComparer.InvariantCultureIgnoreCase) == true)
                            return new SharedAssetInfo(name, SharedAssetType.Rwa, SharedAssetSubType.Commodity);

                        // Stablecoins / Fiat
                        if (LibraryHelpers.IsStableCoin(name))
                            return new SharedAssetInfo(name, SharedAssetType.Crypto, SharedAssetSubType.StableCoin);

                        if (_exchangeSupportedFiatCurrencies.Contains(name))
                            return new SharedAssetInfo(name, SharedAssetType.Fiat, null);

                        return new SharedAssetInfo(name, SharedAssetType.Crypto, null);
                    }

                    using var client = new BinanceRestClient();
                    var tasks = new List<Task>();
                    if (_spotExchangeInfo == null)
                        tasks.Add(client.SpotApi.ExchangeData.GetExchangeInfoAsync(false, Enums.SymbolStatus.Trading));
                    if (_products == null)
                        tasks.Add(client.SpotApi.ExchangeData.GetProductsAsync());
                    await Task.WhenAll(tasks).ConfigureAwait(false);

                    if (_spotExchangeInfo == null || _products == null)
                        throw new Exception($"Failed to retrieve exchange info");

                    var exchangeInfo = new SharedExchangeInfo();
                    foreach(var symbol in _spotExchangeInfo.Symbols)
                    {
                        if (!exchangeInfo.Assets.TryGetValue(symbol.BaseAsset, out var baseAssetInfo))
                        {
                            baseAssetInfo = MapAsset(symbol.BaseAsset, _products);
                            exchangeInfo.Assets.Add(symbol.BaseAsset, baseAssetInfo);
                        }
                        if (!exchangeInfo.Assets.TryGetValue(symbol.QuoteAsset, out var quoteAssetInfo))
                        {
                            quoteAssetInfo = MapAsset(symbol.QuoteAsset, _products);
                            exchangeInfo.Assets.Add(symbol.QuoteAsset, quoteAssetInfo);
                        }

                        exchangeInfo.Symbols.Add(symbol.Name, new SharedSymbolInfo(symbol.Name, baseAssetInfo, quoteAssetInfo));
                    }

                    return exchangeInfo;
                },
            },
            new CacheItemDefinition<SharedExchangeInfo>
            {
                Key = "Binance.UsdtFutures.live.ExchangeInfo",
                Ttl = TimeSpan.FromMinutes(5),
                ValueFactory = async () =>
                {
                    SharedAssetInfo MapAsset(BinanceFuturesUsdtSymbol symbol)
                    {
                        if (symbol.ContractType == ContractType.PerpetualTradFi)
                        {
                            if (symbol.UnderlyingType == UnderlyingType.Commodity)
                                return new SharedAssetInfo(symbol.BaseAsset, SharedAssetType.Rwa, SharedAssetSubType.Commodity);

                            if (symbol.UnderlyingType == UnderlyingType.Equity
                            || symbol.UnderlyingType == UnderlyingType.KrEquity
                            || symbol.UnderlyingType == UnderlyingType.PreMarket)
                                return new SharedAssetInfo(symbol.BaseAsset, SharedAssetType.Rwa, SharedAssetSubType.Stock);

                            return new SharedAssetInfo(symbol.BaseAsset, SharedAssetType.Rwa, null);
                        }

                        if (symbol.UnderlyingType == UnderlyingType.Coin || symbol.UnderlyingType == UnderlyingType.Index)
                            return new SharedAssetInfo(symbol.BaseAsset, SharedAssetType.Crypto, null);

                        return new SharedAssetInfo(symbol.BaseAsset, SharedAssetType.Unspecified, null);
                    }

                    using var client = new BinanceRestClient();
                    var tasks = new List<Task>();
                    if (_usdtFuturesExchangeInfo == null)
                        tasks.Add(client.UsdFuturesApi.ExchangeData.GetExchangeInfoAsync());
                    await Task.WhenAll(tasks).ConfigureAwait(false);

                    if (_usdtFuturesExchangeInfo == null)
                        throw new Exception($"Failed to retrieve exchange info");

                    var exchangeInfo = new SharedExchangeInfo();
                    foreach (var symbol in _usdtFuturesExchangeInfo.Symbols)
                    {
                        if (!exchangeInfo.Assets.TryGetValue(symbol.BaseAsset, out var baseAssetInfo))
                        {
                            baseAssetInfo = MapAsset(symbol);
                            exchangeInfo.Assets.Add(symbol.BaseAsset, baseAssetInfo);
                        }

                        if (!exchangeInfo.Assets.TryGetValue(symbol.QuoteAsset, out var quoteAssetInfo))
                        {
                            quoteAssetInfo = new SharedAssetInfo(symbol.QuoteAsset, SharedAssetType.Crypto, SharedAssetSubType.StableCoin);
                            exchangeInfo.Assets.Add(symbol.QuoteAsset, quoteAssetInfo);
                        }

                        exchangeInfo.Symbols.Add(symbol.Name, new SharedSymbolInfo(symbol.Name, baseAssetInfo, quoteAssetInfo));
                    }

                    return exchangeInfo;
                },
            }
        );
    }

    /// <summary>
    /// Rate limiter configuration for the Binance API
    /// </summary>
    public class BinanceRateLimiters
    {
        /// <summary>
        /// Event for when a rate limit is triggered
        /// </summary>
        public event Action<RateLimitEvent> RateLimitTriggered;
        /// <summary>
        /// Event when the rate limit is updated. Note that it's only updated when a request is send, so there are no specific updates when the current usage is decaying.
        /// </summary>
        public event Action<RateLimitUpdateEvent> RateLimitUpdated;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        /// <summary>
        /// ctor
        /// </summary>
        public BinanceRateLimiters()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Initialize();
        }

        /// <summary>
        /// Initialize the rate limits
        /// </summary>
        protected virtual void Initialize()
        {
            EndpointLimit = new RateLimitGate("Endpoint Limit");
            SpotRestIp = new RateLimitGate("Spot Rest")
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, new PathStartFilter("api/"), 6000, TimeSpan.FromMinutes(1), RateLimitWindowType.Fixed)) // IP limit of 6000 request weight per minute to /api endpoints
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerEndpoint, new PathStartFilter("sapi/"), 12000, TimeSpan.FromMinutes(1), RateLimitWindowType.Fixed)); // IP limit of 12000 request weight per endpoint per minute to /sapi endpoints
            SpotRestUid = new RateLimitGate("Spot Rest")
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, new PathStartFilter("api/"), 6000, TimeSpan.FromMinutes(1), RateLimitWindowType.Fixed)) // Uid limit of 6000 request weight per minute to /api endpoints
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerApiKeyPerEndpoint, new PathStartFilter("sapi/"), 180000, TimeSpan.FromMinutes(1), RateLimitWindowType.Fixed)); // Uid limit of 180000 request weight per minute to /sapi endpoints
            SpotSocket = new RateLimitGate("Spot Socket")
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, new IGuardFilter[] { new LimitItemTypeFilter(RateLimitItemType.Connection) }, 300, TimeSpan.FromMinutes(5), RateLimitWindowType.Fixed)) // 300 connections per 5 minutes per host
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerConnection, new IGuardFilter[] { new HostFilter("wss://stream.binance.com"), new LimitItemTypeFilter(RateLimitItemType.Request) }, 4, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding)) // 5 requests per second per path (connection)
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, new IGuardFilter[] { new HostFilter("wss://ws-api.binance.com") }, 6000, TimeSpan.FromMinutes(1), RateLimitWindowType.Fixed, connectionWeight: 2)); // 6000 request weight per minute in total
            FuturesRest = new RateLimitGate("Futures Rest")
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, [], 2400, TimeSpan.FromMinutes(1), RateLimitWindowType.Fixed)); // IP limit of 2400 request weight per minute to fapi/dapi endpoints
            FuturesSocket = new RateLimitGate("Futures Socket")
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerConnection, new IGuardFilter[] { new LimitItemTypeFilter(RateLimitItemType.Request), new HostFilter("wss://dstream.binance.com") }, 9, TimeSpan.FromSeconds(1), RateLimitWindowType.Fixed)) // 10 requests per second per path (connection)
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerConnection, new IGuardFilter[] { new LimitItemTypeFilter(RateLimitItemType.Request), new HostFilter("wss://fstream.binance.com") }, 9, TimeSpan.FromSeconds(1), RateLimitWindowType.Fixed)) // 10 requests per second per path (connection)
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, new IGuardFilter[] { new HostFilter("wss://ws-fapi.binance.com") }, 2400, TimeSpan.FromMinutes(1), RateLimitWindowType.Fixed, connectionWeight: 5));

            EndpointLimit.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            EndpointLimit.RateLimitUpdated += (x) => RateLimitUpdated?.Invoke(x);
            SpotRestIp.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            SpotRestIp.RateLimitUpdated += (x) => RateLimitUpdated?.Invoke(x);
            SpotRestUid.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            SpotRestUid.RateLimitUpdated += (x) => RateLimitUpdated?.Invoke(x);
            SpotSocket.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            SpotSocket.RateLimitUpdated += (x) => RateLimitUpdated?.Invoke(x);
            FuturesRest.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            FuturesRest.RateLimitUpdated += (x) => RateLimitUpdated?.Invoke(x);
            FuturesSocket.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            FuturesSocket.RateLimitUpdated += (x) => RateLimitUpdated?.Invoke(x);
        }

        internal IRateLimitGate EndpointLimit { get; private set; }

        internal IRateLimitGate SpotRestIp { get; private set; }

        internal IRateLimitGate SpotRestUid { get; private set; }

        internal IRateLimitGate SpotSocket { get; private set; }

        internal IRateLimitGate FuturesRest { get; private set; }

        internal IRateLimitGate FuturesSocket { get; private set; }

    }
}
