using DataAccess;
using Domain.Entities;
using System;
using System.Threading.Tasks;
using Domain.Entities.Enum;
using AircashSignature;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Services.HttpRequest;
using AircashSimulator.Configuration;
using Microsoft.Extensions.Options;

namespace Services.AbonOnlinePartner
{
    public class AbonOnlinePartnerService : IAbonOnlinePartnerService
    {
        private AircashSimulatorContext AircashSimulatorContext;
        private IHttpRequestService HttpRequestService;
        private AbonConfiguration AbonConfiguration;

        public AbonOnlinePartnerService(AircashSimulatorContext aircashSimulatorContext, IHttpRequestService httpRequestService, IOptionsMonitor<AbonConfiguration> abonConfiguration)
        {
            AircashSimulatorContext = aircashSimulatorContext;
            HttpRequestService = httpRequestService;
            AbonConfiguration = abonConfiguration.CurrentValue;
        }
        public async Task<object> ValidateCoupon(string couponCode)
        {
            var validateCouponResponse = new object();
            var providerId = new Guid("8F62C8F0-7155-4C0E-8EBE-CD9357CFD1BF");
            var abonValidateCouponRequest = new AbonValidateCouponRequest
            {
                CouponCode = couponCode,
                ProviderId = providerId
            };
            var dataToSign = AircashSignatureService.ConvertObjectToString(abonValidateCouponRequest);
            var signature = AircashSignatureService.GenerateSignature(dataToSign, "C:\\Users\\user\\Desktop\\Mihael\\OpenSSL\\OnlineVirtualPartnerPrivateKey.pfx", "Aircash123");
            abonValidateCouponRequest.Signature = signature;
            DateTime requestDateTime = DateTime.UtcNow;
            var response = await HttpRequestService.SendRequestAircash(abonValidateCouponRequest, HttpMethod.Post, "https://staging-a-bon.aircash.eu/rest/api/OnlineProvider/ValidateCoupon");
            if (response.ResponseCode == System.Net.HttpStatusCode.OK)
            {
                validateCouponResponse = JsonConvert.DeserializeObject<AbonValidateCouponResponse>(response.ResponseContent);
            }
            else
            {
                validateCouponResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.ResponseContent);
            }
            return validateCouponResponse;    
        }

        public async Task<object> ConfirmTransaction(string couponCode, string userId)
        {
            var confirmTransactionResponse = new object();
            var providerId = new Guid("33352406-f672-4c27-a415-e26eb3ecd691");
            var providerTransactionId = new Guid("d0e19ce2-df5f-48d0-8520-7513618a6d72");
            var abonConfirmTransactionRequest = new AbonConfirmTransactionRequest
            {
                CouponCode = couponCode,
                ProviderId = providerId,
                ProviderTransactionId = providerTransactionId,
                UserId = userId
            };
            var dataToSign = AircashSignatureService.ConvertObjectToString(abonConfirmTransactionRequest);
            var signature = AircashSignatureService.GenerateSignature(dataToSign, "C:\\cert\\OnlineVirtualPartnerPrivateKey.pfx", "Aircash123");
            abonConfirmTransactionRequest.Signature = signature;
            DateTime requestDateTime = DateTime.UtcNow;
            var response = await HttpRequestService.SendRequestAircash(abonConfirmTransactionRequest, HttpMethod.Post, "https://staging-a-bon.aircash.eu/rest/api/OnlineProvider/ConfirmTransaction");
            if (response.ResponseCode == System.Net.HttpStatusCode.OK)
            {
                var successResponse = JsonConvert.DeserializeObject<AbonConfirmTransactionResponse>(response.ResponseContent);
                var newTransaction = new TransactionEntity
                {
                    Amount = successResponse.CouponValue,
                    ISOCurrencyId = successResponse.ISOCurrency,
                    CouponCode = couponCode,
                    PartnerId = providerId,
                    TransactionId = new Guid(),
                    RequestDateTimeUTC = requestDateTime,
                    ResponseDateTimeUTC = DateTime.UtcNow,
                    UserId = new Guid(),
                    ServiceId = ServiceEnum.AbonUsed
                };
                AircashSimulatorContext.Add(newTransaction);
                AircashSimulatorContext.SaveChanges();
                confirmTransactionResponse = successResponse;
            }
            else
            {
                confirmTransactionResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.ResponseContent);
            }
            return confirmTransactionResponse;


        }
    }
}
