using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AbonOnlinePartner
{
    public interface IAbonOnlinePartnerService
    {
        Task ValidateCoupon(string CouponCode);
        Task ConfirmTransaction(string CouponCode, string UserId);
    }
}
