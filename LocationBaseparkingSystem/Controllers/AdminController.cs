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
using Microsoft.AspNet.Identity.Owin;

namespace LocationBaseparkingSystem.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public AdminController()
        {
        }

        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
            try
            {
                if (ModelState.IsValid)
                {
                    var user = db.Users.FirstOrDefault(u => u.Email == parkOnVendor.Email);
                    if (user != null)
                    {
                        var vendor = db.ParkOnVendor.FirstOrDefault(u => u.UserId == user.Id);
                        if (vendor != null)
                        {
                            parkOnVendor.UserId = user.Id;
                            vendor = parkOnVendor;
                        }
                    }
                    else
                    {
                        var usr = new ApplicationUser { UserName = parkOnVendor.Email, Email = parkOnVendor.Email };
                        var result = await UserManager.CreateAsync(usr, "P@$$w0rd");
                        if (result.Succeeded)
                        {
                            await UserManager.AddToRoleAsync(usr.Id, "VendorAdmin");
                            parkOnVendor.UserId = usr.Id;
                            parkOnVendor.CreatedDate = DateTime.Now;
                            db.ParkOnVendor.Add(parkOnVendor);

                        }
                    }
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

            }
            return View();
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
