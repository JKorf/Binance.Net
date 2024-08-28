using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models.Spot.CopyTrading;

namespace Binance.Net.Interfaces.Clients.GeneralApi
{
    /// <summary>
    /// Binance copy trading endpoints
    /// </summary>
    public interface IBinanceRestClientGeneralApiCopyTrading
    {
        /// <summary>
        /// Get Futures Lead Trader Status
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#copy-trading-endpoints" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceCopyTradingUserStatus>> GetUserStatusAsync(long? receiveWindow = null, CancellationToken ct = default);


        /// <summary>
        /// Get Futures Lead Trading Symbol Whitelist
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-futures-lead-trading-symbol-whitelist-user_data" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceCopyTradingLeadSymbol>>> GetLeadSymbolAsync(long? receiveWindow = null, CancellationToken ct = default);
    }
}
