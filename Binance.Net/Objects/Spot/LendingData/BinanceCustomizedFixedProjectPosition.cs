﻿using System;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.LendingData
{
    /// <summary>
    /// Customized fixed project position
    /// </summary>
    public class BinanceCustomizedFixedProjectPosition
    {
        /// <summary>
        /// Asset name
        /// </summary>
        public string Asset { get; set; } = "";
        /// <summary>
        /// Can transfer
        /// </summary>
        public bool CanTransfer { get; set; }
        /// <summary>
        /// Create timestamp
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime CreateTimestamp { get; set; }
        /// <summary>
        /// Duration
        /// </summary>
        public int Duration { get; set; }
        /// <summary>
        /// End time
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// Interest
        /// </summary>
        public decimal Interest { get; set; }
        /// <summary>
        /// Interest rate
        /// </summary>
        public decimal InterestRate { get; set; }
        /// <summary>
        /// Lot
        /// </summary>
        public int Lot { get; set; }
        /// <summary>
        /// Position id
        /// </summary>
        public int PositionId { get; set; }
        /// <summary>
        /// Principal
        /// </summary>
        public decimal Principal { get; set; }
        /// <summary>
        /// Project id
        /// </summary>
        public string ProjectId { get; set; } = "";
        /// <summary>
        /// Project name
        /// </summary>
        public string ProjectName { get; set; } = "";
        /// <summary>
        /// Time of purchase
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime PurchaseTime { get; set; }
        /// <summary>
        /// Redeem date
        /// </summary>
        public string RedeemDate { get; set; } = "";
        /// <summary>
        /// Start time
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonConverter(typeof(ProjectStatusConverter))]
        public ProjectStatus Status { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        [JsonConverter(typeof(ProjectTypeConverter))]
        public ProjectType Type { get; set; }

    }
}
