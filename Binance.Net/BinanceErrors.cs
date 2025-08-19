using CryptoExchange.Net.Objects.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binance.Net
{
    internal static class BinanceErrors
    {
        public static ErrorMapping SpotErrors { get; } = new ErrorMapping(
            [
                new ErrorInfo(ErrorType.Unauthorized, false, "Not authorized to execute the request", "-1002"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Invalid API-key, IP, or permissions for action", "-2015"),
                new ErrorInfo(ErrorType.Unauthorized, false, "API key format invalid", "-2014"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Signature invalid, check your API credentials", "-1022"),

                new ErrorInfo(ErrorType.InvalidTimestamp, false, "Request timestamp invalid, check time sync", "-1021"),

                new ErrorInfo(ErrorType.MissingParameter, false, "Empty parameter", "-1105"),

                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid characters in parameter", "-1100"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Too many parameters specified", "-1101"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Mandatory parameter missing or incorrect format", "-1102"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Unknown parameter", "-1103"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Unread parameter", "-1104"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Parameter not required", "-1106"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Parameter overflow", "-1108"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid precision", "-1111"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "TimeInForce sent when not required", "-1114"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "TimeInForce has invalid value", "-1115"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid order type", "-1116"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid side", "-1117"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Client order id not sent", "-1118"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Client order id not sent", "-1119"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid interval", "-1120"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid symbol status", "-1122"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Lookup period too big", "-1127"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Parameter combination invalid", "-1128"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid parameter value", "-1130"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid strategy type value", "-1134"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid ticker type value", "-1139"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid cancel restrictions", "-1145"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Duplicate symbols specified", "-1151"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "OCO order type not supported", "-1158"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "OCO buy limit price must be below", "-1165"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "OCO sell limit price must be above", "-1166"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "One of OCO order should be market order", "-1168"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "OCO buy stop loss price must be above", "-1196"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "OCO sell stop loss price must be below", "-1197"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "OCO buy take profit price must be above", "-1198"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "OCO sell take profit price must be below", "-1199"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid client order id", "-2039"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid listen key", "-1125"),

                new ErrorInfo(ErrorType.RateLimitSubscription, false, "One of OCO order should be market", "-1191"),

                new ErrorInfo(ErrorType.RateLimitConnection, false, "Request rate limit reached", "-1034"),

                new ErrorInfo(ErrorType.RateLimitOrder, false, "Order rate limit reached", "-1015"),

                new ErrorInfo(ErrorType.RateLimitRequest, false, "Request rate limit reached", "-1003"),

                new ErrorInfo(ErrorType.UnknownSymbol, false, "Invalid symbol", "-1121"),

                new ErrorInfo(ErrorType.UnknownOrder, false, "Order does not exist", "-2013"),
                new ErrorInfo(ErrorType.UnknownOrder, false, "Order archived", "-2026"),

                new ErrorInfo(ErrorType.DuplicateSubscription, false, "User subscription already active", "-2035"),
            ],
            [
                new ErrorEvaluator("-1013", (code, msg) => {
                    var topic = msg?.Replace("Filter failure: ", "");
                    return topic switch
                    {
                        "NOTIONAL" => new ErrorInfo(ErrorType.InvalidQuantity, false, "Price * quantity is not within range of the minNotional and maxNotional", code),
                        "LOT_SIZE" => new ErrorInfo(ErrorType.InvalidQuantity, false, "Quantity is too high, too low, and/or not following the step size rule for the symbol", code),
                        "MIN_NOTIONAL" => new ErrorInfo(ErrorType.InvalidQuantity, false, "Price * quantity is too low to be a valid order for the symbol", code),
                        "MARKET_LOT_SIZE" => new ErrorInfo(ErrorType.InvalidQuantity, false, "MARKET order's quantity is too high, too low, and/or not following the step size rule for the symbol", code),

                        "PERCENT_PRICE" => new ErrorInfo(ErrorType.InvalidPrice, false, "Price is X% too high or X% too low from the average weighted price over the last Y minutes", code),
                        "PRICE_FILTER" => new ErrorInfo(ErrorType.InvalidPrice, false, "Price is too high, too low, and/or not following the tick size rule for the symbol", code),

                        "TRAILING_DELTA" => new ErrorInfo(ErrorType.InvalidParameter, false, "TrailingDelta is not within the defined range of the filter for that order type", code),
                        "ICEBERG_PARTS" => new ErrorInfo(ErrorType.InvalidParameter, false, "ICEBERG order would break into too many parts; icebergQty is too small", code),
                        "MAX_POSITION" => new ErrorInfo(ErrorType.MaxPosition, false, "The account's position has reached the maximum defined limit", code),

                        "MAX_NUM_ORDERS" => new ErrorInfo(ErrorType.RateLimitOrder, false, "Account has too many open orders on the symbol", code),
                        "MAX_NUM_ALGO_ORDERS" => new ErrorInfo(ErrorType.RateLimitOrder, false, "Account has too many open stop loss and/or take profit orders on the symbol", code),
                        "MAX_NUM_ICEBERG_ORDERS" => new ErrorInfo(ErrorType.RateLimitOrder, false, "Account has too many open iceberg orders on the symbol", code),
                        "EXCHANGE_MAX_NUM_ORDERS" => new ErrorInfo(ErrorType.RateLimitOrder, false, "Account has too many open orders on the exchange", code),
                        "EXCHANGE_MAX_NUM_ALGO_ORDERS" => new ErrorInfo(ErrorType.RateLimitOrder, false, "Account has too many open stop loss and/or take profit orders on the exchange", code),
                        "EXCHANGE_MAX_NUM_ICEBERG_ORDERS" => new ErrorInfo(ErrorType.RateLimitOrder, false, "Account has too many open iceberg orders on the exchange", code),
                        _ => new ErrorInfo(ErrorType.Unknown, false, msg ?? "Unknown", code),
                    };
                }),
                new ErrorEvaluator(["-1010", "-2010", "-2011", "-2038"], (code, msg) => {
                    msg = msg?.TrimEnd(['.']);
                    return msg switch
                    {
                        "This symbol is not permitted for this account" => new ErrorInfo(ErrorType.Unauthorized, false, msg, code),

                        "Unknown order sent" => new ErrorInfo(ErrorType.UnknownOrder, false, msg, code),

                        "Duplicate order sent" => new ErrorInfo(ErrorType.DuplicateClientOrderId, false, "Client order id already in use", code),

                        "Market is closed" => new ErrorInfo(ErrorType.UnavailableSymbol, false, "Symbol is not currently in trading mode", code),

                        "Account has insufficient balance for requested action" => new ErrorInfo(ErrorType.InsufficientBalance, false, msg, code),

                        "Order amend is not supported for this symbol" => new ErrorInfo(ErrorType.InvalidOperation, false, msg, code),
                        "This action is disabled on this account" => new ErrorInfo(ErrorType.InvalidOperation, false, msg, code),
                        "This account may not place or cancel orders" => new ErrorInfo(ErrorType.InvalidOperation, false, msg, code),
                        "Order would trigger immediately" => new ErrorInfo(ErrorType.InvalidStopParameters, false, msg, code),
                        "OCO orders are not supported for this symbol" => new ErrorInfo(ErrorType.InvalidOperation, false, msg, code),
                        "Order cancel-replace is not supported for this symbol" => new ErrorInfo(ErrorType.InvalidOperation, false, msg, code),
                        "This symbol is restricted for this account" => new ErrorInfo(ErrorType.InvalidOperation, false, msg, code),
                        "Order was not canceled due to cancel restrictions" => new ErrorInfo(ErrorType.InvalidOperation, false, msg, code),
                        "Order amend (quantity increase) is not supported" => new ErrorInfo(ErrorType.InvalidOperation, false, msg, code),
                        _ => new ErrorInfo(ErrorType.InvalidParameter, false, msg ?? "Unknown error", code)
                    };
                })
            ]
        );

        public static ErrorMapping FuturesErrors { get; } = new ErrorMapping(
            [
                new ErrorInfo(ErrorType.Unauthorized, false, "Not authorized to execute the request", "-1002"),
                new ErrorInfo(ErrorType.Unauthorized, false, "API key format invalid", "-2014"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Signature invalid, check your API credentials", "-1022"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Invalid API-key, IP, or permissions for action", "-2015"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Invalid RSA public key", "-4057"),
                new ErrorInfo(ErrorType.Unauthorized, false, "KYC level insufficient", "-4202"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Leverage setting not allowed within period", "-4203"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Leverage setting not allowed within period", "-4205"),
                new ErrorInfo(ErrorType.Unauthorized, false, "User only has reduce only permissions", "-4087"),
                new ErrorInfo(ErrorType.Unauthorized, false, "No order placement permissions", "-4088"),

                new ErrorInfo(ErrorType.InvalidTimestamp, false, "Request timestamp invalid, check time sync", "-1021"),


                new ErrorInfo(ErrorType.RateLimitRequest, false, "Request rate limit reached", "-1003"),

                new ErrorInfo(ErrorType.RateLimitOrder, false, "Order rate limit reached", "-1015"),
                new ErrorInfo(ErrorType.RateLimitOrder, false, "Max open orders reached", "-2025"),
                new ErrorInfo(ErrorType.RateLimitOrder, false, "Max stop orders reached", "-4045"),


                new ErrorInfo(ErrorType.MissingParameter, false, "Empty parameter", "-1105"),

                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid order combination", "-1014"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Start time bigger than end time filter", "-1023"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid characters in parameter", "-1100"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Too many parameters specified", "-1101"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Mandatory parameter missing or incorrect format", "-1102"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Unknown parameter", "-1103"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Unread parameter", "-1104"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Parameter not required", "-1106"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid asset", "-1108"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid symbol type", "-1110"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid precision", "-1111"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Withdrawal amount must be negative", "-1113"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "TimeInForce sent when not required", "-1114"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "TimeInForce has invalid value", "-1115"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid order type", "-1116"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid side", "-1117"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Client order id not sent", "-1118"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Client order id not sent", "-1119"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid interval", "-1120"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid symbol status", "-1122"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Asset not supported", "-1126"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Lookup period too big", "-1127"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Parameter combination invalid", "-1128"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid parameter value", "-1130"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid newOrderRespType", "-1136"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Client order id invalid length", "-4015"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid activation price", "-4135"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Reduce only must be true for closing position", "-4138"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid listen key", "-1125"),

                new ErrorInfo(ErrorType.UnknownSymbol, false, "Invalid symbol", "-1121"),

                new ErrorInfo(ErrorType.Unknown, false, "Batch cancel failed", "-2012"),

                new ErrorInfo(ErrorType.InsufficientBalance, false, "Insufficient balance for order", "-2018"),
                new ErrorInfo(ErrorType.InsufficientBalance, false, "Insufficient margin", "-2019"),
                new ErrorInfo(ErrorType.InsufficientBalance, false, "Position not sufficient", "-2024"),
                new ErrorInfo(ErrorType.InsufficientBalance, false, "Insufficient cross margin balance", "-4050"),
                new ErrorInfo(ErrorType.InsufficientBalance, false, "Insufficient isolated margin balance", "-4051"),

                new ErrorInfo(ErrorType.UnknownOrder, false, "Order does not exist", "-2013"),

                new ErrorInfo(ErrorType.InvalidParameter, false, "Order type not supported with reduce only", "-2026"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Max leverage ratio exceeded", "-2027"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Min leverage ratio exceeded", "-2028"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid leverage", "-4028"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Quantity should be positive", "-4055"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid position side", "-4060"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid position side", "-4061"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Reduce only error", "-4062"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid transaction id length", "-4114"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Duplicate transaction id", "-4115"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Leverage setting not allowed in location", "-4206"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Leverage setting not allowed on symbol", "-4209"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Maximum modify order limit exceeded", "-5026"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid price match", "-5037"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid price match type", "-5038"),

                new ErrorInfo(ErrorType.InvalidPrice, false, "Price less than 0", "-4001"),
                new ErrorInfo(ErrorType.InvalidPrice, false, "Price greater than max price", "-4002"),
                new ErrorInfo(ErrorType.InvalidPrice, false, "Price not increased by tick size", "-4014"),
                new ErrorInfo(ErrorType.InvalidPrice, false, "Price lower than multiplier floor", "-4024"),
                new ErrorInfo(ErrorType.InvalidPrice, false, "Price higher than multiplier cap", "-4016"),
                new ErrorInfo(ErrorType.InvalidPrice, false, "(Stop)price too low", "-4184"),

                new ErrorInfo(ErrorType.InvalidQuantity, false, "Quantity less than 0", "-4003"),
                new ErrorInfo(ErrorType.InvalidQuantity, false, "Quantity less than min quantity", "-4004"),
                new ErrorInfo(ErrorType.InvalidQuantity, false, "Quantity greater than max quantity", "-4005"),
                new ErrorInfo(ErrorType.InvalidQuantity, false, "Quantity not increased by step size", "-4023"),
                new ErrorInfo(ErrorType.InvalidQuantity, false, "Order notional value too small", "-4164"),
                new ErrorInfo(ErrorType.InvalidQuantity, false, "Order notional value too small", "-5029"),

                new ErrorInfo(ErrorType.InvalidStopParameters, false, "Stop price less than 0", "-4006"),
                new ErrorInfo(ErrorType.InvalidStopParameters, false, "Stop price greater than max price", "-4007"),

                new ErrorInfo(ErrorType.AllOrdersFailed, false, "Batch orders failed", "-4083"),

                new ErrorInfo(ErrorType.DuplicateClientOrderId, false, "Duplicate client order id", "-4116"),

                new ErrorInfo(ErrorType.InvalidStopParameters, false, "Take profit or stop order will be triggered immediately", "-4142"),

                new ErrorInfo(ErrorType.UnknownSymbol, false, "Invalid symbol", "-4144"),
                new ErrorInfo(ErrorType.InvalidStopParameters, false, "Stop price higher than price multiplier cap", "-4210"),
                new ErrorInfo(ErrorType.InvalidStopParameters, false, "Stop price lower than price multiplier floor", "-4211"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Feature not allowed in your region", "-4402"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Leverage not allowed in your region", "-4403"),

                new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "FillOrKill order could not be filled immediately", "-5021"),
                new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "PostOnly order could not be placed", "-5022"),
                new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "Only limit orders supported", "-5025"),

                new ErrorInfo(ErrorType.InvalidOperation, false, "Operation not supported", "-1020"),
                new ErrorInfo(ErrorType.InvalidOperation, false, "Margin type can't be changed with open orders", "-4047"),
                new ErrorInfo(ErrorType.InvalidOperation, false, "Margin type can't be changed with open positions", "-4048"),
                new ErrorInfo(ErrorType.InvalidOperation, false, "Position side can't be changed with open orders", "-4067"),
                new ErrorInfo(ErrorType.InvalidOperation, false, "Position side can't be changed with open positions", "-4068"),
                new ErrorInfo(ErrorType.InvalidOperation, false, "Inactive account", "-4109"),
                new ErrorInfo(ErrorType.InvalidOperation, false, "Leverage reduction not allowed with open positions", "-4161"),
                new ErrorInfo(ErrorType.InvalidOperation, false, "Cooling off period", "-4192"),
                new ErrorInfo(ErrorType.InvalidOperation, false, "Symbol not in trading status", "-5024"),

            ]
        );
    }
}
