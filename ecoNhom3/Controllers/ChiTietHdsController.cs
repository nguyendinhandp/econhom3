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

    public class ChiTietHdsController : Controller
    {
        private readonly MyDbContext _context;

        public ChiTietHdsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/ChiTietHds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChiTietHd>>> Index()
        {
            var myDbContext = _context.ChiTietHds.Include(c => c.HangHoa).Include(c => c.HoaDon);
            return await _context.ChiTietHds.ToListAsync();
        }

        // GET: api/ChiTietHds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChiTietHd>> Details(int id)
        {
            var chiTietHd = await _context.ChiTietHds
               .Include(c => c.HangHoa)
               .Include(c => c.HoaDon)
               .FirstOrDefaultAsync(m => m.MaCTHD == id);

            if (chiTietHd == null)
            {
                return NotFound();
            }

            return View(chiTietHd);
        }
        // GET: ChiTietHds/Create
        public IActionResult Create()
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietHd = await _context.ChiTietHds.FindAsync(id);
            if (chiTietHd == null)
            {
                return NotFound();
            }
            ViewData["MaHh"] = new SelectList(_context.HangHoas, "MaHh", "MaHh", chiTietHd.MaHh);
            ViewData["MaHd"] = new SelectList(_context.HoaDons, "MaHd", "MaHd", chiTietHd.MaHd);
            return View(chiTietHd);
        }


        // PUT: api/ChiTietHds/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, ChiTietHd chiTietHd)
        {
            if (id != chiTietHd.MaCTHD)
            {
                return BadRequest();
            }

            _context.Entry(chiTietHd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChiTietHdExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            ViewData["MaHh"] = new SelectList(_context.HangHoas, "MaHh", "MaHh", chiTietHd.MaHh);
            ViewData["MaHd"] = new SelectList(_context.HoaDons, "MaHd", "MaHd", chiTietHd.MaHd);
            return View(chiTietHd);
        }

        // POST: api/ChiTietHds
        [HttpPost]
        public async Task<ActionResult<ChiTietHd>> PostChiTietHd(ChiTietHd chiTietHd)
        {
            _context.ChiTietHds.Add(chiTietHd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChiTietHd", new { id = chiTietHd.MaCTHD }, chiTietHd);
        }
        // GET: ChiTietHds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietHd = await _context.ChiTietHds
                .Include(c => c.HangHoa)
                .Include(c => c.HoaDon)
                .FirstOrDefaultAsync(m => m.MaCTHD == id);
            if (chiTietHd == null)
            {
                return NotFound();
            }

            return View(chiTietHd);
        }

        // DELETE: api/ChiTietHds/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ChiTietHd>> DeleteChiTietHd(int id)
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

        private bool ChiTietHdExists(int id)
        {
            return _context.ChiTietHds.Any(e => e.MaCTHD == id);
        }
    }
}
