using Projet.Models;
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

             
    

        [Authorize (Roles ="Administrateur")]
        public ActionResult AdminIndex()
        {
            return View("AdminIndex", "Admin_Layout");
        }

        [Authorize(Roles = "Secretaire de Production")]
        public ActionResult SecretaireProdIndex()
        {
            return View();
        }


       


    }
}