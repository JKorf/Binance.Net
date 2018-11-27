using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;

namespace Binance.Net
{
    public class BinanceAuthenticationProvider: AuthenticationProvider
    {
        private readonly object locker = new object();
        private readonly HMACSHA256 encryptor;

        public BinanceAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
            encryptor = new HMACSHA256(Encoding.ASCII.GetBytes(credentials.Secret.GetString()));
        }

        public override Dictionary<string, object> AddAuthenticationToParameters(string uri, string method, Dictionary<string, object> parameters, bool signed)
        {
            if (!signed)
                return parameters;

            var query = parameters.CreateParamString(true).Substring(1);
            parameters.Add("signature", ByteToString(encryptor.ComputeHash(Encoding.UTF8.GetBytes(query))));
            return parameters;
        }

        public override Dictionary<string, string> AddAuthenticationToHeaders(string uri, string method, Dictionary<string, object> parameters, bool signed)
        {
            var result = new Dictionary<string, string>();
            result.Add("X-MBX-APIKEY", Credentials.Key.GetString());
            return result;
        }
        
        public override string Sign(string toSign)
        {
            throw new System.NotImplementedException();
        }
    }
}
