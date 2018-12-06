using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudManager.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public int AmountOfDevices { get; set; }
    }
}
