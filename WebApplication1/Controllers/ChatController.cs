using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Entities;

namespace WebApplication1.Controllers
{
    public class ChatController : Controller
    {
        private ChatContext database = ChatContext.Instance;

        public ActionResult GetNguoiDung(string tenDangNhap)
        {
            NguoiDung nguoiDung = database.NguoiDungs.Find(tenDangNhap);
            nguoiDung.HinhAnh = Url.Content(nguoiDung.HinhAnh);
            return Json(new NguoiDung
            {
                TenDangNhap = nguoiDung.TenDangNhap,
                HoTen = nguoiDung.HoTen,
                HinhAnh = nguoiDung.HinhAnh
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTinNhan(string nguoiAy)
        {
            NguoiDung nguoiDungHienTai = Session["NguoiDungDangNhap"] as NguoiDung;
            IEnumerable<object> tinNhans = database.TinNhans
                .Where(item => nguoiAy == item.NguoiNhan && nguoiDungHienTai.TenDangNhap == item.NguoiGoi || nguoiDungHienTai.TenDangNhap == item.NguoiNhan && nguoiAy == item.NguoiGoi)
                .Select(item => new { item.NguoiGoi, item.NguoiNhan, item.NoiDung, item.ThoiGianGoi })
                .AsEnumerable();

            return Json(tinNhans, JsonRequestBehavior.AllowGet);
        }
    }
}