using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AircashSimulator
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        public TransactionController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            //poziv poslovne logike
            return Ok("Transactions...");
        }
    }
}
