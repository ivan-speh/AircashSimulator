using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AircashSignature;

namespace Services.AbonOnlinePartner
{
    public class AbonConfirmTransactionRequest : ISignature
    {
        public string CouponCode { get; set; }
        public Guid ProviderId { get; set; }
        public Guid ProviderTransactionId { get; set; }
        public string UserId { get; set; }
        public string Signature { get; set; }
    }
}
