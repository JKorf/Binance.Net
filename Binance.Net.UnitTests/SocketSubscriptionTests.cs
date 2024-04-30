using Binance.Net.Clients;
using Binance.Net.Interfaces;
using Binance.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Testing;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Binance.Net.UnitTests
{
    [TestFixture]
    public class SocketSubscriptionTests
    {
        [Test]
        public async Task ValidateSpotExchangeDataSubscriptions()
        {
            var client = new BinanceSocketClient(opts =>
            {
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new SocketSubscriptionValidator<BinanceSocketClient>(client, "Sockets/Spot/ExchangeData", "https://api.binance.com", "data", stjCompare: false);
            await tester.ValidateAsync<BinanceStreamTrade>((client, handler) => client.SpotApi.ExchangeData.SubscribeToTradeUpdatesAsync("BTCUSDT", handler), "Trades");
            await tester.ValidateAsync<BinanceStreamAggregatedTrade>((client, handler) => client.SpotApi.ExchangeData.SubscribeToAggregatedTradeUpdatesAsync("BTCUSDT", handler), "AggregatedTrades");
            await tester.ValidateAsync<IBinanceStreamKlineData>((client, handler) => client.SpotApi.ExchangeData.SubscribeToKlineUpdatesAsync("BTCUSDT", Enums.KlineInterval.EightHour, handler), "Klines", ignoreProperties: new List<string> { "B" });
            await tester.ValidateAsync<IBinanceMiniTick>((client, handler) => client.SpotApi.ExchangeData.SubscribeToMiniTickerUpdatesAsync("BTCUSDT", handler), "MiniTicker");
            await tester.ValidateAsync<BinanceStreamBookPrice>((client, handler) => client.SpotApi.ExchangeData.SubscribeToBookTickerUpdatesAsync("BTCUSDT", handler), "BookTicker");
            await tester.ValidateAsync<IBinanceOrderBook>((client, handler) => client.SpotApi.ExchangeData.SubscribeToPartialOrderBookUpdatesAsync("BTCUSDT", 5, 100, handler), "PartialBook");
            await tester.ValidateAsync<IBinanceTick>((client, handler) => client.SpotApi.ExchangeData.SubscribeToTickerUpdatesAsync("BTCUSDT", handler), "Ticker");
            await tester.ValidateAsync<IBinanceTick>((client, handler) => client.SpotApi.ExchangeData.SubscribeToTickerUpdatesAsync("BTCUSDT", handler), "Ticker");
        }

        [Test]
        public async Task ValidateSpotAccountSubscriptions()
        {
            var client = new BinanceSocketClient(opts =>
            {
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new SocketSubscriptionValidator<BinanceSocketClient>(client, "Sockets/Spot/Account", "https://api.binance.com", "data", stjCompare: false);
            await tester.ValidateAsync<BinanceStreamOrderUpdate>((client, handler) => client.SpotApi.Account.SubscribeToUserDataUpdatesAsync("123", onOrderUpdateMessage: handler), "Order");
            await tester.ValidateAsync<BinanceStreamOrderList>((client, handler) => client.SpotApi.Account.SubscribeToUserDataUpdatesAsync("123", onOcoOrderUpdateMessage: handler), "OcoOrder");
            await tester.ValidateAsync<BinanceStreamPositionsUpdate>((client, handler) => client.SpotApi.Account.SubscribeToUserDataUpdatesAsync("123", onAccountPositionMessage: handler), "AccountPosition");
            await tester.ValidateAsync<BinanceStreamBalanceUpdate>((client, handler) => client.SpotApi.Account.SubscribeToUserDataUpdatesAsync("123", onAccountBalanceUpdate: handler), "Balance");
        }
    }
}
