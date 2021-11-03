using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AbonOnlinePartner
{
    public class AbonConfirmTransactionRequest
    {
        public string CouponCode { get; set; }
        public string ProviderId { get; set; }
        public string ProviderTransactionId { get; set; }
        public string UserId { get; set; }
        public string Signature { get; set; }
    }
}
