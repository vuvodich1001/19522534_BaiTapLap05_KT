using BaiTapLab05_19522534.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;

namespace BaiTapLab05_19522534.Controllers
{
    public class TrieuChungController : Controller
    {
        public IActionResult LietkeCongNhanTheoTrieuChung()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ListByTime(int soLan)
        {
            DataContext context = HttpContext.RequestServices.GetService(typeof(BaiTapLab05_19522534.Models.DataContext)) as DataContext;

            return View(context.ListByTime(soLan));
        }
    }
}
