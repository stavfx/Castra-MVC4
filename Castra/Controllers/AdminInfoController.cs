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
    public class AdminInfoController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /AdminInfo/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SupplierCount()
        {
            return View(db.Suppliers.ToList());
        }

        public ActionResult SupplierProducts()
        {
            var suppliers = db.Suppliers;
            var result = new List<SupplierProducts>();
            foreach (var sup in suppliers)
            {
                var item = new SupplierProducts();
                item.Name = sup.Name;
                item.ProductList = "";
                if (sup.Products != null)
                {
                    foreach (var product in sup.Products)
                    {
                        item.ProductList += product.Name + ", ";
                    }
                }
                result.Add(item);
            }
            return View(result);
        }

        public ActionResult ProductsByType()
        {
            var products = from p in db.Products
                           group p by p.Type into g
                           select new ProductsByType { Type = g.FirstOrDefault().Type, Count = g.Count() };

            return View(products);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }

    public class ProductsByType
    {
        public string Type { get; set; }
        public int Count { get; set; }
    }
    public class SupplierProducts
    {
        public string Name { get; set; }
        public string ProductList { get; set; }
    }
}