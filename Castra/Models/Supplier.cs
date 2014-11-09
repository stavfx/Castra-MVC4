using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Castra.Models
{
    public class Supplier
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}