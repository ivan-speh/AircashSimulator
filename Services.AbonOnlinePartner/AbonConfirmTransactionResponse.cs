using Domain.Entities.Enum;
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
        public CurrencyEnum ISOCurrency { get; set; }
        public Guid ProviderTransactionId { get; set; }
    }
}
