using Binance.Net.Logging;
using System;
using System.IO;

namespace Binance.Net
{
    public static class BinanceDefaults
    {
        internal static string ApiKey { get; private set; }
        internal static string ApiSecret { get; private set; }

        internal static bool LogVerbositySet { get; private set; }
        internal static LogVerbosity LogVerbosity { get; private set; }
        internal static TextWriter LogWriter { get; private set; }

        /// <summary>
        /// Sets the API credentials to use. Api keys can be managed at https://www.binance.com/userCenter/createApi.html
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret associated with the key</param>
        public static void SetDefaultApiCredentials(string apiKey, string apiSecret)
        {
            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
                throw new ArgumentException("Api key or secret empty");

            ApiKey = apiKey;
            ApiSecret = apiSecret;
        }

        /// <summary>
        /// Sets the default log verbosity for all new clients
        /// </summary>
        /// <param name="logVerbosity">The minimal verbosity to log</param>
        public static void SetDefaultLogVerbosity(LogVerbosity logVerbosity)
        {
            LogVerbositySet = true;
            LogVerbosity = logVerbosity;
        }

        /// <summary>
        /// Sets the default log output for all new clients
        /// </summary>
        /// <param name="writer">The output writer</param>
        public static void SetDefaultLogOutput(TextWriter writer)
        {
            LogWriter = writer;
        }
    }
}
