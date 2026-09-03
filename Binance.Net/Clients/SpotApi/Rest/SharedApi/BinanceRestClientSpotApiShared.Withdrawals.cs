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
        #region Withdrawal client

        Task<HttpResult<SharedWithdrawal[]>> IWithdrawalRestClient.GetWithdrawalsAsync(GetWithdrawalsRequest request, PageRequest? nextPageToken, CancellationToken ct)
            => GetWithdrawalHistoryAsync(request, nextPageToken, ct);
        GetWithdrawalHistoryOptions IWithdrawalRestClient.GetWithdrawalsOptions => GetWithdrawalHistoryOptions;

        public GetWithdrawalHistoryOptions GetWithdrawalHistoryOptions { get; } = new GetWithdrawalHistoryOptions(_exchangeName, false, true, true, 1000)
        {
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("TravelRuleEndpoint", typeof(bool), "Whether to use the TravelRule endpoint (true) or not (false, default)", true)
            }
        };
        public async Task<HttpResult<SharedWithdrawal[]>> GetWithdrawalHistoryAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetWithdrawalHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedWithdrawal[]>(Exchange, validationError);

            var limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, true, TimeSpan.FromDays(90));

            var traveRule = ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "TravelRuleEndpoint");
            if (traveRule == true)
            {
                var result = await _api.Account.GetTravelRuleWithdrawalHistoryAsync(
                    request.Asset,
                    startTime: pageParams.StartTime,
                    endTime: pageParams.EndTime,
                    limit: limit,
                    offset: pageParams.Offset,
                    ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedWithdrawal[]>(result);

                var nextPageRequest = Pagination.GetNextPageRequest(
                    () => direction == DataDirection.Ascending
                        ? Pagination.NextPageFromId(result.Data!.Max(x => x.Id) + 1)
                        : Pagination.NextPageFromTime(pageParams, result.Data!.Min(x => x.ApplyTime), false),
                    result.Data!.Length,
                    result.Data.Select(x => x.ApplyTime),
                    request.StartTime,
                    request.EndTime ?? DateTime.UtcNow,
                    pageParams,
                    TimeSpan.FromDays(90));

                return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.ApplyTime, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedWithdrawal(
                            x.Asset, 
                            x.Address, 
                            x.Quantity, 
                            x.Status == WithdrawalStatus.Completed,
                            x.ApplyTime,
                            GetWithdrawalStatus(x.Status))
                        {
                            Confirmations = x.ConfirmTimes,
                            Network = x.Network,
                            Tag = x.AddressTag,
                            TransactionId = x.TransactionId,
                            Fee = x.TransactionFee,
                            Id = x.Id
                        })
                    .ToArray(), nextPageRequest);
            }
            else
            {
                var result = await _api.Account.GetWithdrawalHistoryAsync(
                   request.Asset,
                   startTime: pageParams.StartTime,
                   endTime: pageParams.EndTime,
                   limit: limit,
                   offset: pageParams.Offset,
                   ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedWithdrawal[]>(result);

                var nextPageRequest = Pagination.GetNextPageRequest(
                    () => direction == DataDirection.Ascending
                        ? Pagination.NextPageFromId(result.Data!.Max(x => x.Id) + 1)
                        : Pagination.NextPageFromTime(pageParams, result.Data!.Min(x => x.ApplyTime), false),
                    result.Data!.Length,
                    result.Data.Select(x => x.ApplyTime),
                    request.StartTime,
                    request.EndTime ?? DateTime.UtcNow,
                    pageParams,
                    TimeSpan.FromDays(90));

                return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.ApplyTime, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedWithdrawal(
                            x.Asset, 
                            x.Address,
                            x.Quantity,
                            x.Status == WithdrawalStatus.Completed,
                            x.ApplyTime,
                            GetWithdrawalStatus(x.Status))
                        {
                            Confirmations = x.ConfirmTimes,
                            Network = x.Network,
                            Tag = x.AddressTag,
                            TransactionId = x.TransactionId,
                            Fee = x.TransactionFee,
                            Id = x.Id
                        })
                    .ToArray(), nextPageRequest);
            }

        }

        private SharedTransferStatus GetWithdrawalStatus(WithdrawalStatus x)
        {
            if (x == WithdrawalStatus.Canceled || x == WithdrawalStatus.Rejected || x == WithdrawalStatus.Failure)
                return SharedTransferStatus.Failed;

            if (x == WithdrawalStatus.Completed)
                return SharedTransferStatus.Completed;

            if (x == WithdrawalStatus.AwaitingApproval || x == WithdrawalStatus.EmailSend || x == WithdrawalStatus.Processing)
                return SharedTransferStatus.InProgress;

            return SharedTransferStatus.Unknown;
        }

        #endregion

        #region Withdraw client

        public WithdrawOptions WithdrawOptions { get; } = new WithdrawOptions(_exchangeName)
        {
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("TravelRuleQuestionnaire", typeof(BinanceWithdrawQuestionnaire), "Travel rule questionnaire", new BinanceWithdrawQuestionnaireEu())
            }
        };
        public async Task<HttpResult<SharedId>> WithdrawAsync(WithdrawRequest request, CancellationToken ct)
        {
            var validationError = WithdrawOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var questionnaire = ExchangeParameters.GetValue<BinanceWithdrawQuestionnaire?>(request.ExchangeParameters, Exchange, "TravelRuleQuestionnaire");
            if (questionnaire == null)
            {
                var withdrawal = await _api.Account.WithdrawAsync(
                    request.Asset,
                    request.Address,
                    request.Quantity,
                    network: request.Network,
                    addressTag: request.AddressTag,
                    ct: ct).ConfigureAwait(false);
                if (!withdrawal.Success)
                    return HttpResult.Fail<SharedId>(withdrawal);

                return HttpResult.Ok(withdrawal, new SharedId(withdrawal.Data!.Id));
            }
            else
            {
                var withdrawal = await _api.Account.TravelRuleWithdrawAsync(
                    request.Asset,
                    request.Address,
                    request.Quantity,
                    questionnaire,
                    network: request.Network,
                    addressTag: request.AddressTag,
                    ct: ct).ConfigureAwait(false);
                if (!withdrawal.Success)
                    return HttpResult.Fail<SharedId>(withdrawal);

                return HttpResult.Ok(withdrawal, new SharedId(withdrawal.Data!.TravelRuleId.ToString()));
            }

        }

        #endregion
    }
}
