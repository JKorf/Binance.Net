using Binance.Net.Enums;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot.NFT;

namespace Binance.Net.Interfaces.Clients.GeneralApi
{
    /// <summary>
    /// Binance NFT endpoints
    /// </summary>
    public interface IBinanceRestClientGeneralApiNft
    {
        /// <summary>
        /// Get NFT deposit history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/nft/rest-api" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/nft/history/deposit
        /// </para>
        /// </summary>
        /// <param name="startTime">Time to start getting deposit records from</param>
        /// <param name="endTime">Time to stop getting deposit records from</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="page">Page number of the results to fetch</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceListRecords<BinanceNftDeposit>>> GetNftDepositHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? page = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get NFT withdraw history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/nft/rest-api/Get-NFT-Withdraw-History" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/nft/history/withdraw
        /// </para>
        /// </summary>
        /// <param name="startTime">Time to start getting withdraw records from</param>
        /// <param name="endTime">Time to stop getting withdraw records from</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="page">Page number of the results to fetch</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceListRecords<BinanceNftWithdraw>>> GetNftWithdrawHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? page = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get NFT transaction history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/nft/rest-api/Get-NFT-Transaction-History" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/nft/history/transactions
        /// </para>
        /// </summary>
        /// <param name="orderType">Order type</param>
        /// <param name="startTime">Time to start getting transaction from</param>
        /// <param name="endTime">Time to stop getting transaction from</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="page">Page number of the results to fetch</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceListRecords<BinanceNftTransaction>>> GetNftTransactionHistoryAsync(NftOrderType orderType, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? page = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get NFT Asset
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/nft/rest-api/Get-NFT-Asset" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/nft/user/getAsset
        /// </para>
        /// </summary>
        /// <param name="limit">Max number of results</param>
        /// <param name="page">Page number of the results to fetch</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceListRecords<BinanceNftAsset>>> GetNftAssetAsync(int? limit = null, int? page = null, long? receiveWindow = null, CancellationToken ct = default);
    }
}
