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
        IFeeRestClient,
        IFuturesOrderClientIdRestClient,
        IFuturesTriggerOrderRestClient,
        IFuturesTpSlRestClient,
        IBookTickerRestClient
    {
    }

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IBinanceRestClientCoinFuturesSharedApi :
        IGetTickerRest,
        IGetAllTickersRest,
        IGetFuturesSymbolsRest,
        IGetKlinesRest,
        IGetRecentTradesRest,
        IGetTradeHistoryRest,
        IGetLeverageRest,
        ISetLeverageRest,
        IGetMarkPriceKlinesRest,
        IGetIndexPriceKlinesRest,
        IGetOrderBookRest,
        IGetOpenInterestRest,
        IGetFundingRateHistoryRest,
        IGetBalancesRest,
        IGetPositionModeRest,
        ISetPositionModeRest,
        IGetFeesRest,
        IPlaceFuturesOrderRest,
        IGetFuturesOrderRest,
        IGetOpenFuturesOrdersRest,
        IGetClosedFuturesOrdersRest,
        IGetFuturesOrderTradesRest,
        IGetFuturesUserTradeHistoryRest,
        ICancelFuturesOrderRest,
        IGetPositionsRest,
        IGetFuturesOrderByClientOrderIdRest,
        ICancelFuturesOrderByClientOrderIdRest,
        IPlaceFuturesTriggerOrderRest,
        IGetFuturesTriggerOrderRest,
        ICancelFuturesTriggerOrderRest,
        ISetFuturesTpSlRest,
        ICancelFuturesTpSlRest,
        IGetBookTickerRest
    { }
}

