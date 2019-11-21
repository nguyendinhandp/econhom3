using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ecoNhom3.Models;

namespace ecoNhom3.Controllers
{
   
    public class HoaDonsController : Controller
    {
        private readonly MyDbContext _context;

        public HoaDonsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/HoaDons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HoaDon>>> Index()
        {
            var myDbContext = _context.HoaDons.Include(h => h.ThanhVien).Include(h => h.TrangThai);
            return await _context.HoaDons.ToListAsync();
        }

        // GET: api/HoaDons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HoaDon>> Details(int id)
        {
            var hoaDon = await _context.HoaDons
               .Include(h => h.ThanhVien)
               .Include(h => h.TrangThai)
               .FirstOrDefaultAsync(m => m.MaHd == id);

            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        // POST: HoaDons/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHd,MaTv,NgayDat,NgayGiao,HoTen,DiaChi,PhiShip,MaTt,GhiChu")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hoaDon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaTv"] = new SelectList(_context.ThanhViens, "MaTv", "MaTv", hoaDon.MaTv);
          
            ViewData["MaTt"] = new SelectList(_context.TrangThais, "MaTt", "MaTt", hoaDon.MaTrangThai);
           
            return View(hoaDon);
        }
        // PUT: api/HoaDons/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaHd,MaTv,NgayDat,NgayGiao,HoTen,DiaChi,PhiShip,MaTt,GhiChu")] HoaDon hoaDon)
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
        
            ViewData["MaTt"] = new SelectList(_context.TrangThais, "MaTt", "MaTt", hoaDon.MaTrangThai);
     
            return View(hoaDon);
        }
        // POST: api/HoaDons
        [HttpPost]
        public async Task<ActionResult<HoaDon>> PostHoaDon(HoaDon hoaDon)
        {
            _context.HoaDons.Add(hoaDon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHoaDon", new { id = hoaDon.MaHd }, hoaDon);
        }

        // DELETE: api/HoaDons/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HoaDon>> DeleteHoaDon(int id)
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

        private bool HoaDonExists(int id)
        {
            return _context.HoaDons.Any(e => e.MaHd == id);
        }


        public IActionResult chitietdonhang(int? id)
        {
            HoaDonView model = new HoaDonView();
            List<ChiTietHd> ct = new List<ChiTietHd>();
            if (id.HasValue)
            {

                HoaDon hd = _context.HoaDons.Include(x => x.ThanhVien).Include(x => x.TrangThai).Where(p => p.MaHd == id).First();
                ct = _context.ChiTietHds.Include(x => x.HangHoa).Where(p => p.MaHd == hd.MaHd).ToList();
                model.hd = hd;
                model.chitiethd = ct;
            }
            double tongtien = 0;
            foreach (var item in ct)
            {
                tongtien += item.ThanhTien;
            }
            ViewBag.TongTien = tongtien;
            return View(model);
        }
        public IActionResult Search(int? keyword)
        {
            if (keyword != null)
            {

                var data = _context.HoaDons.Include(h => h.ThanhVien).Include(h => h.TrangThai).Where(p => p.MaHd == keyword)
                    .ToList();

                return PartialView(data);
            }
            else
            {
                return PartialView();
            }
        }
        public IActionResult giamtrth(int id)
        {
            HoaDon hd = _context.HoaDons.Where(p => p.MaHd == id).First();
            --hd.MaTrangThai;
            _context.HoaDons.Update(hd);
            _context.SaveChanges();
            return RedirectToAction("index", "HoaDons");
        }
        public IActionResult tangtrth(int id)
        {
            HoaDon hd = _context.HoaDons.Where(p => p.MaHd == id).First();
            ++hd.MaTrangThai;
            _context.HoaDons.Update(hd);
            _context.SaveChanges();
            return RedirectToAction("index", "HoaDons");
        }
    }
}
