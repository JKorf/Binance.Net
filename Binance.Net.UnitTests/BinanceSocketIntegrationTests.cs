using Binance.Net.Clients;
using Binance.Net.Interfaces;
using Binance.Net.Objects.Options;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Binance.Net.UnitTests
{
    [NonParallelizable]
    internal class BinanceSocketIntegrationTests : SocketIntegrationTest<BinanceSocketClient>
    {
        public override bool Run { get; set; } = true;

        public BinanceSocketIntegrationTests()
        {
        }

        public override BinanceSocketClient GetClient(ILoggerFactory loggerFactory, bool newDeserialization)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new BinanceSocketClient(Options.Create(new BinanceSocketOptions
            {
                OutputOriginalData = true,
                UseUpdatedDeserialization = newDeserialization,
                ApiCredentials = Authenticated ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec) : null
            }), loggerFactory);
        }

        private BinanceRestClient GetRestClient()
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new BinanceRestClient(x =>
            {
                x.ApiCredentials = Authenticated ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec) : null;
            });
        }


        [TestCase(false)]
        [TestCase(true)]
        public async Task TestSubscriptions(bool newDeserialization)
        {
            var listenKey = await GetRestClient().SpotApi.Account.StartUserStreamAsync();
            await RunAndCheckUpdate<IBinanceTick>(newDeserialization , (client, updateHandler) => client.SpotApi.Account.SubscribeToUserDataUpdatesAsync(listenKey.Data, default, default, default, default, default, default, default, default), false, true);
            await RunAndCheckUpdate<IBinanceTick>(newDeserialization, (client, updateHandler) => client.SpotApi.ExchangeData.SubscribeToTickerUpdatesAsync("ETHUSDT", updateHandler, default), true, false);

            listenKey = await GetRestClient().UsdFuturesApi.Account.StartUserStreamAsync();
            await RunAndCheckUpdate<IBinanceTick>(newDeserialization, (client, updateHandler) => client.UsdFuturesApi.Account.SubscribeToUserDataUpdatesAsync(listenKey.Data, default, default, default, default, default, default, default, default, default, default, default), false, true);
            await RunAndCheckUpdate<IBinance24HPrice>(newDeserialization, (client, updateHandler) => client.UsdFuturesApi.ExchangeData.SubscribeToTickerUpdatesAsync("ETHUSDT", updateHandler, default), true, false);

            listenKey = await GetRestClient().CoinFuturesApi.Account.StartUserStreamAsync();
            await RunAndCheckUpdate<IBinanceTick>(newDeserialization, (client, updateHandler) => client.CoinFuturesApi.Account.SubscribeToUserDataUpdatesAsync(listenKey.Data, default, default, default, default, default, default, default, default), false, true);
            await RunAndCheckUpdate<IBinance24HPrice>(newDeserialization, (client, updateHandler) => client.CoinFuturesApi.ExchangeData.SubscribeToTickerUpdatesAsync("ETHUSD_PERP", updateHandler, default), true, false);
        } 
    }
}
