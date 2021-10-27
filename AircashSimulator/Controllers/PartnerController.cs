using Microsoft.AspNetCore.Mvc;
using Services.PartnerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AircashSimulator
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PartnerController : ControllerBase
    {
        private IPartnerService PartnerService;
        public PartnerController(IPartnerService partnerService)
        {
            PartnerService = partnerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPartner(int Id)
        {
            return Ok(PartnerService.GetPartner(Id));
        }
    }
}
