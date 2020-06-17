using MvcClothesStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcClothesStore.Controllers
{     /**/
    public class UserController : Controller
    {
        // GET: User
        dbQLClothesDataContext db = new dbQLClothesDataContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }
        public ActionResult SignIn(FormCollection collection)
        {
            var tendn = collection["username"];
            var matkhau = collection["pwd"];
            NguoiDung kh = db.NguoiDungs.SingleOrDefault(n => n.TenUser == tendn && n.MatKhau == matkhau);
            if(kh != null)
            {
                ViewBag.Thongbao = " CHúc mừng bạn đã đăng nhập thành công";
                Session["Taikhoan"] = kh;
            }
            else
            {
                ViewBag.Thongbao = "Ten dang nhap hoặc mật khẩu không đúng!";
            }
            return View();
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(FormCollection collection, NguoiDung kh)
        {
            var hoten = collection["Hoten"];
            var tendn = collection["username"];
            var matkhau = collection["pwd"];
            var diachi = collection["Diachi"];
            var email = collection["Email"];
            var dienthoai = collection["sdt"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["Ngaysinh"]);
            //Gán giá trị cho đối tượng được khởi tạo
                kh.HoVaTen = hoten;
                kh.TenUser = tendn;
                kh.MatKhau = matkhau;
                kh.diachi = diachi;
                kh.email = email;
                kh.phone = int.Parse(dienthoai);
                kh.NgaySinh = DateTime.Parse(ngaysinh);

                db.NguoiDungs.InsertOnSubmit(kh);
                db.SubmitChanges();
            return RedirectToAction("SignIn");
        }
    }
}
////