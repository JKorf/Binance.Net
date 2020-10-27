using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Interfaces.SubClients.IsolatedMargin
{
    /// <summary>
    /// Isolated margin user stream interface
    /// </summary>
    public interface IBinanceClientIsolatedMarginUserStream
    {
        /// <summary>
        /// Starts a user stream  for margin account by requesting a listen key. 
        /// This listen key can be used in subsequent requests to BinanceSocketClient.Spot.SubscribeToUserDataUpdates. 
        /// The stream will close after 60 minutes unless a keep alive is send.
        /// </summary>
        /// <param name="symbol">The isolated symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Listen key</returns>
        WebCallResult<string> StartIsolatedMarginUserStream(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Starts a user stream  for margin account by requesting a listen key. 
        /// This listen key can be used in subsequent requests to  BinanceSocketClient.Spot.SubscribeToUserDataUpdates  
        /// The stream will close after 60 minutes unless a keep alive is send.
        /// </summary>
        /// <param name="symbol">The isolated symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Listen key</returns>
        Task<WebCallResult<string>> StartIsolatedMarginUserStreamAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Sends a keep alive for the current user for margin account stream listen key to keep the stream from closing. 
        /// Stream auto closes after 60 minutes if no keep alive is send. 30 minute interval for keep alive is recommended.
        /// </summary>
        /// <param name="symbol">The isolated symbol</param>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        WebCallResult<object> KeepAliveIsolatedMarginUserStream(string symbol, string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Sends a keep alive for the current user stream for margin account listen key to keep the stream from closing. 
        /// Stream auto closes after 60 minutes if no keep alive is send. 30 minute interval for keep alive is recommended.
        /// </summary>
        /// <param name="symbol">The isolated symbol</param>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<object>> KeepAliveIsolatedMarginUserStreamAsync(string symbol, string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Close the user stream for margin account
        /// </summary>
        /// <param name="symbol">The isolated symbol</param>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        WebCallResult<object> CloseIsolatedMarginUserStream(string symbol, string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Close the user stream for margin account
        /// </summary>
        /// <param name="symbol">The isolated symbol</param>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<object>> CloseIsolatedMarginUserStreamAsync(string symbol, string listenKey, CancellationToken ct = default);
    }
}