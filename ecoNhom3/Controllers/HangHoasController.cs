using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ecoNhom3.Models;
using System.IO;

namespace ecoNhom3.Controllers
{
   
    public class HangHoasController : Controller
    {
        private readonly MyDbContext _context;

        public HangHoasController(MyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var hh = _context.HangHoas.ToList();
           
            return View(hh);
        }


       
        public IActionResult Details()
        {

            return View();
        }

        
        public IActionResult Create()
        {
            ViewData["MaLoai"] = new SelectList(_context.Loais, "MaLoai", "TenLoai");
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCaps, "MaNcc", "TenNcc");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHh,TenHh,MaLoai,DonGia,Hinh,MaNcc,MoTa,GiamGia")] HangHoa hangHoa, IFormFile[] myfile)
        {

            if (ModelState.IsValid)
            {
                string arr = "";

                if (myfile != null)
                {
                    foreach (var item in myfile)
                    {
                        string url = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", item.FileName);
                        using (var f = new FileStream(url, FileMode.Create))
                        {
                            item.CopyTo(f);
                        }
                        arr += item.FileName;
                    }
                    hangHoa.Hinh = arr;
                }
                _context.Add(hangHoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLoai"] = new SelectList(_context.Loais, "MaLoai", "MaLoai", hangHoa.MaLoai);
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCaps, "MaNcc", "MaNcc", hangHoa.MaNcc);
            return View(hangHoa);
        }
        
        public IActionResult Edit(int id)
        {
            HangHoa tv = _context.HangHoas.Where(p => p.MaHh == id).First();

            ViewData["MaLoai"] = new SelectList(_context.Loais, "MaLoai", "MaLoai", tv.MaLoai);
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCaps, "MaNcc", "MaNcc", tv.MaNcc);

            return View(tv);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaHh,TenHh,MaLoai,DonGia,Hinh,MaNcc,MoTa,GiamGia")] HangHoa hangHoa, IFormFile[] myfile)
        {
            if (id != hangHoa.MaHh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string arr = "";

                    if (myfile != null)
                    {
                        foreach (var item in myfile)
                        {
                            string url = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", item.FileName);
                            using (var f = new FileStream(url, FileMode.Create))
                            {
                                item.CopyTo(f);
                            }
                            arr += item.FileName;
                        }
                        hangHoa.Hinh = arr;
                    }
                    _context.Update(hangHoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HangHoaExists(hangHoa.MaHh))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLoai"] = new SelectList(_context.Loais, "MaLoai", "MaLoai", hangHoa.MaLoai);
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCaps, "MaNcc", "MaNcc", hangHoa.MaNcc);
            return View(hangHoa);
        }


    

        [HttpGet]
        public async Task<ActionResult<HangHoa>> DeleteHangHoa(int id)
        {
            var hangHoa = await _context.HangHoas.FindAsync(id);
            if (hangHoa == null)
            {
                return NotFound();
            }

            _context.HangHoas.Remove(hangHoa);
            await _context.SaveChangesAsync();
            return RedirectToAction("index", "HangHoas");
        }

        private bool HangHoaExists(int id)
        {
            return _context.HangHoas.Any(e => e.MaHh == id);
        }




        
        public IActionResult ChiTiet(int? id)
        {
            ChiTietView model = new ChiTietView();
            List<HangHoa> dshhcl = new List<HangHoa>();
            List<HangHoa> dshhcnhacc = new List<HangHoa>();
            HangHoa hh = new HangHoa();

            if (id.HasValue)
            {
                hh = _context.HangHoas.SingleOrDefault(p => p.MaHh == id);

            }
            else
            {
                hh = _context.HangHoas.SingleOrDefault(p => p.MaHh == HttpContext.Session.Get<int>("MaHh"));

            }
            dshhcl = _context.HangHoas.Where(p => p.MaLoai == hh.MaLoai).ToList();
            dshhcnhacc = _context.HangHoas.Where(p => p.MaNcc == hh.MaNcc).ToList();
            model.hh = hh;
            model.hhCungLoai = dshhcl;
            model.hhCungNcc = dshhcnhacc;

           

            return View(model);
        }

        [HttpPost]
        public IActionResult Mua(int mahh, int soluong, int id)
        {

            //if (mahh != 0)
            //{
            //    HangHoa hh = new HangHoa();
            //    hh = _context.HangHoas.Where(p => p.MaHh == mahh).First();
            //    if (HttpContext.Session.Get<int>("xacnhanmuaxong") != 1)
            //    {

            //        if (HttpContext.Session.Get<ThanhVien>("MaTv") == null)
            //        {
            //            HttpContext.Session.Set<int>("a", 3);
            //            return RedirectToAction("index", "Home");
            //        }
            //        else
            //        {
            //            ThanhVien tv = HttpContext.Session.Get<ThanhVien>("MaTv");
            //            HoaDon hd = new HoaDon();
            //            hd.MaTv = tv.MaTv;
            //            DateTime d = DateTime.Now;

            //            hd.NgayDat = d;
            //            hd.NgayGiao = d;

            //            hd.HoTen = tv.TenTv;
            //            hd.DiaChi = tv.DiaChi;
            //            hd.PhiShip = 0;
            //            hd.MaTrangThai = 1;
            //            hd.GhiChu = tv.DiaChi;
            //            _context.HoaDons.Add(hd);
            //            _context.SaveChanges();
            //            HttpContext.Session.Set("MaHd", hd.MaHd);
            //            ChiTietHd cthd = new ChiTietHd();

                       
            //            cthd.MaHh = mahh;
            //            cthd.DonGia = hh.DonGia;
            //            cthd.SoLuong = soluong;

            //            cthd.GiamGia = hh.GiamGia;

            //            _context.ChiTietHds.Add(cthd);
            //            _context.SaveChanges();
            //            HttpContext.Session.Set("xacnhanmuaxong", 1);
            //        }
            //    }
            //    else
            //    {

            //       // ChiTietHd cthd = new ChiTietHd();

            //     //   cthd.MaHd = HttpContext.Session.Get<int>("MaHd");

            //      //  cthd.MaHh = mahh;
            //     //   cthd.DonGia = hh.DonGia;
            //   //     cthd.SoLuong = soluong;

            //   //     cthd.GiamGia = hh.GiamGia;

            //    //    _context.ChiTietHds.Add(cthd);
            //    //    _context.SaveChanges();
            //    //    HttpContext.Session.Set("xacnhanmuaxong", 1);

            //    }
            //}
            //else
            //{
            //    var ct = _context.ChiTietHds.Find(id);
            //    _context.ChiTietHds.Remove(ct);
            //    _context.SaveChanges();
            //}


            //List<ChiTietHd> dscts = new List<ChiTietHd>();
            
            //double tongtien = 0;
            //foreach (var item in dscts)
            //{
            //    tongtien += item.ThanhTien;
            //}
            //ViewBag.TongTien = tongtien;
         //   return View(dscts);
            return View();




        }
        public IActionResult LapHoaDon()
        {
            ThanhToanView model = new ThanhToanView();
            HoaDon hd = new HoaDon();
            hd = _context.HoaDons.Where(p => p.MaHd == HttpContext.Session.Get<int>("MaHd")).First();
            ThanhVien tv = new ThanhVien();
            tv = _context.ThanhViens.Where(p => p.MaTv == hd.MaTv).First();

            Thongtinkh tt = new Thongtinkh();
           
            tt.TENKH = tv.TenTv;
            tt.SDT = tv.DienThoai;
           
            tt.DCNHAN = tv.DiaChi;
         
            tt.PHISHIP = hd.PhiShip;
            model.thongtinKH = tt;
            List<ChiTietHd> dscts = new List<ChiTietHd>();
           
            model.Cthd = dscts;
            double tongtien = 0;
            foreach (var item in dscts)
            {
                tongtien += item.ThanhTien;
            }
            ViewBag.TongTien = tongtien;
           
            return View(model);
        }

        public IActionResult Hoanttatdonhang(int tt, int vc)
        {
            HoaDon hd = _context.HoaDons.Where(p => p.MaHd == HttpContext.Session.Get<int>("MaHd")).First();
            hd.MaTrangThai = 3;
           
            _context.HoaDons.Update(hd);
            _context.SaveChanges();

            List<ChiTietHd> cts = new List<ChiTietHd>();
           // cts = _context.ChiTietHds.Where(p => p.MaHd == hd.MaHd).ToList();
           

            return RedirectToAction("index", "Home");
        }
        public IActionResult ViewGioHang()
        {
            List<ChiTietHd> dscts = new List<ChiTietHd>();
        //    dscts = _context.ChiTietHds.Include(x => x.HangHoa).Where(p => p.MaHd == HttpContext.Session.Get<int>("MaHd")).ToList();
            double tongtien = 0;
            foreach (var item in dscts)
            {
                tongtien += item.ThanhTien;
            }
            ViewBag.TongTien = tongtien;
            return View(dscts);
        }
     


    }
}
