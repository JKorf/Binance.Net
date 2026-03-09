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
        /// ["<c>TRANSFER</c>"] Transfer
        /// </summary>
        [Map("TRANSFER")]
        Transfer,
        /// <summary>
        /// ["<c>BORROW</c>"] Borrow
        /// </summary>
        [Map("BORROW")]
        Borrow,
        /// <summary>
        /// ["<c>REPAY</c>"] Repay
        /// </summary>
        [Map("REPAY")]
        Repay,
        /// <summary>
        /// ["<c>BUY_INCOME</c>"] Buy-Trading Income
        /// </summary>
        [Map("BUY_INCOME")]
        BuyTradingIncome,
        /// <summary>
        /// ["<c>BUY_EXPENSE</c>"] Buy-Trading Expense
        /// </summary>
        [Map("BUY_EXPENSE")]
        BuyTradingExpense,
        /// <summary>
        /// ["<c>SELL_INCOME</c>"] Sell-Trading Income
        /// </summary>
        [Map("SELL_INCOME")]
        SellTradingIncome,
        /// <summary>
        /// ["<c>SELL_EXPENSE</c>"] Sell-Trading Expense
        /// </summary>
        [Map("SELL_EXPENSE")]
        SellTradingExpense,
        /// <summary>
        /// ["<c>TRADING_COMMISSION</c>"] Trading Commission
        /// </summary>
        [Map("TRADING_COMMISSION")]
        TradingCommission,
        /// <summary>
        /// ["<c>BUY_LIQUIDATION</c>"] Buy by Liquidation
        /// </summary>
        [Map("BUY_LIQUIDATION")]
        BuyLiquidation,
        /// <summary>
        /// ["<c>SELL_LIQUIDATION</c>"] Sell by Liquidation
        /// </summary>
        [Map("SELL_LIQUIDATION")]
        SellLiquidation,
        /// <summary>
        /// ["<c>REPAY_LIQUIDATION</c>"] Repay by Liquidation
        /// </summary>
        [Map("REPAY_LIQUIDATION")]
        RepayLiquidation,
        /// <summary>
        /// ["<c>OTHER_LIQUIDATION</c>"] Other Liquidation
        /// </summary>
        [Map("OTHER_LIQUIDATION")]
        OtherLiquidation,
        /// <summary>
        /// ["<c>LIQUIDATION_FEE</c>"] Liquidation Fee
        /// </summary>
        [Map("LIQUIDATION_FEE")]
        LiquidationFee,
        /// <summary>
        /// ["<c>SMALL_BALANCE_CONVERT</c>"] Small Balance Convert
        /// </summary>
        [Map("SMALL_BALANCE_CONVERT")]
        SmallBalanceConvert,
        /// <summary>
        /// ["<c>COMMISSION_RETURN</c>"] Commission Return
        /// </summary>
        [Map("COMMISSION_RETURN")]
        CommissionReturn,
        /// <summary>
        /// ["<c>SMALL_CONVERT</c>"] Small Convert
        /// </summary>
        [Map("SMALL_CONVERT")]
        SmallConvert
    }
}

