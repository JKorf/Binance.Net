namespace Binance.Net.Interfaces.Clients.GeneralApi
{
    /// <summary>
    /// Binance general API endpoints
    /// </summary>
    public interface IBinanceRestClientGeneralApi : IRestApiClient, IDisposable
    {
        /// <summary>
        /// Endpoints related to brokerage
        /// </summary>
        /// <see cref="IBinanceRestClientGeneralApiBrokerage"/>
        public IBinanceRestClientGeneralApiBrokerage Brokerage { get; }

        /// <summary>
        /// Endpoints related to futures account interactions
        /// </summary>
        /// <see cref="IBinanceRestClientGeneralApiFutures"/>
        public IBinanceRestClientGeneralApiFutures Futures { get; }

        /// <summary>
        /// Endpoints related to crypto loans
        /// </summary>
        /// <see cref="IBinanceRestClientGeneralApiLoans"/>
        public IBinanceRestClientGeneralApiLoans CryptoLoans { get; }

        /// <summary>
        /// Endpoints related to auto invest
        /// </summary>
        /// <see cref="IBinanceRestClientGeneralApiAutoInvest"/>
        public IBinanceRestClientGeneralApiAutoInvest AutoInvest { get; }

        /// <summary>
        /// Endpoints related to mining
        /// </summary>
        /// <see cref="IBinanceRestClientGeneralApiMining"/>
        public IBinanceRestClientGeneralApiMining Mining { get; }

        /// <summary>
        /// Endpoints related to requesting data for and controlling sub accounts
        /// </summary>
        /// <see cref="IBinanceRestClientGeneralApiSubAccount"/>
        public IBinanceRestClientGeneralApiSubAccount SubAccount { get; }

        /// <summary>
        /// Endpoints related to staking
        /// </summary>
        /// <see cref="IBinanceRestClientGeneralApiStaking"/>
        IBinanceRestClientGeneralApiStaking Staking { get; }

        /// <summary>
        /// Endpoints related to Binance Simple Earn
        /// </summary>
        /// <see cref="IBinanceRestClientGeneralApiSimpleEarn"/>
        IBinanceRestClientGeneralApiSimpleEarn SimpleEarn { get; }

        /// <summary>
        /// Endpoints related to Binance Copy Trading
        /// </summary>
        /// <see cref="IBinanceRestClientGeneralApiCopyTrading"/>
        IBinanceRestClientGeneralApiCopyTrading CopyTrading { get; }

        /// <summary>
        /// Endpoints related to Binance Gift Cards
        /// </summary>
        public IBinanceRestClientGeneralApiGiftCard GiftCard { get; }
    }
}
