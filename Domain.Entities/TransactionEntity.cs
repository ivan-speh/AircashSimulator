using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class TransactionEntity
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int ISOCurrencyId { get; set; }
        public string CouponCode { get; set; }
        public string PartnerId { get; set; }
        public string AircashTransactionId { get; set; }
        public string TransactionId { get; set; }
        public DateTime RequestDateTimeUTC { get; set; }
        public DateTime ResponseDateTimeUTC { get; set; }
        public int ServiceId { get; set; }
        public string UserId { get; set; }
        public string PointOfSaleId { get; set; }
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
