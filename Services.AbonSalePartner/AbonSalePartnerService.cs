using AircashSignature;
using AircashSimulator.Configuration;
using DataAccess;
using Domain.Entities;
using Domain.Entities.Enum;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Services.HttpRequest;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace Services.AbonSalePartner
{
    public class AbonSalePartnerService : IAbonSalePartnerService
    {
        private AircashSimulatorContext AircashSimulatorContext;
        private IHttpRequestService HttpRequestService;
        private AbonConfiguration AbonConfiguration;

        public AbonSalePartnerService(AircashSimulatorContext aircashSimulatorContext, IHttpRequestService httpRequestService, IOptionsMonitor<AbonConfiguration> abonConfiguration)
        {
            AircashSimulatorContext = aircashSimulatorContext;
            HttpRequestService = httpRequestService;
            AbonConfiguration = abonConfiguration.CurrentValue;
        }

        public async Task<object> CreateCoupon(decimal value, string pointOfSaleId, Guid partnerId)
        {
            var createCouponResponse=new object();
            var partner = AircashSimulatorContext.Partners.Where(x => x.PartnerId == partnerId).FirstOrDefault();
            var requestDateTimeUTC = DateTime.UtcNow;
            var transactionId = Guid.NewGuid();
            var createCouponRequest = new CreateCouponRequest()
            {
                PartnerId = partnerId.ToString(),
                Value = value,
                PointOfSaleId = pointOfSaleId,
                ISOCurrencySymbol = ((CurrencyEnum)partner.CurrencyId).ToString(),
                PartnerTransactionId = transactionId.ToString(),
                ContentType = null,
                ContentWidth = null
            };
            var sequence = AircashSignatureService.ConvertObjectToString(createCouponRequest);
            var signature = AircashSignatureService.GenerateSignature(sequence, partner.PrivateKey, partner.PrivateKeyPass);
            //var signature = AircashSignatureService.GenerateSignature(sequence, "C:\\cert\\OnlineVirtualPartnerPrivateKey.pfx", partner.PrivateKeyPass);
            createCouponRequest.Signature = signature;
            //var responseString = await HttpRequestService.SendRequestAircash(createCouponRequest, HttpMethod.Post, "https://staging-a-bon.aircash.eu/rest/api/CashRegister/CreateCoupon");
            var response = await HttpRequestService.SendRequestAircash(createCouponRequest, HttpMethod.Post, $"{AbonConfiguration.BaseUrl}{AbonConfiguration.CreateCouponEndpoint}");
            if(response.ResponseCode == System.Net.HttpStatusCode.OK)
            {
                var successResponse = JsonConvert.DeserializeObject<CreateCouponResponse>(response.ResponseContent);
                var responseDateTimeUTC = DateTime.UtcNow;
                AircashSimulatorContext.Transactions.Add(new TransactionEntity
                {
                    Amount = value,
                    ISOCurrencyId = (CurrencyEnum)partner.CurrencyId,
                    CouponCode = successResponse.CouponCode,
                    PartnerId = partnerId,
                    AircashTransactionId = successResponse.SerialNumber,
                    TransactionId = transactionId,
                    ServiceId = ServiceEnum.AbonIssued,
                    UserId = Guid.NewGuid(),
                    PointOfSaleId = createCouponRequest.PointOfSaleId,
                    RequestDateTimeUTC = requestDateTimeUTC,
                    ResponseDateTimeUTC = responseDateTimeUTC
                });

                //spremanje kupona u Coupons tablicu
                AircashSimulatorContext.Coupons.Add(new CouponEntity
                {
                    SerialNumber = successResponse.CouponCode,
                    PurchasedPartnerID = partner.Id,
                    PurchasedAmount = value,
                    PurchasedCurrency = (CurrencyEnum)partner.CurrencyId,
                    PurchasedCountryIsoCode = partner.CountryCode,
                    PurchasedOnUTC = requestDateTimeUTC,
                    Content = successResponse.Content
                });
                AircashSimulatorContext.SaveChanges();
                createCouponResponse = successResponse;
            }
            else
            {
                createCouponResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.ResponseContent);
            }
            return createCouponResponse;
        }

        public async Task<object> CancelCoupon(string serialNumber, string pointOfSaleId, Guid partnerId)
        {
            var cancelCouponResponse = new object();
            var partner = AircashSimulatorContext.Partners.Where(x => x.PartnerId == partnerId).FirstOrDefault();
            var coupon = AircashSimulatorContext.Coupons.Where(x => x.SerialNumber == serialNumber).FirstOrDefault();
            //var transaction = AircashSimulatorContext.Transactions.Where(x => x.CouponCode == serialNumber).FirstOrDefault();
            var requestDateTimeUTC = DateTime.UtcNow;
            if (coupon != null)
            {
                var cancelCouponRequest = new CancelCouponRequest()
                {
                    PartnerId = partnerId.ToString(),
                    SerialNumber = serialNumber,
                    PartnerTransactionId = "C41FAB14-D6C8-42FE-8EB3-677C7F47ECD1", //=null
                    PointOfSaleId = pointOfSaleId,
                };
                var sequence = AircashSignatureService.ConvertObjectToString(cancelCouponRequest);
                var signature = AircashSignatureService.GenerateSignature(sequence, partner.PrivateKey, partner.PrivateKeyPass);
                cancelCouponRequest.Signature = signature;
                var response=await HttpRequestService.SendRequestAircash(cancelCouponRequest, HttpMethod.Post, $"{AbonConfiguration.BaseUrl}{AbonConfiguration.CancelCouponEndpoint}");
                if (response.ResponseCode == System.Net.HttpStatusCode.OK)
                {   //var createCouponResponse = JsonConvert.DeserializeObject<CreateCouponResponse>(responseString);
                    var responseDateTimeUTC = DateTime.UtcNow;
                    coupon.CancelledOnUTC = responseDateTimeUTC;
                    AircashSimulatorContext.Coupons.Update(coupon);
                    AircashSimulatorContext.Transactions.Add(new TransactionEntity
                    {
                        Amount = coupon.PurchasedAmount,
                        ISOCurrencyId = (CurrencyEnum)partner.CurrencyId,
                        CouponCode = serialNumber,
                        PartnerId = partnerId,
                        //AircashTransactionId = serialNumber,
                        TransactionId = new Guid("C41FAB14-D6C8-42FE-8EB3-677C7F47ECD1"),
                        ServiceId = ServiceEnum.AbonCancelled,
                        UserId = Guid.NewGuid(),
                        PointOfSaleId = pointOfSaleId,
                        RequestDateTimeUTC = requestDateTimeUTC,
                        ResponseDateTimeUTC = responseDateTimeUTC
                    });
                    AircashSimulatorContext.SaveChanges();
                    cancelCouponResponse = null;
                }
                else
                {
                    cancelCouponResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.ResponseContent);
                }
               
            }
            return cancelCouponResponse;
        }
    }
}
