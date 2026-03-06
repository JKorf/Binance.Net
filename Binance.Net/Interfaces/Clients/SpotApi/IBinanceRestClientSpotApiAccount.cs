using Binance.Net.Enums;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.IsolatedMargin;
using Binance.Net.Objects.Models.Spot.Margin;
using Binance.Net.Objects.Models.Spot.PortfolioMargin;

namespace Binance.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Binance Spot account endpoints. Account endpoints include balance info, withdraw/deposit info, and account settings.
    /// </summary>
    public interface IBinanceRestClientSpotApiAccount
    {
        /// <summary>
        /// Gets account information, including balances
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/account-endpoints" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/account
        /// </para>
        /// </summary>
        /// <param name="omitZeroBalances">["<c>omitZeroBalances</c>"] When true only return non-zero balances in the account</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The account information</returns>
        Task<WebCallResult<BinanceAccountInfo>> GetAccountInfoAsync(bool? omitZeroBalances = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a daily spot account snapshot (balances)
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/account/daily-account-snapshoot" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/accountSnapshot
        /// </para>
        /// </summary>
        /// <param name="startTime">["<c>startTime</c>"] The start time</param>
        /// <param name="endTime">["<c>endTime</c>"] The end time</param>
        /// <param name="limit">["<c>limit</c>"] The amount of days to retrieve</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Daily spot account snapshots</returns>
        Task<WebCallResult<BinanceSpotAccountSnapshot[]>> GetDailySpotAccountSnapshotAsync(
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Gets a daily margin account snapshot (assets)
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/account/daily-account-snapshoot" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/accountSnapshot
        /// </para>
        /// </summary>
        /// <param name="startTime">["<c>startTime</c>"] The start time</param>
        /// <param name="endTime">["<c>endTime</c>"] The end time</param>
        /// <param name="limit">["<c>limit</c>"] The amount of days to retrieve</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Daily margin account snapshots</returns>
        Task<WebCallResult<BinanceMarginAccountSnapshot[]>> GetDailyMarginAccountSnapshotAsync(
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Gets a daily futures account snapshot (assets and positions)
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/account/daily-account-snapshoot" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/accountSnapshot
        /// </para>
        /// </summary>
        /// <param name="startTime">["<c>startTime</c>"] The start time</param>
        /// <param name="endTime">["<c>endTime</c>"] The end time</param>
        /// <param name="limit">["<c>limit</c>"] The amount of days to retrieve</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Daily futures account snapshots</returns>
        Task<WebCallResult<BinanceFuturesAccountSnapshot[]>> GetDailyFutureAccountSnapshotAsync(
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Gets the status of the account associated with the api key/secret
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/account/account-status" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/account/status
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Account status</returns>
        Task<WebCallResult<BinanceAccountStatus>> GetAccountStatusAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets funding wallet assets
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/asset/funding-wallet" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/asset/get-funding-asset
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>asset</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="needBtcValuation">["<c>needBtcValuation</c>"] Return BTC valuation</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of assets</returns>
        Task<WebCallResult<BinanceFundingAsset[]>> GetFundingWalletAsync(string? asset = null, bool? needBtcValuation = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets permission info for the current API key
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/account/api-key-permission" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/account/apiRestrictions
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Permission info</returns>
        Task<WebCallResult<BinanceAPIKeyPermissions>> GetAPIKeyPermissionsAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets information of assets for a user
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/capital" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/capital/config/getall
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Assets info</returns>
        Task<WebCallResult<BinanceUserAsset[]>> GetUserAssetsAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves balance info
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/asset/user-assets" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v3/asset/getUserAsset
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>asset</c>"] Return for this asset, for example `ETH`</param>
        /// <param name="needBtcValuation">["<c>needBtcValuation</c>"] Whether the response should include the BtcValuation. If false (default) BtcValuation will be 0.</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>User balances</returns>
        Task<WebCallResult<BinanceUserBalance[]>> GetBalancesAsync(string? asset = null, bool? needBtcValuation = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets balances of the different user wallets
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/asset/query-user-wallet-balance" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/asset/wallet/balance
        /// </para>
        /// </summary>
        /// <param name="quoteAsset">["<c>quoteAsset</c>"] Quote asset, for example `USDT`, `ETH`, `USDC`, `BNB`, etc. default `BTC`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Wallet balances</returns>
        Task<WebCallResult<BinanceWalletBalance[]>> GetWalletBalancesAsync(string quoteAsset = "BTC", int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets asset dividend records
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/asset/assets-divided-record" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/asset/assetDividend
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>asset</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time from</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time till</param>
        /// <param name="limit">["<c>limit</c>"] Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Dividend records</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceDividendRecord>>> GetAssetDividendRecordsAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// This request will disable fastwithdraw switch under your account.
        /// You need to enable "trade" option for the api key which requests this endpoint.
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/account/disable-fast-withdraw-switch" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/account/disableFastWithdrawSwitch
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>A successful response</returns>
        Task<WebCallResult> DisableFastWithdrawSwitchAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// This request will enable fastwithdraw switch under your account.
        /// You need to enable "trade" option for the api key which requests this endpoint.
        ///
        /// When Fast Withdraw Switch is on, transferring funds to a Binance account will be done instantly.
        /// There is no on-chain transaction, no transaction ID and no withdrawal fee.
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/account/enable-fast-withdraw-switch" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/account/enableFastWithdrawSwitch
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>A successful response</returns>
        Task<WebCallResult> EnableFastWithdrawSwitchAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the history of dust conversions
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/asset/dust-log" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/asset/dribblet
        /// </para>
        /// </summary>
        /// <param name="startTime">["<c>startTime</c>"] The start time</param>
        /// <param name="endTime">["<c>endTime</c>"] The end time</param>
        /// <param name="accountType">["<c>accountType</c>"] Spot or Margin account</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The history of dust conversions</returns>
        Task<WebCallResult<BinanceDustLogList>> GetDustLogAsync(DateTime? startTime = null, DateTime? endTime = null, AccountType? accountType = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets assets that can be converted to BNB
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/asset/assets-can-convert-bnb" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/asset/dust-btc
        /// </para>
        /// </summary>
        /// <param name="accountType">["<c>accountType</c>"] Spot or Margin account</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Eligible dust assets</returns>
        Task<WebCallResult<BinanceEligibleDusts>> GetAssetsForDustTransferAsync(AccountType? accountType = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Converts dust (small amounts of) assets to BNB 
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/asset/dust-transfer" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/asset/dust
        /// </para>
        /// </summary>
        /// <param name="assets">["<c>asset</c>"] The assets to convert to BNB, for example `ETH`</param>
        /// <param name="accountType">["<c>accountType</c>"] Spot or Margin account</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Dust transfer result</returns>
        Task<WebCallResult<BinanceDustTransferResult>> DustTransferAsync(IEnumerable<string> assets, AccountType? accountType = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the status of the BNB burn switch for spot trading and margin interest
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/account/Get-BNB-Burn-Status" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/bnbBurn
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>BNB burn switch status</returns>
        Task<WebCallResult<BinanceBnbBurnStatus>> GetBnbBurnStatusAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Sets the status of the BNB burn switch for spot trading and margin interest
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/asset/Toggle-BNB-Burn-On-Spot-Trade-And-Margin-Interest" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/bnbBurn
        /// </para>
        /// </summary>
        /// <param name="spotTrading">["<c>spotBNBBurn</c>"] If BNB burning should be enabled for spot trading</param>
        /// <param name="marginInterest">["<c>interestBNBBurn</c>"] If BNB burning should be enabled for margin interest</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Updated BNB burn switch status</returns>
        Task<WebCallResult<BinanceBnbBurnStatus>> SetBnbBurnStatusAsync(bool? spotTrading = null, bool? marginInterest = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Transfers between accounts
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/asset/user-universal-transfer" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/asset/transfer
        /// </para>
        /// </summary>
        /// <param name="type">["<c>type</c>"] The type of transfer</param>
        /// <param name="asset">["<c>asset</c>"] The asset to transfer, for example `ETH`</param>
        /// <param name="quantity">["<c>amount</c>"] The quantity to transfer</param>
        /// <param name="fromSymbol">["<c>fromSymbol</c>"] From symbol when transferring from/to isolated margin</param>
        /// <param name="toSymbol">["<c>toSymbol</c>"] To symbol when transferring from/to isolated margin</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transfer transaction details</returns>
        Task<WebCallResult<BinanceTransaction>> TransferAsync(UniversalTransferType type, string asset, decimal quantity, string? fromSymbol = null, string? toSymbol = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets transfer history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/asset/query-user-universal-transfer" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/asset/transfer
        /// </para>
        /// </summary>
        /// <param name="type">["<c>type</c>"] The type of transfer</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="page">["<c>current</c>"] The page</param>
        /// <param name="pageSize">["<c>size</c>"] Results per page</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transfer history</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceTransfer>>> GetTransfersAsync(UniversalTransferType type, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets fiat payment history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/fiat/rest-api/Get-Fiat-Payments-History" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/fiat/payments
        /// </para>
        /// </summary>
        /// <param name="side">["<c>transactionType</c>"] Filter by side</param>
        /// <param name="startTime">["<c>beginTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="page">["<c>page</c>"] Return a specific page</param>
        /// <param name="limit">["<c>limit</c>"] The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Fiat payment history</returns>
        Task<WebCallResult<BinanceFiatPayment[]>> GetFiatPaymentHistoryAsync(OrderSide side, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets fiat deposit and withdrawal history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/fiat/rest-api" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/fiat/orders
        /// </para>
        /// </summary>
        /// <param name="side">["<c>transactionType</c>"] Filter by side</param>
        /// <param name="startTime">["<c>beginTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="page">["<c>page</c>"] Return a specific page</param>
        /// <param name="limit">["<c>limit</c>"] The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Fiat deposit and withdrawal history</returns>
        Task<WebCallResult<BinanceFiatWithdrawDeposit[]>> GetFiatDepositWithdrawHistoryAsync(TransactionType side, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Withdraw assets from Binance to an address
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/capital/withdraw" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/capital/withdraw/apply
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>coin</c>"] The asset to withdraw, for example `ETH`</param>
        /// <param name="address">["<c>address</c>"] The address to send the funds to</param>
        /// <param name="addressTag">["<c>addressTag</c>"] Secondary address identifier for assets like XRP,XMR etc.</param>
        /// <param name="withdrawOrderId">["<c>withdrawOrderId</c>"] Custom client order id</param>
        /// <param name="transactionFeeFlag">["<c>transactionFeeFlag</c>"] When making internal transfer, true for returning the fee to the destination account; false for returning the fee back to the departure account. Default false.</param>
        /// <param name="quantity">["<c>amount</c>"] The quantity to withdraw</param>
        /// <param name="network">["<c>network</c>"] The network to use</param>
        /// <param name="walletType">["<c>walletType</c>"] The wallet type for withdraw</param>
        /// <param name="name">["<c>name</c>"] Description of the address</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Withdrawal confirmation</returns>
        Task<WebCallResult<BinanceWithdrawalPlaced>> WithdrawAsync(string asset, string address, decimal quantity, string? withdrawOrderId = null, string? network = null, string? addressTag = null, string? name = null, bool? transactionFeeFlag = null, WalletType? walletType = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the withdrawal history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/capital/withdraw-history" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/capital/withdraw/history
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>asset</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="withdrawOrderId">["<c>withdrawOrderId</c>"] Filter by withdraw order id</param>
        /// <param name="status">["<c>status</c>"] Filter by status</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter start time from</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter end time till</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <param name="limit">["<c>limit</c>"] Add limit. Default: 1000, Max: 1000</param>
        /// <param name="offset">["<c>offset</c>"] Add offset</param>
        /// <param name="ids">["<c>idList</c>"] Filter by withdrawal ids</param>
        /// <returns>List of withdrawals</returns>
        Task<WebCallResult<BinanceWithdrawal[]>> GetWithdrawalHistoryAsync(string? asset = null, string? withdrawOrderId = null, WithdrawalStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? receiveWindow = null, int? limit = null, int? offset = null, IEnumerable<string>? ids = null, CancellationToken ct = default);

        /// <summary>
        /// Gets withdrawal addresses
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/capital/fetch-withdraw-address" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/capital/withdraw/address/list
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Withdrawal addresses</returns>
        Task<WebCallResult<BinanceWithdrawalAddress[]>> GetWithdrawalAddressesAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the deposit history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/capital/deposite-history" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/capital/deposit/hisrec
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>coin</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="status">["<c>status</c>"] Filter by status</param>
        /// <param name="limit">["<c>limit</c>"] Amount of results</param>
        /// <param name="offset">["<c>offset</c>"] Offset the results</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter start time from</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter end time till</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="includeSource">["<c>includeSource</c>"] Include source address to response</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of deposits</returns>
        Task<WebCallResult<BinanceDeposit[]>> GetDepositHistoryAsync(string? asset = null, DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? limit = null, int? receiveWindow = null, bool includeSource = false, CancellationToken ct = default);

        /// <summary>
        /// Gets the deposit address for an asset
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/capital/deposite-address" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/capital/deposit/address
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>coin</c>"] Asset to get address for, for example `ETH`</param>
        /// <param name="network">["<c>network</c>"] Network</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Deposit address</returns>
        Task<WebCallResult<BinanceDepositAddress>> GetDepositAddressAsync(string asset, string? network = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets all deposit addresses for an asset
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/capital/fetch-deposit-address-list-with-network" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/capital/deposit/address/list
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>coin</c>"] Asset to get address for, for example `ETH`</param>
        /// <param name="network">["<c>network</c>"] Filter by network</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Deposit addresses</returns>
        Task<WebCallResult<BinanceDepositAddress[]>> GetDepositAddressesAsync(string asset, string? network = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets personal margin level information for your account
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/account/Get-Summary-Of-Margin-Account" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/tradeCoeff
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin level information</returns>
        Task<WebCallResult<BinanceMarginLevel>> GetMarginLevelInformationAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Borrows an asset by submitting a margin loan request
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/borrow-and-repay/Margin-Account-Borrow-Repay" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/margin/borrow-repay
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>asset</c>"] The asset to borrow, for example `ETH`</param>
        /// <param name="quantity">["<c>amount</c>"] The quantity to borrow</param>
        /// <param name="isIsolated">["<c>isIsolated</c>"] Whether to use isolated margin</param>
        /// <param name="symbol">["<c>symbol</c>"] The isolated symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Borrow transaction details</returns>
        Task<WebCallResult<BinanceTransaction>> MarginBorrowAsync(string asset, decimal quantity, bool? isIsolated = null, string? symbol = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Repays a margin loan
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/borrow-and-repay/Margin-Account-Borrow-Repay" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/margin/borrow-repay
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>asset</c>"] The asset to repay, for example `ETH`</param>
        /// <param name="quantity">["<c>amount</c>"] The quantity to repay</param>
        /// <param name="isIsolated">["<c>isIsolated</c>"] Whether to use isolated margin</param>
        /// <param name="symbol">["<c>symbol</c>"] The isolated symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Repay transaction details</returns>
        Task<WebCallResult<BinanceTransaction>> MarginRepayAsync(string asset, decimal quantity, bool? isIsolated = null, string? symbol = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets margin transfer history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/transfer" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/transfer
        /// </para>
        /// </summary>
        /// <param name="direction">["<c>direction</c>"] The transfer direction to retrieve</param>
        /// <param name="page">["<c>current</c>"] Results page</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time from</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time from</param>
        /// <param name="limit">["<c>size</c>"] Limit of the amount of results</param>
        /// <param name="isolatedSymbol">["<c>isolatedSymbol</c>"] Filter by isolated symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of transfers</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceTransferHistory>>> GetMarginTransferHistoryAsync(TransferDirection direction, int? page = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, string? isolatedSymbol = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets margin borrow records
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/borrow-and-repay/Query-Borrow-Repay" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/borrow-repay
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>asset</c>"] The records asset, for example `ETH`</param>
        /// <param name="transactionId">["<c>txId</c>"] The loan transaction id</param>
        /// <param name="startTime">["<c>startTime</c>"] Time to start getting records from</param>
        /// <param name="endTime">["<c>endTime</c>"] Time to stop getting records to</param>
        /// <param name="current">["<c>current</c>"] Page number</param>
        /// <param name="isolatedSymbol">["<c>isolatedSymbol</c>"] Filter by isolated symbol</param>
        /// <param name="limit">["<c>size</c>"] Number of records per page</param>
        /// <param name="archived">["<c>archived</c>"] Set to true for archived data from 6 months ago</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Loan records</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceLoan>>> GetMarginLoansAsync(string asset, long? transactionId = null, DateTime? startTime = null, DateTime? endTime = null, int? current = 1, int? limit = 10, string? isolatedSymbol = null, bool? archived = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets margin repay records
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/borrow-and-repay/Query-Borrow-Repay" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/borrow-repay
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>asset</c>"] The records asset, for example `ETH`</param>
        /// <param name="transactionId">["<c>txId</c>"] The repay transaction id</param>
        /// <param name="startTime">["<c>startTime</c>"] Time to start getting records from</param>
        /// <param name="endTime">["<c>endTime</c>"] Time to stop getting records to</param>
        /// <param name="current">["<c>current</c>"] Filter by number</param>
        /// <param name="isolatedSymbol">["<c>isolatedSymbol</c>"] Filter by isolated symbol</param>
        /// <param name="size">["<c>size</c>"] Number of records per page</param>
        /// <param name="archived">["<c>archived</c>"] Set to true for archived data from 6 months ago</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Repay records</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceRepay>>> GetMarginRepaysAsync(string asset, long? transactionId = null, DateTime? startTime = null, DateTime? endTime = null, int? current = null, int? size = null, string? isolatedSymbol = null, bool? archived = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets margin interest history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/borrow-and-repay/Get-Interest-History" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/interestHistory
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>coin</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="page">["<c>page</c>"] Results page</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time from</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time from</param>
        /// <param name="isolatedSymbol">["<c>isolatedSymbol</c>"] Filter by isolated symbol</param>
        /// <param name="limit">["<c>limit</c>"] Limit of the amount of results</param>
        /// <param name="archived">["<c>archived</c>"] Set to true for archived data from 6 months ago</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of interest events</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceInterestHistory>>> GetMarginInterestHistoryAsync(string? asset = null, int? page = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, string? isolatedSymbol = null, bool? archived = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets margin interest rate history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/borrow-and-repay/Query-Margin-Interest-Rate-History" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/interestRateHistory
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>coin</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="vipLevel">["<c>vipLevel</c>"] VIP level</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by startTime from</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by endTime from</param>
        /// <param name="limit">["<c>limit</c>"] Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Interest rate history</returns>
        Task<WebCallResult<BinanceInterestRateHistory[]>> GetMarginInterestRateHistoryAsync(string asset, string? vipLevel = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets cross margin interest data
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/account/Query-Cross-Margin-Fee-Data" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/crossMarginData
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>coin</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="vipLevel">["<c>vipLevel</c>"] VIP level</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Cross margin interest data</returns>
        Task<WebCallResult<BinanceInterestMarginData[]>> GetInterestMarginDataAsync(string? asset = null, string? vipLevel = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get history of forced liquidations
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/trade" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/forceLiquidationRec
        /// </para>
        /// </summary>
        /// <param name="page">["<c>page</c>"] Results page</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by startTime from</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by endTime from</param>
        /// <param name="isolatedSymbol">["<c>isolatedSymbol</c>"] Filter by isolated symbol</param>
        /// <param name="limit">["<c>limit</c>"] Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of forced liquidations</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceForcedLiquidation>>> GetMarginForcedLiquidationHistoryAsync(int? page = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, string? isolatedSymbol = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets cross margin account details
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/account/Query-Cross-Margin-Account-Details" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/account
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The margin account information</returns>
        Task<WebCallResult<BinanceMarginAccount>> GetMarginAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the maximum borrowable amount
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/borrow-and-repay/Query-Max-Borrow" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/maxBorrowable
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>asset</c>"] The asset, for example `ETH`</param>
        /// <param name="isolatedSymbol">["<c>isolatedSymbol</c>"] The isolated symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Max quantity</returns>
        Task<WebCallResult<BinanceMarginAmount>> GetMarginMaxBorrowAmountAsync(string asset, string? isolatedSymbol = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the maximum transferable amount
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/transfer/Query-Max-Transfer-Out-Amount" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/maxTransferable
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>asset</c>"] The asset, for example `ETH`</param>
        /// <param name="isolatedSymbol">["<c>isolatedSymbol</c>"] The isolated symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Max quantity</returns>
        Task<WebCallResult<decimal>> GetMarginMaxTransferAmountAsync(string asset, string? isolatedSymbol = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets isolated margin account information
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/account/Query-Isolated-Margin-Account-Info" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/isolated/account
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Isolated margin account information</returns>
        Task<WebCallResult<BinanceIsolatedMarginAccount>> GetIsolatedMarginAccountAsync(
            int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the maximum number of enabled isolated margin accounts
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/account/Query-Enabled-Isolated-Margin-Account-Limit" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/isolated/accountLimit
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Enabled isolated margin account limit</returns>
        Task<WebCallResult<IsolatedMarginAccountLimit>> GetEnabledIsolatedMarginAccountLimitAsync(
            int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Enable an isolated margin account
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/account/Enable-Isolated-Margin-Account" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/margin/isolated/account
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol to enable isolated margin account for, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Created isolated margin account details</returns>
        Task<WebCallResult<CreateIsolatedMarginAccountResult>> EnableIsolatedMarginAccountAsync(string symbol, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Disables an isolated margin account
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/account/Disable-Isolated-Margin-Account" /><br />
        /// Endpoint:<br />
        /// DELETE /sapi/v1/margin/isolated/account
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol to disable isolated margin account for, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Disabled isolated margin account details</returns>
        Task<WebCallResult<CreateIsolatedMarginAccountResult>> DisableIsolatedMarginAccountAsync(string symbol,
            int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets isolated margin order rate limits
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/trade/Query-Current-Margin-Order-Count-Usage" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/rateLimit/order
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Current margin order rate limit usage</returns>
        Task<WebCallResult<BinanceCurrentRateLimit[]>> GetMarginOrderRateLimitStatusAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Creates a listenToken for subscribing to the cross margin user data stream.
        /// The token is valid for up to 24 hours.
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/trade-data-stream" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/userDataStream
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol for isolated margin, null for cross margin</param>
        /// <param name="validity">["<c>validity</c>"] Validity of the token, max 24 hours</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>ListenToken and expiration time</returns>
        Task<WebCallResult<BinanceListenToken>> GetMarginUserListenTokenAsync(string? symbol = null, TimeSpan? validity = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the trading status for the current account
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/account/account-api-trading-status" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/account/apiTradingStatus
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The trading status of the account</returns>
        Task<WebCallResult<BinanceTradingStatus>> GetTradingStatusAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the current used order rate limits
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/account-endpoints#query-unfilled-order-count-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/rateLimit/order
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Current order rate limit usage</returns>
        Task<WebCallResult<BinanceCurrentRateLimit[]>> GetOrderRateLimitStatusAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets rebate history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/rebate/rest-api" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/rebate/taxQuery
        /// </para>
        /// </summary>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="page">["<c>page</c>"] Results per page</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Rebate history</returns>
        Task<WebCallResult<BinanceRebateWrapper>> GetRebateHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets portfolio margin account information
        /// <para>
        /// Docs:<br />
        /// <a href="https://binance-docs.github.io/apidocs/spot/en/#get-classic-portfolio-margin-account-info-user_data" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/portfolio/account
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Portfolio margin account information</returns>
        Task<WebCallResult<BinancePortfolioMarginInfo>> GetPortfolioMarginAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets portfolio margin account collateral rates
        /// <para>
        /// Docs:<br />
        /// <a href="https://binance-docs.github.io/apidocs/spot/en/#classic-portfolio-margin-collateral-rate-market_data" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/portfolio/collateralRate
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Portfolio margin collateral rates</returns>
        Task<WebCallResult<BinancePortfolioMarginCollateralRate[]>> GetPortfolioMarginCollateralRateAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets portfolio margin bankruptcy loan amount
        /// <para>
        /// Docs:<br />
        /// <a href="https://binance-docs.github.io/apidocs/spot/en/#query-classic-portfolio-margin-bankruptcy-loan-amount-user_data" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/portfolio/pmLoan
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Portfolio margin bankruptcy loan information</returns>
        Task<WebCallResult<BinancePortfolioMarginLoan>> GetPortfolioMarginBankruptcyLoanAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Repays portfolio margin bankruptcy loan
        /// <para>
        /// Docs:<br />
        /// <a href="https://binance-docs.github.io/apidocs/spot/en/#classic-portfolio-margin-bankruptcy-loan-repay" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/portfolio/repay
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Repay transaction details</returns>
        Task<WebCallResult<BinanceTransaction>> PortfolioMarginBankruptcyLoanRepayAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets BUSD convert history
        /// <para>
        /// Docs:<br />
        /// <a href="https://binance-docs.github.io/apidocs/spot/en/#busd-convert-history-user_data" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/asset/convert-transfer/queryByPage
        /// </para>
        /// </summary>
        /// <param name="transferId">["<c>tranId</c>"] Filter by transferId</param>
        /// <param name="clientTransferId">["<c>clientTranId</c>"] Filter by clientTransferId</param>
        /// <param name="asset">["<c>asset</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="page">["<c>current</c>"] Page</param>
        /// <param name="pageSize">["<c>size</c>"] Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>BUSD convert history</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceBusdHistory>>> GetBusdConvertHistoryAsync(DateTime startTime, DateTime endTime, long? transferId = null, string? clientTransferId = null, string? asset = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets cloud mining payment and refund history
        /// <para>
        /// Docs:<br />
        /// <a href="https://binance-docs.github.io/apidocs/spot/en/#get-cloud-mining-payment-and-refund-history-user_data" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/asset/ledger-transfer/cloud-mining/queryByPage
        /// </para>
        /// </summary>
        /// <param name="transferId">["<c>tranId</c>"] Filter by transferId</param>
        /// <param name="clientTransferId">["<c>clientTranId</c>"] Filter by clientTransferId</param>
        /// <param name="asset">["<c>asset</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="page">["<c>current</c>"] Page</param>
        /// <param name="pageSize">["<c>size</c>"] Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Cloud mining payment and refund history</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceCloudMiningHistory>>> GetCloudMiningHistoryAsync(DateTime startTime, DateTime endTime, long? transferId = null, string? clientTransferId = null, string? asset = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Adjusts cross margin maximum leverage
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/account" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/margin/max-leverage
        /// </para>
        /// </summary>
        /// <param name="maxLeverage">["<c>maxLeverage</c>"] Max leverage, can only adjust 3 or 5</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Updated cross margin leverage settings</returns>
        Task<WebCallResult<BinanceCrossMarginLeverageResult>> CrossMarginAdjustMaxLeverageAsync(int maxLeverage, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets isolated margin fee data for a specific VIP level or the current user level
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/account/Query-Isolated-Margin-Fee-Data" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/isolatedMarginData
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="vipLevel">["<c>vipLevel</c>"] VIP level to query. If omitted, current user margin data is returned</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Isolated margin fee data</returns>
        Task<WebCallResult<BinanceIsolatedMarginFeeData[]>> GetIsolatedMarginFeeDataAsync(string? symbol = null, int? vipLevel = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets cross and isolated margin capital flow data for the last 90 days
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/account/Query-Cross-Isolated-Margin-Capital-Flow" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/capital-flow
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>asset</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="type">["<c>type</c>"] Filter by transaction type</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="fromId">["<c>fromId</c>"] If set, data with 'Id' greater than 'fromId' will be returned. Otherwise, the latest data will be returned</param>
        /// <param name="limit">["<c>limit</c>"] Number of records to retrieve. Default: 500, Max: 1000</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin capital flow data</returns>
        Task<WebCallResult<BinanceMarginCapitalFlowData[]>> GetMarginCapitalFlowDataAsync(string? asset = null, string? symbol = null, CapitalTransactionType? type = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets assets eligible for small liability exchange
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/trade/Get-Small-Liability-Exchange-Coin-List" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/exchange-small-liability
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Assets eligible for small liability exchange</returns>
        Task<WebCallResult<BinanceSmallLiabilityAsset[]>> GetCrossMarginSmallLiabilityExchangeAssetsAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Executes a cross margin small liability exchange
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/trade/Small-Liability-Exchange" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/margin/exchange-small-liability
        /// </para>
        /// </summary>
        /// <param name="assets">["<c>assetNames</c>"] Assets, for example `ETH`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>A successful response</returns>
        Task<WebCallResult> CrossMarginSmallLiabilityExchangeAsync(IEnumerable<string> assets, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets small liability exchange history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/trade/Get-Small-Liability-Exchange-History" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/exchange-small-liability-history
        /// </para>
        /// </summary>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="page">["<c>current</c>"] The page</param>
        /// <param name="limit">["<c>size</c>"] Results per page</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Small liability exchange history</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceSmallLiabilityHistory>>> GetCrossMarginSmallLiabilityExchangeHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the trade fee for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/asset/trade-fee" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/asset/tradeFee
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol to get withdrawal fee for, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Trade fees</returns>
        Task<WebCallResult<BinanceTradeFee[]>> GetTradeFeeAsync(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets account VIP level and margin/futures enabled status
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/account" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/account/info
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Account VIP level and margin/futures status</returns>
        Task<WebCallResult<BinanceVipLevelAndStatus>> GetAccountVipLevelAndStatusAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets current account commission rates
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/account-endpoints#query-commission-rates-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/account/commission
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Commission rates for the symbol</returns>
        Task<WebCallResult<BinanceCommissions>> GetCommissionRatesAsync(string symbol, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Starts a risk data user stream by requesting a listen key. The stream closes after 60 minutes unless keep-alive requests are sent.
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/risk-data-stream/Start-User-Data-Stream" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/margin/listen-key
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Listen key</returns>
        Task<WebCallResult<string>> StartRiskDataUserStreamAsync(CancellationToken ct = default);

        /// <summary>
        /// Sends a keep-alive for the current risk data user stream listen key. The stream auto-closes after 60 minutes if no keep-alive is sent.
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/risk-data-stream/Keepalive-User-Data-Stream" /><br />
        /// Endpoint:<br />
        /// PUT /sapi/v1/margin/listen-key
        /// </para>
        /// </summary>
        /// <param name="listenKey">["<c>listenKey</c>"] The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>A successful response</returns>
        Task<WebCallResult> KeepAliveRiskDataUserStreamAsync(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Stops the current user stream
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/risk-data-stream/Close-User-Data-Stream" /><br />
        /// Endpoint:<br />
        /// DELETE /sapi/v1/margin/listen-key
        /// </para>
        /// </summary>
        /// <param name="listenKey">["<c>listenKey</c>"] The listen key to stop</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>A successful response</returns>
        Task<WebCallResult> StopRiskDataUserStreamAsync(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Submit a withdraw request with questionnaire required for certain regions
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/travel-rule/withdraw" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/localentity/withdraw/apply
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>coin</c>"] The asset to withdraw, for example `ETH`</param>
        /// <param name="address">["<c>address</c>"] The address to send the funds to</param>
        /// <param name="addressTag">["<c>addressTag</c>"] Secondary address identifier for assets like XRP,XMR etc.</param>
        /// <param name="withdrawOrderId">["<c>withdrawOrderId</c>"] Custom client order id</param>
        /// <param name="transactionFeeFlag">["<c>transactionFeeFlag</c>"] When making internal transfer, true for returning the fee to the destination account; false for returning the fee back to the departure account. Default false.</param>
        /// <param name="quantity">["<c>amount</c>"] The quantity to withdraw</param>
        /// <param name="questionnaire">["<c>questionnaire</c>"] Questionnaire answers. Use BinanceWithdrawQuestionnaire to create the question list, for example <code>var questionnaire = BinanceWithdrawQuestionnaire.Eu;</code></param>
        /// <param name="network">["<c>network</c>"] The network to use</param>
        /// <param name="walletType">["<c>walletType</c>"] The wallet type for withdraw</param>
        /// <param name="name">["<c>name</c>"] Description of the address</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Withdrawal confirmation</returns>
        Task<WebCallResult<BinanceTravelWithdrawalResponse>> TravelRuleWithdrawAsync(
            string asset,
            string address,
            decimal quantity,
            BinanceWithdrawQuestionnaire questionnaire,
            string? withdrawOrderId = null,
            string? network = null,
            string? addressTag = null,
            string? name = null,
            bool? transactionFeeFlag = null,
            WalletType? walletType = null,
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Gets travel rule withdrawal history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/travel-rule/withdraw-history-v2" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v2/localentity/withdraw/history
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>coin</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="withdrawOrderId">["<c>withdrawOrderId</c>"] Filter by withdraw order id</param>
        /// <param name="status">["<c>travelRuleStatus</c>"] Filter by status</param>
        /// <param name="network">["<c>network</c>"] Filter by network</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter start time from</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter end time till</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="limit">["<c>limit</c>"] Add limit. Default: 1000, Max: 1000</param>
        /// <param name="offset">["<c>offset</c>"] Add offset</param>
        /// <param name="travelRuleIds">["<c>trId</c>"] Filter by travel rule ids</param>
        /// <param name="transactionIds">["<c>txId</c>"] Filter by transaction ids</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of withdrawals</returns>
        Task<WebCallResult<BinanceTravelRuleWithdrawal[]>> GetTravelRuleWithdrawalHistoryAsync(
            string? asset = null,
            string? withdrawOrderId = null,
            TravelRuleApproveStatus? status = null,
            string? network = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            int? offset = null,
            IEnumerable<string>? travelRuleIds = null,
            IEnumerable<string>? transactionIds = null,
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Gets travel rule requirements
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/travel-rule/questionnaire-requirements" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/localentity/questionnaire-requirements
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Travel rule requirements</returns>
        Task<WebCallResult<BinanceTravelRuleRequirement>> GetTravelRuleRequirementAsync(
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Gets the travel rule address verification list
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/travel-rule/address-verification-list" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/addressVerify/list
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Travel rule address verification list</returns>
        Task<WebCallResult<BinanceTravelRuleAddress[]>> GetTravelRuleAddressVerificationListAsync(
           int? receiveWindow = null,
           CancellationToken ct = default);

        /// <summary>
        /// Gets the travel rule VASP list
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/travel-rule/onboarded-vasp-list" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/localentity/vasp
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Travel rule VASP list</returns>
        Task<WebCallResult<BinanceTravelRuleVasp[]>> GetTravelRuleVaspListAsync(
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Gets travel rule deposit history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/travel-rule/deposit-history-v2" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v2/localentity/deposit/history
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>coin</c>"] Filter by asset</param>
        /// <param name="depositIds">["<c>depositId</c>"] Filter by deposit ids</param>
        /// <param name="transactionIds">["<c>txId</c>"] Filter by transaction ids</param>
        /// <param name="network">["<c>network</c>"] Filter by network</param>
        /// <param name="retrieveQuestionnaire">["<c>retrieveQuestionnaire</c>"] Whether to return questionnaire answers</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter start time from</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter end time till</param>
        /// <param name="limit">["<c>limit</c>"] Add limit. Default: 1000, Max: 1000</param>
        /// <param name="offset">["<c>offset</c>"] Add offset</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Travel rule deposit history</returns>
        Task<WebCallResult<BinanceTravelRuleDeposit[]>> GetTravelRuleDepositHistoryAsync(
            string? asset = null,
            IEnumerable<string>? depositIds = null,
            IEnumerable<string>? transactionIds = null,
            string? network = null,
            bool? retrieveQuestionnaire = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            int? offset = null,
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Submits a deposit travel rule questionnaire
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/travel-rule/deposit-provide-info-v2" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v2/localentity/deposit/provide-info
        /// </para>
        /// </summary>
        /// <param name="depositId">["<c>depositId</c>"] Deposit id</param>
        /// <param name="questionnaire">["<c>questionnaire</c>"] Questionnaire answers. Use BinanceDepositQuestionnaire to create the question list, for example <code>var questionnaire = BinanceDepositQuestionnaire.Eu;</code></param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Travel rule questionnaire submission result</returns>
        Task<WebCallResult<BinanceTravelRuleSubmitResult>> SubmitTravelRuleQuestionnaireAsync(
            long depositId,
            BinanceDepositQuestionnaire questionnaire,
            int? receiveWindow = null,
            CancellationToken ct = default);
    }
}





