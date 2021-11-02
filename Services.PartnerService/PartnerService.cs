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
            Guid partnerId = partner.PartnerId;
            var partnerDb = AircashSimulatorContext.Partners.FirstOrDefault(x => x.PartnerId == partnerId);
            if (partnerDb == null)
            {
                var newPartnerEntity = new PartnerEntity
                {
                    Id = partner.Id,
                    PartnerId = partner.PartnerId,
                    PartnerName = partner.PartnerName,
                    PrivateKey = partner.PrivateKey,
                    PrivateKeyPass = partner.PrivateKeyPass,
                    CurrencyId = partner.CurrencyId,
                    CountryCode = partner.CountryCode
                };
                AircashSimulatorContext.Partners.Add(newPartnerEntity);
                AircashSimulatorContext.SaveChanges();
            }
            else
            {
                partnerDb.Id = partner.Id;
                partnerDb.PartnerId = partner.PartnerId;
                partnerDb.PartnerName = partner.PartnerName;
                partnerDb.PrivateKey = partner.PrivateKey;
                partnerDb.PrivateKeyPass = partner.PrivateKeyPass;
                partnerDb.CurrencyId = partner.CurrencyId;
                partnerDb.CountryCode = partner.CountryCode;
                AircashSimulatorContext.SaveChanges();
            }
        }
    }
}
