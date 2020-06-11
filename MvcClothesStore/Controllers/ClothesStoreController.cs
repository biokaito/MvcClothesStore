using MvcClothesStore.Models;
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
        dbQLClothesDataContext db = new dbQLClothesDataContext();
        private List<SanPham> Getproducts(int count)
        {
            return db.SanPhams.Take(count).ToList();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {
            var item = Getproducts(9);
            return View(item);
        }
    }
}