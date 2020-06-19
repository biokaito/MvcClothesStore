using MvcClothesStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace MvcClothesStore.Controllers
{
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
                Session["Taikhoan"] = kh;
                return RedirectToAction("Home", "ClothesStore");
            }
            else
            {
                ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng!";
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session["Taikhoan"] = null;
            return RedirectToAction("Home","ClothesStore");
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
        //public class UserProfileEdit
        //{
        //    [Required]
        //    [Display(Name = "First name")]
        //    public string username { get; set; }

        //    [Required]
        //    [Display(Name = "Last name")]
        //    public string Password { get; set; }

        //    [Required]
        //    [Display(Name = "Email")]
        //    [DataType(DataType.EmailAddress)]
        //    public string Email { get; set; }
        //}
        [HttpGet]
        public ActionResult UpdateAccount(int id)
        {
            NguoiDung user = db.NguoiDungs.SingleOrDefault(n => n.id_user == id);
            if (user == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult UpdateAccount(NguoiDung nd)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var editUser = db.NguoiDungs.SingleOrDefault(u => u.id_user == nd.id_user);
                    editUser.TenUser = nd.TenUser; 
                    editUser.email = editUser.email;
                    editUser.MatKhau = nd.MatKhau;
                    editUser.diachi = nd.diachi;
                    editUser.phone = nd.phone;
                    editUser.NgaySinh = nd.NgaySinh;
                    UpdateModel(editUser);
                    db.SubmitChanges();
                    ViewBag.Thongbao = "Update successfully";
                    return View();
                }
                catch(Exception ex)
                {
                    return HttpNotFound();
                }
            }
            else
            {
                ViewBag.Thongbao = "Error";
                return View();
            }
        
        }
    }
}