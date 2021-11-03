using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AbonOnlinePartner
{
    class AbonValidateCouponResponse
    {
        public decimal CouponValue { get; set; }
        public bool IsValid { get; set; }
        public string ISOCurrency { get; set; }
        public string ProviderTransactionId { get; set; }
        public decimal OriginalCouponValue { get; set; }
        public string OriginalISOCurrency { get; set; }
    }
}
