using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocationBaseparkingSystem.Models
{
    public class ParkOnVendor
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Longitude { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Address { get; set; }

        public string LandMark { get; set; }

        public int? NoOfParkingSpace { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }

        public int? HourRate { get; set; }

        public string Area { get; set; }
        
    }
}