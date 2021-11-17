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



        public async Task CreateCoupon(decimal value, string pointOfSaleId, Guid partnerId)
        {
            var partner = AircashSimulatorContext.Partners.Where(x => x.PartnerId == partnerId).FirstOrDefault();
            var requestDateTimeUTC = DateTime.UtcNow;
            var createCouponRequest = new CreateCouponRequest()
            {
                PartnerId = partnerId.ToString(),
                Value = value,
                PointOfSaleId = pointOfSaleId,
                ISOCurrencySymbol = "HRK",
                PartnerTransactionId = Guid.NewGuid().ToString(),
                ContentType = null,
                ContentWidth = null
            };
            var sequence = AircashSignatureService.ConvertObjectToString(createCouponRequest);
            var signature = AircashSignatureService.GenerateSignature(sequence, partner.PrivateKey, partner.PrivateKeyPass);
            //var signature = AircashSignatureService.GenerateSignature(sequence, "C:\\cert\\OnlineVirtualPartnerPrivateKey.pfx", partner.PrivateKeyPass);
            createCouponRequest.Signature = signature;
            var responseString = await HttpRequestService.SendRequestAircash(createCouponRequest, HttpMethod.Post, "https://staging-a-bon.aircash.eu/rest/api/CashRegister/CreateCoupon");
            var createCouponResponse = JsonConvert.DeserializeObject<CreateCouponResponse>(responseString);

         
            var responseDateTimeUTC = DateTime.UtcNow;
            AircashSimulatorContext.Transactions.Add(new TransactionEntity 
            { 
                Amount=value,
                ISOCurrencyId=CurrencyEnum.HRK,
                CouponCode=createCouponResponse.CouponCode,
                PartnerId=partnerId,
                AircashTransactionId=createCouponResponse.SerialNumber,
                TransactionId= Guid.NewGuid(),
                ServiceId=ServiceEnum.AbonIssued,
                UserId=Guid.NewGuid(),
                PointOfSaleId=createCouponRequest.PointOfSaleId,
                RequestDateTimeUTC=requestDateTimeUTC,
                ResponseDateTimeUTC=responseDateTimeUTC
                
            });

            //spremanje kupona u Coupons tablicu
            AircashSimulatorContext.Coupons.Add(new CouponEntity
            {
                SerialNumber = createCouponResponse.CouponCode,
                PurchasedPartnerID = partner.Id,
                PurchasedAmount = value,
                PurchasedCurrency = (CurrencyEnum)partner.CurrencyId,
                PurchasedCountryIsoCode=partner.CountryCode,
                PurchasedOnUTC= requestDateTimeUTC,
                Content=createCouponResponse.Content

            });
            AircashSimulatorContext.SaveChanges();

            
        }

        public async Task CancelCoupon(string serialNumber, string pointOfSaleId)
        {
            var cancelCouponRequest = new CancelCouponRequest()
            {
                PartnerId=Guid.NewGuid().ToString(),
                SerialNumber=serialNumber,
                PartnerTransactionId=Guid.NewGuid().ToString(),
                PointOfSaleId=pointOfSaleId, 
                Signature=null
            };
            
        }
    }
}
