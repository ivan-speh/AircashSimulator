using System;
using System.Collections.Generic;
using System.Text;

namespace Services.AbonSalePartner
{
    class CancelCouponRequest
    {
        public string PartnerId { get; set; }
        public string SerialNumber { get; set; }
        public string PartnerTransactionId { get; set; }
        public string PointOfSaleId { get; set; }
        public string Signature { get; set; }
    }
}
