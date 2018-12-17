using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CloudManager.Models
{
    public class Device
    {
        [DisplayName("Customer ID")]
        [Required]
        public int CustomerID { get; set; }

        [DisplayName("Device ID")]
        [Key]
        public int DeviceID { get; set; }

        [DisplayName("Authentication Key")]
        [Editable(false)]
        public string AuthKey { get; set; }

        [DisplayName("Date created")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        //Virtual allows Entity Framework to automatically manage proxies & DB
        public virtual Customer Customer { get; set; }
    }
}
