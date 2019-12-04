using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ecoNhom3.Models;


namespace ecoNhom3.Controllers
{
    public class HomeController : Controller
    {
        private MyDbContext context;
        public HomeController()
        {
            context = new MyDbContext();
        }
        
        public IActionResult Index()
        {
            

            return View();
        }
        [Route("thong-tin-cua-hang")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }
        [Route("lien-he")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
        [HttpPost]
        [Route("ket-qua-tim-kiem")]
        public IActionResult SearchSP()
        {
            
            string key = Request.Form["keysearch"];
            key = Helper.FriendlyUrlHelper.Search(key);
            var sp = from s in context.HangHoas
                       join l in context.Loais
                       on s.MaLoai equals l.MaLoai
                       join n in context.NhaCungCaps
                       on s.MaNcc equals n.MaNcc
                       where s.TenHhTk.Contains(key) || l.TenLoaiTk.Contains(key) || n.TenNccTk.Contains(key)
                       select new HangHoa
                       {
                           MaHh = s.MaHh,
                           TenHh = s.TenHh,
                           MaLoai = l.MaLoai,
                           MaNcc = n.MaNcc,
                           DonGia = s.DonGia,
                           GiamGia = s.GiamGia,
                           Hinh = s.Hinh,
                           MoTa = s.MoTa
                       };
           
            return View(sp);

           
        }


        [Route("nuoc-ngot")]
        public IActionResult ShowNuocNgot(string title)
        {
          
            return View();
        }
        [Route("nuoc-suoi")]
        public IActionResult ShowNuocSuoi()
        {

            return View();
        }
        [Route("ruou")]
        public IActionResult ShowRuou()
        {

            return View();

        }
        [Route("bia")]
        public IActionResult ShowBia()
        {

            return View();
        }
      

    }
}
