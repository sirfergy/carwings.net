using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace carwings.net
{
    public class BatteryStatusRecordsResponse
    {
        public int Status { get; set; }

        public string Message { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public BatteryStatusRecords BatteryStatusRecords { get; set; }
    }
}
