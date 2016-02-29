using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace carwings.net
{
    public class CheckRequestResponse
    {
        public string Message { get; set; }

        public string ResultKey { get; set; }

        public int Status { get; set; }

        public string UserId { get; set; }

        public string Vin { get; set; }
    }
}
