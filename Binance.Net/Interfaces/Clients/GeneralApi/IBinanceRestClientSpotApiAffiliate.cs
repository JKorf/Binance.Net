using Binance.Net.Objects.Models.General.Affiliate;

namespace Binance.Net.Interfaces.Clients.GeneralApi
{
    /// <summary>
    /// Binance Affiliate Performance.
    /// </summary>
    public interface IBinanceRestClientSpotApiAffiliate
    {
        /// <summary>
        /// Get Invitee Performance
        /// <para>
        /// Endpoint:<br />
        /// GET /sapi/v1/affiliate/performance/invitee
        /// </para>
        /// </summary>
        /// <param name="userId">The invitee's user ID</param>
        /// <param name="startDate">	Start date for filtering metrics. ISO 8601 format: YYYY-MM-DD</param>
        /// <param name="endDate">End date for filtering metrics. ISO 8601 format: YYYY-MM-DD</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BinanceAffiliateResponse<BinanceInviteePerformance>>> GetPerformanceByInviteeAsync(string userId, DateTime? startDate = null, DateTime? endDate = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get Code Performance
        /// <para>
        /// Endpoint:<br />
        /// GET /sapi/v1/affiliate/performance/code
        /// </para>
        /// </summary>
        /// <param name="code">The referral code to query</param>
        /// <param name="startDate">	Start date for filtering metrics. ISO 8601 format: YYYY-MM-DD</param>
        /// <param name="endDate">End date for filtering metrics. ISO 8601 format: YYYY-MM-DD</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<BinanceAffiliateResponse<BinanceCodePerformance>>> GetPerformanceByCodeAsync(string code, DateTime? startDate = null, DateTime? endDate = null, long? receiveWindow = null, CancellationToken ct = default);
    }
}