using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ecoNhom3.Models;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;

namespace ecoNhom3.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        private MyDbContext _context;

        public CartController()
        {
            _context = new MyDbContext();
        }

        // GET: GioHang
        [Route("index")]
        public ActionResult Index()
        {
            var cart = Models.SessionExtensions.Get<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.HangHoa.Price * item.Quantity);
            return View();
        }

        [HttpGet("{id},{sl}")]
        [Route("buy/{id}")]
        public IActionResult Buy(int id, int sl)
        {
            if (Models.SessionExtensions.Get<List<Item>>(HttpContext.Session, "cart") == null)
            {
                var cart = new List<Item>();
                cart.Add(new Item() { HangHoa = _context.HangHoas.Find(id), Quantity = sl });
                Models.SessionExtensions.Set(HttpContext.Session, "cart", cart);
            }
            else
            {
                var cart = Models.SessionExtensions.Get<List<Item>>(HttpContext.Session, "cart");
                int index = Exists(cart, id);
                if(index == -1)
                {
                    cart.Add(new Item() { HangHoa = _context.HangHoas.Find(id), Quantity = sl});
                }
                else
                {
                    cart[index].Quantity += sl;
                }
                Models.SessionExtensions.Set(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        private int Exists(List<Item> cart, int id)
        {
           
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].HangHoa.MaHh == id)
                {
                    return i;
                }
            }
            return -1;
        }

        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<Item> cart = Models.SessionExtensions.Get<List<Item>>(HttpContext.Session, "cart");
            int index = Exists(cart,id);
            cart.RemoveAt(index);
            Models.SessionExtensions.Set(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        [HttpGet("{id},{sl}")]
        [Route("Update/{id}")]
        public IActionResult Update(int id, int sl)
        {

            if (Models.SessionExtensions.Get<List<Item>>(HttpContext.Session, "cart") != null)
            {
                
                var cart = Models.SessionExtensions.Get<List<Item>>(HttpContext.Session, "cart");
                int index = Exists(cart, id);
                if (index != -1)
                {
                     cart[index].Quantity = sl;
                }
                Models.SessionExtensions.Set(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }


        // GET: GioHang/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GioHang/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GioHang/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GioHang/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GioHang/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GioHang/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GioHang/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}