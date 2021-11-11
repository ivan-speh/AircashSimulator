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

namespace Services.AbonOnlinePartner
{
    public class AbonOnlinePartnerService : IAbonOnlinePartnerService
    {
        private AircashSimulatorContext AircashSimulatorContext;
        private IHttpRequestService HttpRequestService;
        public AbonOnlinePartnerService(AircashSimulatorContext aircashSimulatorContext, IHttpRequestService httpRequestService)
        {
            AircashSimulatorContext = aircashSimulatorContext;
            HttpRequestService = httpRequestService;
        }
        public async Task ValidateCoupon(string couponCode)
        {
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
            var responseString = await HttpRequestService.SendRequestAircash(abonValidateCouponRequest, HttpMethod.Post, "https://staging-a-bon.aircash.eu/rest/api/OnlineProvider/ValidateCoupon");
            var abonValidateCouponResponse = JsonConvert.DeserializeObject<AbonValidateCouponResponse>(responseString);
        }

        public async Task ConfirmTransaction(string couponCode, string userId)
        {
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
            var signature = AircashSignatureService.GenerateSignature(dataToSign, "C:\\Users\\user\\Desktop\\Mihael\\OpenSSL\\PrivateKeyPfxFile.pfx", "Aircash123");
            abonConfirmTransactionRequest.Signature = signature;
            DateTime requestDateTime = DateTime.UtcNow;
            var responseString = await HttpRequestService.SendRequestAircash(abonConfirmTransactionRequest, HttpMethod.Post, "https://staging-a-bon.aircash.eu/rest/api/OnlinePartner/ValidateCoupon");
            var abonConfirmTransactionResponse = JsonConvert.DeserializeObject<AbonConfirmTransactionResponse>(responseString);
            var newTransaction = new TransactionEntity
            {
                Amount = abonConfirmTransactionResponse.CouponValue,
                ISOCurrencyId = abonConfirmTransactionResponse.ISOCurrency,
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
        }
    }
}
