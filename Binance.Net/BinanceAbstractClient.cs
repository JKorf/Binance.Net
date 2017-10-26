using Binance.Net.Logging;
using Binance.Net.Objects;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Binance.Net
{
    public abstract class BinanceAbstractClient
    {
        protected string key;
        protected HMACSHA256 encryptor;

        internal Log log;

        protected BinanceAbstractClient()
        {
            log = new Log();

            if (BinanceDefaults.LogWriter != null)
                SetLogOutput(BinanceDefaults.LogWriter);

            if (BinanceDefaults.LogVerbositySet)
                SetLogVerbosity(BinanceDefaults.LogVerbosity);

            if (BinanceDefaults.ApiKey != null && BinanceDefaults.ApiSecret != null)
                SetAPICredentials(BinanceDefaults.ApiKey, BinanceDefaults.ApiSecret);
        }

        /// <summary>
        /// Sets the API credentials to use. Api keys can be managed at https://www.binance.com/userCenter/createApi.html
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret associated with the key</param>
        public void SetAPICredentials(string apiKey, string apiSecret)
        {
            SetAPIKey(apiKey);
            SetAPISecret(apiSecret);
        }

        /// <summary>
        /// Sets the API Key. Api keys can be managed at https://www.binance.com/userCenter/createApi.html
        /// </summary>
        /// <param name="apiKey">The api key</param>
        public void SetAPIKey(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentException("Api key empty");

            key = apiKey;
        }

        /// <summary>
        /// Sets the API Secret. Api keys can be managed at https://www.binance.com/userCenter/createApi.html
        /// </summary>
        /// <param name="apiSecret">The api secret</param>
        public void SetAPISecret(string apiSecret)
        {
            if (string.IsNullOrEmpty(apiSecret))
                throw new ArgumentException("Api secret empty");

            encryptor = new HMACSHA256(Encoding.UTF8.GetBytes(apiSecret));
        }

        /// <summary>
        /// Sets the verbosity of the log messages
        /// </summary>
        /// <param name="verbosity">Verbosity level</param>
        public void SetLogVerbosity(LogVerbosity verbosity)
        {
            log.Level = verbosity;
        }

        /// <summary>
        /// Sets the log output
        /// </summary>
        /// <param name="writer">The output writer</param>
        public void SetLogOutput(TextWriter writer)
        {
            log.TextWriter = writer;
        }

        protected ApiResult<T> ThrowErrorMessage<T>(string message)
        {
            log.Write(LogVerbosity.Warning, $"Call failed: {message}");
            var result = (ApiResult<T>)Activator.CreateInstance(typeof(ApiResult<T>));
            result.Error = new BinanceError()
            {
                Message = message
            };
            return result;
        }
    }
}
