using CryptoExchange.Net.SharedApis;

namespace Binance.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Shared interface for Spot socket API usage
    /// </summary>
    public interface IBinanceSocketClientSpotApiShared :
        ITickerSocketClient,
        ITickersSocketClient,
        ISpotOrderSocketClient,
        ITradeSocketClient,
        IBookTickerSocketClient,
        IBalanceSocketClient,
        IKlineSocketClient,
        IOrderBookSocketClient
    {
    }
}
