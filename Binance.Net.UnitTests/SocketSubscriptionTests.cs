using Binance.Net.Clients;
using Binance.Net.Interfaces;
using Binance.Net.Objects.Models.Futures.Socket;
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
            var tester = new SocketSubscriptionValidator<BinanceSocketClient>(client, "Subscriptions/Spot/ExchangeData", "https://api.binance.com", "data", stjCompare: true);
            //await tester.ValidateAsync<BinanceStreamTrade>((client, handler) => client.SpotApi.ExchangeData.SubscribeToTradeUpdatesAsync("BTCUSDT", handler), "Trades");
            //await tester.ValidateAsync<BinanceStreamAggregatedTrade>((client, handler) => client.SpotApi.ExchangeData.SubscribeToAggregatedTradeUpdatesAsync("BTCUSDT", handler), "AggregatedTrades");
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
            var tester = new SocketSubscriptionValidator<BinanceSocketClient>(client, "Subscriptions/Spot/Account", "https://api.binance.com", "data", stjCompare: true);
            await tester.ValidateAsync<BinanceStreamOrderUpdate>((client, handler) => client.SpotApi.Account.SubscribeToUserDataUpdatesAsync("123", onOrderUpdateMessage: handler), "Order");
            await tester.ValidateAsync<BinanceStreamOrderList>((client, handler) => client.SpotApi.Account.SubscribeToUserDataUpdatesAsync("123", onOcoOrderUpdateMessage: handler), "OcoOrder");
            await tester.ValidateAsync<BinanceStreamPositionsUpdate>((client, handler) => client.SpotApi.Account.SubscribeToUserDataUpdatesAsync("123", onAccountPositionMessage: handler), "AccountPosition");
            await tester.ValidateAsync<BinanceStreamBalanceUpdate>((client, handler) => client.SpotApi.Account.SubscribeToUserDataUpdatesAsync("123", onAccountBalanceUpdate: handler), "Balance");
        }

        [Test]
        public async Task ValidateUsdFuturesSubscriptions()
        {
            var client = new BinanceSocketClient(opts =>
            {
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new SocketSubscriptionValidator<BinanceSocketClient>(client, "Subscriptions/UsdFutures", "https://fapi.binance.com", "data", stjCompare: true);
            await tester.ValidateAsync<BinanceFuturesUsdtStreamMarkPrice>((client, handler) => client.UsdFuturesApi.SubscribeToMarkPriceUpdatesAsync("BTCUSDT", 1000, handler), "MarkPrice");
            await tester.ValidateAsync<IBinanceStreamKlineData>((client, handler) => client.UsdFuturesApi.SubscribeToKlineUpdatesAsync("BTCUSDT", Enums.KlineInterval.OneMonth, handler), "Klines", ignoreProperties: new List<string> { "B" });
            await tester.ValidateAsync<BinanceStreamContinuousKlineData>((client, handler) => client.UsdFuturesApi.SubscribeToContinuousContractKlineUpdatesAsync("BTCUSDT", Enums.ContractType.Perpetual, Enums.KlineInterval.OneMonth, handler), "ContKlines", ignoreProperties: new List<string> { "B" });
            await tester.ValidateAsync<IBinanceMiniTick>((client, handler) => client.UsdFuturesApi.SubscribeToMiniTickerUpdatesAsync("BTCUSDT", handler), "MiniTicker");
            await tester.ValidateAsync<IBinance24HPrice>((client, handler) => client.UsdFuturesApi.SubscribeToTickerUpdatesAsync("BTCUSDT", handler), "Ticker");
            await tester.ValidateAsync<BinanceFuturesStreamCompositeIndex>((client, handler) => client.UsdFuturesApi.SubscribeToCompositeIndexUpdatesAsync("BTCUSDT", handler), "CompositIndex");
            await tester.ValidateAsync<BinanceStreamAggregatedTrade>((client, handler) => client.UsdFuturesApi.SubscribeToAggregatedTradeUpdatesAsync("BTCUSDT", handler), "AggTrades");
            await tester.ValidateAsync<BinanceFuturesStreamBookPrice>((client, handler) => client.UsdFuturesApi.SubscribeToBookTickerUpdatesAsync("BTCUSDT", handler), "BookTicker");
            await tester.ValidateAsync<BinanceFuturesStreamLiquidation>((client, handler) => client.UsdFuturesApi.SubscribeToLiquidationUpdatesAsync("BTCUSDT", handler), "Liquidations", "data.o", ignoreProperties: new List<string> { "e", "E" });
            await tester.ValidateAsync<IBinanceFuturesEventOrderBook>((client, handler) => client.UsdFuturesApi.SubscribeToPartialOrderBookUpdatesAsync("BTCUSDT", 5, 100, handler), "PartialBook");
            await tester.ValidateAsync<IBinanceFuturesEventOrderBook>((client, handler) => client.UsdFuturesApi.SubscribeToOrderBookUpdatesAsync("BTCUSDT", 100, handler), "Book");
            await tester.ValidateAsync<BinanceFuturesStreamSymbolUpdate>((client, handler) => client.UsdFuturesApi.SubscribeToSymbolUpdatesAsync(handler), "SymbolUpdates");
            await tester.ValidateAsync<IEnumerable<BinanceFuturesStreamAssetIndexUpdate>>((client, handler) => client.UsdFuturesApi.SubscribeToAssetIndexUpdatesAsync(handler), "AssetIndex");
            await tester.ValidateAsync<BinanceFuturesStreamConfigUpdate>((client, handler) => client.UsdFuturesApi.SubscribeToUserDataUpdatesAsync("123", onLeverageUpdate: handler), "Leverage");
            await tester.ValidateAsync<BinanceFuturesStreamConfigUpdate>((client, handler) => client.UsdFuturesApi.SubscribeToUserDataUpdatesAsync("123", onLeverageUpdate: handler), "MultiAssetMode");
            await tester.ValidateAsync<BinanceFuturesStreamMarginUpdate>((client, handler) => client.UsdFuturesApi.SubscribeToUserDataUpdatesAsync("123", onMarginUpdate: handler), "MarginUpdate");
            await tester.ValidateAsync<BinanceFuturesStreamAccountUpdate>((client, handler) => client.UsdFuturesApi.SubscribeToUserDataUpdatesAsync("123", onAccountUpdate: handler), "AccountUpdate");
            await tester.ValidateAsync<BinanceFuturesStreamOrderUpdate>((client, handler) => client.UsdFuturesApi.SubscribeToUserDataUpdatesAsync("123", onOrderUpdate : handler), "OrderUpdate", ignoreProperties: new List<string> { "si", "ss" });
            await tester.ValidateAsync<BinanceStrategyUpdate>((client, handler) => client.UsdFuturesApi.SubscribeToUserDataUpdatesAsync("123", onStrategyUpdate : handler), "StrategyUpdate");
            await tester.ValidateAsync<BinanceConditionOrderTriggerRejectUpdate>((client, handler) => client.UsdFuturesApi.SubscribeToUserDataUpdatesAsync("123", onConditionalOrderTriggerRejectUpdate: handler), "ConditionalTrigger");
        }
    }
}
