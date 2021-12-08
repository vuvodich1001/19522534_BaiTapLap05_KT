using BaiTapLab05_19522534.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaiTapLab05_19522534.Controllers
{
    public class DiemCachLyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string ThemDiemCachLy(DiemCachLyModel diemCachLy)
        {
            DataContext context = HttpContext.RequestServices.GetService(typeof(BaiTapLab05_19522534.Models.DataContext)) as DataContext;
            int count = context.InsertDiemCachLy(diemCachLy);
            if(count == 1)
            {
                return "Them thanh cong";
            }
            return "Them that bai";
        }

        public IActionResult LietKeDiemCachLy()
        {
            DataContext context = HttpContext.RequestServices.GetService(typeof(BaiTapLab05_19522534.Models.DataContext)) as DataContext;

            return View(context.LietKeDiemCachLy());
        }

        public IActionResult ListCongNhanTheoDiemCachLy(string maDiemCachLy)
        {
            DataContext context = HttpContext.RequestServices.GetService(typeof(BaiTapLab05_19522534.Models.DataContext)) as DataContext;
            return View(context.ListCongNhanTheoDiemCachLy(maDiemCachLy));
        }
        [HttpGet("{controller}/{action}/{maCongNhan}")]
        public IActionResult XoaCongNhan(string maCongNhan)
        {
            DataContext context = HttpContext.RequestServices.GetService(typeof(BaiTapLab05_19522534.Models.DataContext)) as DataContext;
            context.XoaCongNhan(maCongNhan);
            return RedirectToAction("LietKeDiemCachLy", "DiemCachLy");
        }

        [HttpGet("{controller}/{action}/{maCongNhan}")]
        public IActionResult ChiTietCongNhan(string maCongNhan)
        {
            DataContext context = HttpContext.RequestServices.GetService(typeof(BaiTapLab05_19522534.Models.DataContext)) as DataContext;
            return View(context.ChiTietCongNhan(maCongNhan));
        }
    }
}
