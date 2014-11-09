using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Castra.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}