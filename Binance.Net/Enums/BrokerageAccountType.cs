using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Brokerage account type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<BrokerageAccountType>))]
    public enum BrokerageAccountType
    {
        /// <summary> 
        /// ["<c>SPOT</c>"] Spot 
        /// </summary>
        [Map("SPOT")]
        Spot,
        /// <summary> 
        /// ["<c>USDT_FUTURE</c>"] Futures USDT
        /// </summary>
        [Map("USDT_FUTURE")]
        FuturesUsdt,
        /// <summary>
        /// ["<c>COIN_FUTURE</c>"] Futures Coin
        /// </summary>
        [Map("COIN_FUTURE")]
        FuturesCoin
    }
}
