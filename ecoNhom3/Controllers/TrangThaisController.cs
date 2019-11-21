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
  
    public class TrangThaisController : Controller
    {
        private readonly MyDbContext _context;

        public TrangThaisController(MyDbContext context)
        {
            _context = context;
        }

      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrangThai>>> Index()
        {
            return await _context.TrangThais.ToListAsync();
        }

        
        public async Task<ActionResult<TrangThai>> Details(int id)
        {
            var trangThai = await _context.TrangThais.FindAsync(id);

            if (trangThai == null)
            {
                return NotFound();
            }

            return trangThai;
        }

     

        private bool TrangThaiExists(int id)
        {
            return _context.TrangThais.Any(e => e.MaTt == id);
        }
    }
}
