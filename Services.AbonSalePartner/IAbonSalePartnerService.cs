using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.AbonSalePartner
{
    public interface IAbonSalePartnerService
    {
        Task CreateCoupon(decimal value, string pointOfSaleId);
        Task CancelCoupon(string serialNumber, string pointOfSaleId);
    }
}
