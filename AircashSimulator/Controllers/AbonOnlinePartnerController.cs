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
            var response=await AbonOnlinePartnerService.ValidateCoupon(validateCouponRequest.CouponCode);
            return Ok(response);
            //return Ok(AbonOnlinePartnerService.ValidateCoupon(validateCouponRequest.CouponCode));
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmTransaction(ConfirmTransactionRequest confirmTransactionRequest)
        {
            var response=await AbonOnlinePartnerService.ConfirmTransaction(confirmTransactionRequest.CouponCode, confirmTransactionRequest.UserId);
            return Ok(response);
            //return Ok(AbonOnlinePartnerService.ConfirmTransaction(confirmTransactionRequest.CouponCode, confirmTransactionRequest.UserId).ToString());
        }
    }
}
