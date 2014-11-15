using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castra.Models;
using Castra.Filters;

namespace Castra.Controllers
{
    [InitializeSimpleMembership]
    [Authorize(Roles = "Admin")]
    public class SupplierController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Supplier/
        public ActionResult Index(string name, string country)
        {
            return View(GetFilteredSuppliers(name, country));
        }

        public ActionResult SearchPartial(string name, string country)
        {
            return View("~/Views/Shared/_SuppliersPartial.cshtml", GetFilteredSuppliers(name, country));
        }

        public IEnumerable<Supplier> GetFilteredSuppliers(string name, string country)
        {
            var suppliers = from s in db.Suppliers select s;

            if (!String.IsNullOrEmpty(country))
            {
                country = country.ToLower();
                suppliers = suppliers.Where(s => s.Country.ToLower().Contains(country));
            }

            if (!String.IsNullOrEmpty(name))
            {
                name = name.ToLower();
                suppliers = suppliers.Where(p => p.Name.ToLower().Contains(name));
            }

            return suppliers.ToList();
        }
        //
        // GET: /Supplier/Details/5

        public ActionResult Details(int id = 0)
        {
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        //
        // GET: /Supplier/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Supplier/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Suppliers.Add(supplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(supplier);
        }

        //
        // GET: /Supplier/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        //
        // POST: /Supplier/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        //
        // GET: /Supplier/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        //
        // POST: /Supplier/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplier supplier = db.Suppliers.Find(id);
            db.Suppliers.Remove(supplier);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}