using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;

namespace Binance.Net.Clients.SpotApi
{
    internal partial class BinanceSocketClientSpotSharedApi :
        SharedApiBase,
        IBinanceSocketClientSpotApiShared,
        IBinanceSocketClientSpotSharedApi
    {
        private readonly BinanceSocketClientSpotApi _api;

        private const string _exchangeName = "Binance";
        private const string _topicId = "BinanceSpot";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(BinanceExchange.Metadata, this);

        public BinanceSocketClientSpotSharedApi(BinanceSocketClientSpotApi api)
            : base(
                  SharedTransport.Socket,
                  api.Exchange,
                  new[] { TradingMode.Spot },
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
               SubscribeSpotOrderOptions,
               SubscribeKlineOptions,
               SubscribeOrderBookOptions,
               PlaceSpotOrderOptions,
               CancelSpotOrderOptions
               );
        }
    }
}
