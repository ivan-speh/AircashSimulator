using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AbonOnlinePartner
{
    public class AbonValidateCouponRequest
    {
        public string CouponCode { get; set; }
        public string ProviderId { get; set; }
        public string Signature { get; set; }
    }
}
