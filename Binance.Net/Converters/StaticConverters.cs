using Binance.Net.Enums;

namespace Binance.Net.Converters
{
    internal class StaticConverters
    {        
        private const string MARKET = "MARKET";
        private const string LIMIT_MAKER = "LIMIT_MAKER";
        private const string STOP_LOSS = "STOP_LOSS";
        private const string STOP_LOSS_LIMIT = "STOP_LOSS_LIMIT";
        private const string STOP = "STOP";
        private const string STOP_MARKET = "STOP_MARKET";
        private const string TAKE_PROFIT = "TAKE_PROFIT";
        private const string TAKE_PROFIT_MARKET = "TAKE_PROFIT_MARKET";
        private const string TAKE_PROFIT_LIMIT = "TAKE_PROFIT_LIMIT";
        private const string TRAILING_STOP_MARKET = "TRAILING_STOP_MARKET";
        private const string LIQUIDATION = "LIQUIDATION";
        private const string UNEXPECTED = "UNEXPECTED";
        private const string BUY = "BUY";
        private const string SELL = "SELL";
        private const string LIMIT = "LIMIT";
        private const string GTC = "GTC";
        private const string IOC = "IOC";
        private const string FOK = "FOK";
        private const string GTX = "GTX";
        private const string GTE_GTC = "GTE_GTC";
        private const string EFFECT_NONE = "NO_SIDE_EFFECT";
        private const string EFFECT_MARGIN_BUY = "MARGIN_BUY";
        private const string EFFECT_AUTO_REPAY = "AUTO_REPAY";
        private const string ACK = "ACK";
        private const string RESULT = "RESULT";
        private const string FULL = "FULL";
        private const string SPOT = "SPOT";
        private const string ISOLATED_MARGIN = "ISOLATED_MARGIN";
        private const string ROLL_IN = "ROLL_IN";
        private const string ROLL_OUT = "ROLL_OUT";
        private const string ONE = "1";
        private const string TWO = "2";
        private const string ONE_MINUTE = "1m";
        private const string THREE_MINUTE = "3m";
        private const string FIVE_MINUTE = "5m";
        private const string FIFTEEN_MINUTE = "15m";
        private const string THIRTY_MINUTE = "30m";
        private const string ONE_HOUR = "1h";
        private const string TWO_HOUR = "2h";
        private const string FOUR_HOUR = "4h";
        private const string SIX_HOUR = "6h";
        private const string EIGHT_HOUR = "8h";
        private const string TWELVE_HOUR = "12h";
        private const string ONE_DAY = "1d";
        private const string THREE_DAY = "3d";
        private const string ONE_WEEK = "1w";
        private const string ONE_MONTH = "1M";
        private const string ACTIVITY = "ACTIVITY";
        private const string CUSTOMIZED_FIXED = "CUSTOMIZED_FIXED";
        private const string DAILY = "DAILY";
        private const string MAIN_FUNDING = "MAIN_FUNDING";
        private const string MAIN_UMFUTURE = "MAIN_UMFUTURE";
        private const string MAIN_CMFUTURE = "MAIN_CMFUTURE";
        private const string MAIN_MARGIN = "MAIN_MARGIN";
        private const string MAIN_MINING = "MAIN_MINING";
        private const string FUNDING_MAIN = "FUNDING_MAIN";
        private const string FUNDING_UMFUTURE = "FUNDING_UMFUTURE";
        private const string FUNDING_MARGIN = "FUNDING_MARGIN";
        private const string UMFUTURE_MAIN = "UMFUTURE_MAIN";
        private const string UMFUTURE_FUNDING = "UMFUTURE_FUNDING";
        private const string UMFUTURE_MARGIN = "UMFUTURE_MARGIN";
        private const string CMFUTURE_MAIN = "CMFUTURE_MAIN";
        private const string CMFUTURE_MARGIN = "CMFUTURE_MARGIN";
        private const string MARGIN_ISOLATEDMARGIN = "MARGIN_ISOLATEDMARGIN ";
        private const string ISOLATEDMARGIN_MARGIN = "ISOLATEDMARGIN_MARGIN";
        private const string ISOLATEDMARGIN_ISOLATEDMARGIN = "ISOLATEDMARGIN_ISOLATEDMARGIN";
        private const string MARGIN_MAIN = "MARGIN_MAIN";
        private const string MARGIN_UMFUTURE = "MARGIN_UMFUTURE";
        private const string MARGIN_CMFUTURE = "MARGIN_CMFUTURE";
        private const string MARGIN_MINING = "MARGIN_MINING";
        private const string MARGIN_FUNDING = "MARGIN_FUNDING";
        private const string MINING_MAIN = "MINING_MAIN";
        private const string MINING_UMFUTURE = "MINING_UMFUTURE";
        private const string MINING_MARGIN = "MINING_MARGIN";
        private const string FUNDING_CMFUTURE = "FUNDING_CMFUTURE";
        private const string CMFUTURE_FUNDING = "CMFUTURE_FUNDING";

