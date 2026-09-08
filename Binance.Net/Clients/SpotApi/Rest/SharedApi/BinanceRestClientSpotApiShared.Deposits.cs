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
        #region Get Deposit Addresses

        public GetDepositAddressesOptions GetDepositAddressesOptions { get; }
            = new GetDepositAddressesOptions(_exchangeName, true);
        async Task<ICallResult<SharedDepositAddress[]>> IGetDepositAddresses.GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
            => await GetDepositAddressesAsync(request, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedDepositAddress[]>> GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
        {
            var validationError = GetDepositAddressesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedDepositAddress[]>(Exchange, validationError);

            var depositAddresses = await _api.Account.GetDepositAddressAsync(request.Asset, request.Network, ct: ct).ConfigureAwait(false);
            if (!depositAddresses.Success)
                return HttpResult.Fail<SharedDepositAddress[]>(depositAddresses);

            return HttpResult.Ok(depositAddresses, new[] {
                    new SharedDepositAddress(depositAddresses.Data!.Asset, depositAddresses.Data.Address)
                    {
                        TagOrMemo = depositAddresses.Data.Tag
                    }
                });

        }

        #endregion

        #region Get Deposit History

        Task<HttpResult<SharedDeposit[]>> IDepositRestClient.GetDepositsAsync(GetDepositsRequest request, PageRequest? nextPageToken, CancellationToken ct)
            => GetDepositHistoryAsync(request, nextPageToken, ct);
        GetDepositHistoryOptions IDepositRestClient.GetDepositsOptions => GetDepositHistoryOptions;

        public GetDepositHistoryOptions GetDepositHistoryOptions { get; } = new GetDepositHistoryOptions(_exchangeName, false, true, true, 1000)
        {
            ExchangeParameterRules = [
                ExchangeParameterRule.Optional("TravelRuleEndpoint", "Whether to use the TravelRule endpoint (true) or not (false, default)", true)
            ]
        };
        async Task<ICallResult<SharedDeposit[]>> IGetDepositHistory.GetDepositHistoryAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetDepositHistoryAsync(request, pageRequest, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedDeposit[]>> GetDepositHistoryAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetDepositHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedDeposit[]>(Exchange, validationError);

            var limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, true, TimeSpan.FromDays(90));

            var traveRule = ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "TravelRuleEndpoint");
            if (traveRule == true)
            {
                // Get data
                var result = await _api.Account.GetTravelRuleDepositHistoryAsync(
                        request.Asset,
                        startTime: pageParams.StartTime,
                        endTime: pageParams.EndTime,
                        limit: limit,
                        offset: pageParams.Offset,
                        ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedDeposit[]>(result);

                var nextPageRequest = Pagination.GetNextPageRequest(
                    () => Pagination.NextPageFromTime(pageParams, result.Data!.Min(x => x.InsertTime), false),
                    result.Data!.Length,
                    result.Data.Select(x => x.InsertTime),
                    request.StartTime,
                    request.EndTime ?? DateTime.UtcNow,
                    pageParams,
                    TimeSpan.FromDays(90));

                return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.InsertTime, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedDeposit(
                            x.Asset,
                            x.Quantity,
                            x.Status == DepositStatus.Success,
                            x.InsertTime,
                            ParseTransferStatus(x.Status))
                        {
                            Confirmations = x.Confirmations.Contains("/") ? int.Parse(x.Confirmations.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries)[0]) : null,
                            Network = x.Network,
                            TransactionId = x.TransactionId,
                            Tag = x.AddressTag,
                            Id = x.Id
                        }).ToArray(), nextPageRequest);
            }
            else
            {
                var result = await _api.Account.GetDepositHistoryAsync(
                    request.Asset,
                    startTime: pageParams.StartTime,
                    endTime: pageParams.EndTime,
                    limit: limit,
                    offset: pageParams.Offset,
                    ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedDeposit[]>(result);

                var nextPageRequest = Pagination.GetNextPageRequest(
                    () => direction == DataDirection.Ascending
                        ? Pagination.NextPageFromId(result.Data!.Max(x => x.Id) + 1)
                        : Pagination.NextPageFromTime(pageParams, result.Data!.Min(x => x.InsertTime), false),
                    result.Data!.Length,
                    result.Data.Select(x => x.InsertTime),
                    request.StartTime,
                    request.EndTime ?? DateTime.UtcNow,
                    pageParams,
                    TimeSpan.FromDays(90));

                return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.InsertTime, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedDeposit(
                            x.Asset,
                            x.Quantity,
                            x.Status == DepositStatus.Success,
                            x.InsertTime,
                            ParseTransferStatus(x.Status))
                        {
                            Confirmations = x.Confirmations.Contains("/") ? int.Parse(x.Confirmations.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries)[0]) : null,
                            Network = x.Network,
                            TransactionId = x.TransactionId,
                            Tag = x.AddressTag,
                            Id = x.Id
                        }).ToArray(), nextPageRequest);
            }

        }

        #endregion

        private SharedTransferStatus ParseTransferStatus(DepositStatus status)
        {
            if (status == DepositStatus.Success)
                return SharedTransferStatus.Completed;
            if (status == DepositStatus.Pending || status == DepositStatus.Credited || status == DepositStatus.WaitingUserConfirm)
                return SharedTransferStatus.InProgress;
            if (status == DepositStatus.Rejected || status == DepositStatus.WrongDeposit)
                return SharedTransferStatus.Failed;

            return SharedTransferStatus.Unknown;
        }

    }
}
