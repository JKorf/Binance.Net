using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.Caching;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Binance.Net.Clients.SpotApi
{
    internal partial class BinanceRestClientSpotSharedApi
    {
        #region Transfer

        public TransferOptions TransferOptions { get; } = new TransferOptions(_exchangeName, [
            SharedAccountType.Funding,
            SharedAccountType.Spot,
            SharedAccountType.PerpetualLinearFutures,
            SharedAccountType.PerpetualInverseFutures,
            SharedAccountType.DeliveryLinearFutures,
            SharedAccountType.DeliveryInverseFutures,
            SharedAccountType.CrossMargin,
            SharedAccountType.IsolatedMargin,
            SharedAccountType.Option
            ]);
        async Task<ICallResult<SharedId>> ITransfer.TransferAsync(TransferRequest request, CancellationToken ct)
            => await TransferAsync(request, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedId>> TransferAsync(TransferRequest request, CancellationToken ct)
        {
            var validationError = TransferOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var transferType = GetTransferType(request);
            if (transferType == null)
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid("To/From AccountType", "invalid to/from account combination"));

            // Get data
            var transfer = await _api.Account.TransferAsync(
                transferType.Value,
                request.Asset,
                request.Quantity,
                request.FromSymbol,
                request.ToSymbol,
                ct: ct).ConfigureAwait(false);
            if (!transfer.Success)
                return HttpResult.Fail<SharedId>(transfer);

            return HttpResult.Ok(transfer, new SharedId(transfer.Data!.TransactionId.ToString()));

        }

        #endregion

        private UniversalTransferType? GetTransferType(TransferRequest request)
        {
            if (request.FromAccountType == SharedAccountType.Funding)
            {
                if (request.ToAccountType == SharedAccountType.CrossMargin) return UniversalTransferType.FundingToMargin;
                if (request.ToAccountType == SharedAccountType.PerpetualInverseFutures || request.ToAccountType == SharedAccountType.DeliveryInverseFutures) return UniversalTransferType.FundingToCoinFutures;
                if (request.ToAccountType == SharedAccountType.PerpetualLinearFutures || request.ToAccountType == SharedAccountType.DeliveryLinearFutures) return UniversalTransferType.FundingToUsdFutures;
                if (request.ToAccountType == SharedAccountType.Option) return UniversalTransferType.FundingToOption;
                if (request.ToAccountType == SharedAccountType.Spot) return UniversalTransferType.FundingToMain;
            }
            else if (request.FromAccountType == SharedAccountType.Spot)
            {
                if (request.ToAccountType == SharedAccountType.CrossMargin) return UniversalTransferType.MainToMargin;
                if (request.ToAccountType == SharedAccountType.IsolatedMargin) return UniversalTransferType.MainToIsolatedMargin;
                if (request.ToAccountType == SharedAccountType.PerpetualInverseFutures || request.ToAccountType == SharedAccountType.DeliveryInverseFutures) return UniversalTransferType.MainToCoinFutures;
                if (request.ToAccountType == SharedAccountType.PerpetualLinearFutures || request.ToAccountType == SharedAccountType.DeliveryLinearFutures) return UniversalTransferType.MainToUsdFutures;
                if (request.ToAccountType == SharedAccountType.Option) return UniversalTransferType.MainToOption;
                if (request.ToAccountType == SharedAccountType.Funding) return UniversalTransferType.MainToFunding;
            }
            else if (request.FromAccountType == SharedAccountType.CrossMargin)
            {
                if (request.ToAccountType == SharedAccountType.Option) return UniversalTransferType.MarginToOption;
                if (request.ToAccountType == SharedAccountType.Funding) return UniversalTransferType.MarginToFunding;
                if (request.ToAccountType == SharedAccountType.Spot) return UniversalTransferType.MarginToMain;
                if (request.ToAccountType == SharedAccountType.PerpetualInverseFutures || request.ToAccountType == SharedAccountType.DeliveryInverseFutures) return UniversalTransferType.MarginToCoinFutures;
                if (request.ToAccountType == SharedAccountType.PerpetualLinearFutures || request.ToAccountType == SharedAccountType.DeliveryLinearFutures) return UniversalTransferType.MarginToUsdFutures;
                if (request.ToAccountType == SharedAccountType.IsolatedMargin) return UniversalTransferType.MarginToIsolatedMargin;
            }
            else if (request.FromAccountType == SharedAccountType.IsolatedMargin)
            {
                if (request.ToAccountType == SharedAccountType.Spot) return UniversalTransferType.IsolatedMarginToMain;
                if (request.ToAccountType == SharedAccountType.CrossMargin) return UniversalTransferType.IsolatedMarginToMargin;
                if (request.ToAccountType == SharedAccountType.IsolatedMargin) return UniversalTransferType.IsolatedMarginToIsolatedMargin;
            }
            else if (request.FromAccountType == SharedAccountType.PerpetualLinearFutures || request.FromAccountType == SharedAccountType.DeliveryLinearFutures)
            {
                if (request.ToAccountType == SharedAccountType.CrossMargin) return UniversalTransferType.UsdFuturesToMargin;
                if (request.ToAccountType == SharedAccountType.Option) return UniversalTransferType.UsdFuturesToOption;
                if (request.ToAccountType == SharedAccountType.Funding) return UniversalTransferType.UsdFuturesToFunding;
                if (request.ToAccountType == SharedAccountType.Spot) return UniversalTransferType.UsdFuturesToMain;
            }
            else if (request.FromAccountType == SharedAccountType.PerpetualInverseFutures || request.FromAccountType == SharedAccountType.DeliveryInverseFutures)
            {
                if (request.ToAccountType == SharedAccountType.CrossMargin) return UniversalTransferType.CoinFuturesToMargin;
                if (request.ToAccountType == SharedAccountType.Funding) return UniversalTransferType.CoinFuturesToFunding;
                if (request.ToAccountType == SharedAccountType.Spot) return UniversalTransferType.CoinFuturesToMain;
            }
            else if (request.FromAccountType == SharedAccountType.Option)
            {
                if (request.ToAccountType == SharedAccountType.Funding) return UniversalTransferType.OptionToFunding;
                if (request.ToAccountType == SharedAccountType.Spot) return UniversalTransferType.OptionToMain;
                if (request.ToAccountType == SharedAccountType.CrossMargin) return UniversalTransferType.OptionToMargin;
                if (request.ToAccountType == SharedAccountType.PerpetualLinearFutures || request.ToAccountType == SharedAccountType.DeliveryLinearFutures) return UniversalTransferType.OptionToUsdFutures;
            }

            return null;
        }

    }
}
