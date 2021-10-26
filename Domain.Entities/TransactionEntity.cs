using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class TransactionEntity
    {
        public int Id { get; set; }
        public decimal Amount { get; set; } // decimal(18,2), not null
        public int ISOCurrencyId { get; set; } // not null
        public string CouponCode { get; set; } // preimenovati u Code, max 16, not null
        public string PartnerId { get; set; } // promijeniti tip podatka u Guid, not null
        public string AircashTransactionId { get; set; } // null
        public string TransactionId { get; set; } // promijeniti tip podatka u Guid, not null
        public DateTime RequestDateTimeUTC { get; set; } // column type datetime2, null
        public DateTime ResponseDateTimeUTC { get; set; } // column type datetime2, null
        public int ServiceId { get; set; } // not null
        public string UserId { get; set; } // promijentii tip podatka u guid, not null
        public string PointOfSaleId { get; set; } // max 128, null

        // pobrisati ove komentare
        /*public string generateGuid()
        {
            string id = Guid.NewGuid().ToString();
            return id;
        }
        private static int id = 1;
        static int generateId()
        {
            return id++;
        }
        public TransactionEntity()
        {
            Id = generateId();
            TransactionId = generateGuid();
            AircashTransactionId = generateGuid();
        }*/
    }
}
