using Projet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Projet.Controllers
{
    public class MyAccountController : Controller
    {
        // GET: MyAccount
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login l, string ReturnUrl="")
        {
            //using (DBEntities db = new DBEntities())
            //{
            //    var user = db.Users.Where(a => a.Username.Equals(l.Username) && a.Password.Equals(l.Password)).FirstOrDefault();
            //    if(user != null)
            //    {
            //        FormsAuthentication.SetAuthCookie(user.Username, l.RememberMe);
            //        if(Url.IsLocalUrl(ReturnUrl))
            //        {
            //            return Redirect(ReturnUrl);
            //        }
            //        else
            //        {
            //            return RedirectToAction("MyProfile", "Home");
            //        }

            //    }
            //}

            // 2ème code authentification

            //if (ModelState.IsValid)
            //{
            //    var isValidUser = Membership.ValidateUser(l.Username, l.Password);
            //    if(isValidUser)
            //    {
            //        FormsAuthentication.SetAuthCookie(l.Username, l.RememberMe);
            //        if(Url.IsLocalUrl(ReturnUrl))
            //        {
            //            return Redirect(ReturnUrl);
            //        }
            //        else
            //        {
            //            return RedirectToAction("Index","Home");
            //        }
            //    }

            //}

            if (ModelState.IsValid)
            {
                bool isValidUser = Membership.ValidateUser(l.Username, l.Password);
                if (isValidUser)
                {
                    Users user = null;
                    using (DBEntities db = new DBEntities())
                    {
                        user = db.Users.Where(a => a.Username.Equals(l.Username)).FirstOrDefault();
                    }

                    if(user != null)
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        string data = js.Serialize(user);
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.Username, DateTime.Now, DateTime.Now.AddMinutes(30), l.RememberMe, data);
                        string encToken = FormsAuthentication.Encrypt(ticket);
                        HttpCookie authoCookies = new HttpCookie(FormsAuthentication.FormsCookieName, encToken);
                        Response.Cookies.Add(authoCookies);
                        return Redirect(ReturnUrl);
                    }
                }
                   
            }


            ModelState.Remove("Password");
                return View();
        }


        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}