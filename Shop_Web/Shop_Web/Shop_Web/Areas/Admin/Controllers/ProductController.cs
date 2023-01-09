using Shop_Web.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop_Web.Models;
using PagedList;

namespace Shop_Web.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        ShopWebEntities objShopWebEntities = new ShopWebEntities();
        // GET: Admin/Product
        public ActionResult Index(string SearchString, string currentFilter,int ?page)
        {
            var lstProduct = new List<Product>();   
            if(SearchString!= null)
            {
                page= 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if(!string.IsNullOrEmpty(SearchString))
            {
                lstProduct =objShopWebEntities.Products.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstProduct = objShopWebEntities.Products.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstProduct=lstProduct.OrderByDescending(n=>n.Id).ToList();
            return View(lstProduct.ToPagedList(pageNumber,pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Product objProduct)
        {
            objShopWebEntities.Products.Add(objProduct);
            objShopWebEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objProduct = objShopWebEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = objShopWebEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Delete(Product objPro)
        {
            var objProduct = objShopWebEntities.Products.Where(n => n.Id == objPro.Id).FirstOrDefault();

            objShopWebEntities.Products.Remove(objProduct);
            objShopWebEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var objProduct = objShopWebEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Product objProduct)
        {
            try
            {
                // Sửa sản phẩm theo mã sản phẩm
                var oldItem = objShopWebEntities.Products.Find(objProduct.Id);
                oldItem.Name = objProduct.Name;
                oldItem.Avartar = objProduct.Avartar   ;
                oldItem.CategoryId = objProduct.CategoryId;
                oldItem.ShortDes = objProduct.ShortDes;
                oldItem.FullDescription = objProduct.FullDescription;
                oldItem.Price = objProduct.Price;
                oldItem.PriceDiscount = objProduct.PriceDiscount;
                oldItem.TypeId = objProduct.TypeId;
                oldItem.Slug = objProduct.Slug;
                oldItem.BrandId = objProduct.BrandId;
                // lưu lại
                objShopWebEntities.SaveChanges();
                // xong chuyển qua index
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


    }
}