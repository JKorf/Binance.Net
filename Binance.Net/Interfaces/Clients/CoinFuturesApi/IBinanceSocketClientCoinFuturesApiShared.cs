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
        IPositionSocketClient
    {
    }
}
