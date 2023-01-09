using Shop_Web.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Shop_Web.Controllers
{
    public class ProfileController : Controller
    {
        ShopWebEntities objShopWebEntities = new ShopWebEntities();
        // GET: Profile

        public ActionResult Index(int? id)
        {
            return View();
        }
        }
     
    }
