using CryptoExchange.Net.SharedApis;

namespace Binance.Net.Interfaces.Clients.CoinFuturesApi
{
    /// <summary>
    /// Shared interface for COIN-M Futures rest API usage
    /// </summary>
    public interface IBinanceRestClientCoinFuturesApiShared :
        IFuturesTickerRestClient,
        IFuturesSymbolRestClient,
        IFuturesOrderRestClient,
        IKlineRestClient,
        IRecentTradeRestClient,
        ITradeHistoryRestClient,
        ILeverageRestClient,
        IMarkPriceKlineRestClient,
        IIndexPriceKlineRestClient,
        IOrderBookRestClient,
        IOpenInterestRestClient,
        IFundingRateRestClient,
        IBalanceRestClient,
        IPositionModeRestClient,
        IListenKeyRestClient,
        IFeeRestClient,
        IFuturesOrderClientIdClient,
        IFuturesTriggerOrderRestClient,
        IFuturesTpSlRestClient
    {
    }
}
