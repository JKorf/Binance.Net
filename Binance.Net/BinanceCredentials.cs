using System.Net;

namespace Binance.Net
{
    /// <summary>
    /// Binance API credentials
    /// </summary>
    public class BinanceCredentials : ApiCredentials
    {
        internal CredentialSet? Credential { get; set; }

        /// <summary>
        /// HMAC credentials
        /// </summary>
        public HMACCredential? HMAC
        {
            get => Credential as HMACCredential;
            set { if (value != null) Credential = value; }
        }

        /// <summary>
        /// RSA credentials in XML format
        /// </summary>
        public RSAXmlCredential? RSAXml
        {
            get => Credential as RSAXmlCredential;
            set { if (value != null) Credential = value; }
        }

#if NETSTANDARD2_1_OR_GREATER || NET7_0_OR_GREATER
        /// <summary>
        /// RSA credentials in PEM/Base64 format
        /// </summary>
        public RSAPemCredential? RSAPem
        {
            get => Credential as RSAPemCredential;
            set { if (value != null) Credential = value; }
        }
#endif

#if NET8_0_OR_GREATER
        /// <summary>
        /// Ed25519 credentials
        /// </summary>
        public Ed25519Credential? Ed25519
        {
            get => Credential as Ed25519Credential;
            set { if (value != null) Credential = value; }
        }
#endif

        /// <summary>
        /// Create new credentials
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public BinanceCredentials() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        
        /// <summary>
        /// Create new credentials providing HMAC credentials
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="secret">API secret</param>
        public BinanceCredentials(string key, string secret)
        {
            Credential = new HMACCredential(key, secret);
        }

        /// <summary>
        /// Create new credentials providing HMAC credentials
        /// </summary>
        /// <param name="credential">HMAC Credentials</param>
        public BinanceCredentials(HMACCredential credential)
        {
            Credential = credential;
        }

        /// <summary>
        /// Create new credentials providing RSA credentials in XML format
        /// </summary>
        /// <param name="credential">RSA Credentials in XML format</param>
        public BinanceCredentials(RSAXmlCredential credential)
        {
            Credential = credential;
        }

#if NETSTANDARD2_1_OR_GREATER || NET7_0_OR_GREATER
        /// <summary>
        /// Create new credentials providing RSA credentials in PEM/Base64 format
        /// </summary>
        /// <param name="credential">RSA Credentials in PEM/Base64 format</param>
        public BinanceCredentials(RSAPemCredential credential)
        {
            Credential = credential;
        }
#endif

#if NET8_0_OR_GREATER
        /// <summary>
        /// Create new credentials providing Ed25519 credentials
        /// </summary>
        /// <param name="credential">RSA Credentials in PEM/Base64 format</param>
        public BinanceCredentials(Ed25519Credential credential)
        {
            Credential = credential;
        }
#endif

        /// <summary>
        /// Specify the HMAC credentials
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="secret">API secret</param>
        public BinanceCredentials WithHMAC(string key, string secret)
        {
            if (Credential != null) throw new InvalidOperationException("Credentials already set");

            Credential = new HMACCredential(key, secret);
            return this;
        }

        /// <summary>
        /// Specify the RSA credentials in XML format
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="privateKey">Private key</param>
        public BinanceCredentials WithRSAXml(string key, string privateKey)
        {
            if (Credential != null) throw new InvalidOperationException("Credentials already set");

            Credential = new RSAXmlCredential(key, privateKey);
            return this;
        }

#if NETSTANDARD2_1_OR_GREATER || NET7_0_OR_GREATER
        /// <summary>
        /// Specify the RSA credentials in PEM/Base64 format
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="privateKey">Private key</param>
        public BinanceCredentials WithRSAPem(string key, string privateKey)
        {
            if (Credential != null) throw new InvalidOperationException("Credentials already set");

            Credential = new RSAPemCredential(key, privateKey);
            return this;
        }
#endif

#if NET8_0_OR_GREATER
        /// <summary>
        /// Specify the RSA credentials in Ed25519 format
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="privateKey">Private key</param>
        public BinanceCredentials WithEd25519(string key, string privateKey)
        {
            if (Credential != null) throw new InvalidOperationException("Credentials already set");

            Credential = new Ed25519Credential(key, privateKey);
            return this;
        }
#endif

        /// <inheritdoc />
        public override ApiCredentials Copy() => new BinanceCredentials { Credential = Credential };

        /// <inheritdoc />
        public override void Validate()
        {
            if (Credential == null)
                throw new ArgumentException($"No credentials provided on {GetType().Name}");

            Credential.Validate();
        }
    }
}
