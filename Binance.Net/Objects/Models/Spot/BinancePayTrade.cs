using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Binance pay trade
    /// </summary>
    public class BinancePayTrade
    {
        /// <summary>
        /// Uid
        /// </summary>
        [JsonProperty("uid")]
        public long? Uid { get; set; }
        /// <summary>
        /// Counter party id
        /// </summary>
        [JsonProperty("counterpartyId")]
        public long? CounterPartyId { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Note
        /// </summary>
        [JsonProperty("note")]
        public string? Note { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public PayOrderType OrderType { get; set; }
        /// <summary>
        /// Transaction id
        /// </summary>
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// Transaction time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime TransactionTime { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Total fee
        /// </summary>
        [JsonProperty("totalPaymentFee")]
        public decimal TotalPaymentFee { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Main wallet type
        /// </summary>
        [JsonProperty("walletType"), JsonConverter(typeof(EnumConverter))]
        public PayWalletType WalletType { get; set; }
        /// <summary>
        /// Fund details
        /// </summary>
        [JsonProperty("fundsDetail")]
        public IEnumerable<BinancePayTradeDetails> Details { get; set; } = Array.Empty<BinancePayTradeDetails>();
        /// <summary>
        /// Payer info
        /// </summary>
        [JsonProperty("payerInfo")]
        public BinancePayTradeParticipantInfo PayerInfo { get; set; } = new BinancePayTradeParticipantInfo();
        /// <summary>
        /// Receiver info
        /// </summary>
        [JsonProperty("receiverInfo")]
        public BinancePayTradeParticipantInfo ReceiverInfo { get; set; } = new BinancePayTradeParticipantInfo();
    }

    /// <summary>
    /// Pay trade funds details
    /// </summary>
    public class BinancePayTradeDetails
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
    }

    /// <summary>
    /// Pay trade receiver info
    /// </summary>
    public class BinancePayTradeParticipantInfo
    {
        /// <summary>
        /// Nickname or merchant name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Account type，USER for personal，MERCHANT for merchant
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// Email
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Binance uid
        /// </summary>
        [JsonProperty("binanceId")]
        public string BinanceId { get; set; } = string.Empty;
        /// <summary>
        /// Binance pay id
        /// </summary>
        [JsonProperty("accountId")]
        public string AccountId { get; set; } = string.Empty;
        /// <summary>
        /// International area code
        /// </summary>
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; } = string.Empty;
        /// <summary>
        /// Phone number
        /// </summary>
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; } = string.Empty;
        /// <summary>
        /// Country code
        /// </summary>
        [JsonProperty("mobileCode")]
        public string MobileCode { get; set; } = string.Empty;
        /// <summary>
        /// Unmask data
        /// </summary>
        [JsonProperty("unmaskData")]
        public bool? UnmaskData { get; set; }
    }
}
