using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ecoNhom3.Models;
using System.Web;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Session;
using static System.Collections.Specialized.BitVector32;

namespace ecoNhom3.Controllers
{
   
    public class ThanhViensController : Controller
    {
        private MyDbContext _context;
       
        public ThanhViensController()
        {
            _context = new MyDbContext();
        }

     
        public IActionResult Index()
        {
           
            if (HttpContext.Session.GetInt32("MaTv") == null) return RedirectToAction("Login", "ThanhViens");
            
            var tv = _context.ThanhViens.ToList();
            return View(tv);
        }


        public IActionResult Details(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var thanhVien= _context.ThanhViens.SingleOrDefault(m => m.MaTv == id);
            if (thanhVien == null) return NotFound();
           
            return View(thanhVien);
        }

      

       
        public IActionResult Create()
        {
            ViewData["LoaiTv"] = new SelectList(_context.PhanLoaiTVs, "LoaiTv", "LoaiTv");
            return View();
        }
        [HttpPost]
        public async Task<ActionResult<ThanhVien>> Create([Bind("MaTv,TenTv,GioiTinh,NgaySinh,DiaChi,DienThoai,Email,LoaiTv,TaiKhoan,MatKhau")] ThanhVien thanhVien)
        {
            _context.ThanhViens.Add(thanhVien);
            await _context.SaveChangesAsync();
            ViewData["LoaiTv"] = new SelectList(_context.PhanLoaiTVs, "LoaiTv", "LoaiTv", thanhVien.LoaiTv);
            return CreatedAtAction("GetThanhVien", new { id = thanhVien.MaTv }, thanhVien);
        }

     
        [HttpDelete("{id}")]
        public async Task<ActionResult<ThanhVien>> Delete(int id)
        {
            var thanhVien = await _context.ThanhViens.FindAsync(id);
            if (thanhVien == null)
            {
                return NotFound();
            }

            _context.ThanhViens.Remove(thanhVien);
            await _context.SaveChangesAsync();

            return thanhVien;
        }

        private bool ThanhVienExists(int id)
        {
            return _context.ThanhViens.Any(e => e.MaTv == id);
        }

       
        
        public IActionResult Dangky()
        {
            ViewData["LoaiTv"] = new SelectList(_context.PhanLoaiTVs, "LoaiTv", "LoaiTv");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Dangky([Bind("MaTv,TenTv,GioiTinh,NgaySinh,DiaChi,LoaiTv,DienThoai,Email,TaiKhoan,MatKhau")] ThanhVien thanhVien)
        {
            if (ModelState.IsValid)
            {
                thanhVien.MaTv = 3;
               
                _context.Add(thanhVien);
                HttpContext.Session.Set<int>("a", 2);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Home");
            }
           
            ViewData["LoaiTv"] = new SelectList(_context.PhanLoaiTVs, "LoaiTv", "LoaiTv", thanhVien.LoaiTv);
            return View(thanhVien);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            ThanhVien tv = _context.ThanhViens.Where(p => p.MaTv == id).First();
            
            ViewData["LoaiTv"] = new SelectList(_context.PhanLoaiTVs, "LoaiTv", "LoaiTv", tv.LoaiTv);

            return View(tv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int ltv, [Bind("MaTv,TenTv,GioiTinh,NgaySinh,DiaChi,LoaiTv,DienThoai,Email,TaiKhoan")] ThanhVien thanhVien)
        {
            if (id != thanhVien.MaTv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Sửa lại update
                    thanhVien.LoaiTv = ltv;
                    _context.Update(thanhVien);
                    await _context.SaveChangesAsync();
                    HttpContext.Session.Set("MaTv",thanhVien);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThanhVienExists(thanhVien.MaTv))
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
      
     
        public IActionResult Customer()
        {
            List<ThanhVien> Dstv = new List<ThanhVien>();
            Dstv = _context.ThanhViens.Where(h => h.LoaiTv == 3).ToList();
            return View(Dstv.Select(h => new ThanhVien
            {
                TenTv = h.TenTv,
                MaTv = h.MaTv,
                GioiTinh = h.GioiTinh,
                NgaySinh = h.NgaySinh,
                DiaChi = h.DiaChi,
                DienThoai = h.DienThoai,
                Email = h.Email,
                
                LoaiTv = h.LoaiTv,
                TaiKhoan = h.TaiKhoan,
                MatKhau = h.MatKhau
            }));
        }
        public IActionResult Admin()
        {
            List<ThanhVien> Dstv = new List<ThanhVien>();
            Dstv = _context.ThanhViens.Where(h => h.LoaiTv == 1).ToList();
            return View(Dstv.Select(h => new ThanhVien
            {
                TenTv = h.TenTv,
                MaTv = h.MaTv,
                GioiTinh = h.GioiTinh,
                NgaySinh = h.NgaySinh,
                DiaChi = h.DiaChi,
                DienThoai = h.DienThoai,
                Email = h.Email,

                LoaiTv = h.LoaiTv,
                TaiKhoan = h.TaiKhoan,
                MatKhau = h.MatKhau
            }));
        }

        public IActionResult SearchAdmin(string keyword = "")
        {
            if (keyword != null)
            {
                keyword = keyword.ToLower();
                var data = _context.ThanhViens.Include(n => n.PhanLoaiTV).Where(p => p.TenTv.ToLower().Contains(keyword))
                    .ToList();

                return PartialView(data);
            }
            else
            {
                return PartialView();
            }
        }
        public IActionResult SearchUser(string keyword = "")
        {
            if (keyword != null)
            {
                keyword = keyword.ToLower();
                var data = _context.ThanhViens.Include(n => n.PhanLoaiTV).Where(p => p.TenTv.ToLower().Contains(keyword))
                    .ToList();

                return PartialView(data);
            }
            else
            {
                return PartialView();
            }
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login (Login Model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Login tv = (from ThanhVien in _context.ThanhViens
                                  where ThanhVien.TaiKhoan == Model.TaiKhoan && ThanhVien.MatKhau == Model.MatKhau
                                  select new Login
                                  {
                                      MaTv = ThanhVien.MaTv,
                                      TaiKhoan = ThanhVien.TaiKhoan,
                                      MatKhau = ThanhVien.MatKhau,
                                      LoaiTv = ThanhVien.LoaiTv
                                  }).SingleOrDefault();
            if (tv == null)
            {
                ModelState.AddModelError("", "Sai tài khoản hoặc mật khẩu");
                return View();
            }
            else
            {
                HttpContext.Session.SetInt32("MaTv", tv.MaTv);
                HttpContext.Session.SetString("TaiKhoan", tv.TaiKhoan);
                HttpContext.Session.SetString("MatKhau", tv.MatKhau);
                HttpContext.Session.SetInt32("LoaiTv", tv.LoaiTv);
            }
            return RedirectToAction("Details", "ThanhViens", new { id = tv.MaTv });

        }


        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("MaTv");
            HttpContext.Session.Remove("TaiKhoan");
            HttpContext.Session.Remove("MatKhau");
            HttpContext.Session.Remove("LoaiTv");
            return RedirectToAction("Index", "Home");
        }

    }
}
