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
    public class BranchController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Branch/

        public ActionResult Index(string name, string address, string phone)
        {
            return View(GetFilteredBranches(name, address, phone));
        }

        //Partial view
        public ActionResult SearchPartial(string name, string address, string phone)
        {
            return View("~/Views/Shared/_BranchesPartial.cshtml", GetFilteredBranches(name, address, phone));
        }

        public IEnumerable<Branch> GetFilteredBranches(string name, string address, string phone)
        {
            //LINQ - select from DB
            var branches = from b in db.Branches select b;

            if (!String.IsNullOrEmpty(address))
            {
                address = address.ToLower();
                branches = branches.Where(b => b.Address.ToLower().Contains(address));
            }

            if (!String.IsNullOrEmpty(phone))
            {
                phone = phone.ToLower();
                branches = branches.Where(b => b.Phone.ToLower().Contains(phone));
            }

            if (!String.IsNullOrEmpty(name))
            {
                name = name.ToLower();
                branches = branches.Where(p => p.Name.ToLower().Contains(name));
            }

            return branches.ToList();
        }

        //
        // GET: /Branch/Details/5

        public ActionResult Details(int id = 0)
        {
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        //
        // GET: /Branch/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Branch/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Branches.Add(branch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(branch);
        }

        //
        // GET: /Branch/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id = 0)
        {
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        //
        // POST: /Branch/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(branch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(branch);
        }

        //
        // GET: /Branch/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id = 0)
        {
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        //
        // POST: /Branch/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Branch branch = db.Branches.Find(id);
            db.Branches.Remove(branch);
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