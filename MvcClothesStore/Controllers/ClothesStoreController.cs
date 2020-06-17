using MvcClothesStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace MvcClothesStore.Controllers
{
    public class ClothesStoreController : Controller
    {
        // GET: ClothesStore
        dbQLClothesDataContext db = new dbQLClothesDataContext();
        private List<SanPham> Getproducts()
        {
            return db.SanPhams.ToList();
        }

        private List<SanPham> Getnewproducts(int count)
        {
            return db.SanPhams.OrderByDescending(a => a.NgayDang).Take(count).ToList();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Home(int ? page)
        {
            int pageSize = 5;
            int pageNum = (page ?? 1);
            var item = Getproducts();
            return View(item.ToPagedList(pageNum,pageSize));
        }
        public ActionResult NewProducts()
        {
            var item = Getnewproducts(5);
            return View(item);
        }
        public ActionResult hdsw()
        {
            var item = from i in db.SanPhams where i.id_con==2 select i;
            return View(item);
        }
        public ActionResult backpack()
        {
            var item = from i in db.SanPhams where i.id_con == 3 select i;
            return View(item);
        }
        public ActionResult Shortpant()
        {
            var item = from i in db.SanPhams where i.id_con == 4 select i;
            return View(item);
        }
        public ActionResult Jacket()
        {
            var item = from i in db.SanPhams where i.id_con == 5 select i;
            return View(item);
        }
        public ActionResult Details(string id)
        {
            var item = from i in db.SanPhams where i.MaSP == id select i;
            return View(item.Single());
        }
    }
}