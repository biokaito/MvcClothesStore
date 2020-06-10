using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcClothesStore.Controllers
{
    public class ClothesStoreController : Controller
    {
        // GET: ClothesStore
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }
    }
}