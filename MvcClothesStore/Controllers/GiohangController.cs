using MvcClothesStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using FormCollection = System.Web.Mvc.FormCollection;

namespace MvcClothesStore.Controllers
{
    public class GiohangController : Controller
    {
        // GET: Giohang
        dbQLClothesDataContext db = new dbQLClothesDataContext();
        public List<Giohang> Laygiohang()
        {
            List<Giohang> giohangs = Session["giohang"] as List<Giohang>;
            if (giohangs == null)
            {
                giohangs = new List<Giohang>();
                Session["giohang"] = giohangs;
            }
            return giohangs;
        }
        public ActionResult Index()
        {
            List<Giohang> giohangs = Session["giohang"] as List<Giohang>;
            return View(giohangs);
        }
        public RedirectToRouteResult ThemVaoGio(string iMaSP)
        {
            if (Session["giohang"] == null)
            {
                Session["giohang"] = new List<Giohang>();
            }

            List<Giohang> giohang = Session["giohang"] as List<Giohang>;


            if (giohang.FirstOrDefault(m => m.iMaSP == iMaSP) == null)
            {
                SanPham sp = db.SanPhams.FirstOrDefault(n => n.MaSP == iMaSP);

                Giohang newItem = new Giohang()
                {
                    iMaSP = iMaSP,
                    iTenSP = sp.TenSP,
                    iSL = 1,
                    iAnhSP = sp.AnhSP,
                    iGiaHienTai = (float)sp.GiaHienTai

                };
                ViewBag.TongTien = TongTien();
                giohang.Add(newItem);
            }
            else
            {
                Giohang giohang1 = giohang.FirstOrDefault(m => m.iMaSP == iMaSP);
                giohang1.iSL++;
            }
            return RedirectToAction("Index", "Giohang", new { id = iMaSP });
        }
        public RedirectToRouteResult XoaKhoiGio(string iMaSP)
        {
            List<Giohang> giohang = Session["giohang"] as List<Giohang>;
            Giohang itemXoa = giohang.FirstOrDefault(m => m.iMaSP == iMaSP);
            if (itemXoa != null)
            {
                giohang.Remove(itemXoa);
            }
            return RedirectToAction("Index");
        }
        public int a()
        {
            Random r = new Random();
            int n = r.Next(15, 50);
            return n;
        }
        public double TongTien()
        {
            double iTongTien = 0;
            List<Giohang> giohangs = Session["giohang"] as List<Giohang>;
            if (giohangs != null)
            {
                iTongTien = giohangs.Sum(n => n.iThanhTien);
            }
            return iTongTien;
        }
        public ActionResult DatHang()
        {
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                DialogResult dialogResult = MessageBox.Show("Bạn đã có tài khoản chưa?", "Thông báo", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    return RedirectToAction("SignIn", "User");
                }
                else if (dialogResult == DialogResult.No)
                {
                    return RedirectToAction("SignUp", "User");
                }
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Home", "ClothesStore");
            }
            List<Giohang> giohangs = Laygiohang();
            return View(giohangs);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            var hoten = collection["GhiChu"];
            DonHang donHang = new DonHang();
            NguoiDung nguoiDung = (NguoiDung)Session["Taikhoan"];
            List<Giohang> giohangs = Laygiohang();
            donHang.ID = a();
            donHang.TenKH = nguoiDung.HoVaTen;
            donHang.DiaChi = nguoiDung.diachi;
            donHang.Email = nguoiDung.email;
            donHang.Ghichu = hoten;
            donHang.TrangThai = false;
            donHang.TongTien = TongTien();
            db.DonHangs.InsertOnSubmit(donHang);
            db.SubmitChanges();
            Session["giohang"] = null;
            return RedirectToAction("Xacnhandonhang", "Giohang");
        }
        public ActionResult Xacnhandonhang()
        {
            return View();
        }
    }
}