﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        [Editable(false)]
        public DateTime Date { get; set; }

        //Virtual allows Entity Framework to automatically manage proxies & DB
        //Enables dropdown datafeed
        public virtual Customer Customer { get; set; }
    }
}
