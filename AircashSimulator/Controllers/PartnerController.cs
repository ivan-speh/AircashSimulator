using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public PartnerController()
        {
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPartners()
        {
            return Ok("Partners...");
        }
    }
}
