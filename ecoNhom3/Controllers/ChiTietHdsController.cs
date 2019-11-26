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


namespace ecoNhom3.Controllers
{
    public class ChiTietHdsController : Controller
    {
        private readonly MyDbContext _context;

        public ChiTietHdsController(MyDbContext context)
        {
            _context = context;
        }
        // GET: ChiTietHds
        public ActionResult Index()
        {
            return View();
        }

        // GET: ChiTietHds/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ChiTietHds/Create
        public ActionResult Create()
        {
            ViewData["MaHh"] = new SelectList(_context.HangHoas, "MaHh", "MaHh");
            ViewData["MaHd"] = new SelectList(_context.HoaDons, "MaHd", "MaHd");
            return View();
        }

        // POST: ChiTietHds/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
    
        public async Task<IActionResult> Create([Bind("MaCTHD,MaHd,MaHh,DonGia,SoLuong,GiamGia")] ChiTietHd chiTietHd)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chiTietHd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaHh"] = new SelectList(_context.HangHoas, "MaHh", "MaHh", chiTietHd.MaHh);
            ViewData["MaHd"] = new SelectList(_context.HoaDons, "MaHd", "MaHd", chiTietHd.MaHd);

            return View(chiTietHd);
        }
        // GET: ChiTietHds/Edit/5
        public ActionResult Edit(int id)
        {
            ChiTietHd ct = _context.ChiTietHds.Where(p => p.MaCTHD == id).First();
            ViewData["MaHh"] = new SelectList(_context.HangHoas, "MaHh", "MaHh", ct.MaHh);
            ViewData["MaHd"] = new SelectList(_context.HoaDons, "MaHd", "MaHd", ct.MaHd);

            return View(ct);
        }

        // POST: ChiTietHds/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaCTHD,MaHd,MaHh,DonGia,SoLuong,GiamGia")] ChiTietHd chiTietHd)
        {
            if (id != chiTietHd.MaCTHD)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiTietHd);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoaDonExists(chiTietHd.MaHd))
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
            ViewData["MaHh"] = new SelectList(_context.HangHoas, "MaHh", "MaHh", chiTietHd.MaHh);
            ViewData["MaHd"] = new SelectList(_context.HoaDons, "MaHd", "MaHd", chiTietHd.MaHd);

            return View(chiTietHd);
        }

        private bool HoaDonExists(int id)
        {
            return _context.ChiTietHds.Any(e => e.MaCTHD == id);
        }

        public async Task<ActionResult<ChiTietHd>> Delete(int id)
        {
            var chiTietHd = await _context.ChiTietHds.FindAsync(id);
            if (chiTietHd == null)
            {
                return NotFound(); 
            }

            _context.ChiTietHds.Remove(chiTietHd);
            await _context.SaveChangesAsync();

            return chiTietHd;
        }

    }
}