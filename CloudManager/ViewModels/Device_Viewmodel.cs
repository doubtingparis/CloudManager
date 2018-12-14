using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudManager.Models;

namespace CloudManager.ViewModels
{
    public class Device_ViewModel
    {
        //Device instance
        public Device Device { get; set; }

        //IEnumerable for customer dropdown content
        public IEnumerable<Customer> CustomerSelection { get; set; }
    }
}
