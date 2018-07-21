using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocationBaseparkingSystem.Models
{
    public class ParkOnVendor
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public decimal? Longitude { get; set; }

        public decimal? Latitude { get; set; }

        public string Address { get; set; }

        public string LandMark { get; set; }

        public int? NoOfParkingSpace { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }

        public decimal? HourRate { get; set; }

        public string Area { get; set; }
        
    }

    public class ParkOnVendorModel
    {
        public string Name { get; set; }

        public decimal? Longitude { get; set; }

        public decimal? Latitude { get; set; }

        public string Address { get; set; }

        public int? NoOfParkingSpace { get; set; }

        public int? NoOfRemainingParking { get; set; }

    }
}