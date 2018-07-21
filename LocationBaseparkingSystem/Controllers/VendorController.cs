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
    public class VendorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Vendor
        public ActionResult Index()
        {
            return View(db.ParkOnVendorTrans.ToList());
        }

        // GET: Vendor/Details/5
        public ActionResult Details(int? id)
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

        // GET: Vendor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vendor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,VenderID,CustomerName,PhoneNo,CarNo,EntryTime,ExitTime,IsOut,TotalHours,TotalCost")] ParkOnVendorTrans parkOnVendorTrans)
        {
            if (ModelState.IsValid)
            {

                parkOnVendorTrans.EntryTime = DateTime.Now;

                db.ParkOnVendorTrans.Add(parkOnVendorTrans);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(parkOnVendorTrans);
        }

        // GET: Vendor/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Vendor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,VenderID,CustomerName,PhoneNo,CarNo,EntryTime,ExitTime,IsOut,TotalHours,TotalCost")] ParkOnVendorTrans parkOnVendorTrans)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parkOnVendorTrans).State = EntityState.Modified;
                db.SaveChanges();
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
