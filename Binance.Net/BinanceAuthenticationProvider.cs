using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;

namespace Binance.Net
{
    internal class BinanceAuthenticationProvider: AuthenticationProvider
    {
        private readonly HMACSHA256 encryptor;
        private readonly ArrayParametersSerialization arraySerialization;

        public BinanceAuthenticationProvider(ApiCredentials credentials, ArrayParametersSerialization arraySerialization) : base(credentials)
        {
            if (credentials.Secret == null)
                throw new ArgumentException("No valid API credentials provided. Key/Secret needed.");

            encryptor = new HMACSHA256(Encoding.ASCII.GetBytes(credentials.Secret.GetString()));
            this.arraySerialization = arraySerialization;
        }

        public override Dictionary<string, object> AddAuthenticationToParameters(string uri, HttpMethod method, Dictionary<string, object> parameters, bool signed)
        {
            if (!signed)
                return parameters;

            var query = parameters.CreateParamString(true, arraySerialization);
            parameters.Add("signature", ByteToString(encryptor.ComputeHash(Encoding.UTF8.GetBytes(query))));
            return parameters;
        }

        public override Dictionary<string, string> AddAuthenticationToHeaders(string uri, HttpMethod method, Dictionary<string, object> parameters, bool signed)
        {
            if (Credentials.Key == null)
                throw new ArgumentException("No valid API credentials provided. Key/Secret needed.");

            return new Dictionary<string, string> {{"X-MBX-APIKEY", Credentials.Key.GetString()}};
        }
        
        public override string Sign(string toSign)
        {
            throw new System.NotImplementedException();
        }
    }
}
