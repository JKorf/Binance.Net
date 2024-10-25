using Binance.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;

namespace Binance.Net.Interfaces
{
    /// <summary>
    /// Binance order book factory
    /// </summary>
    public interface IBinanceOrderBookFactory
    {
        /// <summary>
        /// Spot order book factory methods
        /// </summary>
        public IOrderBookFactory<BinanceOrderBookOptions> Spot { get; }
        /// <summary>
        /// USD Futures order book factory methods
        /// </summary>
        public IOrderBookFactory<BinanceOrderBookOptions> UsdFutures { get; }
        /// <summary>
        /// Coin Futures order book factory methods
        /// </summary>
        public IOrderBookFactory<BinanceOrderBookOptions> CoinFutures { get; }

        /// <summary>
        /// Create a SymbolOrderBook for the symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="options">Book options</param>
        /// <returns></returns>
        ISymbolOrderBook Create(SharedSymbol symbol, Action<BinanceOrderBookOptions>? options = null);

        /// <summary>
        /// Create a Spot SymbolOrderBook
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="options">Book options</param>
        /// <returns></returns>
        ISymbolOrderBook CreateSpot(string symbol, Action<BinanceOrderBookOptions>? options = null);

        /// <summary>
        /// Create a Usdt Futures SymbolOrderBook
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="options">Book options</param>
        /// <returns></returns>
        ISymbolOrderBook CreateUsdtFutures(string symbol, Action<BinanceOrderBookOptions>? options = null);

        /// <summary>
        /// Create a Coin Futures SymbolOrderBook
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="options">Book options</param>
        /// <returns></returns>
        ISymbolOrderBook CreateCoinFutures(string symbol, Action<BinanceOrderBookOptions>? options = null);
    }
}