        internal readonly static WorkingTypeConverter StaticWorkingTypeConverter = new WorkingTypeConverter(false);

        internal readonly static FuturesOrderTypeConverter StaticFuturesOrderTypeConverter = new FuturesOrderTypeConverter(false);

        internal readonly static WalletTypeConverter StaticWalletTypeConverter = new WalletTypeConverter(false);

        internal readonly static WithdrawalStatusConverter StaticWithdrawalStatusConverter = new WithdrawalStatusConverter(false);

        internal readonly static DepositStatusConverter StaticDepositStatusConverter = new DepositStatusConverter(false);

        internal readonly static ContractTypeConverter StaticContractTypeConverter = new ContractTypeConverter(false);

        internal readonly static FuturesMarginTypeConverter StaticFuturesMarginTypeConverter = new FuturesMarginTypeConverter(false);

        internal readonly static FuturesMarginChangeDirectionTypeConverter StaticFuturesMarginChangeDirectionTypeConverter = new FuturesMarginChangeDirectionTypeConverter(false);

        internal readonly static PeriodIntervalConverter StaticPeriodIntervalConverter = new PeriodIntervalConverter(false);

        internal readonly static PositionSideConverter StaticPositionSideConverter = new PositionSideConverter(false);

        internal static string IsolatedMarginTransferDirectionConverter(ref IsolatedMarginTransferDirection imtd)
        {
            return imtd is IsolatedMarginTransferDirection.Spot ? SPOT : ISOLATED_MARGIN;
        }

        internal static string IsolatedMarginTransferDirectionConverter(ref IsolatedMarginTransferDirection? imtd)
        {
            return imtd is IsolatedMarginTransferDirection.Spot ? SPOT : ISOLATED_MARGIN;
        }

        internal static string TransferDirectionRollConverter(ref TransferDirection td)
        {
            return td is TransferDirection.RollIn ? ROLL_IN : ROLL_OUT;
        }

        internal static string TransferDirectionConverter(ref TransferDirectionType tdt)
        {
            return tdt is TransferDirectionType.MainToMargin ? ONE : TWO;
        }

        internal static string KlineIntervalConverter(ref KlineInterval ki)
        {
            return ki is KlineInterval.OneMinute ? ONE_MINUTE :
                ki is KlineInterval.ThreeMinutes ? THREE_MINUTE :
                ki is KlineInterval.FiveMinutes ? FIVE_MINUTE :
                ki is KlineInterval.FifteenMinutes ? FIFTEEN_MINUTE :
                ki is KlineInterval.ThirtyMinutes ? THIRTY_MINUTE :
                ki is KlineInterval.OneHour ? ONE_HOUR :
                ki is KlineInterval.TwoHour ? TWO_HOUR :
                ki is KlineInterval.FourHour ? FOUR_HOUR :
                ki is KlineInterval.SixHour ? SIX_HOUR :
                ki is KlineInterval.EightHour ? EIGHT_HOUR :
                ki is KlineInterval.TwelveHour ? TWELVE_HOUR :
                ki is KlineInterval.OneDay ? ONE_DAY :
                ki is KlineInterval.ThreeDay ? THREE_DAY :
                ki is KlineInterval.OneWeek ? ONE_WEEK :
                ki is KlineInterval.OneMonth ? ONE_MONTH :
                UNEXPECTED;
        }

