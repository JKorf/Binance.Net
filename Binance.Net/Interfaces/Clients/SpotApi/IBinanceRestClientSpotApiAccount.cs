using Binance.Net.Enums;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.IsolatedMargin;
using Binance.Net.Objects.Models.Spot.Margin;
using Binance.Net.Objects.Models.Spot.PortfolioMargin;

namespace Binance.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Binance Spot account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IBinanceRestClientSpotApiAccount
    {
        /// <summary>
        /// Gets account information, including balances
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/account-endpoints" /></para>
        /// </summary>
        /// <param name="omitZeroBalances">When true only return non-zero balances in the account</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The account information</returns>
        Task<WebCallResult<BinanceAccountInfo>> GetAccountInfoAsync(bool? omitZeroBalances = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get a daily account snapshot (balances)
        /// <para><a href="https://developers.binance.com/docs/wallet/account/daily-account-snapshoot" /></para>
        /// </summary>
        /// <param name="startTime">The start time</param>
        /// <param name="endTime">The end time</param>
        /// <param name="limit">The amount of days to retrieve</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceSpotAccountSnapshot[]>> GetDailySpotAccountSnapshotAsync(
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get a daily account snapshot (assets)
        /// <para><a href="https://developers.binance.com/docs/wallet/account/daily-account-snapshoot" /></para>
        /// </summary>
        /// <param name="startTime">The start time</param>
        /// <param name="endTime">The end time</param>
        /// <param name="limit">The amount of days to retrieve</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceMarginAccountSnapshot[]>> GetDailyMarginAccountSnapshotAsync(
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get a daily account snapshot (assets and positions)
        /// <para><a href="https://developers.binance.com/docs/wallet/account/daily-account-snapshoot" /></para>
        /// </summary>
        /// <param name="startTime">The start time</param>
        /// <param name="endTime">The end time</param>
        /// <param name="limit">The amount of days to retrieve</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceFuturesAccountSnapshot[]>> GetDailyFutureAccountSnapshotAsync(
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Gets the status of the account associated with the api key/secret
        /// <para><a href="https://developers.binance.com/docs/wallet/account/account-status" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Account status</returns>
        Task<WebCallResult<BinanceAccountStatus>> GetAccountStatusAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get funding wallet assets
        /// <para><a href="https://developers.binance.com/docs/wallet/asset/funding-wallet" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="needBtcValuation">Return BTC valuation</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of assets</returns>
        Task<WebCallResult<BinanceFundingAsset[]>> GetFundingWalletAsync(string? asset = null, bool? needBtcValuation = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get permission info for the current API key
        /// <para><a href="https://developers.binance.com/docs/wallet/account/api-key-permission" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Permission info</returns>
        Task<WebCallResult<BinanceAPIKeyPermissions>> GetAPIKeyPermissionsAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets information of assets for a user
        /// <para><a href="https://developers.binance.com/docs/wallet/capital" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Assets info</returns>
        Task<WebCallResult<BinanceUserAsset[]>> GetUserAssetsAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieve balance info
        /// <para><a href="https://developers.binance.com/docs/wallet/asset/user-assets" /></para>
        /// </summary>
        /// <param name="asset">Return for this asset, for example `ETH`</param>
        /// <param name="needBtcValuation">Whether the response should include the BtcValuation. If false (default) BtcValuation will be 0.</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceUserBalance[]>> GetBalancesAsync(string? asset = null, bool? needBtcValuation = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Receive balances of the different user wallets
        /// <para><a href="https://developers.binance.com/docs/wallet/asset/query-user-wallet-balance" /></para>
        /// </summary>
        /// <param name="quoteAsset">Quote asset, for example `USDT`, `ETH`, `USDC`, `BNB`, etc. default `BTC`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceWalletBalance[]>> GetWalletBalancesAsync(string quoteAsset = "BTC", int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get asset dividend records
        /// <para><a href="https://developers.binance.com/docs/wallet/asset/assets-divided-record" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="startTime">Filter by start time from</param>
        /// <param name="endTime">Filter by end time till</param>
        /// <param name="limit">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Dividend records</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceDividendRecord>>> GetAssetDividendRecordsAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// This request will disable fastwithdraw switch under your account.
        /// You need to enable "trade" option for the api key which requests this endpoint.
        /// <para><a href="https://developers.binance.com/docs/wallet/account/disable-fast-withdraw-switch" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> DisableFastWithdrawSwitchAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// This request will enable fastwithdraw switch under your account.
        /// You need to enable "trade" option for the api key which requests this endpoint.
        ///
        /// When Fast Withdraw Switch is on, transferring funds to a Binance account will be done instantly.
        /// There is no on-chain transaction, no transaction ID and no withdrawal fee.
        /// <para><a href="https://developers.binance.com/docs/wallet/account/enable-fast-withdraw-switch" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> EnableFastWithdrawSwitchAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the history of dust conversions
        /// <para><a href="https://developers.binance.com/docs/wallet/asset/dust-log" /></para>
        /// </summary>
        /// <param name="startTime">The start time</param>
        /// <param name="endTime">The end time</param>
        /// <param name="accountType">Spot or Margin account</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The history of dust conversions</returns>
        Task<WebCallResult<BinanceDustLogList>> GetDustLogAsync(DateTime? startTime = null, DateTime? endTime = null, AccountType? accountType = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get assets that can be converted to BNB
        /// <para><a href="https://developers.binance.com/docs/wallet/asset/assets-can-convert-bnb" /></para>
        /// </summary>
        /// <param name="accountType">Spot or Margin account</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceEligibleDusts>> GetAssetsForDustTransferAsync(AccountType? accountType = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Converts dust (small amounts of) assets to BNB 
        /// <para><a href="https://developers.binance.com/docs/wallet/asset/dust-transfer" /></para>
        /// </summary>
        /// <param name="assets">The assets to convert to BNB, for example `ETH`</param>
        /// <param name="accountType">Spot or Margin account</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Dust transfer result</returns>
        Task<WebCallResult<BinanceDustTransferResult>> DustTransferAsync(IEnumerable<string> assets, AccountType? accountType = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the status of the BNB burn switch for spot trading and margin interest
        /// <para><a href="https://developers.binance.com/docs/margin_trading/account/Get-BNB-Burn-Status" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceBnbBurnStatus>> GetBnbBurnStatusAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Sets the status of the BNB burn switch for spot trading and margin interest
        /// <para><a href="https://developers.binance.com/docs/wallet/asset/Toggle-BNB-Burn-On-Spot-Trade-And-Margin-Interest" /></para>
        /// </summary>
        /// <param name="spotTrading">If BNB burning should be enabled for spot trading</param>
        /// <param name="marginInterest">If BNB burning should be enabled for margin interest</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceBnbBurnStatus>> SetBnbBurnStatusAsync(bool? spotTrading = null, bool? marginInterest = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Transfers between accounts
        /// <para><a href="https://developers.binance.com/docs/wallet/asset/user-universal-transfer" /></para>
        /// </summary>
        /// <param name="type">The type of transfer</param>
        /// <param name="asset">The asset to transfer, for example `ETH`</param>
        /// <param name="quantity">The quantity to transfer</param>
        /// <param name="fromSymbol">From symbol when transferring from/to isolated margin</param>
        /// <param name="toSymbol">To symbol when transferring from/to isolated margin</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceTransaction>> TransferAsync(UniversalTransferType type, string asset, decimal quantity, string? fromSymbol = null, string? toSymbol = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get transfer history
        /// <para><a href="https://developers.binance.com/docs/wallet/asset/query-user-universal-transfer" /></para>
        /// </summary>
        /// <param name="type">The type of transfer</param>
        /// <param name="startTime">Filter by startTime</param>
        /// <param name="endTime">Filter by endTime</param>
        /// <param name="page">The page</param>
        /// <param name="pageSize">Results per page</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceTransfer>>> GetTransfersAsync(UniversalTransferType type, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get Fiat payment history
        /// <para><a href="https://developers.binance.com/docs/fiat/rest-api/Get-Fiat-Payments-History" /></para>
        /// </summary>
        /// <param name="side">Filter by side</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Return a specific page</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceFiatPayment[]>> GetFiatPaymentHistoryAsync(OrderSide side, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get Fiat deposit/withdrawal history
        /// <para><a href="https://developers.binance.com/docs/fiat/rest-api" /></para>
        /// </summary>
        /// <param name="side">Filter by side</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Return a specific page</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceFiatWithdrawDeposit[]>> GetFiatDepositWithdrawHistoryAsync(TransactionType side, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Starts a user stream by requesting a listen key. This listen key can be used in subsequent requests to SubscribeToUserDataUpdates. The stream will close after 60 minutes unless a keep alive is send.
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/user-data-stream#create-a-listenkey-user_stream" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Listen key</returns>
        Task<WebCallResult<string>> StartUserStreamAsync(CancellationToken ct = default);

        /// <summary>
        /// Sends a keep alive for the current user stream listen key to keep the stream from closing. Stream auto closes after 60 minutes if no keep alive is send. 30 minute interval for keep alive is recommended.
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/user-data-stream#pingkeep-alive-a-listenkey-user_stream" /></para>
        /// </summary>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> KeepAliveUserStreamAsync(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Stops the current user stream
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/user-data-stream#close-a-listenkey-user_stream" /></para>
        /// </summary>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> StopUserStreamAsync(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Withdraw assets from Binance to an address
        /// <para><a href="https://developers.binance.com/docs/wallet/capital/withdraw" /></para>
        /// </summary>
        /// <param name="asset">The asset to withdraw, for example `ETH`</param>
        /// <param name="address">The address to send the funds to</param>
        /// <param name="addressTag">Secondary address identifier for assets like XRP,XMR etc.</param>
        /// <param name="withdrawOrderId">Custom client order id</param>
        /// <param name="transactionFeeFlag">When making internal transfer, true for returning the fee to the destination account; false for returning the fee back to the departure account. Default false.</param>
        /// <param name="quantity">The quantity to withdraw</param>
        /// <param name="network">The network to use</param>
        /// <param name="walletType">The wallet type for withdraw</param>
        /// <param name="name">Description of the address</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Withdrawal confirmation</returns>
        Task<WebCallResult<BinanceWithdrawalPlaced>> WithdrawAsync(string asset, string address, decimal quantity, string? withdrawOrderId = null, string? network = null, string? addressTag = null, string? name = null, bool? transactionFeeFlag = null, WalletType? walletType = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the withdrawal history
        /// <para><a href="https://developers.binance.com/docs/wallet/capital/withdraw-history" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="withdrawOrderId">Filter by withdraw order id</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter start time from</param>
        /// <param name="endTime">Filter end time till</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <param name="limit">Add limit. Default: 1000, Max: 1000</param>
        /// <param name="offset">Add offset</param>
        /// <param name="ids">Filter by withdrawal ids</param>
        /// <returns>List of withdrawals</returns>
        Task<WebCallResult<BinanceWithdrawal[]>> GetWithdrawalHistoryAsync(string? asset = null, string? withdrawOrderId = null, WithdrawalStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? receiveWindow = null, int? limit = null, int? offset = null, IEnumerable<string>? ids = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of withdrawal addresses
        /// <para><a href="https://developers.binance.com/docs/wallet/capital/fetch-withdraw-address" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceWithdrawalAddress[]>> GetWithdrawalAddressesAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the deposit history
        /// <para><a href="https://developers.binance.com/docs/wallet/capital/deposite-history" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="status">Filter by status</param>
        /// <param name="limit">Amount of results</param>
        /// <param name="offset">Offset the results</param>
        /// <param name="startTime">Filter start time from</param>
        /// <param name="endTime">Filter end time till</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="includeSource">Include source address to response</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of deposits</returns>
        Task<WebCallResult<BinanceDeposit[]>> GetDepositHistoryAsync(string? asset = null, DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? limit = null, int? receiveWindow = null, bool includeSource = false, CancellationToken ct = default);

        /// <summary>
        /// Gets the deposit address for an asset
        /// <para><a href="https://developers.binance.com/docs/wallet/capital/deposite-address" /></para>
        /// </summary>
        /// <param name="asset">Asset to get address for, for example `ETH`</param>
        /// <param name="network">Network</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Deposit address</returns>
        Task<WebCallResult<BinanceDepositAddress>> GetDepositAddressAsync(string asset, string? network = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get all deposit addresses for an asset
        /// <para><a href="https://developers.binance.com/docs/wallet/capital/fetch-deposit-address-list-with-network" /></para>
        /// </summary>
        /// <param name="asset">Asset to get address for, for example `ETH`</param>
        /// <param name="network">Filter by network</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceDepositAddress[]>> GetDepositAddressesAsync(string asset, string? network = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get personal margin level information for your account
        /// <para><a href="https://developers.binance.com/docs/margin_trading/account/Get-Summary-Of-Margin-Account" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin Level Information</returns>
        Task<WebCallResult<BinanceMarginLevel>> GetMarginLevelInformationAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Borrow. Apply for a loan. 
        /// <para><a href="https://developers.binance.com/docs/margin_trading/borrow-and-repay/Margin-Account-Borrow-Repay" /></para>
        /// </summary>
        /// <param name="asset">The asset being borrow, for example `ETH`</param>
        /// <param name="quantity">The quantity to be borrow</param>
        /// <param name="isIsolated">For isolated margin or not</param>
        /// <param name="symbol">The isolated symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transaction Id</returns>
        Task<WebCallResult<BinanceTransaction>> MarginBorrowAsync(string asset, decimal quantity, bool? isIsolated = null, string? symbol = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Repay loan for margin account.
        /// <para><a href="https://developers.binance.com/docs/margin_trading/borrow-and-repay/Margin-Account-Borrow-Repay" /></para>
        /// </summary>
        /// <param name="asset">The asset being repay, for example `ETH`</param>
        /// <param name="quantity">The quantity to be borrow</param>
        /// <param name="isIsolated">For isolated margin or not</param>
        /// <param name="symbol">The isolated symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transaction Id</returns>
        Task<WebCallResult<BinanceTransaction>> MarginRepayAsync(string asset, decimal quantity, bool? isIsolated = null, string? symbol = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get history of transfers
        /// <para><a href="https://developers.binance.com/docs/margin_trading/transfer" /></para>
        /// </summary>
        /// <param name="direction">The direction of the the transfers to retrieve</param>
        /// <param name="page">Results page</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="isolatedSymbol">Filter by isolated symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of transfers</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceTransferHistory>>> GetMarginTransferHistoryAsync(TransferDirection direction, int? page = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, string? isolatedSymbol = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Query loan records
        /// <para><a href="https://developers.binance.com/docs/margin_trading/borrow-and-repay/Query-Borrow-Repay" /></para>
        /// </summary>
        /// <param name="asset">The records asset, for example `ETH`</param>
        /// <param name="transactionId">The id of loan transaction</param>
        /// <param name="startTime">Time to start getting records from</param>
        /// <param name="endTime">Time to stop getting records to</param>
        /// <param name="current">Number of page records</param>
        /// <param name="isolatedSymbol">Filter by isolated symbol</param>
        /// <param name="limit">The records count size need show</param>
        /// <param name="archived">Set to true for archived data from 6 months ago</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Loan records</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceLoan>>> GetMarginLoansAsync(string asset, long? transactionId = null, DateTime? startTime = null, DateTime? endTime = null, int? current = 1, int? limit = 10, string? isolatedSymbol = null, bool? archived = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Query repay records
        /// <para><a href="https://developers.binance.com/docs/margin_trading/borrow-and-repay/Query-Borrow-Repay" /></para>
        /// </summary>
        /// <param name="asset">The records asset, for example `ETH`</param>
        /// <param name="transactionId">The id of repay transaction</param>
        /// <param name="startTime">Time to start getting records from</param>
        /// <param name="endTime">Time to stop getting records to</param>
        /// <param name="current">Filter by number</param>
        /// <param name="isolatedSymbol">Filter by isolated symbol</param>
        /// <param name="size">The records count size need show</param>
        /// <param name="archived">Set to true for archived data from 6 months ago</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Repay records</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceRepay>>> GetMarginRepaysAsync(string asset, long? transactionId = null, DateTime? startTime = null, DateTime? endTime = null, int? current = null, int? size = null, string? isolatedSymbol = null, bool? archived = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get history of interest
        /// <para><a href="https://developers.binance.com/docs/margin_trading/borrow-and-repay/Get-Interest-History" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="page">Results page</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="isolatedSymbol">Filter by isolated symbol</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="archived">Set to true for archived data from 6 months ago</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of interest events</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceInterestHistory>>> GetMarginInterestHistoryAsync(string? asset = null, int? page = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, string? isolatedSymbol = null, bool? archived = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get history of interest rate
        /// <para><a href="https://developers.binance.com/docs/margin_trading/borrow-and-repay/Query-Margin-Interest-Rate-History" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="vipLevel">Vip level</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of interest rate</returns>
        Task<WebCallResult<BinanceInterestRateHistory[]>> GetMarginInterestRateHistoryAsync(string asset, string? vipLevel = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin interest data
        /// <para><a href="https://developers.binance.com/docs/margin_trading/account/Query-Cross-Margin-Fee-Data" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="vipLevel">Vip level</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceInterestMarginData[]>> GetInterestMarginDataAsync(string? asset = null, string? vipLevel = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get history of forced liquidations
        /// <para><a href="https://developers.binance.com/docs/margin_trading/trade" /></para>
        /// </summary>
        /// <param name="page">Results page</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="isolatedSymbol">Filter by isolated symbol</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of forced liquidations</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceForcedLiquidation>>> GetMarginForcedLiquidationHistoryAsync(int? page = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, string? isolatedSymbol = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Query margin account details
        /// <para><a href="https://developers.binance.com/docs/margin_trading/account/Query-Cross-Margin-Account-Details" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The margin account information</returns>
        Task<WebCallResult<BinanceMarginAccount>> GetMarginAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Query max borrow quantity
        /// <para><a href="https://developers.binance.com/docs/margin_trading/borrow-and-repay/Query-Max-Borrow" /></para>
        /// </summary>
        /// <param name="asset">The records asset, for example `ETH`</param>
        /// <param name="isolatedSymbol">The isolated symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Max quantity</returns>
        Task<WebCallResult<BinanceMarginAmount>> GetMarginMaxBorrowAmountAsync(string asset, string? isolatedSymbol = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Query max transfer-out quantity 
        /// <para><a href="https://developers.binance.com/docs/margin_trading/transfer/Query-Max-Transfer-Out-Amount" /></para>
        /// </summary>
        /// <param name="asset">The records asset, for example `ETH`</param>
        /// <param name="isolatedSymbol">The isolated symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Max quantity</returns>
        Task<WebCallResult<decimal>> GetMarginMaxTransferAmountAsync(string asset, string? isolatedSymbol = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin tier data
        /// <para><a href="https://developers.binance.com/docs/margin_trading/market-data/Query-Isolated-Margin-Tier-Data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH`</param>
        /// <param name="tier">Tier</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceIsolatedMarginTierData[]>> GetIsolatedMarginTierDataAsync(string symbol, int? tier = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Isolated margin account info
        /// <para><a href="https://developers.binance.com/docs/margin_trading/account/Query-Isolated-Margin-Account-Info" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceIsolatedMarginAccount>> GetIsolatedMarginAccountAsync(
            int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get max number of enabled isolated margin accounts
        /// <para><a href="https://developers.binance.com/docs/margin_trading/account/Query-Enabled-Isolated-Margin-Account-Limit" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IsolatedMarginAccountLimit>> GetEnabledIsolatedMarginAccountLimitAsync(
            int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Enable an isolated margin account
        /// <para><a href="https://developers.binance.com/docs/margin_trading/account/Enable-Isolated-Margin-Account" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to enable isolated margin account for, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CreateIsolatedMarginAccountResult>> EnableIsolatedMarginAccountAsync(string symbol, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Disabled an isolated margin account info
        /// <para><a href="https://developers.binance.com/docs/margin_trading/account/Disable-Isolated-Margin-Account" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to enable isolated margin account for, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CreateIsolatedMarginAccountResult>> DisableIsolatedMarginAccountAsync(string symbol,
            int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin order rate limits
        /// <para><a href="https://developers.binance.com/docs/margin_trading/trade/Query-Current-Margin-Order-Count-Usage" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceCurrentRateLimit[]>> GetMarginOrderRateLimitStatusAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Starts a user stream by requesting a listen key. This listen key can be used in subsequent requests to BinanceSocketClient.SpotApi.Account..SubscribeToUserDataUpdates. The stream will close after 60 minutes unless a keep alive is send.
        /// <para><a href="https://developers.binance.com/docs/margin_trading/trade-data-stream/Start-Margin-User-Data-Stream" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Listen key</returns>
        Task<WebCallResult<string>> StartMarginUserStreamAsync(CancellationToken ct = default);

        /// <summary>
        /// Sends a keep alive for the current user stream listen key to keep the stream from closing. Stream auto closes after 60 minutes if no keep alive is send. 30 minute interval for keep alive is recommended.
        /// <para><a href="https://developers.binance.com/docs/margin_trading/trade-data-stream/Keepalive-Margin-User-Data-Stream" /></para>
        /// </summary>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> KeepAliveMarginUserStreamAsync(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Stops the current user stream
        /// <para><a href="https://developers.binance.com/docs/margin_trading/trade-data-stream/Close-Margin-User-Data-Stream" /></para>
        /// </summary>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> StopMarginUserStreamAsync(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Starts a user stream  for margin account by requesting a listen key. 
        /// This listen key can be used in subsequent requests to  BinanceSocketClient.SpotApi.Account.SubscribeToUserDataUpdates  
        /// The stream will close after 60 minutes unless a keep alive is send.
        /// <para><a href="https://developers.binance.com/docs/margin_trading/trade-data-stream/Start-Isolated-Margin-User-Data-Stream" /></para>
        /// </summary>
        /// <param name="symbol">The isolated symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Listen key</returns>
        Task<WebCallResult<string>> StartIsolatedMarginUserStreamAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Sends a keep alive for the current user stream for margin account listen key to keep the stream from closing. 
        /// Stream auto closes after 60 minutes if no keep alive is send. 30 minute interval for keep alive is recommended.
        /// <para><a href="https://developers.binance.com/docs/margin_trading/trade-data-stream/Keepalive-Isolated-Margin-User-Data-Stream" /></para>
        /// </summary>
        /// <param name="symbol">The isolated symbol, for example `ETHUSDT`</param>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> KeepAliveIsolatedMarginUserStreamAsync(string symbol, string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Close the user stream for margin account
        /// <para><a href="https://developers.binance.com/docs/margin_trading/trade-data-stream/Close-Isolated-Margin-User-Data-Stream" /></para>
        /// </summary>
        /// <param name="symbol">The isolated symbol, for example `ETHUSDT`</param>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> CloseIsolatedMarginUserStreamAsync(string symbol, string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Gets the trading status for the current account
        /// <para><a href="https://developers.binance.com/docs/wallet/account/account-api-trading-status" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The trading status of the account</returns>
        Task<WebCallResult<BinanceTradingStatus>> GetTradingStatusAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get the current used order rate limits
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/account-endpoints#query-unfilled-order-count-user_data" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceCurrentRateLimit[]>> GetOrderRateLimitStatusAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get rebate history
        /// <para><a href="https://developers.binance.com/docs/rebate/rest-api" /></para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Results per page</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceRebateWrapper>> GetRebateHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Portfolio margin account info
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-classic-portfolio-margin-account-info-user_data" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinancePortfolioMarginInfo>> GetPortfolioMarginAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get portfolio margin account collateral rates
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#classic-portfolio-margin-collateral-rate-market_data" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinancePortfolioMarginCollateralRate[]>> GetPortfolioMarginCollateralRateAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get portfolio margin bankrupty loan amount
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#query-classic-portfolio-margin-bankruptcy-loan-amount-user_data" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinancePortfolioMarginLoan>> GetPortfolioMarginBankruptcyLoanAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Repay portfolio margin bankruptcy loan
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#classic-portfolio-margin-bankruptcy-loan-repay" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceTransaction>> PortfolioMarginBankruptcyLoanRepayAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get Busd convert history
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#busd-convert-history-user_data" /></para>
        /// </summary>
        /// <param name="transferId">Filter by transferId</param>
        /// <param name="clientTransferId">Filter by clientTransferId</param>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceBusdHistory>>> GetBusdConvertHistoryAsync(DateTime startTime, DateTime endTime, long? transferId = null, string? clientTransferId = null, string? asset = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get the query of Cloud-Mining payment and refund history
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-cloud-mining-payment-and-refund-history-user_data" /></para>
        /// </summary>
        /// <param name="transferId">Filter by transferId</param>
        /// <param name="clientTransferId">Filter by clientTransferId</param>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceCloudMiningHistory>>> GetCloudMiningHistoryAsync(DateTime startTime, DateTime endTime, long? transferId = null, string? clientTransferId = null, string? asset = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Adjust cross margin max leverage
        /// <para><a href="https://developers.binance.com/docs/margin_trading/account" /></para>
        /// </summary>
        /// <param name="maxLeverage">Max leverage, can only adjust 3 or 5</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceCrossMarginLeverageResult>> CrossMarginAdjustMaxLeverageAsync(int maxLeverage, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin fee data collection with any vip level or user's current specific data as https://www.binance.com/en/margin-fee
        /// <para><a href="https://developers.binance.com/docs/margin_trading/account/Query-Isolated-Margin-Fee-Data" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="vipLevel">User's current specific margin data will be returned if vipLevel is omitted</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceIsolatedMarginFeeData[]>> GetIsolatedMarginFeeDataAsync(string? symbol = null, int? vipLevel = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Query the coins which can be small liability exchange
        /// <para><a href="https://developers.binance.com/docs/margin_trading/trade/Get-Small-Liability-Exchange-Coin-List" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceSmallLiabilityAsset[]>> GetCrossMarginSmallLiabilityExchangeAssetsAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Cross Margin Small Liability Exchange
        /// <para><a href="https://developers.binance.com/docs/margin_trading/trade/Small-Liability-Exchange" /></para>
        /// </summary>
        /// <param name="assets">Assets, for example `ETH`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> CrossMarginSmallLiabilityExchangeAsync(IEnumerable<string> assets, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get Small liability Exchange History
        /// <para><a href="https://developers.binance.com/docs/margin_trading/trade/Get-Small-Liability-Exchange-History" /></para>
        /// </summary>
        /// <param name="startTime">Filter by startTime</param>
        /// <param name="endTime">Filter by endTime</param>
        /// <param name="page">The page</param>
        /// <param name="limit">Results per page</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceSmallLiabilityHistory>>> GetCrossMarginSmallLiabilityExchangeHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the trade fee for a symbol
        /// <para><a href="https://developers.binance.com/docs/wallet/asset/trade-fee" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to get withdrawal fee for, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Trade fees</returns>
        Task<WebCallResult<BinanceTradeFee[]>> GetTradeFeeAsync(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get account VIP level and margin/futures enabled status
        /// <para><a href="https://developers.binance.com/docs/wallet/account" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceVipLevelAndStatus>> GetAccountVipLevelAndStatusAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get current account commission rates.
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/account-endpoints#query-commission-rates-user_data" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceCommissions>> GetCommissionRatesAsync(string symbol, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Starts a user stream by requesting a listen key. This listen key can be used in subsequent requests to SubscribeToUserDataUpdates. The stream will close after 60 minutes unless a keep alive is send.
        /// <para><a href="https://developers.binance.com/docs/margin_trading/risk-data-stream/Start-User-Data-Stream" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Listen key</returns>
        Task<WebCallResult<string>> StartRiskDataUserStreamAsync(CancellationToken ct = default);

        /// <summary>
        /// Sends a keep alive for the current user stream listen key to keep the stream from closing. Stream auto closes after 60 minutes if no keep alive is send. 30 minute interval for keep alive is recommended.
        /// <para><a href="https://developers.binance.com/docs/margin_trading/risk-data-stream/Keepalive-User-Data-Stream" /></para>
        /// </summary>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> KeepAliveRiskDataUserStreamAsync(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Stops the current user stream
        /// <para><a href="https://developers.binance.com/docs/margin_trading/risk-data-stream/Close-User-Data-Stream" /></para>
        /// </summary>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> StopRiskDataUserStreamAsync(string listenKey, CancellationToken ct = default);

    }
}
