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
        IOrderBookSocketClient,
        ISpotOrderManagementSocketClient
    {
    }

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IBinanceSocketClientSpotSharedApi :
        ISubscribeTickerSocket,
        ISubscribeAllTickersSocket,
        ISubscribeSpotOrdersSocket,
        ISubscribeTradesSocket,
        ISubscribeBookTickerSocket,
        ISubscribeBalancesSocket,
        ISubscribeKlinesSocket,
        ISubscribeOrderBookSocket,
        IPlaceSpotOrderSocket,
        ICancelSpotOrderSocket
    { }
}
