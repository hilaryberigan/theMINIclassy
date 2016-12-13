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
    public class ProductFabricQuantitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductFabricQuantitiesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: ProductFabricQuantities
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductFabricQuantity.Include(p => p.Fabric).Include(p => p.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductFabricQuantities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productFabricQuantity = await _context.ProductFabricQuantity.SingleOrDefaultAsync(m => m.Id == id);
            if (productFabricQuantity == null)
            {
                return NotFound();
            }

            return View(productFabricQuantity);
        }

        // GET: ProductFabricQuantities/Create
        public IActionResult Create(int? id)
        {
            ViewData["FabricId"] = new SelectList(_context.Fabric, "Id", "Title");
            ViewData["ProductId"] = id;
            return View();
        }

        // POST: ProductFabricQuantities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FabricId,ProductId,QtyFabricOnProduct")] ProductFabricQuantity productFabricQuantity)
        {
            var x = productFabricQuantity.Id;
            productFabricQuantity.ProductId = x;
            productFabricQuantity.Id = 0;
            if (ModelState.IsValid)
            {
                _context.Add(productFabricQuantity);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }else
            {
                _context.Add(productFabricQuantity);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Products", new { id = productFabricQuantity.ProductId });
            }
            ViewData["FabricId"] = new SelectList(_context.Fabric, "Id", "Title", productFabricQuantity.FabricId);
            ViewData["ProductId"] = productFabricQuantity.ProductId;
            return View(productFabricQuantity);
        }

        // GET: ProductFabricQuantities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productFabricQuantity = await _context.ProductFabricQuantity.SingleOrDefaultAsync(m => m.Id == id);
            if (productFabricQuantity == null)
            {
                return NotFound();
            }
            ViewData["FabricId"] = new SelectList(_context.Fabric, "Id", "Id", productFabricQuantity.FabricId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", productFabricQuantity.ProductId);
            return View(productFabricQuantity);
        }

        // POST: ProductFabricQuantities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FabricId,ProductId,QtyFabricOnProduct")] ProductFabricQuantity productFabricQuantity)
        {
            //productFabricQuantity.ProductId = productFabricQuantity.Id;
            //productFabricQuantity.Id = 0;
            if (id != productFabricQuantity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productFabricQuantity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductFabricQuantityExists(productFabricQuantity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details","Products", new { id = productFabricQuantity.ProductId });
            }
            ViewData["FabricId"] = new SelectList(_context.Fabric, "Id", "Id", productFabricQuantity.FabricId);
            ViewData["ProductId"] = productFabricQuantity.ProductId;
            return View(productFabricQuantity);
        }

        // GET: ProductFabricQuantities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productFabricQuantity = await _context.ProductFabricQuantity.SingleOrDefaultAsync(m => m.Id == id);
            if (productFabricQuantity == null)
            {
                return NotFound();
            }

            return View(productFabricQuantity);
        }

        // POST: ProductFabricQuantities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productFabricQuantity = await _context.ProductFabricQuantity.SingleOrDefaultAsync(m => m.Id == id);
            _context.ProductFabricQuantity.Remove(productFabricQuantity);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProductFabricQuantityExists(int id)
        {
            return _context.ProductFabricQuantity.Any(e => e.Id == id);
        }
    }
}
