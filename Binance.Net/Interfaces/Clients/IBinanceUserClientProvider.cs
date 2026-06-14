using CryptoExchange.Net.Clients;

namespace Binance.Net.Interfaces.Clients
{
    /// <summary>
    /// Provider for clients with credentials for specific users
    /// </summary>
    public interface IBinanceUserClientProvider : IUserClientProvider<IBinanceRestClient, IBinanceSocketClient, BinanceCredentials, BinanceEnvironment>
    {
    }
}