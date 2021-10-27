using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess;
using Domain.Entities;

namespace Services.PartnerService
{
    public class PartnerService : IPartnerService
    {
        private AircashSimulatorContext AircashSimulatorContext;
        public PartnerService(AircashSimulatorContext aircashSimulatorContext) 
        {
            AircashSimulatorContext = aircashSimulatorContext;
        }
        public PartnerDto GetPartner(int id)
        {
            var partnerDb = AircashSimulatorContext.Partners.FirstOrDefault(x => x.Id == id);
            var partner = new PartnerDto();
            if (partnerDb != null)
            {
                partner.Id = partnerDb.Id;
                partner.PartnerId = partnerDb.PartnerId;
                partner.PartnerName = partnerDb.PartnerName;
                partner.PrivateKey = partnerDb.PrivateKey;
                partner.PrivateKeyPass = partnerDb.PrivateKeyPass;
                partner.CurrencyId = partnerDb.CurrencyId;
                partner.CountryCode = partnerDb.CountryCode;
            }
            return partner;
        }

        public List<PartnerDto> GetPartners()
        {
            var partners = AircashSimulatorContext.Partners.Select(partnerEntity => new PartnerDto
            {       
                Id = partnerEntity.Id,
                PartnerId = partnerEntity.PartnerId,
                PartnerName = partnerEntity.PartnerName,
                PrivateKey = partnerEntity.PrivateKey,
                PrivateKeyPass = partnerEntity.PrivateKeyPass,
                CurrencyId = partnerEntity.CurrencyId,
                CountryCode = partnerEntity.CountryCode 
            }).ToList();
            return partners;
        }

        public void SavePartner(PartnerDto partner)
        {
            throw new NotImplementedException();
        }
    }
}
