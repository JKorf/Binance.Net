using Binance.Net.Clients;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Objects;
using CryptoExchange.Test.Net;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binance.Net.UnitTests
{
    [TestFixture]
    public class SocketTests
    {
        [Test]
        public async Task ValidateSpotSocketSubscriptions()
        {
            var client = new BinanceSocketClient(opts =>
            {
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new SocketTester<BinanceSocketClient>(client, "Sockets/Spot/ExchangeData", "https://api.binance.com", "data");
            await tester.ValidateAsync<BinanceSocketClient, BinanceStreamTrade>((client, handler) => client.SpotApi.ExchangeData.SubscribeToTradeUpdatesAsync("BTCUSDT", handler), "Trades");
            await tester.ValidateAsync<BinanceSocketClient, BinanceStreamAggregatedTrade>((client, handler) => client.SpotApi.ExchangeData.SubscribeToAggregatedTradeUpdatesAsync("BTCUSDT", handler), "AggregatedTrades", null, new List<string> { "id" });
        }
    }
}
