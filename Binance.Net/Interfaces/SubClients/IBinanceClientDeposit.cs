using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Enums;
using Binance.Net.Objects.Spot.WalletData;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Interfaces.SubClients
{
    /// <summary>
    /// Deposit interface
    /// </summary>
    public interface IBinanceClientDeposit
    {
        /// <summary>
        /// Gets the deposit history
        /// </summary>
        /// <param name="coin">Filter by asset</param>
        /// <param name="status">Filter by status</param>
        /// <param name="limit">Amount of results</param>
        /// <param name="offset">Offset the results</param>
        /// <param name="startTime">Filter start time from</param>
        /// <param name="endTime">Filter end time till</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of deposits</returns>
        WebCallResult<IEnumerable<BinanceDeposit>> GetDepositHistory(string? coin = null, DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the deposit history
        /// </summary>
        /// <param name="coin">Filter by asset</param>
        /// <param name="status">Filter by status</param>
        /// <param name="limit">Amount of results</param>
        /// <param name="offset">Offset the results</param>
        /// <param name="startTime">Filter start time from</param>
        /// <param name="endTime">Filter end time till</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of deposits</returns>
        Task<WebCallResult<IEnumerable<BinanceDeposit>>> GetDepositHistoryAsync(string? coin = null, DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the deposit address for an asset
        /// </summary>
        /// <param name="coin">Asset to get address for</param>
        /// <param name="network">Network</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Deposit address</returns>
        WebCallResult<BinanceDepositAddress> GetDepositAddress(string coin, string? network = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the deposit address for an asset
        /// </summary>
        /// <param name="coin">Asset to get address for</param>
        /// <param name="network">Network</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Deposit address</returns>
        Task<WebCallResult<BinanceDepositAddress>> GetDepositAddressAsync(string coin, string? network = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the withdraw/deposit details for an asset
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Asset detail</returns>
        WebCallResult<Dictionary<string, BinanceAssetDetails>> GetAssetDetails(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the withdraw/deposit details for an asset
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Asset detail</returns>
        Task<WebCallResult<Dictionary<string, BinanceAssetDetails>>> GetAssetDetailsAsync(int? receiveWindow = null, CancellationToken ct = default);
    }
}