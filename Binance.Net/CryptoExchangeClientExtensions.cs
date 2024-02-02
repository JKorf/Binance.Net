using Binance.Net.Clients;
using Binance.Net.Interfaces.Clients;
using CryptoExchange.Net.Interfaces.CommonClients;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoExchange.Net.Clients
{
    public static class CryptoExchangeClientExtensions
    {
        public static IBinanceRestClient Binance(this ICryptoExchangeClient baseClient) => baseClient.TryGet<IBinanceRestClient>() ?? new BinanceRestClient();
    }
}
