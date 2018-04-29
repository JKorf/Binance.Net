using System.Security.Cryptography;
using System.Text;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;

namespace Binance.Net
{
    public class BinanceAuthenticationProvider: AuthenticationProvider
    {
        private readonly object locker = new object();
        private readonly HMACSHA256 encryptor;

        public BinanceAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
            encryptor = new HMACSHA256(Encoding.ASCII.GetBytes(credentials.Secret));
        }

        public override string AddAuthenticationToUriString(string uri, bool signed)
        {
            if (!signed)
                return uri;

            if (!uri.Contains("?"))
                uri += "?";

            var query = uri.Split('?');

            if (!uri.EndsWith("?"))
                uri += "&";
            
            lock(locker)
                uri += $"signature={ByteToString(encryptor.ComputeHash(Encoding.UTF8.GetBytes(query.Length > 1 ? query[1]: "")))}";
            return uri;
        }

        public override IRequest AddAuthenticationToRequest(IRequest request, bool signed)
        {
            request.Headers.Add("X-MBX-APIKEY", Credentials.Key);
            return request;
        }

        public override string Sign(string toSign)
        {
            throw new System.NotImplementedException();
        }
    }
}
