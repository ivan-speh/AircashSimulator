using DataAccess;
using Domain.Entities;
using Domain.Entities.Enum;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Services.AbonSalePartner
{
    public class AbonSalePartnerService : IAbonSalePartnerService
    {
        private AircashSimulatorContext AircashSimulatorContext;
        public AbonSalePartnerService(AircashSimulatorContext aircashSimulatorContext)
        {
            AircashSimulatorContext = aircashSimulatorContext;
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
                //ISOCurrencySymbol = "HRK",
                PartnerTransactionId = Guid.NewGuid().ToString(),
                ContentType = null,
                ContentWidth=null,
                Signature=null

            };

            var createCouponResponse = new CreateCouponResponse()
            {
                SerialNumber="5722230340416087",
                CouponCode="3542893940565049",
                Value=value,
                IsoCurrencySymbol="EUR",
                Content="abc...123",
                PartnerTransactionId="0b376282-1a6f-4c1a-a7a1-36153086d760"

            };
            var responseDateTimeUTC = DateTime.UtcNow;
            AircashSimulatorContext.Transactions.Add(new TransactionEntity { 
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
                PurchasedOnUTC= requestDateTimeUTC

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
