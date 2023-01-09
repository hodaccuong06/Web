using Shop_Web.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Web.Models
{
    public class HomeModel
    {
        public List<Product> ListProduct { get; set; }

        public List<Category> ListCategory { get; set; }
    }
}