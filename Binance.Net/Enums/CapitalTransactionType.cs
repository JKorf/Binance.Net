using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Types of capital flow transactions
    /// </summary>
    [JsonConverter(typeof(EnumConverter<CapitalTransactionType>))]
    public enum CapitalTransactionType
    {
        /// <summary>
        /// Transfer
        /// </summary>
        [Map("TRANSFER")]
        Transfer,
        /// <summary>
        /// Borrow
        /// </summary>
        [Map("BORROW")]
        Borrow,
        /// <summary>
        /// Repay
        /// </summary>
        [Map("REPAY")]
        Repay,
        /// <summary>
        /// Buy-Trading Income
        /// </summary>
        [Map("BUY_INCOME")]
        BuyTradingIncome,
        /// <summary>
        /// Buy-Trading Expense
        /// </summary>
        [Map("BUY_EXPENSE")]
        BuyTradingExpense,
        /// <summary>
        /// Sell-Trading Income
        /// </summary>
        [Map("SELL_INCOME")]
        SellTradingIncome,
        /// <summary>
        /// Sell-Trading Expense
        /// </summary>
        [Map("SELL_EXPENSE")]
        SellTradingExpense,
        /// <summary>
        /// Trading Commission
        /// </summary>
        [Map("TRADING_COMMISSION")]
        TradingCommission,
        /// <summary>
        /// Buy by Liquidation
        /// </summary>
        [Map("BUY_LIQUIDATION")]
        BuyLiquidation,
        /// <summary>
        /// Sell by Liquidation
        /// </summary>
        [Map("SELL_LIQUIDATION")]
        SellLiquidation,
        /// <summary>
        /// Repay by Liquidation
        /// </summary>
        [Map("REPAY_LIQUIDATION")]
        RepayLiquidation,
        /// <summary>
        /// Other Liquidation
        /// </summary>
        [Map("OTHER_LIQUIDATION")]
        OtherLiquidation,
        /// <summary>
        /// Liquidation Fee
        /// </summary>
        [Map("LIQUIDATION_FEE")]
        LiquidationFee,
        /// <summary>
        /// Small Balance Convert
        /// </summary>
        [Map("SMALL_BALANCE_CONVERT")]
        SmallBalanceConvert,
        /// <summary>
        /// Commission Return
        /// </summary>
        [Map("COMMISSION_RETURN")]
        CommissionReturn,
        /// <summary>
        /// Small Convert
        /// </summary>
        [Map("SMALL_CONVERT")]
        SmallConvert
    }
}
