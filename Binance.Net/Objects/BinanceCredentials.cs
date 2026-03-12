using CryptoExchange.Net.Authentication;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Binance API credentials
    /// </summary>
    public class BinanceCredentials : ApiCredentials
    {
        public ApiCredentialsType CredentialType => CredentialPairs.First().CredentialType;
        public string ApiKey =>
            CredentialType == ApiCredentialsType.Hmac ? GetCredential<HMACCredential>()?.Key!
            : CredentialType == ApiCredentialsType.Rsa ? GetCredential<RSACredential>()?.PublicKey!
#if NET8_0_OR_GREATER
            : CredentialType == ApiCredentialsType.Ed25519 ? GetCredential<ED25519Credential>()?.PublicKey!
#endif
            : null!;
        
        public BinanceCredentials() { }

        public BinanceCredentials(string apiKey, string secretKey)
            : this(new HMACCredential(apiKey, secretKey)) { }

        public BinanceCredentials(HMACCredential hmacCredential)
            : base(hmacCredential) 
        {
        }

        public BinanceCredentials(RSACredential rsaCredential)
            : base(rsaCredential)
        {
        }

#if NET8_0_OR_GREATER
        public BinanceCredentials(ED25519Credential ed25519Credential)
            : base(ed25519Credential)
        {
        }
#endif

        /// <inheritdoc />
        public override ApiCredentials Copy() =>
            CredentialType switch
            {
                ApiCredentialsType.Hmac => new BinanceCredentials(GetCredential<HMACCredential>()!),
                ApiCredentialsType.Rsa => new BinanceCredentials(GetCredential<RSACredential>()!),
#if NET8_0_OR_GREATER
                ApiCredentialsType.Ed25519 => new BinanceCredentials(GetCredential<ED25519Credential>()!),
#endif
            };
    }
}
