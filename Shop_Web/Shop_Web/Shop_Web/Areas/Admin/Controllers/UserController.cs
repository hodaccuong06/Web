using PagedList;
using Shop_Web.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Shop_Web.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        ShopWebEntities objShopWebEntities = new ShopWebEntities();
        // GET: Admin/User
        public ActionResult Index(string SearchString, string currentFilter, int? page)
        {
            var lstUser = new List<User>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstUser = objShopWebEntities.Users.Where(n => n.FirstName.Contains(SearchString)).ToList();
                lstUser = objShopWebEntities.Users.Where(n => n.LastName.Contains(SearchString)).ToList();
            }
            else
            {
                lstUser = objShopWebEntities.Users.ToList();
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstUser = lstUser.OrderByDescending(n => n.idUser).ToList();
            return View(lstUser.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User objUser)
        {
            objShopWebEntities.Users.Add(objUser);
            objShopWebEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Khai báo một người dùng theo mã
            User nguoidung = objShopWebEntities.Users.Find(id);
            if (nguoidung == null)
            {
                return HttpNotFound();
            }
            // trả về trang chi tiết người dùng
            return View(nguoidung);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User nguoidung = objShopWebEntities.Users.Find(id);
            if (nguoidung == null)
            {
                return HttpNotFound();
            }
            return View(nguoidung);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User nguoidung = objShopWebEntities.Users.Find(id);
            objShopWebEntities.Users.Remove(nguoidung);
            objShopWebEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objUser = objShopWebEntities.Users.Where(n => n.idUser == id).FirstOrDefault();
            return View(objUser);
        }
        [HttpPost]
        public ActionResult Edit(User objUser)
        {
            try
            {
                // Sửa sản phẩm theo mã sản phẩm
                var oldUser = objShopWebEntities.Users.Find(objUser.idUser);
                oldUser.FirstName= objUser.FirstName;
                oldUser.LastName = objUser.LastName;
                oldUser.Email= objUser.Email;
                oldUser.Password = objUser.Password;
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