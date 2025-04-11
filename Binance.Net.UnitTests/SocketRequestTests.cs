using Binance.Net.Clients;
using Binance.Net.Interfaces;
using Binance.Net.Objects.Models.Futures;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Options;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Binance.Net.UnitTests
{
    [TestFixture]
    public class SocketRequestTests
    {
        private BinanceSocketClient CreateClient()
        {
            var fact = new LoggerFactory();
            fact.AddProvider(new TraceLoggerProvider());
            var client = new BinanceSocketClient(Options.Create(new BinanceSocketOptions
            {
                RequestTimeout = TimeSpan.FromSeconds(1),
                ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456")
            }), fact);
            return client;
        }

        [Test]
        public async Task ValidateUsdFuturesAccountCalls()
        {
            var tester = new SocketRequestValidator<BinanceSocketClient>("Socket/UsdFutures/Account");
            await tester.ValidateAsync(CreateClient(), client => client.UsdFuturesApi.Account.GetAccountInfoAsync(), "GetAccountInfo", responseMapper: x => x.Result, nestedJsonProperty: "result");
            await tester.ValidateAsync(CreateClient(), client => client.UsdFuturesApi.Account.GetBalancesAsync(), "GetBalances", responseMapper: x => x.Result, nestedJsonProperty: "result");

        }

        [Test]
        public async Task ValidateUsdFuturesExchangeDataCalls()
        {
            var tester = new SocketRequestValidator<BinanceSocketClient>("Socket/UsdFutures/ExchangeData");
            await tester.ValidateAsync(CreateClient(), client => client.UsdFuturesApi.ExchangeData.GetOrderBookAsync("ETHUSDT"), "GetOrderBook", responseMapper: x => x.Result, nestedJsonProperty: "result");
            await tester.ValidateAsync(CreateClient(), client => client.UsdFuturesApi.ExchangeData.GetPriceAsync("ETHUSDT"), "GetPrice", responseMapper: x => x.Result, nestedJsonProperty: "result");
            await tester.ValidateAsync(CreateClient(), client => client.UsdFuturesApi.ExchangeData.GetPricesAsync(), "GetPrices", responseMapper: x => x.Result, nestedJsonProperty: "result");
            await tester.ValidateAsync(CreateClient(), client => client.UsdFuturesApi.ExchangeData.GetBookPriceAsync("ETHUSDT"), "GetBookPrice", responseMapper: x => x.Result, nestedJsonProperty: "result");
            await tester.ValidateAsync(CreateClient(), client => client.UsdFuturesApi.ExchangeData.GetBookPricesAsync(), "GetBookPrices", responseMapper: x => x.Result, nestedJsonProperty: "result");

        }

        [Test]
        public async Task ValidateUsdFuturesTradingCalls()
        {
            var tester = new SocketRequestValidator<BinanceSocketClient>("Socket/UsdFutures/Trading");
            await tester.ValidateAsync(CreateClient(), client => client.UsdFuturesApi.Trading.PlaceOrderAsync("ETHUSDT", Enums.OrderSide.Buy, Enums.FuturesOrderType.Limit, 1), "PlaceOrder", responseMapper: x => x.Result, nestedJsonProperty: "result");
            await tester.ValidateAsync(CreateClient(), client => client.UsdFuturesApi.Trading.EditOrderAsync("ETHUSDT", Enums.OrderSide.Buy, 1, orderId: 123), "EditOrder", responseMapper: x => x.Result, nestedJsonProperty: "result");
            await tester.ValidateAsync(CreateClient(), client => client.UsdFuturesApi.Trading.CancelOrderAsync("ETHUSDT", 123), "CancelOrder", responseMapper: x => x.Result, nestedJsonProperty: "result");
            await tester.ValidateAsync(CreateClient(), client => client.UsdFuturesApi.Trading.GetOrderAsync("ETHUSDT", 123), "GetOrder", responseMapper: x => x.Result, nestedJsonProperty: "result");
            await tester.ValidateAsync(CreateClient(), client => client.UsdFuturesApi.Trading.GetPositionsAsync("ETHUSDT"), "GetPositions", responseMapper: x => x.Result, nestedJsonProperty: "result");

        }

        [Test]
        public async Task ValidateSpotAccountCalls()
        {
            var tester = new SocketRequestValidator<BinanceSocketClient>("Socket/Spot/Account");
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.Account.GetAccountInfoAsync(), "GetAccountInfo", responseMapper: x => x.Result, nestedJsonProperty: "result", ignoreProperties: new List<string> { "commissionRates" });
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.Account.GetOrderRateLimitsAsync(), "GetOrderRateLimits", responseMapper: x => x.Result, nestedJsonProperty: "result");
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.Account.StartUserStreamAsync(), "StartUserStream", responseMapper: x => x.Result, nestedJsonProperty: "result.listenKey");
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.Account.KeepAliveUserStreamAsync("123"), "KeepAliveUserStream", responseMapper: x => x.Result, nestedJsonProperty: "response");
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.Account.StopUserStreamAsync("123"), "StopUserStream", responseMapper: x => x.Result, nestedJsonProperty: "response");

        }

        [Test]
        public async Task ValidateSpotExchangeDataCalls()
        {
            var tester = new SocketRequestValidator<BinanceSocketClient>("Socket/Spot/ExchangeData");
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.ExchangeData.GetExchangeInfoAsync(), "GetExchangeInfo", responseMapper: x => x.Result, nestedJsonProperty: "result", ignoreProperties: new List<string> { "orderTypes", "timeInForce", "quoteAssetPrecision", "permissionSets" });
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.ExchangeData.GetOrderBookAsync("ETHUSDT"), "GetOrderBook", responseMapper: x => x.Result, nestedJsonProperty: "result", ignoreProperties: new List<string> { });
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.ExchangeData.GetRecentTradesAsync("ETHUSDT"), "GetRecentTrades", responseMapper: x => x.Result, nestedJsonProperty: "result", ignoreProperties: new List<string> { });
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.ExchangeData.GetTradeHistoryAsync("ETHUSDT"), "GetTradeHistory", responseMapper: x => x.Result, nestedJsonProperty: "result", ignoreProperties: new List<string> { });
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.ExchangeData.GetAggregatedTradeHistoryAsync("ETHUSDT"), "GetAggregatedTradeHistory", responseMapper: x => x.Result, nestedJsonProperty: "result", ignoreProperties: new List<string> { });
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay), "GetKlines", responseMapper: x => x.Result, nestedJsonProperty: "result", ignoreProperties: new List<string> { });
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.ExchangeData.GetUIKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay), "GetUIKlines", responseMapper: x => x.Result, nestedJsonProperty: "result", ignoreProperties: new List<string> { });
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.ExchangeData.GetCurrentAvgPriceAsync("ETHUSDT"), "GetCurrentAvgPrice", responseMapper: x => x.Result, nestedJsonProperty: "result", ignoreProperties: new List<string> { });
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.ExchangeData.GetTickersAsync(), "GetTickers", responseMapper: x => x.Result, nestedJsonProperty: "result", ignoreProperties: new List<string> { });
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.ExchangeData.GetRollingWindowTickersAsync(["ETHUSDT"]), "GetRollingWindowTickers", responseMapper: x => x.Result, nestedJsonProperty: "result", ignoreProperties: new List<string> { });
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.ExchangeData.GetBookTickersAsync(), "GetBookTickers", responseMapper: x => x.Result, nestedJsonProperty: "result", ignoreProperties: new List<string> { });

        }

        [Test]
        public async Task ValidateSpotTradingCalls()
        {
            var tester = new SocketRequestValidator<BinanceSocketClient>("Socket/Spot/Trading");
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.Trading.PlaceOrderAsync("ETHUSDT", Enums.OrderSide.Buy, Enums.SpotOrderType.Limit), "PlaceOrder", responseMapper: x => x.Result, nestedJsonProperty: "result", ignoreProperties: new List<string> { "" });
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.Trading.PlaceTestOrderAsync("ETHUSDT", Enums.OrderSide.Buy, Enums.SpotOrderType.Limit), "PlaceTestOrder", responseMapper: x => x.Result, nestedJsonProperty: "result", ignoreProperties: new List<string> { "" });
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.Trading.GetOrderAsync("ETHUSDT", 123), "GetOrder", responseMapper: x => x.Result, nestedJsonProperty: "result", ignoreProperties: new List<string> { "" });
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.Trading.CancelOrderAsync("ETHUSDT", 123), "CancelOrder", responseMapper: x => x.Result, nestedJsonProperty: "result", ignoreProperties: new List<string> { "" });
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.Trading.ReplaceOrderAsync("ETHUSDT", Enums.OrderSide.Buy, Enums.SpotOrderType.Limit, Enums.CancelReplaceMode.AllowFailure, 123), "ReplaceOrder", responseMapper: x => x.Result, nestedJsonProperty: "result", ignoreProperties: new List<string> { "" });
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.Trading.GetOpenOrdersAsync("ETHUSDT"), "GetOpenOrders", responseMapper: x => x.Result, nestedJsonProperty: "result", ignoreProperties: new List<string> { "" });
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.Trading.CancelAllOrdersAsync("ETHUSDT"), "CancelAllOrders", responseMapper: x => x.Result, nestedJsonProperty: "result", ignoreProperties: new List<string> { "" });
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.Trading.PlaceOcoOrderListAsync("ETHUSDT", Enums.OrderSide.Buy, 1, Enums.SpotOrderType.Limit, Enums.SpotOrderType.Limit), "PlaceOcoOrder", responseMapper: x => x.Result, nestedJsonProperty: "result", ignoreProperties: new List<string> { "" });
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.Trading.GetOcoOrderAsync(123), "GetOcoOrder", responseMapper: x => x.Result, nestedJsonProperty: "result", ignoreProperties: new List<string> { "" });
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.Trading.CancelOcoOrderAsync("ETHUSDT"), "CancelOcoOrder", responseMapper: x => x.Result, nestedJsonProperty: "result", ignoreProperties: new List<string> { "" });
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.Trading.GetOpenOcoOrdersAsync(), "GetOpenOcoOrders", responseMapper: x => x.Result, nestedJsonProperty: "result", ignoreProperties: new List<string> { "" });
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.Trading.GetUserTradesAsync("ETHUSDT"), "GetUserTrades", responseMapper: x => x.Result, nestedJsonProperty: "result", ignoreProperties: new List<string> { "" });
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.Trading.GetPreventedTradesAsync("ETHUSDT"), "GetPreventedTrades", responseMapper: x => x.Result, nestedJsonProperty: "result", ignoreProperties: new List<string> { "" });
        }

        [Test]
        public async Task ValidateCoinFuturesAccountCalls()
        {
            var tester = new SocketRequestValidator<BinanceSocketClient>("Socket/CoinFutures/Account");
            await tester.ValidateAsync(CreateClient(), client => client.CoinFuturesApi.Account.GetAccountInfoAsync(), "GetAccountInfo", responseMapper: x => x.Result, nestedJsonProperty: "result");
            await tester.ValidateAsync(CreateClient(), client => client.CoinFuturesApi.Account.GetBalancesAsync(), "GetBalances", responseMapper: x => x.Result, nestedJsonProperty: "result");

        }

        [Test]
        public async Task ValidateCoinFuturesExchangeDataCalls()
        {
            var tester = new SocketRequestValidator<BinanceSocketClient>("Socket/CoinFutures/ExchangeData");
            // No queries
        }

        [Test]
        public async Task ValidateCoinFuturesTradingCalls()
        {
            var tester = new SocketRequestValidator<BinanceSocketClient>("Socket/CoinFutures/Trading");

            await tester.ValidateAsync(CreateClient(), client => client.CoinFuturesApi.Trading.PlaceOrderAsync("ETHUSD_PERP", Enums.OrderSide.Buy, Enums.FuturesOrderType.Limit, 1), "PlaceOrder", responseMapper: x => x.Result, nestedJsonProperty: "result");
            await tester.ValidateAsync(CreateClient(), client => client.CoinFuturesApi.Trading.EditOrderAsync("ETHUSD_PERP", Enums.OrderSide.Buy, 1, orderId: 123), "EditOrder", responseMapper: x => x.Result, nestedJsonProperty: "result");
            await tester.ValidateAsync(CreateClient(), client => client.CoinFuturesApi.Trading.CancelOrderAsync("ETHUSD_PERP", 123), "CancelOrder", responseMapper: x => x.Result, nestedJsonProperty: "result");
            await tester.ValidateAsync(CreateClient(), client => client.CoinFuturesApi.Trading.GetOrderAsync("ETHUSD_PERP", 123), "GetOrder", responseMapper: x => x.Result, nestedJsonProperty: "result");
            await tester.ValidateAsync(CreateClient(), client => client.CoinFuturesApi.Trading.GetPositionsAsync("ETHUSD_PERP"), "GetPositions", responseMapper: x => x.Result, nestedJsonProperty: "result");

        }
    }
}
