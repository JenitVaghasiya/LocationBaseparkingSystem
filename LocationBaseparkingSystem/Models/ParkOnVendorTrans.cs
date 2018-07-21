using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocationBaseparkingSystem.Models
{
    public class ParkOnVendorTrans
    {
        public int Id { get; set; }

        public int VenderID { get; set; }

        public string CustomerName { get; set; }

        public string PhoneNo { get; set; }

        public string CarNo { get; set; }

        public DateTime EntryTime { get; set; }

        public DateTime? ExitTime { get; set; }

        public bool IsOut { get; set; }

        public decimal? TotalHours { get; set; }

        public decimal? TotalCost { get; set; }
    }
}