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
        /// ["<c>TRADING</c>"] Trading
        /// </summary>
        [Map("TRADING")]
        Trading,       
        /// <summary>
        /// ["<c>HALT</c>"] Halted
        /// </summary>
        [Map("HALT")]
        Halt,        
        /// <summary>
        /// ["<c>BREAK</c>"] 
        /// </summary>
        [Map("BREAK")]
        Break        
    }
}

