using Binance.Net.Clients.UsdFuturesApi;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using Binance.Net.Objects.Models.Futures;
using CryptoExchange.Net.Caching;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using System.Collections.Concurrent;

namespace Binance.Net.Clients.CoinFuturesApi
{
    internal partial class BinanceRestClientCoinFuturesSharedApi
    {
        #region Futures Symbol client
        public SharedSymbolCatalog? FuturesSymbolCatalog => ExchangeSymbolCache.GetSymbolCatalog(_exchangeName, _topicId, _api.EnvironmentName, null);

        public GetFuturesSymbolsOptions GetFuturesSymbolsOptions { get; } = new GetFuturesSymbolsOptions(_exchangeName, false);
        public async Task<HttpResult<SharedFuturesSymbol[]>> GetFuturesSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesSymbolsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesSymbol[]>(Exchange, validationError);

            var exchangeInfo = await _api.ExchangeData.GetExchangeInfoAsync(ct).ConfigureAwait(false);
            if (!exchangeInfo.Success)
                return HttpResult.Fail<SharedFuturesSymbol[]>(exchangeInfo);

            var data = exchangeInfo.Data.Symbols
                .Select(x => ParseSymbol(x)!)
                .Where(x => x != null)
                .ToArray();

            ExchangeSymbolCache.UpdateSymbolInfo(_topicId, _api.EnvironmentName, null, data);
            return HttpResult.Ok(exchangeInfo, SharedUtils.ApplySymbolFilter(data, request));
        }

        private SharedFuturesSymbol ParseSymbol(BinanceFuturesCoinSymbol s)
        {
            SharedAssetInfo MapAsset(BinanceFuturesCoinSymbol symbol)
            {
                if (symbol.ContractType == ContractType.PerpetualTradFi)
                {
                    if (symbol.UnderlyingType == UnderlyingType.Commodity)
                        return new SharedAssetInfo(symbol.BaseAsset, SharedAssetType.TradFi, SharedAssetSubType.Commodity);

                    if (symbol.UnderlyingType == UnderlyingType.Equity
                    || symbol.UnderlyingType == UnderlyingType.KrEquity
                    || symbol.UnderlyingType == UnderlyingType.HkEquity
                    || symbol.UnderlyingType == UnderlyingType.CnEquity
                    || symbol.UnderlyingType == UnderlyingType.PreMarket)
                    {
                        return new SharedAssetInfo(symbol.BaseAsset, SharedAssetType.TradFi, SharedAssetSubType.Equity);
                    }

                    return new SharedAssetInfo(symbol.BaseAsset, SharedAssetType.TradFi, null);
                }

                if (symbol.UnderlyingType == UnderlyingType.Coin || symbol.UnderlyingType == UnderlyingType.Index)
                {
                    if (LibraryHelpers.IsStableCoin(symbol.BaseAsset))
                        return new SharedAssetInfo(symbol.BaseAsset, SharedAssetType.Crypto, SharedAssetSubType.StableCoin);

                    return new SharedAssetInfo(symbol.BaseAsset, SharedAssetType.Crypto, null);
                }

                return new SharedAssetInfo(symbol.BaseAsset, SharedAssetType.Unspecified, null);
            }

            var baseAssetInfo = MapAsset(s);
            var isPerp = s.ContractType == ContractType.Perpetual || s.ContractType == ContractType.PerpetualTradFi || s.ContractType == ContractType.PerpetualDelivering;
            return new SharedFuturesSymbol(
                    isPerp ? TradingMode.PerpetualInverse : TradingMode.DeliveryInverse,
                    s.BaseAsset,
                    s.QuoteAsset,
                    s.Name,
                    s.Status == SymbolStatus.Trading)
            {
                MinTradeQuantity = s.LotSizeFilter?.MinQuantity,
                MaxTradeQuantity = s.LotSizeFilter?.MaxQuantity,
                QuantityStep = s.LotSizeFilter?.StepSize,
                PriceStep = s.PriceFilter?.TickSize,
                ContractSize = s.ContractSize,
                DeliveryTime = s.DeliveryDate.Year == 2100 ? null : s.DeliveryDate,
                DisplayName = s.Name,
                BaseAssetType = baseAssetInfo?.Type ?? SharedAssetType.Unspecified,
                BaseAssetSubType = baseAssetInfo?.SubType,
                QuoteAssetType = SharedAssetType.Fiat,
                QuoteAssetSubType = null,
            };
        }

        public async Task<ExchangeCallResult<SharedSymbol[]>> GetFuturesSymbolsForBaseAssetAsync(string baseAsset)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId, _api.EnvironmentName, null))
            {
                var symbols = await GetFuturesSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<SharedSymbol[]>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<SharedSymbol[]>.Ok(Exchange, ExchangeSymbolCache.GetSymbolsForBaseAsset(_topicId, _api.EnvironmentName, null, baseAsset));
        }

        public async Task<ExchangeCallResult<bool>> SupportsFuturesSymbolAsync(SharedSymbol symbol)
        {
            if (symbol.TradingMode == TradingMode.Spot)
                throw new ArgumentException(nameof(symbol), "Spot symbols not allowed");

            if (!ExchangeSymbolCache.HasCached(_topicId, _api.EnvironmentName, null))
            {
                var symbols = await GetFuturesSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, _api.EnvironmentName, null, symbol));
        }

        public async Task<ExchangeCallResult<bool>> SupportsFuturesSymbolAsync(string symbolName)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId, _api.EnvironmentName, null))
            {
                var symbols = await GetFuturesSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, _api.EnvironmentName, null, symbolName));
        }
        #endregion
    }
}
