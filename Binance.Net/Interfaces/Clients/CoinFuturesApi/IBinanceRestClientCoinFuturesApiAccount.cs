using Binance.Net.Enums;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures;

namespace Binance.Net.Interfaces.Clients.CoinFuturesApi
{
    /// <summary>
    /// Binance COIN-M futures account endpoints. Account endpoints include balance info, withdraw/deposit info, and account settings.
    /// </summary>
    public interface IBinanceRestClientCoinFuturesApiAccount
    {
        /// <summary>
        /// Gets account position information
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/trade/rest-api/Position-Information" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/positionRisk
        /// </para>
        /// </summary>
        /// <param name="marginAsset">["<c>marginAsset</c>"] Filter by margin asset, for example `ETH`</param>
        /// <param name="pair">["<c>pair</c>"] Filter by pair, for example `BTCUSD`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of positions</returns>
        Task<WebCallResult<BinancePositionDetailsCoin[]>> GetPositionInformationAsync(string? marginAsset = null, string? pair = null,
            long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets account information, including balances
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/account/rest-api/Account-Information" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/account
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The account information</returns>
        Task<WebCallResult<BinanceFuturesCoinAccountInfo>> GetAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets account balances
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/account/rest-api/Futures-Account-Balance" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/balance
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Account balances</returns>
        Task<WebCallResult<BinanceCoinFuturesAccountBalance[]>> GetBalancesAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Changes user position mode (Hedge Mode or One-way Mode) for every symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/trade/rest-api/Change-Position-Mode" /><br />
        /// Endpoint:<br />
        /// POST /dapi/v1/positionSide/dual
        /// </para>
        /// </summary>
        /// <param name="dualPositionSide">["<c>dualSidePosition</c>"] User position mode</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Whether the request was successful</returns>
        Task<WebCallResult<BinanceResult>> ModifyPositionModeAsync(bool dualPositionSide, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets user position mode (Hedge Mode or One-way Mode) for every symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/account/rest-api/Get-Current-Position-Mode" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/positionSide/dual
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Whether the request was successful</returns>
        Task<WebCallResult<BinanceFuturesPositionMode>> GetPositionModeAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Requests to change the initial leverage of the given symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/trade/rest-api/Change-Initial-Leverage" /><br />
        /// Endpoint:<br />
        /// POST /dapi/v1/leverage
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol to change the initial leverage for, for example `BTCUSD_PERP`</param>
        /// <param name="leverage">["<c>leverage</c>"] The amount of initial leverage to change to</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Result of the initial leverage change request</returns>
        Task<WebCallResult<BinanceFuturesInitialLeverageChangeResult>> ChangeInitialLeverageAsync(string symbol, int leverage, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Change the margin type for an open position
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/trade/rest-api/Change-Margin-Type" /><br />
        /// Endpoint:<br />
        /// POST /dapi/v1/marginType
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol to change the position type for, for example `BTCUSD_PERP`</param>
        /// <param name="marginType">["<c>marginType</c>"] The type of margin to use</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Whether the request was successful</returns>
        Task<WebCallResult<BinanceFuturesChangeMarginTypeResult>> ChangeMarginTypeAsync(string symbol, FuturesMarginType marginType, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Change the margin on an open position
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/trade/rest-api/Modify-Isolated-Position-Margin" /><br />
        /// Endpoint:<br />
        /// POST /dapi/v1/positionMargin
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol to adjust the position margin for, for example `BTCUSD_PERP`</param>
        /// <param name="amount">["<c>amount</c>"] The amount of margin to be used</param>
        /// <param name="type">["<c>type</c>"] Whether to reduce or add margin to the position</param>
        /// <param name="positionSide">["<c>positionSide</c>"] Default BOTH for One-way Mode ; LONG or SHORT for Hedge Mode. It must be sent with Hedge Mode.</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The new position margin</returns>
        Task<WebCallResult<BinanceFuturesPositionMarginResult>> ModifyPositionMarginAsync(string symbol, decimal amount, FuturesMarginChangeDirectionType type, PositionSide? positionSide = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Requests the margin change history for a specific symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/trade/rest-api/Get-Position-Margin-Change-History" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/positionMargin/history
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol to get margin history for, for example `BTCUSD_PERP`</param>
        /// <param name="type">["<c>type</c>"] Filter the history by the direction of margin change</param>
        /// <param name="startTime">["<c>startTime</c>"] Margin changes newer than this date will be retrieved</param>
        /// <param name="endTime">["<c>endTime</c>"] Margin changes older than this date will be retrieved</param>
        /// <param name="limit">["<c>limit</c>"] The max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of all margin changes for the symbol</returns>
        Task<WebCallResult<BinanceFuturesMarginChangeHistoryResult[]>> GetMarginChangeHistoryAsync(string symbol, FuturesMarginChangeDirectionType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the income history for the futures account
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/account/rest-api/Get-Income-History" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/income
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to get income history from, for example `BTCUSD_PERP`</param>
        /// <param name="incomeType">["<c>incomeType</c>"] The income type filter to apply to the request</param>
        /// <param name="startTime">["<c>startTime</c>"] Time to start getting income history from</param>
        /// <param name="endTime">["<c>endTime</c>"] Time to stop getting income history from</param>
        /// <param name="page">["<c>page</c>"] Page number</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The income history for the futures account</returns>
        Task<WebCallResult<BinanceFuturesIncomeHistory[]>> GetIncomeHistoryAsync(string? symbol = null, string? incomeType = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets Notional and Leverage Brackets.
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/account/rest-api/Notional-Bracket-for-Pair" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v2/leverageBracket
        /// </para>
        /// </summary>
        /// <param name="symbolOrPair">["<c>pair</c>"] The symbol or pair to get the data for, for example `BTCUSD_PERP`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Notional and Leverage Brackets</returns>
        Task<WebCallResult<BinanceFuturesSymbolBracket[]>> GetBracketsAsync(string? symbolOrPair = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets position ADL quantile estimations
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/trade/rest-api/Position-ADL-Quantile-Estimation" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/adlQuantile
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Only get for this symbol, for example `BTCUSD_PERP`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Position ADL quantile estimations</returns>
        Task<WebCallResult<BinanceFuturesQuantileEstimation[]>> GetPositionAdlQuantileEstimationAsync(
            string? symbol = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets account commission rates
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/account/rest-api/User-Commission-Rate" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/commissionRate
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `BTCUSD_PERP`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>User commission rate information</returns>
        Task<WebCallResult<BinanceFuturesAccountUserCommissionRate>> GetUserCommissionRateAsync(string symbol, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Starts a user stream. The resulting listen key can be used to subscribe to the user stream using the socket client. The stream will close after 60 minutes unless <see cref="KeepAliveUserStreamAsync">KeepAliveUserStreamAsync</see> is called.
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/user-data-streams/Start-User-Data-Stream" /><br />
        /// Endpoint:<br />
        /// POST /dapi/v1/listenKey
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Listen key for the user stream</returns>
        Task<WebCallResult<string>> StartUserStreamAsync(CancellationToken ct = default);

        /// <summary>
        /// Keeps the user stream alive. This should be called every 30 minutes to prevent the user stream from being stopped
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/user-data-streams/Keepalive-User-Data-Stream" /><br />
        /// Endpoint:<br />
        /// PUT /dapi/v1/listenKey
        /// </para>
        /// </summary>
        /// <param name="listenKey">["<c>listenKey</c>"] The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>A successful response</returns>
        Task<WebCallResult> KeepAliveUserStreamAsync(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Stops the user stream; no updates will be sent anymore
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/user-data-streams/Close-User-Data-Stream" /><br />
        /// Endpoint:<br />
        /// DELETE /dapi/v1/listenKey
        /// </para>
        /// </summary>
        /// <param name="listenKey">["<c>listenKey</c>"] The listen key to stop</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>A successful response</returns>
        Task<WebCallResult> StopUserStreamAsync(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Gets download id for downloading transaction history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/account/rest-api/Get-Download-Id-For-Futures-Transaction-History" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/income/asyn
        /// </para>
        /// </summary>
        /// <param name="startTime">["<c>startTime</c>"] Start time of the data to download</param>
        /// <param name="endTime">["<c>endTime</c>"] End time of the data to download</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Download id information</returns>
        Task<WebCallResult<BinanceFuturesDownloadIdInfo>> GetDownloadIdForTransactionHistoryAsync(DateTime startTime, DateTime endTime, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the download link for transaction history by download id
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/account/rest-api/Get-Futures-Transaction-History-Download-Link-by-Id" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/income/asyn/id
        /// </para>
        /// </summary>
        /// <param name="downloadId">["<c>downloadId</c>"] The download id as requested by <see cref="GetDownloadIdForTransactionHistoryAsync" /></param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Download link information</returns>
        Task<WebCallResult<BinanceFuturesDownloadLink>> GetDownloadLinkForTransactionHistoryAsync(string downloadId, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets download id for downloading order history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/account/rest-api/Get-Download-Id-For-Futures-Order-History" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/order/asyn
        /// </para>
        /// </summary>
        /// <param name="startTime">["<c>startTime</c>"] Start time of the data to download</param>
        /// <param name="endTime">["<c>endTime</c>"] End time of the data to download</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Download id information</returns>
        Task<WebCallResult<BinanceFuturesDownloadIdInfo>> GetDownloadIdForOrderHistoryAsync(DateTime startTime, DateTime endTime, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the download link for order history by download id
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/account/rest-api/Get-Futures-Order-History-Download-Link-by-Id" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/order/asyn/id
        /// </para>
        /// </summary>
        /// <param name="downloadId">["<c>downloadId</c>"] The download id as requested by <see cref="GetDownloadIdForOrderHistoryAsync" /></param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Download link information</returns>
        Task<WebCallResult<BinanceFuturesDownloadLink>> GetDownloadLinkForOrderHistoryAsync(string downloadId, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets download id for downloading trade history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/account/rest-api/Get-Download-Id-For-Futures-Trade-History" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/trade/asyn
        /// </para>
        /// </summary>
        /// <param name="startTime">["<c>startTime</c>"] Start time of the data to download</param>
        /// <param name="endTime">["<c>endTime</c>"] End time of the data to download</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Download id information</returns>
        Task<WebCallResult<BinanceFuturesDownloadIdInfo>> GetDownloadIdForTradeHistoryAsync(DateTime startTime, DateTime endTime, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the download link for trade history by download id
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/account/rest-api/Get-Futures-Trade-Download-Link-by-Id" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/trade/asyn/id
        /// </para>
        /// </summary>
        /// <param name="downloadId">["<c>downloadId</c>"] The download id as requested by <see cref="GetDownloadIdForTradeHistoryAsync" /></param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Download link information</returns>
        Task<WebCallResult<BinanceFuturesDownloadLink>> GetDownloadLinkForTradeHistoryAsync(string downloadId, long? receiveWindow = null, CancellationToken ct = default);

    }
}

