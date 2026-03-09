using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Transfer account type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TransferAccountType>))]
    public enum TransferAccountType
    {
        /// <summary>
        /// ["<c>SPOT</c>"] Spot
        /// </summary>
        [Map("SPOT")]
        Spot,
        /// <summary>
        /// ["<c>USDT_FUTURE</c>"] USDT-M future
        /// </summary>
        [Map("USDT_FUTURE")]
        UsdtFuture,
        /// <summary>
        /// ["<c>COIN_FUTURE</c>"] Coin-M future
        /// </summary>
        [Map("COIN_FUTURE")]
        CoinFuture,
        /// <summary>
        /// ["<c>MARGIN</c>"] Margin
        /// </summary>
        [Map("MARGIN")]
        Margin,
        /// <summary>
        /// ["<c>ISOLATED_MARGIN</c>"] Isolated margin
        /// </summary>
        [Map("ISOLATED_MARGIN")]
        IsolatedMargin
    }
}

