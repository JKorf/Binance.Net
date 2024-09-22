using CryptoExchange.Net.SharedApis.Interfaces.Rest;
using CryptoExchange.Net.SharedApis.Interfaces.Rest.Futures;
using CryptoExchange.Net.SharedApis.Interfaces.Rest.Spot;
using System;
using System.Collections.Generic;
using System.Text;

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
        IListenKeyRestClient
    {
    }
}
