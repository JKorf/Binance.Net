namespace Binance.Net.Converters
{
    internal class StaticConverters
    {
        internal readonly static OrderSideConverter StaticOrderSideConverter = new OrderSideConverter(false);

        internal readonly static TimeInForceConverter StaticTimeInForceConverter = new TimeInForceConverter(false);

        internal readonly static SideEffectTypeConverter StaticSideEffectTypeConverter = new SideEffectTypeConverter(false);

        internal readonly static PositionSideConverter StaticPositionSideConverter = new PositionSideConverter(false);

        internal readonly static WorkingTypeConverter StaticWorkingTypeConverter = new WorkingTypeConverter(false);

        internal readonly static OrderResponseTypeConverter StaticOrderResponseTypeConverter = new OrderResponseTypeConverter(false);

        internal readonly static FuturesOrderTypeConverter StaticFuturesOrderTypeConverter = new FuturesOrderTypeConverter(false);

        internal readonly static WalletTypeConverter StaticWalletTypeConverter = new WalletTypeConverter(false);

        internal readonly static WithdrawalStatusConverter StaticWithdrawalStatusConverter = new WithdrawalStatusConverter(false);

        internal readonly static DepositStatusConverter StaticDepositStatusConverter = new DepositStatusConverter(false);

        internal readonly static UniversalTransferTypeConverter StaticUniversalTransferTypeConverter = new UniversalTransferTypeConverter(false);

        internal readonly static TransferDirectionTypeConverter StaticTransferDirectionTypeConverter = new TransferDirectionTypeConverter(false);

        internal readonly static TransferDirectionConverter StaticTransferDirectionConverter = new TransferDirectionConverter(false);

        internal readonly static IsolatedMarginTransferDirectionConverter StaticIsolatedMarginTransferDirectionConverter = new IsolatedMarginTransferDirectionConverter(false);

        internal readonly static KlineIntervalConverter StaticKlineIntervalConverter = new KlineIntervalConverter(false);

        internal readonly static ContractTypeConverter StaticContractTypeConverter = new ContractTypeConverter(false);

        internal readonly static SpotOrderTypeConverter StaticSpotOrderTypeConverter = new SpotOrderTypeConverter(false);

        internal readonly static FuturesMarginTypeConverter StaticFuturesMarginTypeConverter = new FuturesMarginTypeConverter(false);

        internal readonly static FuturesMarginChangeDirectionTypeConverter StaticFuturesMarginChangeDirectionTypeConverter = new FuturesMarginChangeDirectionTypeConverter(false);

        internal readonly static PeriodIntervalConverter StaticPeriodIntervalConverter = new PeriodIntervalConverter(false);
    }
}
