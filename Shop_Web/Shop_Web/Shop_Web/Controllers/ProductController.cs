using Shop_Web.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Web.Controllers
{
    public class ProductController : Controller
    {
        ShopWebEntities objShopWebEntities = new ShopWebEntities();
        // GET: Product
        public ActionResult Detail(int Id)
        {
            var objProduct = objShopWebEntities.Products.Where(n=>n.Id == Id).FirstOrDefault();

            return View(objProduct);
        }
    }
}