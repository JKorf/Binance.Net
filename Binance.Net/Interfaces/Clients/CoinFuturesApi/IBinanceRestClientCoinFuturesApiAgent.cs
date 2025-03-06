using Binance.Net.Objects.Models.Futures;

namespace Binance.Net.Interfaces.Clients.CoinFuturesApi
{
    /// <summary>
    /// Binance Futures Api-Agent Endpoints.
    /// </summary>
    public interface IBinanceRestClientCoinFuturesApiAgent
    {
        /// <summary>
        /// Query Client If The New User.
        /// <para><a href="https://binance-docs.github.io/apiAgent-API-EN/api_rebate_endpoints_futures_EN/" /></para>
        /// <para><a href="https://binance-docs.github.io/apiAgent-API-CN/api_rebate_endpoints_futures_CN/" /></para>
        /// </summary>
        /// <param name="brokerId">Api Broker Id</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>USER DATA</returns>
        Task<WebCallResult<BinanceFuturesIfNewUser>> GetIfNewUserAsync(string brokerId,
            int? receiveWindow = null,
            CancellationToken ct = default);
    }
}
