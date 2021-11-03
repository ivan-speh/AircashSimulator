using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.AbonOnlinePartner;

namespace AircashSimulator
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AbonOnlinePartnerController : ControllerBase
    {
        private IAbonOnlinePartnerService AbonOnlinePartnerService;
        public AbonOnlinePartnerController(IAbonOnlinePartnerService abonOnlinePartnerService)
        {
            AbonOnlinePartnerService = abonOnlinePartnerService;
        }

        [HttpPost]
        public async Task<IActionResult> ValidateCoupon(ValidateCouponRequest validateCouponRequest)
        {
            await AbonOnlinePartnerService.ValidateCoupon(validateCouponRequest.CouponCode);
            return Ok(validateCouponRequest);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmTransaction(ConfirmTransactionRequest confirmTransactionRequest)
        {
            await AbonOnlinePartnerService.ConfirmTransaction(confirmTransactionRequest.CouponCode, confirmTransactionRequest.UserId);
            return Ok(confirmTransactionRequest);
        }
    }
}
