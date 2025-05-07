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
        /// <para><a href="https://developers.binance.com/docs/nft/rest-api" /></para>
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
        /// <para><a href="https://developers.binance.com/docs/nft/rest-api/Get-NFT-Withdraw-History" /></para>
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
        /// <para><a href="https://developers.binance.com/docs/nft/rest-api/Get-NFT-Transaction-History" /></para>
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
        /// <para><a href="https://developers.binance.com/docs/nft/rest-api/Get-NFT-Asset" /></para>
        /// </summary>
        /// <param name="limit">Max number of results</param>
        /// <param name="page">Page number of the results to fetch</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceListRecords<BinanceNftAsset>>> GetNftAssetAsync(int? limit = null, int? page = null, long? receiveWindow = null, CancellationToken ct = default);
    }
}
