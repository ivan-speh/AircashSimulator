using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.PartnerService
{
    public interface IPartnerService
    {
        void SavePartner(PartnerDto partner);
        PartnerDto GetPartner(int partnerId);
        List<PartnerDto> GetPartners();
    }
}
