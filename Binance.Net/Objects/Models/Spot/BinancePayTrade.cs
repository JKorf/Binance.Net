﻿using System;
using System.Collections.Generic;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Binance pay trade
    /// </summary>
    public class BinancePayTrade
    {
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
        /// Asset
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Fund details
        /// </summary>
        [JsonProperty("fundsDetail")]
        public IEnumerable<BinancePayTradeDetails> Details { get; set; } = Array.Empty<BinancePayTradeDetails>();
        /// <summary>
        /// Payer info
        /// </summary>
        [JsonProperty("payerInfo")]
        public BinancePayTradePayerInfo PayerInfo { get; set; } = new BinancePayTradePayerInfo();
        /// <summary>
        /// Receiver info
        /// </summary>
        [JsonProperty("receiverInfo")]
        public BinancePayTradeReceiverInfo ReceiverInfo { get; set; } = new BinancePayTradeReceiverInfo();
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
    /// Pay trade payer info
    /// </summary>
    public class BinancePayTradePayerInfo
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
        /// Binance uid
        /// </summary>
        [JsonProperty("binanceId")]
        public string BinanceId { get; set; } = string.Empty;
        /// <summary>
        /// Binance pay id
        /// </summary>
        [JsonProperty("accountId")]
        public string AccountId { get; set; } = string.Empty;
    }

    /// <summary>
    /// Pay trade receiver info
    /// </summary>
    public class BinancePayTradeReceiverInfo
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
    }
}
