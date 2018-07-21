using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocationBaseparkingSystem.Models
{
    public class ParkOnTransModel
    {
        public int NoOfRemainingParking { get; set; }

        public int VendorId { get; set; }

        public List<ParkOnVendorTrans> ParkOnVendorTrans { get; set; }
    }
}