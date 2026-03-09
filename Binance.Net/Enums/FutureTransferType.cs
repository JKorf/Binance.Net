using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Futures account transfer type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesTransferType>))]
    public enum FuturesTransferType
    {
        /// <summary>
        /// ["<c>1</c>"] From spot to USDT-M futures account
        /// </summary>
        [Map("1")]
        FromSpotToUsdtFutures,
        /// <summary>
        /// ["<c>2</c>"] From USDT-M futures to spot account
        /// </summary>
        [Map("2")]
        FromUsdtFuturesToSpot,
        /// <summary>
        /// ["<c>3</c>"] From spot to COIN-M futures account
        /// </summary>
        [Map("3")]
        FromSpotToCoinFutures,
        /// <summary>
        /// ["<c>4</c>"] From COIN-M futures to spot account
        /// </summary>
        [Map("4")]
        FromCoinFuturesToSpot
    }
}

