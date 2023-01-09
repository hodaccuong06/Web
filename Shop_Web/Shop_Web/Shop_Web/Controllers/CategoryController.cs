using Shop_Web.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Web.Controllers
{
    public class CategoryController : Controller
    {
        ShopWebEntities objShopWebEntities = new ShopWebEntities();
        // GET: Category
        public ActionResult Category()
        {
            var lstCategory = objShopWebEntities.Categories.ToList();
            return View(lstCategory);
        }
        public ActionResult ProductCategory(int Id) 
        {
            var listProduct = objShopWebEntities.Products.Where(n=>n.CategoryId == Id).ToList();
            return View(listProduct);
        }
    }
}