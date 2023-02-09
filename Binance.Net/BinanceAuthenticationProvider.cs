using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Binance.Net.Objects;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;

namespace Binance.Net
{
    internal class BinanceAuthenticationProvider : AuthenticationProvider
    {
        public BinanceAuthenticationProvider(BinanceApiCredentials credentials) : base(credentials)
        {
        }

        public override void AuthenticateRequest(RestApiClient apiClient, Uri uri, HttpMethod method, Dictionary<string, object> providedParameters, bool auth, ArrayParametersSerialization arraySerialization, HttpMethodParameterPosition parameterPosition, out SortedDictionary<string, object> uriParameters, out SortedDictionary<string, object> bodyParameters, out Dictionary<string, string> headers)
        {
            uriParameters = parameterPosition == HttpMethodParameterPosition.InUri ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
            bodyParameters = parameterPosition == HttpMethodParameterPosition.InBody ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
            headers = new Dictionary<string, string>() { { "X-MBX-APIKEY", Credentials.Key!.GetString() } };

            if (!auth)
                return;

            var parameters = parameterPosition == HttpMethodParameterPosition.InUri ? uriParameters : bodyParameters;
            var timestamp = GetMillisecondTimestamp(apiClient);
            parameters.Add("timestamp", timestamp);

            if (((BinanceApiCredentials)Credentials).Type == ApiCredentialsType.Hmac)
            {
                uri = uri.SetParameters(uriParameters, arraySerialization);
                parameters.Add("signature", SignHMACSHA256(parameterPosition == HttpMethodParameterPosition.InUri ? uri.Query.Replace("?", "") : parameters.ToFormData()));
            }
            else
            {
                var parameterString = parameters.ToFormData();
                var data = SignSHA256Bytes(parameterString);

                using var rsa = RSA.Create();
#if NETSTANDARD2_1_OR_GREATER
                var str = Credentials.Secret!.GetString();
                if (str.StartsWith("<"))
                {
                    // Assuming XML string
                    rsa.FromXmlString(str);
                }
                else
                {
                    // Read from private key
                    rsa.ImportPkcs8PrivateKey(Convert.FromBase64String(str), out _);
                }
#else
                // Can only parse from XML
                rsa.FromXmlString(Credentials.Secret!.GetString());
#endif
                var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
                rsaFormatter.SetHashAlgorithm("SHA256");
                var sign = rsaFormatter.CreateSignature(data);

                parameters.Add("signature", Convert.ToBase64String(sign));
            }
        }
    }
}
