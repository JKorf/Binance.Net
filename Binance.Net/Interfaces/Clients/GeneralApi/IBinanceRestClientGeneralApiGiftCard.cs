using Binance.Net.Objects.Models.Spot.GiftCard;

namespace Binance.Net.Interfaces.Clients.GeneralApi
{
    /// <summary>
    /// Binance gift card endpoints
    /// </summary>
    public interface IBinanceRestClientGeneralApiGiftCard
    {
        /// <summary>
        /// Create a single-token gift card
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/gift_card/market-data" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/giftcard/createCode
        /// </para>
        /// </summary>
        /// <param name="token">The token type contained in the Binance Gift Card</param>
        /// <param name="amount">The amount of the token contained in the Binance Gift Card</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceGiftCardResponse<BinaceGiftCardData>>> CreateSingleTokenGiftCardAsync(string token, double amount, long? receiveWindow = null, CancellationToken ct = default);
        
        /// <summary>
        /// Create a dual-token gift card
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/gift_card/market-data/Create-a-dual-token-gift-card" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/giftcard/buyCode
        /// </para>
        /// </summary>
        /// <param name="baseToken">The token you want to pay, example: BUSD</param>
        /// <param name="faceToken">The token you want to buy, example: BNB</param>
        /// <param name="baseTokenAmount">The base token asset quantity, example: 1.002</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceGiftCardResponse<BinaceGiftCardData>>> CreateDualTokenGiftCardAsync(string baseToken, string faceToken, double baseTokenAmount, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Redeem a Binance Gift Card
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/gift_card/market-data/Redeem-a-Binance-Gift-Card" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/giftcard/redeemCode
        /// </para>
        /// </summary>
        /// <param name="code">Redemption code of Binance Gift Card to be redeemed, supports both Plaintext and Encrypted code.</param>
        /// <param name="externalUid">External UID</param>
        /// <param name="useEncryption">Encrypts the code if true, sends as plaintext otherwise</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceGiftCardResponse<BinanceGiftCardRedeemData>>> RedeemGiftCardAsync(string code, string? externalUid = null, bool useEncryption = true, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Verify Binance Gift Card by Gift Card Number
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/gift_card/market-data/Verify-Binance-Gift-Card-by-Gift-Card-Number" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/giftcard/verify
        /// </para>
        /// </summary>
        /// <param name="referenceNumber">The Gift Card Number</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceGiftCardResponse<BinanceGiftCardValidity>>> VerifyGiftCardByNumberAsync(string referenceNumber, long? receiveWindow = null, CancellationToken ct = default);
        
        /// <summary>
        /// Get Token Limit
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/gift_card/market-data/Fetch-Token-Limit" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/giftcard/buyCode/token-limit
        /// </para>
        /// </summary>
        /// <param name="baseToken">The token you want to pay, example: BUSD</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        
        Task<WebCallResult<BinanceGiftCardResponse<BinanceGiftCardTokenLimit[]>>> GetTokenLimitAsync(string baseToken, long? receiveWindow = null, CancellationToken ct = default);
        
        /// <summary>
        /// Get RSA public key
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/gift_card/market-data/Fetch-RSA-Public-Key" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/giftcard/cryptography/rsa-public-key
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceGiftCardResponse<string>>> GetRsaPublicKeyAsync(long? receiveWindow = null, CancellationToken ct = default);
    }
}