        internal static string LendingTypeConverter(ref LendingType lt)
        {
            return lt is LendingType.Activity ? ACTIVITY :
                lt is LendingType.CustomizedFixed ? CUSTOMIZED_FIXED :
                lt is LendingType.Daily ? DAILY : UNEXPECTED;
        }

        internal static string UniversalTransferConverter(ref UniversalTransferType utt)
        {
            return utt is UniversalTransferType.MainToFunding ? MAIN_FUNDING :
                utt is UniversalTransferType.MainToUsdFutures ? MAIN_UMFUTURE :
                utt is UniversalTransferType.MainToCoinFutures ? MAIN_CMFUTURE :
                utt is UniversalTransferType.MainToMargin ? MAIN_MARGIN :
                utt is UniversalTransferType.MainToMining ? MAIN_MINING :
                utt is UniversalTransferType.FundingToMain ? FUNDING_MAIN :
                utt is UniversalTransferType.FundingToUsdFutures ? FUNDING_UMFUTURE :
                utt is UniversalTransferType.FundingToMargin ? FUNDING_MARGIN :
                utt is UniversalTransferType.UsdFuturesToMain ? UMFUTURE_MAIN :
                utt is UniversalTransferType.UsdFuturesToFunding ? UMFUTURE_FUNDING :
                utt is UniversalTransferType.UsdFuturesToMargin ? UMFUTURE_MARGIN :
                utt is UniversalTransferType.CoinFuturesToMain ? CMFUTURE_MAIN :
                utt is UniversalTransferType.CoinFuturesToMargin ? CMFUTURE_MARGIN :
                utt is UniversalTransferType.MarginToIsolatedMargin ? MARGIN_ISOLATEDMARGIN :
                utt is UniversalTransferType.IsolatedMarginToMargin ? ISOLATEDMARGIN_MARGIN :
                utt is UniversalTransferType.MarginToMain ? MARGIN_MAIN :
                utt is UniversalTransferType.MarginToUsdFutures ? MARGIN_UMFUTURE :
                utt is UniversalTransferType.MarginToCoinFutures ? MARGIN_CMFUTURE :
                utt is UniversalTransferType.MarginToMining ? MARGIN_MINING :
                utt is UniversalTransferType.MarginToFunding ? MARGIN_FUNDING :
                utt is UniversalTransferType.MiningToMain ? MINING_MAIN :
                utt is UniversalTransferType.MiningToUsdFutures ? MINING_UMFUTURE :
                utt is UniversalTransferType.MiningToMargin ? MINING_MARGIN :
                utt is UniversalTransferType.FundingToCoinFutures ? FUNDING_CMFUTURE :
                utt is UniversalTransferType.CoinFuturesToFunding ? CMFUTURE_FUNDING :
                utt is UniversalTransferType.IsolatedMarginToIsolatedMargin ? ISOLATEDMARGIN_ISOLATEDMARGIN :
                UNEXPECTED;
        }

        /// <summary>
        /// Order Side Parameter Converter
        /// </summary>
        /// <param name="os">OrderSide</param>
        /// <returns></returns>
        internal static string OrderSideConverter(ref OrderSide os)
        {
            return os is OrderSide.Buy ? BUY : SELL;
        }

        /// <inheritdoc cref="OrderSideConverter(ref OrderSide)"/>
        internal static string OrderSideConverter(OrderSide os)
        {
            return os is OrderSide.Buy ? BUY : SELL;
        }

        /// <summary>
        /// Order Type Parameter Converter
        /// </summary>
        /// <param name="ot">OrderType</param>
        /// <returns></returns>
        internal static string OrderTypeConverter(ref SpotOrderType ot)
        {
            return ot is SpotOrderType.Limit ? LIMIT :
                ot is SpotOrderType.Market ? MARKET :
                ot is SpotOrderType.StopLoss ? STOP_LOSS :
                ot is SpotOrderType.StopLossLimit ? STOP_LOSS_LIMIT :
                ot is SpotOrderType.TakeProfit ? TAKE_PROFIT :
                ot is SpotOrderType.TakeProfitLimit ? TAKE_PROFIT_LIMIT :
                ot is SpotOrderType.LimitMaker ? LIMIT_MAKER :
                UNEXPECTED;
        }
        
