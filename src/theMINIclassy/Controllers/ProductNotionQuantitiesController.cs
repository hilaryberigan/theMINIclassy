using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using theMINIclassy.Data;
using theMINIclassy.Models;

namespace theMINIclassy.Controllers
{
    public class ProductNotionQuantitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductNotionQuantitiesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: ProductNotionQuantities
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductNotionQuantity.Include(p => p.Notion).Include(p => p.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductNotionQuantities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productNotionQuantity = await _context.ProductNotionQuantity.SingleOrDefaultAsync(m => m.Id == id);
            if (productNotionQuantity == null)
            {
                return NotFound();
            }

            return View(productNotionQuantity);
        }

        // GET: ProductNotionQuantities/Create
        public IActionResult Create(int? id)
        {
            ViewData["NotionId"] = new SelectList(_context.Notion, "Id", "Title");
            ViewData["ProductId"] = id;
            return View();
        }

        // POST: ProductNotionQuantities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NotionId,ProductId,QtyNotionOnProduct")] ProductNotionQuantity productNotionQuantity)
        {
            productNotionQuantity.ProductId = productNotionQuantity.Id;
            productNotionQuantity.Id = 0;
            if (ModelState.IsValid)
            {
                _context.Add(productNotionQuantity);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                _context.Add(productNotionQuantity);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Products", new { id = productNotionQuantity.ProductId });
            }
            ViewData["NotionId"] = new SelectList(_context.Notion, "Id", "Title", productNotionQuantity.NotionId);
            ViewData["ProductId"] = productNotionQuantity.ProductId;
            return View(productNotionQuantity);
        }

        // GET: ProductNotionQuantities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productNotionQuantity = await _context.ProductNotionQuantity.SingleOrDefaultAsync(m => m.Id == id);
            if (productNotionQuantity == null)
            {
                return NotFound();
            }
            ViewData["NotionId"] = new SelectList(_context.Notion, "Id", "Id", productNotionQuantity.NotionId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", productNotionQuantity.ProductId);
            return View(productNotionQuantity);
        }

        // POST: ProductNotionQuantities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NotionId,ProductId,QtyNotionOnProduct")] ProductNotionQuantity productNotionQuantity)
        {
            if (id != productNotionQuantity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productNotionQuantity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductNotionQuantityExists(productNotionQuantity.Id))
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
            ViewData["NotionId"] = new SelectList(_context.Notion, "Id", "Id", productNotionQuantity.NotionId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", productNotionQuantity.ProductId);
            return View(productNotionQuantity);
        }

        // GET: ProductNotionQuantities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productNotionQuantity = await _context.ProductNotionQuantity.SingleOrDefaultAsync(m => m.Id == id);
            if (productNotionQuantity == null)
            {
                return NotFound();
            }

            return View(productNotionQuantity);
        }

        // POST: ProductNotionQuantities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productNotionQuantity = await _context.ProductNotionQuantity.SingleOrDefaultAsync(m => m.Id == id);
            var productId = productNotionQuantity.ProductId;
            _context.ProductNotionQuantity.Remove(productNotionQuantity);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Products", new { id = productId });
        }

        private bool ProductNotionQuantityExists(int id)
        {
            return _context.ProductNotionQuantity.Any(e => e.Id == id);
        }
    }
}
