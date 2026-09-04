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
        #region Get All Assets

        Task<HttpResult<SharedAsset[]>> IAssetsRestClient.GetAssetsAsync(GetAssetsRequest request, CancellationToken ct)
            => GetAllAssetsAsync(request, ct);
        GetAllAssetsOptions IAssetsRestClient.GetAssetsOptions => GetAllAssetsOptions;

        public GetAllAssetsOptions GetAllAssetsOptions { get; }
            = new GetAllAssetsOptions(_exchangeName, true);

        async Task<ICallResult<SharedAsset[]>> IGetAllAssets.GetAllAssetsAsync(GetAssetsRequest request, CancellationToken ct)
            => await GetAllAssetsAsync(request, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedAsset[]>> GetAllAssetsAsync(GetAssetsRequest request, CancellationToken ct)
        {
            var validationError = GetAllAssetsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedAsset[]>(Exchange, validationError);

            var assets = await _api.Account.GetUserAssetsAsync(ct: ct).ConfigureAwait(false);
            if (!assets.Success)
                return HttpResult.Fail<SharedAsset[]>(assets);

            return HttpResult.Ok(assets, assets.Data!.Select(x => new SharedAsset(x.Asset)
            {
                FullName = x.Name,
                Networks = x.NetworkList.Select(x => new SharedAssetNetwork(x.Network)
                {
                    FullName = x.Name,
                    MinConfirmations = x.MinConfirmations,
                    DepositEnabled = x.DepositEnabled,
                    MinWithdrawQuantity = x.WithdrawMin,
                    MaxWithdrawQuantity = x.WithdrawMax,
                    WithdrawEnabled = x.WithdrawEnabled,
                    WithdrawFee = x.WithdrawFee,
                    ContractAddress = x.ContractAddress
                }).ToArray()
            }).ToArray());

        }

        #endregion

        #region Get Asset

        public GetAssetOptions GetAssetOptions { get; } = new GetAssetOptions(_exchangeName, true);
        async Task<ICallResult<SharedAsset>> IGetAsset.GetAssetAsync(GetAssetRequest request, CancellationToken ct)
            => await GetAssetAsync(request, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedAsset>> GetAssetAsync(GetAssetRequest request, CancellationToken ct)
        {
            var validationError = GetAssetOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedAsset>(Exchange, validationError);

            var assets = await _api.Account.GetUserAssetsAsync(ct: ct).ConfigureAwait(false);
            if (!assets.Success)
                return HttpResult.Fail<SharedAsset>(assets);

            var asset = assets.Data!.SingleOrDefault(x => x.Asset.Equals(request.Asset, StringComparison.InvariantCultureIgnoreCase));
            if (asset == null)
                return HttpResult.Fail<SharedAsset>(Exchange, new ServerError(new ErrorInfo(ErrorType.UnknownAsset, false, "Asset not found")));

            return HttpResult.Ok(assets, new SharedAsset(asset.Asset)
            {
                FullName = asset.Name,
                Networks = asset.NetworkList.Select(x => new SharedAssetNetwork(x.Network)
                {
                    FullName = x.Name,
                    MinConfirmations = x.MinConfirmations,
                    DepositEnabled = x.DepositEnabled,
                    MinWithdrawQuantity = x.WithdrawMin,
                    MaxWithdrawQuantity = x.WithdrawMax,
                    WithdrawEnabled = x.WithdrawEnabled,
                    WithdrawFee = x.WithdrawFee,
                    ContractAddress = x.ContractAddress
                }).ToArray()
            });

        }

        #endregion

    }
}
