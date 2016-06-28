using RentalSite.Helpers;
using RentalSite.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RentalSite.Controllers
{
    public class PropertiesController : Controller
    {
        /// <summary>
        /// Datacontext for PropertyListingModel
        /// </summary>
        private PropertyListingModel db = new PropertyListingModel();

        #region View properties
        // GET: Properties
        [Route("Properties/PropertyList")]
        public ActionResult Index()
        {
            var properties = db.Properties
                .Include(p => p.PropertyAddress)
                .Include(p => p.PropertyDetails)
                .Include(p=>p.PropertyImages);
            return View(properties.ToList());
        }

        // GET: Properties/Details/5
        public ActionResult Details( Guid? id)
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

        //GET: Properties/Details/QuotedPrice
        public ActionResult QuotedPrice (Property model, DateTime arrival,
                        DateTime depart,
                        bool earlyCheckIn,
                        bool lateCheckOut,
                        bool useSofaBeds)
        {
            if (model == null || arrival == null || depart == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var newBooking = new Booking();
            newBooking.BookingId = Guid.NewGuid();
            newBooking.PropertyId = model.PropertyId;
            newBooking.Active = false;

            // First check it is available
            foreach (var booking in model.Bookings)
            {
                if (arrival >= booking.Arrival && arrival < booking.Departure)
                {
                    //Do something to show there is an overlap
                }

                if (depart <= booking.Departure && depart > booking.Arrival )
                {
                    //Do something to show there is an overlap
                }
            }
            
            //Find pricing rates per night
            for (DateTime start = arrival; start < depart; start.AddDays(1))
            {
                var rate = 0.0M; //Find rate for each night
                newBooking.CompleteAmount += rate;
            }

            //Charge for early/late checking
            if (earlyCheckIn || lateCheckOut)
            {
                var earlyLateRate = 0.0M; // Get early/late rate
                    if (earlyCheckIn) { newBooking.CompleteAmount += earlyLateRate; }
                    if (lateCheckOut) { newBooking.CompleteAmount += earlyLateRate; }
            }

            //Charge for sofa beds
            if (useSofaBeds)
            {
                var sofaBedRate = 0.0M; //Get sofa bed rate
                newBooking.CompleteAmount += sofaBedRate;
            }

            // Work out deposit and invoice amount
            newBooking.DepositAmount = Math.Round(newBooking.CompleteAmount / 5,0);
            newBooking.InvoiceAmount = Math.Round(newBooking.CompleteAmount / 20,2);

            model.Bookings.Add(newBooking); 

            return View(newBooking);
        }
        #endregion

        #region Edit properties

        // GET: Properties/Create
        [Route("create-listing")]
        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            Property property = new Property();
            property.PropertyId = Guid.NewGuid();
            property.Active = true;

            return View(property);
        }

        // POST: Properties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PropertyId, Name")] Property property)
        {
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            if (ModelState.IsValid)
            {
                bool isSavedSuccessfully = false;
                await Task.Run(() =>
                    {
                        //property.PropertyDetails.DetailsId = Guid.NewGuid();
                        //property.PropertyDetails.PropertyId = property.PropertyId;

                        //property.PropertyAddress.AddressId = Guid.NewGuid();
                        //property.PropertyAddress.PropertyId = property.PropertyId;

                        db.Properties.Add(property);
                        db.SaveChanges();
                        isSavedSuccessfully = true;
                    });

                if (isSavedSuccessfully)
                {
                  return  RedirectToAction("PropertyEditor",new { id = property.PropertyId});
                }
                else
                {
                    throw new HttpException("There was a problem saving this property");
                }
            }
            throw new HttpException("There was a problem saving this property");
        }

        // GET: Properties/PropertyEditor/5
        public ActionResult PropertyEditor(Guid? id)
        {
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
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
            ViewBag.PropertyId = new SelectList(db.PropertyImages, "PropertyImageId", "PropertyImageId", property.PropertyId);

            return View(property);
        }


        // POST: Save images async from dropzone
        public async Task<ActionResult> SaveImage(Guid propertyId)
        {
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    fName = file.FileName;
                    PropertyImage newImage = new PropertyImage();
                    if (file != null && file.ContentLength > 0)
                    {
                        newImage.PropertyImageId = Guid.NewGuid();
                        newImage.PropertyId = propertyId;
                        newImage.ImageURL = await AzureStorageHelper.UploadPhotoAsync(file);
                        db.PropertyImages.Add(newImage);
                    }

                }
                db.SaveChanges(); 
            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
                AzureStorageHelper.DeletePhotos(db.PropertyImages);
            }


            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }
        
        // GET: Properties/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
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
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
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
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
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
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
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
        #endregion
    }
}
