using LocationBaseparkingSystem.Models;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocationBaseparkingSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                ViewBag.UserName = User.Identity.Name;
                return RedirectToAction("Index", "Admin");
            }
            else if (User.Identity.IsAuthenticated && User.IsInRole("VendorAdmin"))
            {
                ViewBag.UserName = User.Identity.Name;
                return RedirectToAction("Index", "Vendor");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Getparkonvendors(double latitude, double longitude)
        {
            var radius = 5000;
            IList<ParkOnVendorModel> vendorlist = new List<ParkOnVendorModel>();
            var center = new GeoCoordinate(latitude, longitude);
            using (ApplicationDbContext db = new Models.ApplicationDbContext())
            {
                foreach (var vendor in db.ParkOnVendor.ToList())
                {
                    var test = new GeoCoordinate(Convert.ToDouble(vendor.Latitude), Convert.ToDouble(vendor.Longitude));
                    if (test.GetDistanceTo(center) < radius)
                    {
                        vendorlist.Add(new ParkOnVendorModel
                        {
                            Address = vendor.Address,
                            Latitude = vendor.Latitude,
                            Longitude = vendor.Longitude,
                            Name = vendor.Name,
                            NoOfParkingSpace = vendor.NoOfParkingSpace,
                            NoOfRemainingParking = db.ParkOnVendorTrans.Count(x => x.IsOut == false && x.VenderID == vendor.Id)
                        });
                    }
                }
            }
            return Json(vendorlist);
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}