﻿using Binance.Net.Enums;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures;

namespace Binance.Net.Interfaces.Clients.UsdFuturesApi
{
    /// <summary>
    /// Binance USD-M futures account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IBinanceRestClientUsdFuturesApiAccount
    {
        /// <summary>
        /// DEPRECATED; USE Trading.GetPositionsAsync INSTEAD
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/trade/rest-api/Position-Information-V2" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of Positions</returns>
        Task<WebCallResult<BinancePositionDetailsUsdt[]>> GetPositionInformationAsync(string? symbol = null,
            long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Change user's position mode (Hedge Mode or One-way Mode ) on EVERY symbol
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/trade/rest-api/Change-Position-Mode" /></para>
        /// </summary>
        /// <param name="dualPositionSide">User position mode</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Whether the request was successful</returns>
        Task<WebCallResult<BinanceResult>> ModifyPositionModeAsync(bool dualPositionSide, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get user's position mode (Hedge Mode or One-way Mode ) on EVERY symbol
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/account/rest-api/Get-Current-Position-Mode" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Whether the request was successful</returns>
        Task<WebCallResult<BinanceFuturesPositionMode>> GetPositionModeAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Requests to change the initial leverage of the given symbol
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/trade/rest-api/Change-Initial-Leverage" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to change the initial leverage for, for example `ETHUSDT`</param>
        /// <param name="leverage">The amount of initial leverage to change to</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Result of the initial leverage change request</returns>
        Task<WebCallResult<BinanceFuturesInitialLeverageChangeResult>> ChangeInitialLeverageAsync(string symbol, int leverage, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Change the margin type for an open position
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/trade/rest-api/Change-Margin-Type" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to change the position type for, for example `ETHUSDT`</param>
        /// <param name="marginType">The type of margin to use</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Whether the request was successful</returns>
        Task<WebCallResult<BinanceFuturesChangeMarginTypeResult>> ChangeMarginTypeAsync(string symbol, FuturesMarginType marginType, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Change the margin on an open position
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/trade/rest-api/Modify-Isolated-Position-Margin" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to adjust the position margin for, for example `ETHUSDT`</param>
        /// <param name="amount">The amount of margin to be used</param>
        /// <param name="type">Whether to reduce or add margin to the position</param>
        /// <param name="positionSide">Default BOTH for One-way Mode ; LONG or SHORT for Hedge Mode. It must be sent with Hedge Mode.</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The new position margin</returns>
        Task<WebCallResult<BinanceFuturesPositionMarginResult>> ModifyPositionMarginAsync(string symbol, decimal amount, FuturesMarginChangeDirectionType type, PositionSide? positionSide = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Requests the margin change history for a specific symbol
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/trade/rest-api/Get-Position-Margin-Change-History" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to get margin history for, for example `ETHUSDT`</param>
        /// <param name="type">Filter the history by the direction of margin change</param>
        /// <param name="startTime">Margin changes newer than this date will be retrieved</param>
        /// <param name="endTime">Margin changes older than this date will be retrieved</param>
        /// <param name="limit">The max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of all margin changes for the symbol</returns>
        Task<WebCallResult<BinanceFuturesMarginChangeHistoryResult[]>> GetMarginChangeHistoryAsync(string symbol, FuturesMarginChangeDirectionType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the income history for the futures account
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/account/rest-api/Get-Income-History" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get income history from, for example `ETHUSDT`</param>
        /// <param name="incomeType">The income type filter to apply to the request</param>
        /// <param name="startTime">Time to start getting income history from</param>
        /// <param name="endTime">Time to stop getting income history from</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="page">Page number</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The income history for the futures account</returns>
        Task<WebCallResult<BinanceFuturesIncomeHistory[]>> GetIncomeHistoryAsync(string? symbol = null, string? incomeType = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets Notional and Leverage Brackets.
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/account/rest-api/Notional-and-Leverage-Brackets" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Notional and Leverage Brackets</returns>
        Task<WebCallResult<BinanceFuturesSymbolBracket[]>> GetBracketsAsync(string? symbol = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get position ADL quantile estimations
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/trade/rest-api/Position-ADL-Quantile-Estimation" /></para>
        /// </summary>
        /// <param name="symbol">Only get for this symbol, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceFuturesQuantileEstimation[]>> GetPositionAdlQuantileEstimationAsync(
            string? symbol = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get account information, including position and balances
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/account/rest-api/Account-Information-V2" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceFuturesAccountInfo>> GetAccountInfoV2Async(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get account information, including position and balances
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/account/rest-api/Account-Information-V3" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BinanceFuturesAccountInfoV3>> GetAccountInfoV3Async(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets account balances
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/account/rest-api/Futures-Account-Balance-V2" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The account information</returns>
        Task<WebCallResult<BinanceUsdFuturesAccountBalance[]>> GetBalancesAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get user's Multi-Assets mode (Multi-Assets Mode or Single-Asset Mode) on Every symbol
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/account/rest-api/Get-Current-Multi-Assets-Mode" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Multi asset mode</returns>
        Task<WebCallResult<BinanceFuturesMultiAssetMode>> GetMultiAssetsModeAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Set user's Multi-Assets mode (Multi-Assets Mode or Single-Asset Mode) on Every symbol
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/trade/rest-api/Change-Multi-Assets-Mode" /></para>
        /// </summary>
        /// <param name="enabled">Enabled or not</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Success</returns>
        Task<WebCallResult<BinanceResult>> SetMultiAssetsModeAsync(bool enabled, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the current status of the trading rules for the account
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/account/rest-api/Futures-Trading-Quantitative-Rules-Indicators" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of trading rules status per symbol</returns>
        Task<WebCallResult<BinanceFuturesTradingStatus>> GetTradingStatusAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets account commission rates
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/account/rest-api/User-Commission-Rate" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>User commission rate information</returns>
        Task<WebCallResult<BinanceFuturesAccountUserCommissionRate>> GetUserCommissionRateAsync(string symbol, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Start a user stream. The resulting listen key can be used to subscribe to the user stream using the socket client. The stream will close after 60 minutes unless <see cref="KeepAliveUserStreamAsync">KeepAliveUserStreamAsync</see> is called.
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/user-data-streams/Start-User-Data-Stream" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<string>> StartUserStreamAsync(CancellationToken ct = default);

        /// <summary>
        /// Keep alive the user stream. This should be called every 30 minutes to prevent the user stream being stopped
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/user-data-streams/Keepalive-User-Data-Stream" /></para>
        /// </summary>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> KeepAliveUserStreamAsync(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Stop the user stream, no updates will be send anymore
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/user-data-streams/Close-User-Data-Stream" /></para>
        /// </summary>
        /// <param name="listenKey">The listen key to stop</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> StopUserStreamAsync(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Get download id for downloading transaction history
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/account/rest-api/Get-Download-Id-For-Futures-Transaction-History" /></para>
        /// </summary>
        /// <param name="startTime">Start time of the data to download</param>
        /// <param name="endTime">End time of the data to download</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceFuturesDownloadIdInfo>> GetDownloadIdForTransactionHistoryAsync(DateTime startTime, DateTime endTime, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get the download link for transaction history by download id
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/account/rest-api/Get-Futures-Transaction-History-Download-Link-by-Id" /></para>
        /// </summary>
        /// <param name="downloadId">The download id as requested by <see cref="GetDownloadIdForTransactionHistoryAsync" /></param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceFuturesDownloadLink>> GetDownloadLinkForTransactionHistoryAsync(string downloadId, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get download id for downloading order history
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/account/rest-api/Get-Download-Id-For-Futures-Order-History" /></para>
        /// </summary>
        /// <param name="startTime">Start time of the data to download</param>
        /// <param name="endTime">End time of the data to download</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceFuturesDownloadIdInfo>> GetDownloadIdForOrderHistoryAsync(DateTime startTime, DateTime endTime, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get the download link for order history by download id
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/account/rest-api/Get-Futures-Order-History-Download-Link-by-Id" /></para>
        /// </summary>
        /// <param name="downloadId">The download id as requested by <see cref="GetDownloadIdForOrderHistoryAsync" /></param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceFuturesDownloadLink>> GetDownloadLinkForOrderHistoryAsync(string downloadId, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get download id for downloading trade history
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/account/rest-api/Get-Download-Id-For-Futures-Trade-History" /></para>
        /// </summary>
        /// <param name="startTime">Start time of the data to download</param>
        /// <param name="endTime">End time of the data to download</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceFuturesDownloadIdInfo>> GetDownloadIdForTradeHistoryAsync(DateTime startTime, DateTime endTime, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get the download link for order history by download id
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/account/rest-api/Get-Futures-Trade-Download-Link-by-Id" /></para>
        /// </summary>
        /// <param name="downloadId">The download id as requested by <see cref="GetDownloadIdForTradeHistoryAsync" /></param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceFuturesDownloadLink>> GetDownloadLinkForTradeHistoryAsync(string downloadId, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get the order rate limits
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/account/rest-api/Query-Rate-Limit" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceRateLimit[]>> GetOrderRateLimitAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get BNB burn for fee discount status
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/account/rest-api/Get-BNB-Burn-Status" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceBnbBurnStatus>> GetBnbBurnStatusAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Set BNB burn for fee discount status
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/account/rest-api/Toggle-BNB-Burn-On-Futures-Trade" /></para>
        /// </summary>
        /// <param name="feeBurn">Fee burn status</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> SetBnbBurnStatusAsync(bool feeBurn, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get user symbol configuration
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/account/rest-api/Symbol-Config" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BinanceSymbolConfiguration[]>> GetSymbolConfigurationAsync(string? symbol = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get user account configuration
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/account/rest-api/Account-Config" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BinanceFuturesAccountConfiguration>> GetAccountConfigurationAsync(long? receiveWindow = null, CancellationToken ct = default);

    }
}
