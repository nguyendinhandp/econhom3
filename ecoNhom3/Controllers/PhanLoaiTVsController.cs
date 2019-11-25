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
  
    public class PhanLoaiTVsController : Controller
    {
        private readonly MyDbContext _context;

        public PhanLoaiTVsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/PhanLoaiTVs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhanLoaiTV>>> Index()
        {
            return await _context.PhanLoaiTVs.ToListAsync();
        }

        // GET: api/PhanLoaiTVs/5
       
        public async Task<ActionResult<PhanLoaiTV>> Details(int id)
        {
            var phanLoai = await _context.PhanLoaiTVs
                .FirstOrDefaultAsync(m => m.LoaiTv == id);

            if (phanLoai == null)
            {
                return NotFound();
            }

            return View(phanLoai);
        }

        // GET: PhanLoaiTVs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PhanLoaiTVs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoaiTv,LoaiThanhVien")] PhanLoaiTV phanLoai)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phanLoai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(phanLoai);
        }
        // GET: PhanLoaiTVs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phanLoai = await _context.PhanLoaiTVs.FindAsync(id);
            if (phanLoai == null)
            {
                return NotFound();
            }
            return View(phanLoai);
        }
        // PUT: api/PhanLoaiTVs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id,[Bind("LoaiTv,LoaiThanhVien")] PhanLoaiTV phanLoaiTV)
        {
            if (id != phanLoaiTV.LoaiTv)
            {
                return BadRequest();
            }

            _context.Entry(phanLoaiTV).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhanLoaiTVExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return View(phanLoaiTV);
        }

        // POST: api/PhanLoaiTVs
        [HttpPost]
        public async Task<ActionResult<PhanLoaiTV>> PostPhanLoaiTV(PhanLoaiTV phanLoaiTV)
        {
            _context.PhanLoaiTVs.Add(phanLoaiTV);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhanLoaiTV", new { id = phanLoaiTV.LoaiTv }, phanLoaiTV);
        }

        // DELETE: api/PhanLoaiTVs/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phanLoaiTV = await _context.PhanLoaiTVs
                .FirstOrDefaultAsync(m => m.LoaiTv== id);
            if (phanLoaiTV == null)
            {
                return NotFound();
            }

            return View(phanLoaiTV);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<PhanLoaiTV>> Delete(int id)
        {
            var phanLoaiTV = await _context.PhanLoaiTVs.FindAsync(id);
            if (phanLoaiTV == null)
            {
                return NotFound();
            }

            _context.PhanLoaiTVs.Remove(phanLoaiTV);
            await _context.SaveChangesAsync();

            return phanLoaiTV;
        }

        private bool PhanLoaiTVExists(int id)
        {
            return _context.PhanLoaiTVs.Any(e => e.LoaiTv == id);
        }
    }
}
