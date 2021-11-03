using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using AircashSimulator.Controllers;
using Services.AbonSalePartner;

namespace AircashSimulator
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AbonSalePartnerController : ControllerBase
    {
        private IAbonSalePartnerService AbonSalePartnerService;
        public AbonSalePartnerController(IAbonSalePartnerService abonSalePartnerService)
        {
            AbonSalePartnerService = abonSalePartnerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoupon(CreateCouponRequest createCouponRequest)
        {
            await AbonSalePartnerService.CreateCoupon(createCouponRequest.Value, createCouponRequest.PointOfSaleId);
            return Ok(createCouponRequest);
        }
        [HttpPost]
        public async Task<IActionResult> CancelCoupon(CancelCouponRequest cancelCouponRequest)
        {
            await AbonSalePartnerService.CancelCoupon(cancelCouponRequest.SerialNumber, cancelCouponRequest.PointOfSaleId);
            return Ok(cancelCouponRequest);
        }
    }

}
