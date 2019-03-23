using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Entities;

namespace ChatApp.Controllers
{
    public class PostController : Controller
    {
        ChatContext database = ChatContext.Instance;
        // GET: Post
        public ActionResult Index()
        {            
            return View(database.BaiDangs);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PostQuestion(string title, string editor1)
        {
            BaiDang baiDang = new BaiDang
            {
                TieuDe = title,
                NoiDung = editor1,
                TenDangNhap = (Session["NguoiDungDangNhap"] as NguoiDung).TenDangNhap,
                ThoiGian = DateTime.Now
            };

            database.BaiDangs.Add(baiDang);

            database.SaveChanges();            
            
            return View("Index", database.BaiDangs);
        }

        public ActionResult GetBaiDang(int id)
        {
            BaiDang baiDang = database.BaiDangs.Find(id);
            ViewBag.GetBaiDang = baiDang;
            var x = database.BinhLuans.Where(i => i.IDBaiDang == id).ToList();
            ViewBag.GetBinhLuan = x;
            return View("Index", database.BaiDangs);
        }
        
        public void PostBinhLuan(int idBaiPost, string noiDung)
        {
            BinhLuan binhLuan = new BinhLuan
            {
                IDBaiDang = idBaiPost,
                TenNguoiBinhLuan = "Tao",
                //(Session["NguoiDungDangNhap"] as NguoiDung).TenDangNhap,
                NoiDung = noiDung,
                ThoiGian = DateTime.Now,
            };
            database.BinhLuans.Add(binhLuan);
            database.SaveChanges();
        }
    }
}