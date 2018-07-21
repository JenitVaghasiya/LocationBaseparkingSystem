using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LocationBaseparkingSystem.Models
{
    public class ParkOnVendorTrans
    {
        public int Id { get; set; }

        public int VenderID { get; set; }

        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }

        [DisplayName("Phone No")]
        public string PhoneNo { get; set; }

        [DisplayName("Vehical No")]
        public string CarNo { get; set; }

        public DateTime EntryTime { get; set; }

        public DateTime? ExitTime { get; set; }

        [DisplayName("Is Out?")]
        public bool IsOut { get; set; }

        public decimal? TotalHours { get; set; }

        public decimal? TotalCost { get; set; }
    }
}