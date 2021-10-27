﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.PartnerService
{
    public class PartnerDto
    {
        public int Id { get; set; }
        public Guid PartnerId { get; set; }
        public string PartnerName { get; set; }
        public string PrivateKey { get; set; }
        public string PrivateKeyPass { get; set; }
        public int CurrencyId { get; set; }
        public string CountryCode { get; set; }
    }
}
