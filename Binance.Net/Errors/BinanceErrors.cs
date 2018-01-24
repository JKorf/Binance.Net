using System.Collections.Generic;
using System.Linq;
using Binance.Net.Objects;

namespace Binance.Net.Errors
{
    internal static class BinanceErrors
    {
        internal static Dictionary<BinanceErrorKey, BinanceError> ErrorRegistrations =
            new Dictionary<BinanceErrorKey, BinanceError>()
            {
                { BinanceErrorKey.NoApiCredentialsProvided, new BinanceError(5000, "No api credentials provided, can't request private endpoints")},
                { BinanceErrorKey.NoListenKey, new BinanceError(5001, "Listen key is null or empty. Cannot start stream without listen key. Call the StartUserStream function and try again")},
                { BinanceErrorKey.MissingRequiredParameter, new BinanceError(5002, "Missing a required parameter.")},
                { BinanceErrorKey.FailedTradingRules, new BinanceError(5003, "The order does not comply to all trading rules")},

                { BinanceErrorKey.ErrorWeb, new BinanceError(6001, "Server returned a not successful status")},
                { BinanceErrorKey.CantConnectToServer, new BinanceError(6002, "Can't connect to server")},

                { BinanceErrorKey.ParseErrorReader, new BinanceError(7000, "Error reading the returned data. Data was not valid Json")},
                { BinanceErrorKey.ParseErrorSerialization, new BinanceError(7001, "Error parsing the returned data to object.")},

                { BinanceErrorKey.UnknownError, new BinanceError(8000, "An unknown error happened")},
            };

        internal static BinanceError GetError(BinanceErrorKey key)
        {
            return ErrorRegistrations.Single(e => e.Key == key).Value;
        }
    }
}
