using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using System.Security;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Binance Api credentials
    /// </summary>
    public class BinanceApiCredentials : ApiCredentials
    {
        /// <summary>
        /// Type of the credentials
        /// </summary>
        public ApiCredentialsType Type { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="key">The API key</param>
        /// <param name="secret">The API secret (Hmac type) or the RSA private key (Rsa type). When running from .net framework the private key should be in xml format.</param>
        /// <param name="type">The type of authentication</param>
        public BinanceApiCredentials(SecureString key, SecureString secret, ApiCredentialsType type = ApiCredentialsType.Hmac) : base(key, secret)
        {
            Type = type;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="key">The API key</param>
        /// <param name="secret">The API secret (Hmac type) or the RSA private key (Rsa type). When running from .net framework the private key should be in xml format.</param>
        /// <param name="type">The type of authentication</param>
        public BinanceApiCredentials(string key, string secret, ApiCredentialsType type = ApiCredentialsType.Hmac) : base(key, secret)
        {
            Type = type;
        }

        /// <inheritdoc />
        public override ApiCredentials Copy()
        { 
            return new BinanceApiCredentials(Key!.GetString(), Secret!.GetString(), Type);
        }
    }

    /// <summary>
    /// Credentials type
    /// </summary>
    public enum ApiCredentialsType
    {
        /// <summary>
        /// Hmac keys credentials
        /// </summary>
        Hmac,
        /// <summary>
        /// Rsa keys credentials
        /// </summary>
        Rsa
    }
}
