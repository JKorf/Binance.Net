using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Interfaces.SubClients.Margin
{
    /// <summary>
    /// Margin user stream interface
    /// </summary>
    public interface IBinanceClientMarginUserStream
    {
        /// <summary>
        /// Starts a user stream  for margin account by requesting a listen key. 
        /// This listen key can be used in subsequent requests to 
        /// <see cref="BinanceSocketClient.SubscribeToUserDataUpdates"/>. 
        /// The stream will close after 60 minutes unless a keep alive is send.
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Listen key</returns>
        WebCallResult<string> StartMarginUserStream(CancellationToken ct = default);

        /// <summary>
        /// Starts a user stream  for margin account by requesting a listen key. 
        /// This listen key can be used in subsequent requests to 
        /// <see cref="BinanceSocketClient.SubscribeToUserDataUpdates"/>. 
        /// The stream will close after 60 minutes unless a keep alive is send.
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Listen key</returns>
        Task<WebCallResult<string>> StartMarginUserStreamAsync(CancellationToken ct = default);

        /// <summary>
        /// Sends a keep alive for the current user for margin account stream listen key to keep the stream from closing. 
        /// Stream auto closes after 60 minutes if no keep alive is send. 30 minute interval for keep alive is recommended.
        /// </summary>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        WebCallResult<object> KeepAliveMarginUserStream(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Sends a keep alive for the current user stream for margin account listen key to keep the stream from closing. 
        /// Stream auto closes after 60 minutes if no keep alive is send. 30 minute interval for keep alive is recommended.
        /// </summary>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<object>> KeepAliveMarginUserStreamAsync(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Close the user stream for margin account
        /// </summary>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        WebCallResult<object> CloseMarginUserStream(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Close the user stream for margin account
        /// </summary>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<object>> CloseMarginUserStreamAsync(string listenKey, CancellationToken ct = default);
    }
}