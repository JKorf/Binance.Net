using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Type of permission
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PermissionType>))]
    public enum PermissionType
    {
        /// <summary>
        /// Spot trading
        /// </summary>
        [Map("SPOT")]
        Spot,
        /// <summary>
        /// Margin trading
        /// </summary>>
        [Map("MARGIN")]
        Margin,
        /// <summary>
        /// Futures trading
        /// </summary>
        [Map("FUTURES")]
        Futures,
        /// <summary>
        /// Leveraged trading
        /// </summary>
        [Map("LEVERAGED")]
        Leveraged,
        /// <summary>
        /// Pre-market trading
        /// </summary>
        [Map("PRE_MARKET")]
        PreMarket,
        /// <summary>
        /// Trade group 2
        /// </summary>
        [Map("TRD_GRP_002")]
        TradeGroup002,
        /// <summary>
        /// Trade group 3
        /// </summary>
        [Map("TRD_GRP_003")]
        TradeGroup003,
        /// <summary>
        /// Trade group 4
        /// </summary>
        [Map("TRD_GRP_004")]
        TradeGroup004,
        /// <summary>
        /// Trade group 5
        /// </summary>
        [Map("TRD_GRP_005")]
        TradeGroup005,
        /// <summary>
        /// Trade group 6
        /// </summary>
        [Map("TRD_GRP_006")]
        TradeGroup006,
        /// <summary>
        /// Trade group 7
        /// </summary>
        [Map("TRD_GRP_007")]
        TradeGroup007,
        /// <summary>
        /// Trade group 8
        /// </summary>
        [Map("TRD_GRP_008")]
        TradeGroup008,
        /// <summary>
        /// Trade group 9
        /// </summary>
        [Map("TRD_GRP_009")]
        TradeGroup009,
        /// <summary>
        /// Trade group 10
        /// </summary>
        [Map("TRD_GRP_010")]
        TradeGroup010,
        /// <summary>
        /// Trade group 11
        /// </summary>
        [Map("TRD_GRP_011")]
        TradeGroup011,
        /// <summary>
        /// Trade group 12
        /// </summary>
        [Map("TRD_GRP_012")]
        TradeGroup012,
        /// <summary>
        /// Trade group 13
        /// </summary>
        [Map("TRD_GRP_013")]
        TradeGroup013,
        /// <summary>
        /// Trade group 14
        /// </summary>
        [Map("TRD_GRP_014")]
        TradeGroup014,
        /// <summary>
        /// Trade group 15
        /// </summary>
        [Map("TRD_GRP_015")]
        TradeGroup015,
        /// <summary>
        /// Trade group 16
        /// </summary>
        [Map("TRD_GRP_016")]
        TradeGroup016,
        /// <summary>
        /// Trade group 17
        /// </summary>
        [Map("TRD_GRP_017")]
        TradeGroup017,
        /// <summary>
        /// Trade group 18
        /// </summary>
        [Map("TRD_GRP_018")]
        TradeGroup018,
        /// <summary>
        /// Trade group 19
        /// </summary>
        [Map("TRD_GRP_019")]
        TradeGroup019,
        /// <summary>
        /// Trade group 20
        /// </summary>
        [Map("TRD_GRP_020")]
        TradeGroup020,
        /// <summary>
        /// Trade group 21
        /// </summary>
        [Map("TRD_GRP_021")]
        TradeGroup021,
        /// <summary>
        /// Trade group 22
        /// </summary>
        [Map("TRD_GRP_022")]
        TradeGroup022,
        /// <summary>
        /// Trade group 23
        /// </summary>
        [Map("TRD_GRP_023")]
        TradeGroup023,
        /// <summary>
        /// Trade group 24
        /// </summary>
        [Map("TRD_GRP_024")]
        TradeGroup024,
        /// <summary>
        /// Trade group 25
        /// </summary>
        [Map("TRD_GRP_025")]
        TradeGroup025,
        /// <summary>
        /// Trade group 26
        /// </summary>
        [Map("TRD_GRP_026")]
        TradeGroup026,
        /// <summary>
        /// Trade group 27
        /// </summary>
        [Map("TRD_GRP_027")]
        TradeGroup027,
        /// <summary>
        /// Trade group 28
        /// </summary>
        [Map("TRD_GRP_028")]
        TradeGroup028,
        /// <summary>
        /// Trade group 29
        /// </summary>
        [Map("TRD_GRP_029")]
        TradeGroup029,
        /// <summary>
        /// Trade group 30
        /// </summary>
        [Map("TRD_GRP_030")]
        TradeGroup030,
        /// <summary>
        /// Trade group 31
        /// </summary>
        [Map("TRD_GRP_031")]
        TradeGroup031,
        /// <summary>
        /// Trade group 32
        /// </summary>
        [Map("TRD_GRP_032")]
        TradeGroup032,
        /// <summary>
        /// Trade group 33
        /// </summary>
        [Map("TRD_GRP_033")]
        TradeGroup033,
        /// <summary>
        /// Trade group 34
        /// </summary>
        [Map("TRD_GRP_034")]
        TradeGroup034,
        /// <summary>
        /// Trade group 35
        /// </summary>
        [Map("TRD_GRP_035")]
        TradeGroup035,
        /// <summary>
        /// Trade group 36
        /// </summary>
        [Map("TRD_GRP_036")]
        TradeGroup036,
        /// <summary>
        /// Trade group 37
        /// </summary>
        [Map("TRD_GRP_037")]
        TradeGroup037,
        /// <summary>
        /// Trade group 38
        /// </summary>
        [Map("TRD_GRP_038")]
        TradeGroup038,
        /// <summary>
        /// Trade group 39
        /// </summary>
        [Map("TRD_GRP_039")]
        TradeGroup039,
        /// <summary>
        /// Trade group 40
        /// </summary>
        [Map("TRD_GRP_040")]
        TradeGroup040,
        /// <summary>
        /// Trade group 40
        /// </summary>
        [Map("TRD_GRP_238")]
        TradeGroup238
    }
}
