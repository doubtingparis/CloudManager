using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string AuthKey { get; set; }

        [DisplayName("Date created")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        //Virtual allows Entity Framework to automatically manage proxies & DB
        //Enables dropdown datafeed
        public virtual Customer Customer { get; set; }

        [NotMapped]
        public IEnumerable<Customer> CustomerSelection { get; set; }
    }
}
