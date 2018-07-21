using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocationBaseparkingSystem.Models
{
    public class ParkOnVendor
    {
        public int ID { get; set; }

        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Long { get; set; }

        public decimal Lat { get; set; }

        public decimal Address { get; set; }

        public string LandMark { get; set; }

        public int  NoOfParkingSpace{ get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }

        public int HourRate { get; set; }

        public int Area { get; set; }
        public int Email { get; set; }
    }
}