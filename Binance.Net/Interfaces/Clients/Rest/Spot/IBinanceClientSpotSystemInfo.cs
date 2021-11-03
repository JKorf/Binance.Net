using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Objects;
using Binance.Net.Objects.Other;
using Binance.Net.Objects.Spot.MarketData;
using Binance.Net.Objects.Spot.WalletData;
using CryptoExchange.Net;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Binance.Net.Clients.Rest.Spot
{
    /// <summary>
    /// Spot system endpoints
    /// </summary>
    public interface IBinanceClientSpotSystemInfo
    {
        /// <summary>
        /// Gets the withdraw/deposit details for an asset
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Asset detail</returns>
        Task<WebCallResult<Dictionary<string, BinanceAssetDetails>>> GetAssetDetailsAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get general data for the products available on Binance
        /// NOTE: This is not an official endpoint and might be changed or removed at any point by Binance
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceProduct>>> GetProductsAsync(CancellationToken ct = default);

        /// <summary>
        /// Pings the Binance API
        /// </summary>
        /// <returns>True if successful ping, false if no response</returns>
        Task<WebCallResult<long>> PingAsync(CancellationToken ct = default);

        /// <summary>
        /// Requests the server for the local time. This function also determines the offset between server and local time and uses this for subsequent API calls
        /// </summary>
        /// <param name="resetAutoTimestamp">Whether the response should be used for a new auto timestamp calculation</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Server time</returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(bool resetAutoTimestamp = false, CancellationToken ct = default);

        /// <summary>
        /// Get's information about the exchange including rate limits and information on the provided symbol
        /// </summary>
        /// <param name="symbol">Symbol to get data for token</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get's information about the exchange including rate limits and information on the provided symbols
        /// </summary>
        /// <param name="symbols">Symbols to get data for token</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(IEnumerable<string> symbols, CancellationToken ct = default);

        /// <summary>
        /// Get's information about the exchange including rate limits and symbol list
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets the status of the Binance platform
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The system status</returns>
        Task<WebCallResult<BinanceSystemStatus>> GetSystemStatusAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets the trading status for the current account
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The trading status of the account</returns>
        Task<WebCallResult<BinanceTradingStatus>> GetTradingStatusAsync(int? receiveWindow = null, CancellationToken ct = default);

    }
}
