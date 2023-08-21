using Binance.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Binance.Net.Enums;
using CryptoExchange.Net.Objects;
using Binance.Net.Objects.Models.Spot.Staking;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net;
using Binance.Net.Interfaces.Clients.GeneralApi;
using Binance.Net.Objects.Models;

namespace Binance.Net.Clients.GeneralApi
{
    /// <inheritdoc />
    public class BinanceRestClientGeneralApiStaking : IBinanceRestClientGeneralApiStaking
    {
        private readonly BinanceRestClientGeneralApi _baseClient;

        internal BinanceRestClientGeneralApiStaking(BinanceRestClientGeneralApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceStakingResult>> SetAutoStakingAsync(StakingProductType product, string positionId, bool renewable, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "product", EnumConverter.GetString(product) },
                { "positionId", positionId },
                { "renewable", renewable },
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceStakingResult>(_baseClient.GetUrl("staking/setAutoStaking", "sapi", "1"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceStakingPersonalQuota>> GetStakingPersonalQuotaAsync(StakingProductType product, string productId, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "product", EnumConverter.GetString(product) },
                { "productId", productId }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceStakingPersonalQuota>(_baseClient.GetUrl("staking/personalLeftQuota", "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceStakingProduct>>> GetStakingProductsAsync(StakingProductType product, string? asset = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "product", EnumConverter.GetString(product) }
            };
            parameters.AddOptionalParameter("asset", asset);
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceStakingProduct>>(_baseClient.GetUrl("staking/productList", "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceStakingPositionResult>> PurchaseStakingProductAsync(StakingProductType product, string productId, decimal quantity, bool? renewable = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "product", EnumConverter.GetString(product) },
                { "productId", productId },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
            };
            parameters.AddOptionalParameter("renewable", renewable);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceStakingPositionResult>(_baseClient.GetUrl("staking/purchase", "sapi", "1"), HttpMethod.Post, ct, parameters, true, weight: 1).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceStakingResult>> RedeemStakingProductAsync(StakingProductType product, string productId, string? positionId = null, decimal? quantity = null, bool? renewable = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "product", EnumConverter.GetString(product) },
                { "productId", productId },
            };
            parameters.AddOptionalParameter("positionId", positionId);
            parameters.AddOptionalParameter("amount", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("renewable", renewable);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceStakingResult>(_baseClient.GetUrl("staking/redeem", "sapi", "1"), HttpMethod.Post, ct, parameters, true, weight: 1).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceStakingPosition>>> GetStakingPositionsAsync(StakingProductType product, string? productId = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "product", EnumConverter.GetString(product) }
            };
            parameters.AddOptionalParameter("productId", productId);
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceStakingPosition>>(_baseClient.GetUrl("staking/position", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 1).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceStakingHistory>>> GetStakingHistoryAsync(StakingProductType product, StakingTransactionType transactionType, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "product", EnumConverter.GetString(product) },
                { "txnType", EnumConverter.GetString(transactionType) }
            };
            parameters.AddOptionalParameter("asset", asset);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceStakingHistory>>(_baseClient.GetUrl("staking/stakingRecord", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 1).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceStakingResult>> SubscribeEthStakingAsync(decimal quantity, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceStakingResult>(_baseClient.GetUrl("eth-staking/eth/stake", "sapi", "1"), HttpMethod.Post, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceStakingResult>> RedeemEthStakingAsync(decimal quantity, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceStakingResult>(_baseClient.GetUrl("eth-staking/eth/redeem", "sapi", "1"), HttpMethod.Post, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceEthStakingHistory>>> GetEthStakingHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", pageSize);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceQueryRecords<BinanceEthStakingHistory>>(_baseClient.GetUrl("eth-staking/eth/history/stakingHistory", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceEthRedemptionHistory>>> GetEthRedemptionHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", pageSize);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceQueryRecords<BinanceEthRedemptionHistory>>(_baseClient.GetUrl("eth-staking/eth/history/redemptionHistory", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceEthRewardsHistory>>> GetEthRewardsHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", pageSize);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceQueryRecords<BinanceEthRewardsHistory>>(_baseClient.GetUrl("eth-staking/eth/history/rewardsHistory", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceEthStakingQuota>> GetEthStakingQuotaAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceEthStakingQuota>(_baseClient.GetUrl("eth-staking/eth/quota", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceBethRateHistory>>> GetBethRateHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", pageSize);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceQueryRecords<BinanceBethRateHistory>>(_baseClient.GetUrl("eth-staking/eth/history/rateHistory", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceEthStakingAccount>> GetEthStakingAccountAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceEthStakingAccount>(_baseClient.GetUrl("eth-staking/account", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceStakingResult>> WrapBethAsync(decimal quantity, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceStakingResult>(_baseClient.GetUrl("eth-staking/wbeth/wrap", "sapi", "1"), HttpMethod.Post, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceStakingResult>> UnwrapBethAsync(decimal quantity, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceStakingResult>(_baseClient.GetUrl("eth-staking/wbeth/unwrap", "sapi", "1"), HttpMethod.Post, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceBethWrapHistory>>> GetBethWrapHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", pageSize);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceQueryRecords<BinanceBethWrapHistory>>(_baseClient.GetUrl("eth-staking/wbeth/history/wrapHistory", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceBethWrapHistory>>> GetBethUnwrapHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", pageSize);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceQueryRecords<BinanceBethWrapHistory>>(_baseClient.GetUrl("eth-staking/wbeth/history/unwrapHistory ", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }
    }
}
