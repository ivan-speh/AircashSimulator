using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AbonOnlinePartner
{
    public class Data
    {
        public decimal CouponValue { get; set; }
        public string ISOCurrency { get; set; }
        public Guid ProviderTransactionId { get; set; }
    }
}
