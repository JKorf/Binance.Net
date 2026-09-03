using Binance.Net.Clients.SpotApi;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;

namespace Binance.Net.Clients.UsdFuturesApi
{
    internal partial class BinanceSocketClientUsdFuturesSharedApi : 
        SharedApiBase,
        IBinanceSocketClientUsdFuturesApiShared,
        IBinanceSocketClientUsdFuturesSharedApi
    {
        private readonly BinanceSocketClientUsdFuturesApi _api;

        private const string _exchangeName = "Binance";
        private const string _topicId = "BinanceUsdFutures";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(BinanceExchange.Metadata, this);

        public BinanceSocketClientUsdFuturesSharedApi(BinanceSocketClientUsdFuturesApi api)
            : base(
                  SharedTransport.Socket,
                  api.Exchange,
                  new[] { TradingMode.DeliveryLinear, TradingMode.PerpetualLinear },
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                SubscribeTickerOptions,
                SubscribeAllTickersOptions,
                SubscribeTradeOptions,
                SubscribeBookTickerOptions,
                SubscribeBalanceOptions,
                SubscribeFuturesOrderOptions,
                SubscribeKlineOptions,
                SubscribeOrderBookOptions,
                SubscribePositionOptions,
                PlaceFuturesOrderOptions,
                CancelFuturesOrderOptions
                );
        }
    }
}
