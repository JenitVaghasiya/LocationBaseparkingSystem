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
            IList<ParkOnVendor> vendorlist = new List<ParkOnVendor>();
            var center = new GeoCoordinate(latitude, longitude);
            using (ApplicationDbContext db = new Models.ApplicationDbContext())
            {
                //
                //var result = db.ParkOnVendor.Select(x => new GeoCoordinate(x.Latitude, x.Longitude)))
                //                      .Where(x => x.GetDistanceTo(center) < radius).ToList();
                foreach (var vendor in db.ParkOnVendor.ToList())
                {
                    var test = new GeoCoordinate(Convert.ToDouble(vendor.Latitude), Convert.ToDouble(vendor.Longitude));
                    if (test.GetDistanceTo(center) < radius)
                    {
                        vendorlist.Add(vendor);
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