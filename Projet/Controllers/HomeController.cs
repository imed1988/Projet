using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projet.Controllers
{
    public class HomeController : Controller
    {

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

   
        public ActionResult MyProfile()
        {
            return View();
        }

             
    

        [Authorize (Roles ="Admin")]
        public ActionResult AdminIndex()
        {
            return View("AdminIndex", "Admin_Layout");
        }

        [Authorize(Roles = "User")]
        public ActionResult UserIndex()
        {
            return View();
        }


    }
}