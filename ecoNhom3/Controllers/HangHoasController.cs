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
            HangHoa hh = _context.HangHoas.Where(p => p.MaHh == id).First();

            ViewData["MaLoai"] = new SelectList(_context.Loais, "MaLoai", "MaLoai", hh.MaLoai);
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCaps, "MaNcc", "MaNcc", hh.MaNcc);

            return View(hh);
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

    


    }
}
