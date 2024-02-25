using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;

namespace Blazor.DataProvider
{
    public class BinanceDataProvider
    {
        private IBinanceRestClient _client;
        private IBinanceSocketClient _socketClient;

        public BinanceDataProvider(IBinanceRestClient client, IBinanceSocketClient socketClient)
        {
            _client = client;
            _socketClient = socketClient;
        }

        public Task<WebCallResult<IEnumerable<IBinanceTick>>> Get24HPrices()
        {
            return _client.SpotApi.ExchangeData.GetTickersAsync();
        }

        public Task<CallResult<UpdateSubscription>> SubscribeTickerUpdates(Action<DataEvent<IEnumerable<IBinanceTick>>> tickHandler)
        {
            return _socketClient.SpotApi.ExchangeData.SubscribeToAllTickerUpdatesAsync(tickHandler);
        }

        public async Task Unsubscribe(UpdateSubscription subscription)
        {
            await _socketClient.UnsubscribeAsync(subscription);
        }
    }
}
