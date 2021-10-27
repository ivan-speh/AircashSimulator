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
        public string Code { get; set; } 
        public Guid PartnerId { get; set; } 
        public string AircashTransactionId { get; set; }
        public Guid TransactionId { get; set; } 
        public DateTime? RequestDateTimeUTC { get; set; } 
        public DateTime? ResponseDateTimeUTC { get; set; } 
        public int ServiceId { get; set; } 
        public Guid UserId { get; set; } 
        public string PointOfSaleId { get; set; } 


    }
}
