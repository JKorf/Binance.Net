using Binance.Net.Clients.UsdFuturesApi;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;

namespace Binance.Net.Clients.CoinFuturesApi
{
    internal partial class BinanceSocketClientCoinFuturesSharedApi :
        SharedApiBase,
        IBinanceSocketClientCoinFuturesApiShared,
        IBinanceSocketClientCoinFuturesSharedApi
    {
        private readonly BinanceSocketClientCoinFuturesApi _api;

        private const string _exchangeName = "Binance";
        private const string _topicId = "BinanceCoinFutures";
        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(BinanceExchange.Metadata, this);

        public BinanceSocketClientCoinFuturesSharedApi(BinanceSocketClientCoinFuturesApi api)
            : base(
                  SharedTransport.Socket,
                  api.Exchange,
                  new[] { TradingMode.DeliveryInverse, TradingMode.PerpetualInverse },
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
