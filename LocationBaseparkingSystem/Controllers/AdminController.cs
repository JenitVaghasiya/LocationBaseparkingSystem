using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LocationBaseparkingSystem.Models;

namespace LocationBaseparkingSystem.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View(db.ParkOnVendors.ToList());
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkOnVendor parkOnVendor = db.ParkOnVendors.Find(id);
            if (parkOnVendor == null)
            {
                return HttpNotFound();
            }
            return View(parkOnVendor);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserId,Name,Long,Lat,Address,LandMark,NoOfParkingSpace,CreatedDate,IsActive,HourRate,Area,Email")] ParkOnVendor parkOnVendor)
        {
            if (ModelState.IsValid)
            {
                db.ParkOnVendors.Add(parkOnVendor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(parkOnVendor);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkOnVendor parkOnVendor = db.ParkOnVendors.Find(id);
            if (parkOnVendor == null)
            {
                return HttpNotFound();
            }
            return View(parkOnVendor);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserId,Name,Long,Lat,Address,LandMark,NoOfParkingSpace,CreatedDate,IsActive,HourRate,Area,Email")] ParkOnVendor parkOnVendor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parkOnVendor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parkOnVendor);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkOnVendor parkOnVendor = db.ParkOnVendors.Find(id);
            if (parkOnVendor == null)
            {
                return HttpNotFound();
            }
            return View(parkOnVendor);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ParkOnVendor parkOnVendor = db.ParkOnVendors.Find(id);
            db.ParkOnVendors.Remove(parkOnVendor);
            db.SaveChanges();
            return RedirectToAction("Index");
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
