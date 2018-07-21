using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocationBaseparkingSystem.Models
{
    public class ParkOnVendor
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal? Longitude { get; set; }

        [Required]
        public decimal? Latitude { get; set; }

        public string Address { get; set; }

        public string LandMark { get; set; }

        [Required(ErrorMessage ="No of parking space is required.")]
        public int? NoOfParkingSpace { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }

        public decimal? HourRate { get; set; }

        public string Area { get; set; }
        
    }
}