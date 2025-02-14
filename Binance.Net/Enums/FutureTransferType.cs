using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Futures account transfer type
    /// </summary>
    [JsonConverter(typeof(PocAOTEnumConverter<FuturesTransferType>))] public  enum FuturesTransferType
    {
        /// <summary>
        /// From spot to USDT-M futures account
        /// </summary>
        [Map("1")]
        FromSpotToUsdtFutures,
        /// <summary>
        /// From USDT-M futures to spot account
        /// </summary>
        [Map("2")]
        FromUsdtFuturesToSpot,
        /// <summary>
        /// From spot to COIN-M futures account
        /// </summary>
        [Map("3")]
        FromSpotToCoinFutures,
        /// <summary>
        /// From COIN-M futures to spot account
        /// </summary>
        [Map("4")]
        FromCoinFuturesToSpot
    }
}
