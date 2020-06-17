using MvcClothesStore.Models;
using System.Web.Mvc;

namespace MvcClothesStore.Controllers
{
    public class AddProductController : Controller
    {
        // GET: AddProduct
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }
       [HttpPost]
        public ActionResult AddProduct(SanPham a)
        {
            dbQLClothesDataContext ab = new dbQLClothesDataContext();
            ab.SanPhams.InsertOnSubmit(a);
            ab.SubmitChanges();
            return View();
        }////
    }
}