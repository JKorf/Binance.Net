using CryptoExchange.Net.SharedApis;

namespace Binance.Net.Interfaces.Clients.UsdFuturesApi
{
    /// <summary>
    /// Shared interface for USD-M Futures socket API usage
    /// </summary>
    public interface IBinanceSocketClientUsdFuturesApiShared :
        ITickerSocketClient,
        ITickersSocketClient,
        ITradeSocketClient,
        IBookTickerSocketClient,
        IOrderBookSocketClient,
        IKlineSocketClient,
        IBalanceSocketClient,
        IPositionSocketClient,
        IFuturesOrderSocketClient,
        IFuturesOrderManagementSocketClient
    {
    }

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IBinanceSocketClientUsdFuturesSharedApi :
        ISubscribeTickerSocket,
        ISubscribeAllTickersSocket,
        ISubscribeTradesSocket,
        ISubscribeBookTickerSocket,
        ISubscribeOrderBookSocket,
        ISubscribeKlinesSocket,
        ISubscribeBalancesSocket,
        ISubscribePositionsSocket,
        ISubscribeFuturesOrdersSocket,
        IPlaceFuturesOrderSocket,
        ICancelFuturesOrderSocket
    { }
}
