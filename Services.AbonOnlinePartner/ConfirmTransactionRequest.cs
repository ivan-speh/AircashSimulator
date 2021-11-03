using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.AbonOnlinePartner
{
    public class ConfirmTransactionRequest
    {
        public string CouponCode { get; set; }
        public string UserId { get; set; }
    }
}
