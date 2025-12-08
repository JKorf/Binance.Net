using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Status of a symbol
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SymbolStatusFilter>))]
    public enum SymbolStatusFilter
    {
        /// <summary>
        /// Trading
        /// </summary>
        [Map("TRADING")]
        Trading,       
        /// <summary>
        /// Halted
        /// </summary>
        [Map("HALT")]
        Halt,        
        /// <summary>
        /// 
        /// </summary>
        [Map("BREAK")]
        Break        
    }
}
