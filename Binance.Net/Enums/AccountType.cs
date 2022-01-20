namespace Binance.Net.Enums
{
    /// <summary>
    /// Type of account
    /// </summary>
    public enum AccountType
    {
        /// <summary>
        /// Spot account type
        /// </summary>
        Spot,
        /// <summary>
        /// Margin account type
        /// </summary>>
        Margin,
        /// <summary>
        /// Futures account type
        /// </summary>
        Futures,
        /// <summary>
        /// Leveraged account type
        /// </summary>
        Leveraged,
        /// <summary>
        /// See https://github.com/binance/binance-spot-api-docs/blob/master/rest-api.md#enum-definitions
        /// </summary>
        TRD_GRP_002
    }
}
