using CryptoExchange.Net.SharedApis;

namespace Binance.Net.Interfaces.Clients.CoinFuturesApi
{
    /// <summary>
    /// Shared interface for COIN-M Futures socket API usage
    /// </summary>
    public interface IBinanceSocketClientCoinFuturesApiShared :
        ITickerSocketClient,
        ITickersSocketClient,
        ITradeSocketClient,
        IBookTickerSocketClient,
        IOrderBookSocketClient,
        IKlineSocketClient,
        IFuturesOrderSocketClient,
        IBalanceSocketClient,
        IPositionSocketClient,
        IFuturesOrderManagementSocketClient
    {
    }


    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IBinanceSocketClientCoinFuturesSharedApi :
        ISubscribeTickerSocket,
        ISubscribeAllTickersSocket,
        ISubscribeTradesSocket,
        ISubscribeBookTickerSocket,
        ISubscribeOrderBookSocket,
        ISubscribeKlinesSocket,
        ISubscribeFuturesOrdersSocket,
        ISubscribeBalancesSocket,
        ISubscribePositionsSocket,
        IPlaceFuturesOrderSocket,
        ICancelFuturesOrderSocket
    { }
}
