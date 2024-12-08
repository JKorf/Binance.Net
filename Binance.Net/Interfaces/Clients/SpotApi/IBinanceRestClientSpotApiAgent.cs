using Binance.Net.Objects.Models.Spot;

namespace Binance.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Binance Spot Api-Agent Endpoints.
    /// </summary>
    public interface IBinanceRestClientSpotApiAgent
    {
        /// <summary>
        /// Query Client If The New User.
        /// <para><a href="https://binance-docs.github.io/apiAgent-API-EN/api_rebate_endpoints_spot_EN/" /></para>
        /// <para><a href="https://binance-docs.github.io/apiAgent-API-CN/api_rebate_endpoints_spot_CN/" /></para>
        /// </summary>
        /// <param name="apiAgentCode">Api Agent Code</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>USER DATA</returns>
        Task<WebCallResult<BinanceIfNewUser>> IfNewUser(string apiAgentCode,
            int? receiveWindow = null,
            CancellationToken ct = default);

    }
}
