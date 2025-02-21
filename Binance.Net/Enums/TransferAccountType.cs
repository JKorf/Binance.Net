using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Transfer account type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TransferAccountType>))] public  enum TransferAccountType
    {
        /// <summary>
        /// Spot
        /// </summary>
        [Map("SPOT")]
        Spot,
        /// <summary>
        /// USDT-M future
        /// </summary>
        [Map("USDT_FUTURE")]
        UsdtFuture,
        /// <summary>
        /// Coin-M future
        /// </summary>
        [Map("COIN_FUTURE")]
        CoinFuture,
        /// <summary>
        /// Margin
        /// </summary>
        [Map("MARGIN")]
        Margin,
        /// <summary>
        /// Isolated margin
        /// </summary>
        [Map("ISOLATED_MARGIN")]
        IsolatedMargin
    }
}
