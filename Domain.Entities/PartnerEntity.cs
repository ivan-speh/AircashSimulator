using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class PartnerEntity
    {
        public int Id { get; set; }

        public Guid PartnerId { get; set; } // not null
         
        public string PartnerName { get; set; } // max 256, not null

        public string PrivateKey { get; set; } // not null

        public string PrivateKeyPass { get; set; } // not null

        public int CurrencyId { get; set; } // not null

        public string CountryCode { get; set; } // not null
    }
}
