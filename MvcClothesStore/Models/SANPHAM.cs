using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcClothesStore.Models;

namespace MvcClothesStore.Models
{
    public class SANPHAM
    {
        dbQLClothesDataContext db = new dbQLClothesDataContext();
        public string sMaSP { set; get; }
        public string sTenSP { set; get; }
        public string sAnhSP { set; get; }
        public DateTime dNgayDang { set; get; }
        public string sid_con { set; get; }
        public float fGiaHienTai { set; get; }
    }
}