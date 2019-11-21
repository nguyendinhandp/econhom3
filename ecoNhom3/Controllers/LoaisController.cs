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
    
    public class LoaisController : Controller
    {
        private readonly MyDbContext _context;

        public LoaisController(MyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var l = _context.Loais.ToList();
            return View(l);
        }



        public IActionResult Details()
        {

            return View();
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            Loai l = _context.Loais.Where(p => p.MaLoai == id).First();

          
            return View(l);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaLoai, TenLoai")] Loai loai)
        {
            if (id != loai.MaLoai)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   
                    _context.Update(loai);
                    await _context.SaveChangesAsync();
                    HttpContext.Session.Set("MaLoai", loai);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiExists(loai.MaLoai))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }


        public IActionResult Create()
        {
            return View();
        }
       
        [HttpPost]
        public async Task<ActionResult<Loai>> Create(Loai loai)
        {
            _context.Loais.Add(loai);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoai", new { id = loai.MaLoai }, loai);
        }

    
      
        public async Task<ActionResult<Loai>> Delete(int id)
        {
            var loai = await _context.Loais.FindAsync(id);
            if (loai == null)
            {
                return NotFound();
            }

            _context.Loais.Remove(loai);
            await _context.SaveChangesAsync();

            return loai;
        }

        private bool LoaiExists(int id)
        {
            return _context.Loais.Any(e => e.MaLoai == id);
        }
    }
}
