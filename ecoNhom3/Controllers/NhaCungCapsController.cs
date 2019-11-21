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
  
    public class NhaCungCapsController : Controller
    {
        private readonly MyDbContext _context;

        public NhaCungCapsController(MyDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var n = _context.NhaCungCaps.ToList();
            return View(n);
        }



        public IActionResult Details()
        {

            return View();
        }

        // PUT: api/NhaCungCaps/5
       
        public async Task<IActionResult> Edit(int id, NhaCungCap nhaCungCap)
        {
            if (id != nhaCungCap.MaNcc)
            {
                return BadRequest();
            }

            _context.Entry(nhaCungCap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NhaCungCapExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

       
        [HttpPost]
        public async Task<ActionResult<NhaCungCap>> Create (NhaCungCap nhaCungCap)
        {
            _context.NhaCungCaps.Add(nhaCungCap);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNhaCungCap", new { id = nhaCungCap.MaNcc }, nhaCungCap);
        }

    
        public async Task<ActionResult<NhaCungCap>> Delete(int id)
        {
            var nhaCungCap = await _context.NhaCungCaps.FindAsync(id);
            if (nhaCungCap == null)
            {
                return NotFound();
            }

            _context.NhaCungCaps.Remove(nhaCungCap);
            await _context.SaveChangesAsync();

            return nhaCungCap;
        }

        private bool NhaCungCapExists(int id)
        {
            return _context.NhaCungCaps.Any(e => e.MaNcc == id);
        }
      
    }
}
