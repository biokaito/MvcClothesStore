using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcClothesStore.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
    }
}