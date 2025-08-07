using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.GeneralApi;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot.GiftCard;
using CryptoExchange.Net.RateLimiting.Guards;
using System.Security.Cryptography;
using System.Text;

namespace Binance.Net.Clients.GeneralApi
{
    /// <inheritdoc />
    internal class BinanceRestClientGeneralApiGiftCard : IBinanceRestClientGeneralApiGiftCard
    {
        private static readonly RequestDefinitionCache _definitions = new();

        private readonly BinanceRestClientGeneralApi _baseClient;

        internal BinanceRestClientGeneralApiGiftCard(BinanceRestClientGeneralApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Create a single token gift card

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceGiftCardResponse<BinaceGiftCardData>>> CreateSingleTokenGiftCardAsync(string token, double amount, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("token", token);
            parameters.Add("amount", amount);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/giftcard/createCode", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceGiftCardResponse<BinaceGiftCardData>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Create a dual token gift card

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceGiftCardResponse<BinaceGiftCardData>>> CreateDualTokenGiftCardAsync(string baseToken, string faceToken, double baseTokenAmount, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("token", baseToken);
            parameters.Add("faceToken", faceToken);
            parameters.Add("baseTokenAmount", baseTokenAmount);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/giftcard/buyCode", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceGiftCardResponse<BinaceGiftCardData>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Redeem a Binance Gift card

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceGiftCardResponse<BinanceGiftCardRedeemData>>> RedeemGiftCardAsync(string code, string? externalUid = null, bool useEncryption = true, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            #if NETSTANDARD2_0
            if (useEncryption)
                return new WebCallResult<BinanceGiftCardResponse<BinanceGiftCardRedeemData>>(new WebError("Encryption is not supported when targeting NETSTANDARD2_0. Please disable `useEncryption` or upgrade your target framework."));
            else
                parameters.Add("code", code);
            #else
            if (useEncryption)
            {
                var keyResult = await GetRsaPublicKeyAsync(receiveWindow, ct).ConfigureAwait(false);

                if (!keyResult.Success || string.IsNullOrEmpty(keyResult.Data?.Data))
                    return new WebCallResult<BinanceGiftCardResponse<BinanceGiftCardRedeemData>>(keyResult.Error!);

                using var rsa = RSA.Create();
                var publicKeyBytes = Convert.FromBase64String(keyResult.Data.Data);
                rsa.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);

                var plainBytes = Encoding.UTF8.GetBytes(code);
                var encryptedBytes = rsa.Encrypt(plainBytes, RSAEncryptionPadding.OaepSHA256);

                var encryptedBase64 = Convert.ToBase64String(encryptedBytes).TrimEnd('=').Replace('+', '-').Replace('/', '_');

                parameters.Add("code", encryptedBase64);
            }
            else
                parameters.Add("code", code);
            #endif
            parameters.AddOptional("externalUid", externalUid);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/giftcard/redeemCode", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceGiftCardResponse<BinanceGiftCardRedeemData>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Verify Binance Gift Card by Gift Card Number

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceGiftCardResponse<BinanceGiftCardValidity>>> VerifyGiftCardByNumberAsync(string referenceNumber, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("referenceNo", referenceNumber);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/giftcard/verify", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceGiftCardResponse<BinanceGiftCardValidity>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Fetch Token Limit

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceGiftCardResponse<BinanceGiftCardTokenLimit[]>>> GetTokenLimitAsync(string baseToken, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("baseToken", baseToken);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/giftcard/buyCode/token-limit", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceGiftCardResponse<BinanceGiftCardTokenLimit[]>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Fetch RSA Public Key

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceGiftCardResponse<string>>> GetRsaPublicKeyAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/giftcard/cryptography/rsa-public-key", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceGiftCardResponse<string>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
