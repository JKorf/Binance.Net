namespace Binance.Net.Errors
{
    public enum BinanceErrorKey
    {
        NoApiCredentialsProvided,
        NoListenKey,
        MissingRequiredParameter,

        ErrorWeb,

        ParseErrorReader,
        ParseErrorSerialization,
        CantConnectToServer,

        UnknownError
    }
}
