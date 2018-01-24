namespace Binance.Net.Errors
{
    public enum BinanceErrorKey
    {
        NoApiCredentialsProvided,
        NoListenKey,
        MissingRequiredParameter,
        FailedTradingRules,

        ErrorWeb,

        ParseErrorReader,
        ParseErrorSerialization,
        CantConnectToServer,

        UnknownError
    }
}
