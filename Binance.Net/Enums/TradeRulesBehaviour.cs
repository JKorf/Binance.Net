namespace Binance.Net.Enums
{
    /// <summary>
    /// Trade rules behaviour
    /// </summary>
    public enum TradeRulesBehaviour
    {
        /// <summary>
        /// None
        /// </summary>
        None,
        /// <summary>
        /// Throw an error if not complying
        /// </summary>
        ThrowError,
        /// <summary>
        /// Auto adjust order when not complying
        /// </summary>
        AutoComply
    }
}
