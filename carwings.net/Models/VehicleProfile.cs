using System;

namespace carwings.net
{
    public class VehicleProfile
    {
        public string Vin { get; set; }

        public string GdcUserId { get; set; }

        public string GdcPassword { get; set; }

        public string EncAuthToken { get; set; }

        public string DcmId { get; set; }

        public string Nickname { get; set; }

        public string Status { get; set; }

        public DateTime StatusDate { get; set; }
    }
}
