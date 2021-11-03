using DataAccess;
using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Services.AbonOnlinePartner
{
    public class AbonOnlinePartnerService : IAbonOnlinePartnerService
    {
        private AircashSimulatorContext AircashSimulatorContext;
        public AbonOnlinePartnerService(AircashSimulatorContext aircashSimulatorContext)
        {
            AircashSimulatorContext = aircashSimulatorContext;
        }
        public async Task ValidateCoupon(string couponCode)
        {
            var providerId = "fe3cfec2-1d1d-45e1-a49f-d3708db45a5a";
            string signature = "";
            var abonValidateCouponRequest = new AbonValidateCouponRequest
            {
                CouponCode = couponCode,
                ProviderId = providerId,
                Signature = signature
            };
            var newTransaction = new TransactionEntity
            {
                CouponCode = couponCode,
                PartnerId = providerId,
                TransactionId = "d0e19ce2-df5f-48d0-8520-7513618a6d72",
                RequestDateTimeUTC = DateTime.UtcNow,
            };
            AircashSimulatorContext.Transactions.Add(newTransaction);
            AircashSimulatorContext.SaveChanges();

            //mock request na abon servis
            var abonValidateCouponResponse = new AbonValidateCouponResponse
            {
                CouponValue = 100.0M,
                IsValid = true,
                ISOCurrency = "190",
                ProviderTransactionId = "d0e19ce2-df5f-48d0-8520-7513618a6d72",
                OriginalCouponValue = 100.0M,
                OriginalISOCurrency = "HRK"
            };

            newTransaction.Amount = abonValidateCouponResponse.CouponValue;
            newTransaction.ISOCurrencyId = Int32.Parse(abonValidateCouponResponse.ISOCurrency);
            newTransaction.ResponseDateTimeUTC = DateTime.UtcNow;
            
            AircashSimulatorContext.Transactions.Update(newTransaction);
            AircashSimulatorContext.SaveChanges();
        }

        public async Task ConfirmTransaction(string couponCode, string userId)
        {
            var providerId = "fe3cfec2-1d1d-45e1-a49f-d3708db45a5a";
            var providerTransactionId = (new Guid()).ToString();
            string signature = "";
            var abonConfirmTransactionRequest = new AbonConfirmTransactionRequest
            {
                CouponCode = couponCode,
                ProviderId = providerId,
                ProviderTransactionId = providerTransactionId,
                UserId = userId,
                Signature = signature
            };
            DateTime requestDateTime = DateTime.UtcNow;
            //mock request na abon servis
            var abonConfirmTransacionResponse = new AbonConfirmTransactionResponse
            {
                CouponValue = 100.0M,
                ISOCurrency = "190",
                ProviderTransactionId = (new Guid()).ToString()
            };
            var newTransaction = new TransactionEntity
            {
                Amount = abonConfirmTransacionResponse.CouponValue,
                ISOCurrencyId = Int32.Parse(abonConfirmTransacionResponse.ISOCurrency),
                CouponCode = couponCode,
                PartnerId = providerId,
                TransactionId = "d0e19ce2-df5f-48d0-8520-7513618a6d72",
                RequestDateTimeUTC = requestDateTime,
                ResponseDateTimeUTC = DateTime.UtcNow,
                ServiceId = 6,
                UserId = userId,
            };
            AircashSimulatorContext.Transactions.Add(newTransaction);
            AircashSimulatorContext.SaveChanges();
        }
    }
}
