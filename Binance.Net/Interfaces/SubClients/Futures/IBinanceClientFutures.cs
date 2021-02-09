using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Enums;
using Binance.Net.Objects;
using Binance.Net.Objects.Futures.FuturesData;
using Binance.Net.Objects.Futures.MarketData;
using Binance.Net.SubClients.Futures.Coin;
using Binance.Net.SubClients.Futures.Usdt;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Interfaces.SubClients.Futures
{
    /// <summary>
    /// COIN-M futures interface
    /// </summary>
    public interface IBinanceClientFuturesCoin: IBinanceClientFutures
    {
        /// <summary>
        /// Coin futures market endpoints
        /// </summary>
        IBinanceClientFuturesCoinMarket Market { get; }

        /// <summary>
        /// Futures order endpoints
        /// </summary>
        IBinanceClientFuturesCoinOrder Order { get; }

        /// <summary>
        /// Futures order endpoints
        /// </summary>
        IBinanceClientFuturesCoinAccount Account { get; }

        /// <summary>
        /// Futures system endpoints
        /// </summary>
        IBinanceClientFuturesCoinSystem System { get; }

        /// <summary>
        /// Gets account position information
        /// </summary>
        /// <param name="marginAsset">Filter by margin asset</param>
        /// <param name="pair">Filter by pair</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of Positions</returns>
        WebCallResult<IEnumerable<BinancePositionDetailsCoin>> GetPositionInformation(string? marginAsset = null, string? pair = null,
            long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets account position information
        /// </summary>
        /// <param name="marginAsset">Filter by margin asset</param>
        /// <param name="pair">Filter by pair</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of Positions</returns>
        Task<WebCallResult<IEnumerable<BinancePositionDetailsCoin>>> GetPositionInformationAsync(string? marginAsset = null, string? pair = null,
            long? receiveWindow = null, CancellationToken ct = default);
    }

    /// <summary>
    /// USDT-M futures interface
    /// </summary>
    public interface IBinanceClientFuturesUsdt : IBinanceClientFutures
    {
        /// <summary>
        /// Usdt futures market endpoints
        /// </summary>
        IBinanceClientFuturesUsdtMarket Market { get; }


        /// <summary>
        /// Futures order endpoints
        /// </summary>
        IBinanceClientFuturesUsdtOrder Order { get; }

        /// <summary>
        /// Futures order endpoints
        /// </summary>
        IBinanceClientFuturesUsdtAccount Account { get; }
        
        /// <summary>
        /// Futures system endpoints
        /// </summary>
        IBinanceClientFuturesUsdtSystem System { get; }

        /// <summary>
        /// Gets account information
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of Positions</returns>
        WebCallResult<IEnumerable<BinancePositionDetailsUsdt>> GetPositionInformation(string? symbol = null,
            long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets account information
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of Positions</returns>
        Task<WebCallResult<IEnumerable<BinancePositionDetailsUsdt>>> GetPositionInformationAsync(string? symbol = null,
            long? receiveWindow = null, CancellationToken ct = default);
    }

    /// <summary>
    /// Futures interface
    /// </summary>
    public interface IBinanceClientFutures
    {
        /// <summary>
        /// Futures user stream endpoints
        /// </summary>
        IBinanceClientUserStream UserStream { get; }

        /// <summary>
        /// Change user's position mode (Hedge Mode or One-way Mode ) on EVERY symbol
        /// </summary>
        /// <param name="dualPositionSide">User position mode</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Whether the request was successful</returns>
        WebCallResult<BinanceResult> ModifyPositionMode(bool dualPositionSide, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Change user's position mode (Hedge Mode or One-way Mode ) on EVERY symbol
        /// </summary>
        /// <param name="dualPositionSide">User position mode</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Whether the request was successful</returns>
        Task<WebCallResult<BinanceResult>> ModifyPositionModeAsync(bool dualPositionSide, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get user's position mode (Hedge Mode or One-way Mode ) on EVERY symbol
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Whether the request was successful</returns>
        WebCallResult<BinanceFuturesPositionMode> GetPositionMode(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get user's position mode (Hedge Mode or One-way Mode ) on EVERY symbol
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Whether the request was successful</returns>
        Task<WebCallResult<BinanceFuturesPositionMode>> GetPositionModeAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Requests to change the initial leverage of the given symbol
        /// </summary>
        /// <param name="symbol">Symbol to change the initial leverage for</param>
        /// <param name="leverage">The amount of initial leverage to change to</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Result of the initial leverage change request</returns>
        WebCallResult<BinanceFuturesInitialLeverageChangeResult> ChangeInitialLeverage(string symbol, int leverage, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Requests to change the initial leverage of the given symbol
        /// </summary>
        /// <param name="symbol">Symbol to change the initial leverage for</param>
        /// <param name="leverage">The amount of initial leverage to change to</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Result of the initial leverage change request</returns>
        Task<WebCallResult<BinanceFuturesInitialLeverageChangeResult>> ChangeInitialLeverageAsync(string symbol, int leverage, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Change the margin type for an open position
        /// </summary>
        /// <param name="symbol">Symbol to change the position type for</param>
        /// <param name="marginType">The type of margin to use</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Whether the request was successful</returns>
        WebCallResult<BinanceFuturesChangeMarginTypeResult> ChangeMarginType(string symbol, FuturesMarginType marginType, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Change the margin type for an open position
        /// </summary>
        /// <param name="symbol">Symbol to change the position type for</param>
        /// <param name="marginType">The type of margin to use</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Whether the request was successful</returns>
        Task<WebCallResult<BinanceFuturesChangeMarginTypeResult>> ChangeMarginTypeAsync(string symbol, FuturesMarginType marginType, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Change the margin on an open position
        /// </summary>
        /// <param name="symbol">Symbol to adjust the position margin for</param>
        /// <param name="amount">The amount of margin to be used</param>
        /// <param name="type">Whether to reduce or add margin to the position</param>
        /// <param name="positionSide">Default BOTH for One-way Mode ; LONG or SHORT for Hedge Mode. It must be sent with Hedge Mode.</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The new position margin</returns>
        WebCallResult<BinanceFuturesPositionMarginResult> ModifyPositionMargin(string symbol, decimal amount, FuturesMarginChangeDirectionType type, PositionSide? positionSide = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Change the margin on an open position
        /// </summary>
        /// <param name="symbol">Symbol to adjust the position margin for</param>
        /// <param name="amount">The amount of margin to be used</param>
        /// <param name="type">Whether to reduce or add margin to the position</param>
        /// <param name="positionSide">Default BOTH for One-way Mode ; LONG or SHORT for Hedge Mode. It must be sent with Hedge Mode.</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The new position margin</returns>
        Task<WebCallResult<BinanceFuturesPositionMarginResult>> ModifyPositionMarginAsync(string symbol, decimal amount, FuturesMarginChangeDirectionType type, PositionSide? positionSide = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Requests the margin change history for a specific symbol
        /// </summary>
        /// <param name="symbol">Symbol to get margin history for</param>
        /// <param name="type">Filter the history by the direction of margin change</param>
        /// <param name="startTime">Margin changes newer than this date will be retrieved</param>
        /// <param name="endTime">Margin changes older than this date will be retrieved</param>
        /// <param name="limit">The max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of all margin changes for the symbol</returns>
        WebCallResult<IEnumerable<BinanceFuturesMarginChangeHistoryResult>> GetMarginChangeHistory(string symbol, FuturesMarginChangeDirectionType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Requests the margin change history for a specific symbol
        /// </summary>
        /// <param name="symbol">Symbol to get margin history for</param>
        /// <param name="type">Filter the history by the direction of margin change</param>
        /// <param name="startTime">Margin changes newer than this date will be retrieved</param>
        /// <param name="endTime">Margin changes older than this date will be retrieved</param>
        /// <param name="limit">The max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of all margin changes for the symbol</returns>
        Task<WebCallResult<IEnumerable<BinanceFuturesMarginChangeHistoryResult>>> GetMarginChangeHistoryAsync(string symbol, FuturesMarginChangeDirectionType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the income history for the futures account
        /// </summary>
        /// <param name="symbol">The symbol to get income history from</param>
        /// <param name="incomeType">The income type filter to apply to the request</param>
        /// <param name="startTime">Time to start getting income history from</param>
        /// <param name="endTime">Time to stop getting income history from</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The income history for the futures account</returns>
        WebCallResult<IEnumerable<BinanceFuturesIncomeHistory>> GetIncomeHistory(string? symbol = null, string? incomeType = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the income history for the futures account
        /// </summary>
        /// <param name="symbol">The symbol to get income history from</param>
        /// <param name="incomeType">The income type filter to apply to the request</param>
        /// <param name="startTime">Time to start getting income history from</param>
        /// <param name="endTime">Time to stop getting income history from</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The income history for the futures account</returns>
        Task<WebCallResult<IEnumerable<BinanceFuturesIncomeHistory>>> GetIncomeHistoryAsync(string? symbol = null, string? incomeType = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets Notional and Leverage Brackets
        /// </summary>
        /// <param name="symbolOrPair">The symbol or pair to get the data for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Notional and Leverage Brackets info</returns>
        WebCallResult<IEnumerable<BinanceFuturesSymbolBracket>> GetBrackets(string? symbolOrPair = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets Notional and Leverage Brackets.
        /// </summary>
        /// <param name="symbolOrPair">The symbol or pair to get the data for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Notional and Leverage Brackets</returns>
        Task<WebCallResult<IEnumerable<BinanceFuturesSymbolBracket>>> GetBracketsAsync(string? symbolOrPair = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get position ADL quantile estimations
        /// </summary>
        /// <param name="symbol">Only get for this symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        WebCallResult<IEnumerable<BinanceFuturesQuantileEstimation>> GetPositionAdlQuantileEstimation(
            string? symbol = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get position ADL quantile estimations
        /// </summary>
        /// <param name="symbol">Only get for this symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceFuturesQuantileEstimation>>> GetPositionAdlQuantileEstimationAsync(
            string? symbol = null, long? receiveWindow = null, CancellationToken ct = default);
    }
}