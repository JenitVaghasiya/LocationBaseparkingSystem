using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LocationBaseparkingSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace LocationBaseparkingSystem.Controllers
{
    public class VendorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public VendorController()
        {
        }

        public VendorController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Vendor
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var vendor = db.ParkOnVendor.FirstOrDefault(u => u.UserId == userId);
            var vendortrans = db.ParkOnVendorTrans
                              .Where(v => v.VenderID == vendor.Id && v.IsOut == false).ToList();
            var noOfRemainingParking = 0;
            noOfRemainingParking = vendor.NoOfParkingSpace ?? 0;
            noOfRemainingParking = noOfRemainingParking - vendortrans.Count();
            return View(new ParkOnTransModel
            {
                VendorId = vendor.Id,
                NoOfRemainingParking = noOfRemainingParking,
                ParkOnVendorTrans = vendortrans
            });
        }

        // POST: Vendor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,VenderID,CustomerName,PhoneNo,CarNo,EntryTime,ExitTime,IsOut,TotalHours,TotalCost")] ParkOnVendorTrans parkOnVendorTrans)
        {
            if (ModelState.IsValid)
            {

                return RedirectToAction("Index");
            }
            return View(parkOnVendorTrans);
        }

        // GET: Vendor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkOnVendorTrans parkOnVendorTrans = db.ParkOnVendorTrans.Find(id);
            if (parkOnVendorTrans == null)
            {
                return HttpNotFound();
            }
            return View(parkOnVendorTrans);
        }

        // POST: Vendor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ParkOnVendorTrans parkOnVendorTrans = db.ParkOnVendorTrans.Find(id);
            db.ParkOnVendorTrans.Remove(parkOnVendorTrans);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public bool AddUpdateVehicleDetail(ParkOnVendorTrans parkOnVendorTrans)
        {
            if (ModelState.IsValid)
            {
                var vendor = db.ParkOnVendor.FirstOrDefault(u => u.Id == parkOnVendorTrans.VenderID);
                if (parkOnVendorTrans.Id > 0)
                {
                    if (parkOnVendorTrans.IsOut)
                    {
                        parkOnVendorTrans.ExitTime = DateTime.Now;
                        TimeSpan diff = parkOnVendorTrans.EntryTime - DateTime.Now;
                        parkOnVendorTrans.TotalHours = (decimal)diff.TotalHours;
                        parkOnVendorTrans.TotalCost = (decimal)diff.TotalHours * vendor.HourRate;
                    }
                    parkOnVendorTrans.EntryTime = DateTime.Now;
                    db.Entry(parkOnVendorTrans).State = EntityState.Modified;
                }
                else
                {
                    parkOnVendorTrans.VenderID = vendor.Id;
                    parkOnVendorTrans.EntryTime = DateTime.Now;
                    db.ParkOnVendorTrans.Add(parkOnVendorTrans);
                }
                db.SaveChanges();
                return true;
            }
            return false;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
