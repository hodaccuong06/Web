using Shop_Web.Database;
using Shop_Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Shop_Web.Controllers
{
    public class HomeController : Controller
    {
        ShopWebEntities objShopWebEntities = new ShopWebEntities();
        public ActionResult Index()
        {
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListCategory = objShopWebEntities.Categories.ToList();
            objHomeModel.ListProduct = objShopWebEntities.Products.ToList();

            return View(objHomeModel);
        }

        //GET: Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            
                if (ModelState.IsValid)
                {
                    var check = objShopWebEntities.Users.FirstOrDefault(s => s.Email == _user.Email);
                    if (check == null)
                    {
                        _user.Password = GetMD5(_user.Password);
                        objShopWebEntities.Configuration.ValidateOnSaveEnabled = false;
                        objShopWebEntities.Users.Add(_user);
                        objShopWebEntities.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.error = "Email already exists";
                        return View();
                    }

                    
                }
                return View("Index");
        }
        
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(password);
                var data = objShopWebEntities.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().idUser;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Fail = "Đăng nhập thất bại";
                    return View("Dangnhap");
                }
            }
            return View();
        }


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index");
        }
        [HttpGet]
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

        //Logout

    }

    }


    