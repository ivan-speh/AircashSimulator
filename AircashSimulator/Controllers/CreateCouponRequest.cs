using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AircashSimulator.Controllers
{
    public class CreateCouponRequest
    {
        public decimal Value { get; set; }
        public string PointOfSaleId { get; set; }
    }
}
