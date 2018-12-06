using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudManager.Models
{
    public class Device
    {
        public int CustomerID { get; set; }
        public int DeviceID { get; set; }
        public string AuthorizationToken { get; set; }
        public string TargetCloudURL { get; set; }
    }
}
