using Binance.Net.Clients.SpotApi;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using Binance.Net.Objects.Models.Futures;
using CryptoExchange.Net.Caching;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using System.Collections.Concurrent;

namespace Binance.Net.Clients.UsdFuturesApi
{
    internal partial class BinanceRestClientUsdFuturesSharedApi : 
        SharedApiBase,
        IBinanceRestClientUsdFuturesApiShared,
        IBinanceRestClientUsdFuturesSharedApi
    {
        private readonly BinanceRestClientUsdFuturesApi _api;

        private const string _exchangeName = "Binance";
        private const string _topicId = "BinanceUsdFutures";
        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(BinanceExchange.Metadata, this);

        public BinanceRestClientUsdFuturesSharedApi(BinanceRestClientUsdFuturesApi api)
            : base(
                  SharedTransport.Rest,
                  api.Exchange,
                  new[] { TradingMode.DeliveryLinear, TradingMode.PerpetualLinear },
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                GetKlinesOptions,
                GetMarkPriceKlinesOptions,
                GetFuturesSymbolsOptions,
                GetAllFuturesTickersOptions,
                GetFuturesTickerOptions,
                GetBookTickerOptions,
                GetRecentTradesOptions,
                PlaceFuturesOrderOptions,
                GetFuturesOrderOptions,
                GetOpenFuturesOrdersOptions,
                GetClosedFuturesOrdersOptions,
                GetFuturesOrderTradesOptions,
                GetFuturesUserTradeHistoryOptions,
                CancelFuturesOrderOptions,
                GetPositionsOptions,
                ClosePositionOptions,
                GetFuturesOrderByClientOrderIdOptions,
                CancelFuturesOrderByClientOrderIdOptions,
                GetLeverageOptions,
                SetLeverageOptions,
                GetOrderBookOptions,
                GetTradeHistoryOptions,
                GetIndexPriceKlinesOptions,
                GetOpenInterestOptions,
                GetFundingRateHistoryOptions,
                GetBalancesOptions,
                GetPositionModeOptions,
                SetPositionModeOptions,
                GetFeeOptions,
                PlaceFuturesTriggerOrderOptions,
                GetFuturesTriggerOrderOptions,
                CancelFuturesTriggerOrderOptions,
                SetFuturesTpSlOptions,
                CancelFuturesTpSlOptions
                );
        }
    }
}
