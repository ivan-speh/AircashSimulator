using System;
using System.Threading.Tasks;

namespace Services.AbonOnlinePartner
{
    public class AbonOnlinePartnerService : IAbonOnlinePartnerService
    {
        public async Task ValidateCoupon(ValidateCouponRequest validateCouponRequest)
        {
            string couponCode = validateCouponRequest.CouponCode;
            var providerId = (new Guid()).ToString();
            string signature = "";
            var abonValidateCouponRequest = new AbonValidateCouponRequest
            {
                CouponCode = couponCode,
                ProviderId = providerId,
                Signature = signature
            };
        }

        public async Task ConfirmTransaction(ConfirmTransactionRequest confirmTransactionRequest)
        {
            string couponCode = confirmTransactionRequest.CouponCode;
            var providerId = (new Guid()).ToString();
            var providerTransactionId = (new Guid()).ToString();
            var userId = confirmTransactionRequest.UserId;
            string signature = "";
            var abonConfirmTransactionRequest = new AbonConfirmTransactionRequest
            {
                CouponCode = couponCode,
                ProviderId = providerId,
                ProviderTransactionId = providerTransactionId,
                UserId = userId,
                Signature = signature
            };
        }
    }
}
