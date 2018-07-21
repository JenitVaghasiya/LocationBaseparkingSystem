using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult> Index()
        {
            return View(await db.ParkOnVendor.ToListAsync());
        }

        // GET: Admin/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkOnVendor parkOnVendor = await db.ParkOnVendor.FindAsync(id);
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
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,UserId,Email,Name,Longitude,Latitude,Address,LandMark,NoOfParkingSpace,CreatedDate,IsActive,HourRate,Area")] ParkOnVendor parkOnVendor)
        {
            if (ModelState.IsValid)
            {
                db.ParkOnVendor.Add(parkOnVendor);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(parkOnVendor);
        }

        // GET: Admin/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkOnVendor parkOnVendor = await db.ParkOnVendor.FindAsync(id);
            if (parkOnVendor == null)
            {
                return HttpNotFound();
            }
            return View(parkOnVendor);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,UserId,Email,Name,Longitude,Latitude,Address,LandMark,NoOfParkingSpace,CreatedDate,IsActive,HourRate,Area")] ParkOnVendor parkOnVendor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parkOnVendor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(parkOnVendor);
        }

        // GET: Admin/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkOnVendor parkOnVendor = await db.ParkOnVendor.FindAsync(id);
            if (parkOnVendor == null)
            {
                return HttpNotFound();
            }
            return View(parkOnVendor);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ParkOnVendor parkOnVendor = await db.ParkOnVendor.FindAsync(id);
            db.ParkOnVendor.Remove(parkOnVendor);
            await db.SaveChangesAsync();
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
