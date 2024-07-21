using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// The type of project
    /// </summary>
    public enum ProjectType
    {
        /// <summary>
        /// Regular
        /// </summary>
        [Map("ACTIVITY")]
        Activity,
        /// <summary>
        /// Customized fixed
        /// </summary>
        [Map("CUSTOMIZED_FIXED")]
        CustomizedFixed
    }
}
