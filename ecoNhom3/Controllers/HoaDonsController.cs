using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ecoNhom3.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;
using System.IO;
using Microsoft.AspNetCore.Authentication.Cookies;
using static System.Collections.Specialized.BitVector32;

namespace ecoNhom3.Controllers
{
    public class HoaDonsController : Controller
    {
        private readonly MyDbContext _context;

        public HoaDonsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: HoaDons
        public ActionResult Index()
        {
            if (HttpContext.Session.GetInt32("MaTv") == 1)
            {
                var hdon = _context.HoaDons.ToList();
                return View(hdon);
            }

            
            var hd = _context.HoaDons.Where(p => p.MaTv == HttpContext.Session.GetInt32("MaTv"));
            return View(hd);

        }

        // GET: HoaDons/Details/5
        public ActionResult Details(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            HoaDon hd = _context.HoaDons.Where(p => p.MaHd == id).First();
            if (hd == null) return NotFound();

            return View(hd);
        }

        // GET: HoaDons/Create
        public ActionResult Create()
        {
            
            ViewData["MaTv"] = new SelectList(_context.ThanhViens, "MaTv", "MaTv");
            ViewData["MaTt"] = new SelectList(_context.TrangThais, "MaTt", "MaTt");

            return View();
        }

        // POST: HoaDons/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<HoaDon>> Create([Bind("MaHd,MaTv,NgayDat,NgayGiao,HoTen,DiaChi,PhiShip,SdtNguoiNhan,MaTt,GhiChu,TongTienHang")] HoaDon hoaDon)
        {
            var cart = Models.SessionExtensions.Get<List<Item>>(HttpContext.Session, "cart");
            if (ModelState.IsValid)
            {
                
                DateTime d = DateTime.Now;
                
                hoaDon.NgayDat = d;
                hoaDon.NgayGiao = d;
                hoaDon.PhiShip = 20000;
                hoaDon.MaTt = 1;
                hoaDon.TongTienHang = 20000;
              
                _context.Add(hoaDon);
                await _context.SaveChangesAsync();
                
            }

            ViewData["MaTv"] = new SelectList(_context.ThanhViens, "MaTv", "MaTv", hoaDon.MaTv);

            ViewData["MaTt"] = new SelectList(_context.TrangThais, "MaTt", "MaTt", hoaDon.MaTt);


            return RedirectToAction("Details", "HoaDons", new { id = hoaDon.MaHd });
        }

        // GET: HoaDons/Edit/5
        public ActionResult Edit(int id)
        {
            HoaDon hd = _context.HoaDons.Where(p => p.MaHd == id).First();
            ViewData["MaTv"] = new SelectList(_context.ThanhViens, "MaTv", "MaTv", hd.MaTv);
            ViewData["MaTt"] = new SelectList(_context.TrangThais, "MaTt", "MaTt", hd.MaTt);
                      
            return View(hd);
        }

        // POST: HoaDons/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int id, [Bind("MaHd,NgayDat,NgayGiao,HoTen,DiaChi,PhiShip,MaTt,GhiChu,SdtNguoiNhan,TongTienHang")] HoaDon hoaDon)
        {
            if (id != hoaDon.MaHd)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoaDon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoaDonExists(hoaDon.MaHd))
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
            ViewData["MaTv"] = new SelectList(_context.ThanhViens, "MaTv", "MaTv", hoaDon.MaTv);

            ViewData["MaTt"] = new SelectList(_context.TrangThais, "MaTt", "MaTt", hoaDon.MaTt);

            return View(hoaDon);
        }

        private bool HoaDonExists(int id)
        {
            return _context.HoaDons.Any(e => e.MaHd == id);
        }


       
        public async Task<ActionResult<HoaDon>> Delete(int id)
        {
            var hoaDon = await _context.HoaDons.FindAsync(id); 
            if (hoaDon == null)
            {
                return NotFound();
            }

            _context.HoaDons.Remove(hoaDon);
            await _context.SaveChangesAsync();

            return hoaDon;
        }

    }
}