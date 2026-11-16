# Operational considerations

## Credentials

Use placeholders in examples and retrieve real API secrets or private keys from environment variables or secret storage. Match key permissions, signing format, and environment to the requested endpoint. HMAC, RSA XML, RSA PEM, and Ed25519 support varies by target framework; see [authentication](usage.md#authentication). Avoid logging signed requests or key material.

## Trading and account changes

Example prices and quantities are illustrative. Use the selected product's exchange info to check price, lot-size, and notional filters. Limit orders can fill immediately; cancellation is not a guarantee against execution.

For futures, verify position mode before opening or closing. Hedge mode uses `PositionSide.Long`/`Short`; do not apply a one-way `reduceOnly` recipe to every hedge-mode position. COIN-M contract quantities differ from USD-M base-asset quantities. Conditional orders can use dedicated methods such as `PlaceConditionalOrderAsync`; inspect the selected endpoint rather than treating every order type as interchangeable.

Batch and order-list calls can contain nested outcomes; outer success alone does not prove every operation succeeded. Margin borrowing, repayment, and transfers have different account effects from spot order placement.

## Retry decisions

Use `Error.IsTransient` as a retry candidate signal, with bounded backoff and cancellation. An order timeout can leave the exchange outcome unknown; reconcile its state before resubmitting. A transient error alone does not make an account-changing request safe to repeat.

For diagnostics, inspect `ErrorCode`, `Message`, and `ErrorType`. Timestamp errors such as `-1021` call for clock/AutoTimestamp checks; filter failures such as `-1013` call for validating parameters; invalid signatures such as `-1022` call for checking the selected credentials. Do not retry the same invalid parameters unchanged.

See [API surfaces](api-surfaces.md) for order lookup methods and [usage](usage.md) for result handling.
