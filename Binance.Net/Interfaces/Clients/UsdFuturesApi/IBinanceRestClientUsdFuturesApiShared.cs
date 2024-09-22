using CryptoExchange.Net.SharedApis.Interfaces.Rest;
using CryptoExchange.Net.SharedApis.Interfaces.Rest.Futures;
using CryptoExchange.Net.SharedApis.Interfaces.Rest.Spot;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Interfaces.Clients.UsdFuturesApi
{
    /// <summary>
    /// Shared interface for USD-M Futures rest API usage
    /// </summary>
    public interface IBinanceRestClientUsdFuturesApiShared :
        IBalanceRestClient,
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
        IPositionModeRestClient,
        IListenKeyRestClient
    {
    }
}
