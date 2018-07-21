using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocationBaseparkingSystem.Models
{
    public class ParkOnVendorTrans
    {
        public int ID { get; set; }
        public int VenderID { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime ExitTime { get; set; }
        public DateTime CarNo { get; set; }
        public bool IsOut { get; set; }
        public int Hours { get; set; }

        public int Cost { get; set; }

        public string CustomerName { get; set; }
        public string PhoneNo { get; set; }
    }
}