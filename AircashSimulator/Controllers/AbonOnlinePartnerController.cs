using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AircashSimulator.Controllers;

namespace AircashSimulator
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AbonOnlinePartnerController : ControllerBase
    {
        public AbonOnlinePartnerController()
        {
        }

        [HttpPost]
        public async Task<IActionResult> ValidateCoupon(ValidateCouponRequest validateCouponRequest)
        {
            return Ok("Validated");
        }        
        public async Task<IActionResult> ConfirmTransaction(ConfirmTransactionRequest confirmTransactionRequest)
        {
            return Ok("Confirmed");
        }
    }
}
