using Binance.Net.Interfaces.SubClients;
using Binance.Net.Interfaces.SubClients.Futures;
using Binance.Net.Interfaces.SubClients.IsolatedMargin;
using Binance.Net.Interfaces.SubClients.Margin;
using Binance.Net.Interfaces.SubClients.Spot;
using CryptoExchange.Net.Interfaces;

namespace Binance.Net.Interfaces
{
    /// <summary>
    /// Binance interface
    /// </summary>
    public interface IBinanceClient: IRestClient
    {
        /// <summary>
        /// System endpoints
        /// </summary>
        IBinanceClientSystem System { get; }

        /// <summary>
        /// Account endpoints
        /// </summary>
        IBinanceClientAccount Account { get; }

        /// <summary>
        /// Sub account endpoints
        /// </summary>
        IBinanceClientSubAccounts SubAccounts { get; }

        /// <summary>
        /// Margin endpoints
        /// </summary>
        IBinanceClientMargin Margin { get; }

        /// <summary>
        /// Isolated Margin endpoints
        /// </summary>
        IBinanceClientIsolatedMargin IsolatedMargin { get; }

        /// <summary>
        /// Spot endpoints
        /// </summary>
        IBinanceClientSpot Spot { get; }

        /// <summary>
        /// Lending endpoints
        /// </summary>
        IBinanceClientLending Lending { get; }

        /// <summary>
        /// Mining endpoints
        /// </summary>
        IBinanceClientMining Mining { get; }

        /// <summary>
        /// Dust endpoints
        /// </summary>
        IBinanceClientDust Dust { get; }

        /// <summary>
        /// Withdraw endpoints
        /// </summary>
        IBinanceClientWithdraw Withdraw { get; }

        /// <summary>
        /// Deposit endpoints
        /// </summary>
        IBinanceClientDeposit Deposit { get; }

        /// <summary>
        /// Brokerage endpoints
        /// </summary>
        IBinanceClientBrokerage Brokerage { get; }

        /// <summary>
        /// Futures endpoints
        /// </summary>
        IBinanceClientFutures Futures { get; }

        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        void SetApiCredentials(string apiKey, string apiSecret);
    }
}