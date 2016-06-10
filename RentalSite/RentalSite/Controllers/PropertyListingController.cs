using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentalSite.Controllers
{
    public class PropertyListingController : Controller
    {
        // GET: PropertyListing
        public ActionResult Index()
        {
            return View();
        }

        // GET: PropertyListing/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PropertyListing/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PropertyListing/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PropertyListing/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PropertyListing/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PropertyListing/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PropertyListing/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
