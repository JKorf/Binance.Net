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
        /// ["<c>SPOT</c>"] Spot trading
        /// </summary>
        [Map("SPOT")]
        Spot,
        /// <summary>
        /// ["<c>MARGIN</c>"] Margin trading
        /// </summary>>
        [Map("MARGIN")]
        Margin,
        /// <summary>
        /// ["<c>FUTURES</c>"] Futures trading
        /// </summary>
        [Map("FUTURES")]
        Futures,
        /// <summary>
        /// ["<c>LEVERAGED</c>"] Leveraged trading
        /// </summary>
        [Map("LEVERAGED")]
        Leveraged,
        /// <summary>
        /// ["<c>PRE_MARKET</c>"] Pre-market trading
        /// </summary>
        [Map("PRE_MARKET")]
        PreMarket,
        /// <summary>
        /// ["<c>TRD_GRP_002</c>"] Trade group 2
        /// </summary>
        [Map("TRD_GRP_002")]
        TradeGroup002,
        /// <summary>
        /// ["<c>TRD_GRP_003</c>"] Trade group 3
        /// </summary>
        [Map("TRD_GRP_003")]
        TradeGroup003,
        /// <summary>
        /// ["<c>TRD_GRP_004</c>"] Trade group 4
        /// </summary>
        [Map("TRD_GRP_004")]
        TradeGroup004,
        /// <summary>
        /// ["<c>TRD_GRP_005</c>"] Trade group 5
        /// </summary>
        [Map("TRD_GRP_005")]
        TradeGroup005,
        /// <summary>
        /// ["<c>TRD_GRP_006</c>"] Trade group 6
        /// </summary>
        [Map("TRD_GRP_006")]
        TradeGroup006,
        /// <summary>
        /// ["<c>TRD_GRP_007</c>"] Trade group 7
        /// </summary>
        [Map("TRD_GRP_007")]
        TradeGroup007,
        /// <summary>
        /// ["<c>TRD_GRP_008</c>"] Trade group 8
        /// </summary>
        [Map("TRD_GRP_008")]
        TradeGroup008,
        /// <summary>
        /// ["<c>TRD_GRP_009</c>"] Trade group 9
        /// </summary>
        [Map("TRD_GRP_009")]
        TradeGroup009,
        /// <summary>
        /// ["<c>TRD_GRP_010</c>"] Trade group 10
        /// </summary>
        [Map("TRD_GRP_010")]
        TradeGroup010,
        /// <summary>
        /// ["<c>TRD_GRP_011</c>"] Trade group 11
        /// </summary>
        [Map("TRD_GRP_011")]
        TradeGroup011,
        /// <summary>
        /// ["<c>TRD_GRP_012</c>"] Trade group 12
        /// </summary>
        [Map("TRD_GRP_012")]
        TradeGroup012,
        /// <summary>
        /// ["<c>TRD_GRP_013</c>"] Trade group 13
        /// </summary>
        [Map("TRD_GRP_013")]
        TradeGroup013,
        /// <summary>
        /// ["<c>TRD_GRP_014</c>"] Trade group 14
        /// </summary>
        [Map("TRD_GRP_014")]
        TradeGroup014,
        /// <summary>
        /// ["<c>TRD_GRP_015</c>"] Trade group 15
        /// </summary>
        [Map("TRD_GRP_015")]
        TradeGroup015,
        /// <summary>
        /// ["<c>TRD_GRP_016</c>"] Trade group 16
        /// </summary>
        [Map("TRD_GRP_016")]
        TradeGroup016,
        /// <summary>
        /// ["<c>TRD_GRP_017</c>"] Trade group 17
        /// </summary>
        [Map("TRD_GRP_017")]
        TradeGroup017,
        /// <summary>
        /// ["<c>TRD_GRP_018</c>"] Trade group 18
        /// </summary>
        [Map("TRD_GRP_018")]
        TradeGroup018,
        /// <summary>
        /// ["<c>TRD_GRP_019</c>"] Trade group 19
        /// </summary>
        [Map("TRD_GRP_019")]
        TradeGroup019,
        /// <summary>
        /// ["<c>TRD_GRP_020</c>"] Trade group 20
        /// </summary>
        [Map("TRD_GRP_020")]
        TradeGroup020,
        /// <summary>
        /// ["<c>TRD_GRP_021</c>"] Trade group 21
        /// </summary>
        [Map("TRD_GRP_021")]
        TradeGroup021,
        /// <summary>
        /// ["<c>TRD_GRP_022</c>"] Trade group 22
        /// </summary>
        [Map("TRD_GRP_022")]
        TradeGroup022,
        /// <summary>
        /// ["<c>TRD_GRP_023</c>"] Trade group 23
        /// </summary>
        [Map("TRD_GRP_023")]
        TradeGroup023,
        /// <summary>
        /// ["<c>TRD_GRP_024</c>"] Trade group 24
        /// </summary>
        [Map("TRD_GRP_024")]
        TradeGroup024,
        /// <summary>
        /// ["<c>TRD_GRP_025</c>"] Trade group 25
        /// </summary>
        [Map("TRD_GRP_025")]
        TradeGroup025,
        /// <summary>
        /// ["<c>TRD_GRP_026</c>"] Trade group 26
        /// </summary>
        [Map("TRD_GRP_026")]
        TradeGroup026,
        /// <summary>
        /// ["<c>TRD_GRP_027</c>"] Trade group 27
        /// </summary>
        [Map("TRD_GRP_027")]
        TradeGroup027,
        /// <summary>
        /// ["<c>TRD_GRP_028</c>"] Trade group 28
        /// </summary>
        [Map("TRD_GRP_028")]
        TradeGroup028,
        /// <summary>
        /// ["<c>TRD_GRP_029</c>"] Trade group 29
        /// </summary>
        [Map("TRD_GRP_029")]
        TradeGroup029,
        /// <summary>
        /// ["<c>TRD_GRP_030</c>"] Trade group 30
        /// </summary>
        [Map("TRD_GRP_030")]
        TradeGroup030,
        /// <summary>
        /// ["<c>TRD_GRP_031</c>"] Trade group 31
        /// </summary>
        [Map("TRD_GRP_031")]
        TradeGroup031,
        /// <summary>
        /// ["<c>TRD_GRP_032</c>"] Trade group 32
        /// </summary>
        [Map("TRD_GRP_032")]
        TradeGroup032,
        /// <summary>
        /// ["<c>TRD_GRP_033</c>"] Trade group 33
        /// </summary>
        [Map("TRD_GRP_033")]
        TradeGroup033,
        /// <summary>
        /// ["<c>TRD_GRP_034</c>"] Trade group 34
        /// </summary>
        [Map("TRD_GRP_034")]
        TradeGroup034,
        /// <summary>
        /// ["<c>TRD_GRP_035</c>"] Trade group 35
        /// </summary>
        [Map("TRD_GRP_035")]
        TradeGroup035,
        /// <summary>
        /// ["<c>TRD_GRP_036</c>"] Trade group 36
        /// </summary>
        [Map("TRD_GRP_036")]
        TradeGroup036,
        /// <summary>
        /// ["<c>TRD_GRP_037</c>"] Trade group 37
        /// </summary>
        [Map("TRD_GRP_037")]
        TradeGroup037,
        /// <summary>
        /// ["<c>TRD_GRP_038</c>"] Trade group 38
        /// </summary>
        [Map("TRD_GRP_038")]
        TradeGroup038,
        /// <summary>
        /// ["<c>TRD_GRP_039</c>"] Trade group 39
        /// </summary>
        [Map("TRD_GRP_039")]
        TradeGroup039,
        /// <summary>
        /// ["<c>TRD_GRP_040</c>"] Trade group 40
        /// </summary>
        [Map("TRD_GRP_040")]
        TradeGroup040,
        /// <summary>
        /// ["<c>TRD_GRP_238</c>"] Trade group 40
        /// </summary>
        [Map("TRD_GRP_238")]
        TradeGroup238
    }
}

