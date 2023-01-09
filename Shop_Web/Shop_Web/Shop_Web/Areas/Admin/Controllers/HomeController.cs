using Shop_Web.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Web.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        ShopWebEntities objShopWebEntities = new ShopWebEntities();
        // GET: Admin/Home
        public ActionResult Index()
        {
            if (Session["idUser"] != null)
            {
                var lstProduct = objShopWebEntities.Products.ToList();
                return View(lstProduct);
            }
            else
            {
                return View("Login");
            }

        }
    }
}
