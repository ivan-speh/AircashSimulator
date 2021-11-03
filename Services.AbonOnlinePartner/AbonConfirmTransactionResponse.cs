using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AbonOnlinePartner
{
    class AbonConfirmTransactionResponse
    {
        public decimal CouponValue { get; set; }
        public string ISOCurrency { get; set; }
        public string ProviderTransactionId { get; set; }
    }
}
