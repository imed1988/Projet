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
        public ActionResult Login(Login l, string ReturnUrl="" )
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
            //    if (isValidUser)
            //    {
            //        FormsAuthentication.SetAuthCookie(l.Username, l.RememberMe);
            //        if (Url.IsLocalUrl(ReturnUrl))
            //        {
            //            return Redirect(ReturnUrl);
            //        }
            //        else
            //        {
            //            return RedirectToAction("Index", "Home");
            //        }
            //    }

            //}



            // Lets first check if the Model is valid or not
            if (ModelState.IsValid)
            {
                using (DBEntities db = new DBEntities())
                {
                    string username = l.Username;
                    string password = l.Password;

                    // Now if our password was enctypted or hashed we would have done the
                    // same operation on the user entered password here, But for now
                    // since the password is in plain text lets just authenticate directly

                    var isValidUser = Membership.ValidateUser(username, password);

                    // User found in the database
                    if (isValidUser)
                    {

                        FormsAuthentication.SetAuthCookie(username, false);
                        if (Url.IsLocalUrl(ReturnUrl) && ReturnUrl.Length > 1 && ReturnUrl.StartsWith("/")
                            && !ReturnUrl.StartsWith("//") && !ReturnUrl.StartsWith("/\\"))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(l);
        }


        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}