        /// <summary>
        /// Order Type Parameter Converter
        /// </summary>
        /// <param name="ot">OrderType</param>
        /// <returns></returns>
        internal static string OrderTypeConverter(ref FuturesOrderType ot)
        {
            return ot is FuturesOrderType.Stop ? STOP :
                ot is FuturesOrderType.StopMarket ? STOP_MARKET :
                ot is FuturesOrderType.TakeProfit ? TAKE_PROFIT :
                ot is FuturesOrderType.TakeProfitMarket ? TAKE_PROFIT_MARKET :
                ot is FuturesOrderType.TrailingStopMarket ? TRAILING_STOP_MARKET :
                ot is FuturesOrderType.Liquidation ? LIQUIDATION :
                UNEXPECTED;
        }

        /// <inheritdoc cref="OrderTypeConverter(ref FuturesOrderType)"/>
        internal static string OrderTypeConverter(FuturesOrderType ot)
        {
            return ot is FuturesOrderType.Stop ? STOP :
                ot is FuturesOrderType.StopMarket ? STOP_MARKET :
                ot is FuturesOrderType.TakeProfit ? TAKE_PROFIT :
                ot is FuturesOrderType.TakeProfitMarket ? TAKE_PROFIT_MARKET :
                ot is FuturesOrderType.TrailingStopMarket ? TRAILING_STOP_MARKET :
                ot is FuturesOrderType.Liquidation ? LIQUIDATION :
                UNEXPECTED;
        }

        /// <summary>
        /// Time In Force Parameter Converter
        /// </summary>
        /// <param name="tif">TimeInForce</param>
        /// <returns></returns>
        internal static string TimeInForceConverter(ref TimeInForce? tif)
        {
            return tif is TimeInForce.GoodTillCanceled ? GTC :
                tif is TimeInForce.FillOrKill ? FOK :
                tif is TimeInForce.GoodTillCrossing ? GTX :
                tif is TimeInForce.GoodTillExpiredOrCanceled ? GTE_GTC :
                tif is TimeInForce.ImmediateOrCancel ? IOC :
                UNEXPECTED;
        }

        /// <inheritdoc cref="TimeInForceConverter(ref TimeInForce?)"/>
        internal static string TimeInForceConverter(TimeInForce? tif)
        {
            return tif is TimeInForce.GoodTillCanceled ? GTC :
                tif is TimeInForce.FillOrKill ? FOK :
                tif is TimeInForce.GoodTillCrossing ? GTX :
                tif is TimeInForce.GoodTillExpiredOrCanceled ? GTE_GTC :
                tif is TimeInForce.ImmediateOrCancel ? IOC :
                UNEXPECTED;
        }

        /// <summary>
        /// Side Effect Type Parameter Converter
        /// </summary>
        /// <param name="set">SideEffectType</param>
        /// <returns></returns>
        internal static string SideEffectTypeConverter(ref SideEffectType? set)
        {
            return set is SideEffectType.NoSideEffect ? EFFECT_NONE :
                set is SideEffectType.MarginBuy ? EFFECT_MARGIN_BUY :
                set is SideEffectType.AutoRepay ? EFFECT_AUTO_REPAY :
                UNEXPECTED;
        }

        /// <summary>
        /// Order Response Type Parameter Converter
        /// </summary>
        /// <param name="ort">OrderResponseType</param>
        /// <returns></returns>
        internal static string OrderResponseTypeConverter(ref OrderResponseType? ort)
        {
            return ort is OrderResponseType.Acknowledge ? ACK :
                ort is OrderResponseType.Result ? RESULT :
                ort is OrderResponseType.Full ? FULL :
                UNEXPECTED;
        }
    }
}
