using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bookish.Web.Models;
using Bookish.Web.ViewModels;

namespace Bookish.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home(LoginRegisterData loginRegisterData)
        {
            LoginRegister loginRegister = new LoginRegister();
            if (loginRegisterData.UserName !=null && loginRegisterData.Password != null && loginRegister.validate(loginRegisterData))
            {
                UserData userData = new UserData();
                userData.GetUserData(loginRegisterData);
                return View(userData);
            }
            LoginRegisterData error = new LoginRegisterData() { Error = "Invalid credentials." };
            return View("Index", error);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}