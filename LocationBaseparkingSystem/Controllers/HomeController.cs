using System;
using System.Collections.Generic;
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