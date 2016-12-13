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
    public class ProductLabelQuantitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductLabelQuantitiesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: ProductLabelQuantities
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductLabelQuantity.Include(p => p.Label).Include(p => p.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductLabelQuantities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productLabelQuantity = await _context.ProductLabelQuantity.SingleOrDefaultAsync(m => m.Id == id);
            if (productLabelQuantity == null)
            {
                return NotFound();
            }

            return View(productLabelQuantity);
        }

        // GET: ProductLabelQuantities/Create
        public IActionResult Create(int? id)
        {
            ViewData["LabelId"] = new SelectList(_context.Label, "Id", "Title");
            ViewData["ProductId"] = id;
            return View();
        }

        // POST: ProductLabelQuantities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LabelId,ProductId,QtyLabelOnProduct")] ProductLabelQuantity productLabelQuantity)
        {
            productLabelQuantity.ProductId = productLabelQuantity.Id;
            productLabelQuantity.Id = 0;
            if (ModelState.IsValid)
            {
                _context.Add(productLabelQuantity);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                _context.Add(productLabelQuantity);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Products", new { id = productLabelQuantity.ProductId });
            }
            ViewData["LabelId"] = new SelectList(_context.Label, "Id", "Title", productLabelQuantity.LabelId);
            ViewData["ProductId"] = productLabelQuantity.ProductId;
            return View(productLabelQuantity);
        }

        // GET: ProductLabelQuantities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productLabelQuantity = await _context.ProductLabelQuantity.SingleOrDefaultAsync(m => m.Id == id);
            if (productLabelQuantity == null)
            {
                return NotFound();
            }
            ViewData["LabelId"] = new SelectList(_context.Label, "Id", "Id", productLabelQuantity.LabelId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", productLabelQuantity.ProductId);
            return View(productLabelQuantity);
        }

        // POST: ProductLabelQuantities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LabelId,ProductId,QtyLabelOnProduct")] ProductLabelQuantity productLabelQuantity)
        {
            if (id != productLabelQuantity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productLabelQuantity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductLabelQuantityExists(productLabelQuantity.Id))
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
            ViewData["LabelId"] = new SelectList(_context.Label, "Id", "Id", productLabelQuantity.LabelId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", productLabelQuantity.ProductId);
            return View(productLabelQuantity);
        }

        // GET: ProductLabelQuantities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productLabelQuantity = await _context.ProductLabelQuantity.SingleOrDefaultAsync(m => m.Id == id);
            if (productLabelQuantity == null)
            {
                return NotFound();
            }

            return View(productLabelQuantity);
        }

        // POST: ProductLabelQuantities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productLabelQuantity = await _context.ProductLabelQuantity.SingleOrDefaultAsync(m => m.Id == id);
            _context.ProductLabelQuantity.Remove(productLabelQuantity);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProductLabelQuantityExists(int id)
        {
            return _context.ProductLabelQuantity.Any(e => e.Id == id);
        }
    }
}
