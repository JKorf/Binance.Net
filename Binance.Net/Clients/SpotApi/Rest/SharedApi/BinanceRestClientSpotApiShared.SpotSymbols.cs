using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.Caching;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Binance.Net.Clients.SpotApi
{
    internal partial class BinanceRestClientSpotSharedApi
    {
        #region Get Spot Symbols

        public SharedSymbolCatalog? SpotSymbolCatalog => ExchangeSymbolCache.GetSymbolCatalog(_exchangeName, _topicId, _api.EnvironmentName, null);

        public GetSpotSymbolsOptions GetSpotSymbolsOptions { get; }
            = new GetSpotSymbolsOptions(_exchangeName, false);

        async Task<ICallResult<SharedSpotSymbol[]>> IGetSpotSymbols.GetSpotSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
            => await GetSpotSymbolsAsync(request, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedSpotSymbol[]>> GetSpotSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = GetSpotSymbolsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotSymbol[]>(Exchange, validationError);

            var exchangeInfoTask = _api.ExchangeData.GetExchangeInfoAsync(false, SymbolStatus.Trading, ct: ct);
            var productsTask = _api.ExchangeData.GetProductsAsync(ct: ct);
            await Task.WhenAll(exchangeInfoTask, productsTask).ConfigureAwait(false);
            var exchangeInfoResult = exchangeInfoTask.Result;
            var productsResult = productsTask.Result;
            if (!exchangeInfoResult.Success)
                return HttpResult.Fail<SharedSpotSymbol[]>(exchangeInfoResult);

            var data = exchangeInfoResult.Data.Symbols
                .Select(x => ParseSymbol(x, productsResult.Data)!)
                .Where(x => x != null)
                .ToArray();

            ExchangeSymbolCache.UpdateSymbolInfo(_topicId, _api.EnvironmentName, null, data);
            return HttpResult.Ok(exchangeInfoResult, SharedUtils.ApplySymbolFilter(data, request));
        }

        #endregion

        private SharedSpotSymbol ParseSymbol(BinanceSymbol symbol, BinanceProduct[]? products)
        {
            SharedAssetInfo MapAsset(string name)
            {
                var product = products?.FirstOrDefault(p => p.BaseAsset == name);
                if (product?.Tags.Contains("bStocks", StringComparer.InvariantCultureIgnoreCase) == true)
                    return new SharedAssetInfo(name, SharedAssetType.TradFi, SharedAssetSubType.Equity);

                if (product?.Tags.Contains("tCommodities", StringComparer.InvariantCultureIgnoreCase) == true)
                    return new SharedAssetInfo(name, SharedAssetType.TradFi, SharedAssetSubType.Commodity);

                // Stablecoins / Fiat
                if (LibraryHelpers.IsStableCoin(name))
                    return new SharedAssetInfo(name, SharedAssetType.Crypto, SharedAssetSubType.StableCoin);

                if (_exchangeSupportedFiatCurrencies.Contains(name))
                    return new SharedAssetInfo(name, SharedAssetType.Fiat, null);

                if (products == null)
                    return new SharedAssetInfo(name, SharedAssetType.Unspecified, null);

                return new SharedAssetInfo(name, SharedAssetType.Crypto, null);
            }

            var baseAssetInfo = MapAsset(symbol.BaseAsset);
            var quoteAssetInfo = MapAsset(symbol.QuoteAsset);
            return new SharedSpotSymbol(symbol.BaseAsset, symbol.QuoteAsset, symbol.Name, symbol.Status == SymbolStatus.Trading && symbol.IsSpotTradingAllowed)
            {
                MinTradeQuantity = symbol.LotSizeFilter?.MinQuantity,
                MaxTradeQuantity = symbol.LotSizeFilter?.MaxQuantity,
                MinNotionalValue = symbol.MinNotionalFilter?.MinNotional ?? symbol.NotionalFilter?.MinNotional,
                QuantityStep = symbol.LotSizeFilter?.StepSize,
                PriceStep = symbol.PriceFilter?.TickSize,
                DisplayName = symbol.Name,
                BaseAssetType = baseAssetInfo?.Type ?? SharedAssetType.Unspecified,
                BaseAssetSubType = baseAssetInfo?.SubType,
                QuoteAssetType = quoteAssetInfo?.Type ?? SharedAssetType.Unspecified,
                QuoteAssetSubType = quoteAssetInfo?.SubType
            };
        }

        public async Task<ExchangeCallResult<SharedSymbol[]>> GetSpotSymbolsForBaseAssetAsync(string baseAsset)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId, _api.EnvironmentName, null))
            {
                var symbols = await GetSpotSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<SharedSymbol[]>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<SharedSymbol[]>.Ok(Exchange, ExchangeSymbolCache.GetSymbolsForBaseAsset(_topicId, _api.EnvironmentName, null, baseAsset));
        }

        public async Task<ExchangeCallResult<bool>> SupportsSpotSymbolAsync(SharedSymbol symbol)
        {
            if (symbol.TradingMode != TradingMode.Spot)
                throw new ArgumentException(nameof(symbol), "Only Spot symbols allowed");

            if (!ExchangeSymbolCache.HasCached(_topicId, _api.EnvironmentName, null))
            {
                var symbols = await GetSpotSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, _api.EnvironmentName, null, symbol));
        }

        public async Task<ExchangeCallResult<bool>> SupportsSpotSymbolAsync(string symbolName)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId, _api.EnvironmentName, null))
            {
                var symbols = await GetSpotSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, _api.EnvironmentName, null, symbolName));
        }
    }
}
