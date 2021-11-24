using System;

namespace AircashSimulator.Configuration
{
    public class AbonConfiguration
    {
        public string BaseUrl { get; set; }
        public string CreateCouponEndpoint { get; set; }
        public string CancelCouponEndpoint { get; set; }
    }
}
