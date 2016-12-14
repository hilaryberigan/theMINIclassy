using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using theMINIclassy.Data;
using theMINIclassy.Models;
using Microsoft.AspNetCore.Authorization;

namespace theMINIclassy.Controllers
{
    public class ProductTagQuantitiesController : Controller
    {
        private readonly ApplicationDbContext _context;
   
        public ProductTagQuantitiesController(ApplicationDbContext context)
        {
            _context = context;    
        }
        [Authorize]
        // GET: ProductTagQuantities
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductTagQuantity.Include(p => p.Product).Include(p => p.Tag);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize]
        // GET: ProductTagQuantities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productTagQuantity = await _context.ProductTagQuantity.SingleOrDefaultAsync(m => m.Id == id);
            if (productTagQuantity == null)
            {
                return NotFound();
            }

            return View(productTagQuantity);
        }
        [Authorize]
        // GET: ProductTagQuantities/Create
        public IActionResult Create(int? id)
        {
            ViewData["ProductId"] = id;
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Title");
            return View();
        }
        [Authorize]
        // POST: ProductTagQuantities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,QtyTagOnProduct,TagId")] ProductTagQuantity productTagQuantity)
        {
            productTagQuantity.ProductId = productTagQuantity.Id;
            productTagQuantity.Id = 0;
            if (ModelState.IsValid)
            {
                _context.Add(productTagQuantity);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                _context.Add(productTagQuantity);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Products", new { id = productTagQuantity.ProductId });
            }
            ViewData["ProductId"] = productTagQuantity.ProductId;
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Title", productTagQuantity.TagId);
            return View(productTagQuantity);
        }
        [Authorize]
        // GET: ProductTagQuantities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productTagQuantity = await _context.ProductTagQuantity.SingleOrDefaultAsync(m => m.Id == id);
            if (productTagQuantity == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", productTagQuantity.ProductId);
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Id", productTagQuantity.TagId);
            return View(productTagQuantity);
        }
        [Authorize]
        // POST: ProductTagQuantities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,QtyTagOnProduct,TagId")] ProductTagQuantity productTagQuantity)
        {
            if (id != productTagQuantity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productTagQuantity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductTagQuantityExists(productTagQuantity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", productTagQuantity.ProductId);
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Id", productTagQuantity.TagId);
            return View(productTagQuantity);
        }
        [Authorize]
        // GET: ProductTagQuantities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productTagQuantity = await _context.ProductTagQuantity.SingleOrDefaultAsync(m => m.Id == id);
            if (productTagQuantity == null)
            {
                return NotFound();
            }

            return View(productTagQuantity);
        }
        [Authorize]
        // POST: ProductTagQuantities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productTagQuantity = await _context.ProductTagQuantity.SingleOrDefaultAsync(m => m.Id == id);
            var productId = productTagQuantity.ProductId;
            _context.ProductTagQuantity.Remove(productTagQuantity);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Products", new { id = productId });
        }
        [Authorize]
        private bool ProductTagQuantityExists(int id)
        {
            return _context.ProductTagQuantity.Any(e => e.Id == id);
        }
    }
}
