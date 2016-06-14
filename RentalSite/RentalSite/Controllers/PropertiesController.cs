using RentalSite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RentalSite.Helpers;

namespace RentalSite.Controllers
{
    public class PropertiesController : Controller
    {
        /// <summary>
        /// Datacontext for PropertyListingModel
        /// </summary>
        private PropertyListingModel db = new PropertyListingModel();

        // GET: Properties
        public ActionResult Index()
        {
            var properties = db.Properties.Include(p => p.PropertyAddress).Include(p => p.PropertyDetails);
            return View(properties.ToList());
        }

        // GET: Properties/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // GET: Properties/Create
        public ActionResult Create()
        {
            Property property = new Property();
            
            return View(property);
        }

        // POST: Properties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Create([Bind(Include = "PropertyId,Name, PropertyAddress, PropertyDetails")] Property property)
        {
            if (ModelState.IsValid)
            {
                property.PropertyId = Guid.NewGuid();
                property.Active = true;

                property.PropertyDetails.PropertyDetailsId = Guid.NewGuid();
                property.PropertyDetails.PropertyId = property.PropertyId;

                property.PropertyAddress.AddressId = Guid.NewGuid();
                property.PropertyAddress.PropertyId = property.PropertyId;
                
                db.Properties.Add(property);
                db.SaveChanges();
                return RedirectToAction("AddImages", "Properties", property);
            }

            return View(property);
        }

        // GET: Properties/AddImages
        public ActionResult AddImages(Property property)
        {
            ViewBag.PropertyId = property.PropertyId;
            ViewBag.PropertyName = property.Name;
            return View(ViewBag);
        }

        // GET: Properties/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            ViewBag.PropertyId = new SelectList(db.Addresses, "AddressId", "AddressLine1", property.PropertyId);
            ViewBag.PropertyId = new SelectList(db.Details, "PropertyDetailsId", "PropertyDetailsId", property.PropertyId);
            return View(property);
        }

        // POST: Properties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PropertyId,Name")] Property property)
        {
            if (ModelState.IsValid)
            {
                db.Entry(property).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PropertyId = new SelectList(db.Addresses, "AddressId", "AddressLine1", property.PropertyId);
            ViewBag.PropertyId = new SelectList(db.Details, "PropertyDetailsId", "PropertyDetailsId", property.PropertyId);
            return View(property);
        }

        // GET: Properties/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // POST: Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Property property = db.Properties.Find(id);
            db.Properties.Remove(property);
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
