using Binance.Net.Interfaces.SubClients;
using Binance.Net.Interfaces.SubClients.Futures;
using Binance.Net.Interfaces.SubClients.IsolatedMargin;
using Binance.Net.Interfaces.SubClients.Margin;
using Binance.Net.Interfaces.SubClients.Spot;
using Binance.Net.SubClients;
using CryptoExchange.Net.Interfaces;

namespace Binance.Net.Interfaces
{
    /// <summary>
    /// Binance interface
    /// </summary>
    public interface IBinanceClient: IRestClient
    {
        /// <summary>
        /// General endpoints
        /// </summary>
        IBinanceClientGeneral General { get; }

        /// <summary>
        /// Sub account endpoints
        /// </summary>
        IBinanceClientSubAccount SubAccount { get; }

        /// <summary>
        /// (Isolated) Margin endpoints
        /// </summary>
        IBinanceClientMargin Margin { get; }

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
        /// Withdraw/Deposit endpoints
        /// </summary>
        IBinanceClientWithdrawDeposit WithdrawDeposit { get; }

        /// <summary>
        /// Brokerage endpoints
        /// </summary>
        IBinanceClientBrokerage Brokerage { get; }

        /// <summary>
        /// Usdt-M futures endpoints
        /// </summary>
        IBinanceClientFuturesUsdt FuturesUsdt { get; }
        /// <summary>
        /// Coin-M futures endpoints
        /// </summary>
        IBinanceClientFuturesCoin FuturesCoin { get; }

        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        void SetApiCredentials(string apiKey, string apiSecret);
    }
}