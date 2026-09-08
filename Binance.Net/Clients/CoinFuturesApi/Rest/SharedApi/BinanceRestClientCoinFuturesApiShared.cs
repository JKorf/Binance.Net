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
    internal partial class BinanceRestClientCoinFuturesSharedApi :
        SharedApiBase,
        IBinanceRestClientCoinFuturesApiShared,
        IBinanceRestClientCoinFuturesSharedApi
    {
        private readonly BinanceRestClientCoinFuturesApi _api;

        private const string _exchangeName = "Binance";
        private const string _topicId = "BinanceCoinFutures";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(BinanceExchange.Metadata, this);

        public BinanceRestClientCoinFuturesSharedApi(BinanceRestClientCoinFuturesApi api)
            : base(
                  SharedTransport.Rest,
                  api.Exchange,
                  new[] { TradingMode.DeliveryInverse, TradingMode.PerpetualInverse },
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                GetKlinesOptions,
                GetMarkPriceKlinesOptions,
                GetFuturesSymbolsOptions,
                GetAllTickersOptions,
                GetTickerOptions,
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
