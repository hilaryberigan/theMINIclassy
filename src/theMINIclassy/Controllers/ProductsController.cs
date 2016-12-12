using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using theMINIclassy.Data;
using theMINIclassy.Models;
using theMINIclassy.Models.ManageViewModels;

namespace theMINIclassy.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Product.Include(p => p.Collection).Include(p => p.Style).Include(p => p.Variation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CollectionId"] = new SelectList(_context.Collection, "Id", "Title");
            ViewData["StyleId"] = new SelectList(_context.Style, "Id", "Title");
            ViewData["VariationId"] = new SelectList(_context.Variation, "Id", "Title");
            ViewData["ProductFabricQuantity"] = new SelectList(_context.Fabric, "Id", "Title");
            List<Fabric> allFabs = new List<Fabric>();
            foreach (var item in _context.Fabric)
            {

                allFabs.Add(item);
            }
            var model = new ProductViewModel
            {
                Product = new Product(),
                Collections = _context.Collection.ToList(),
                Fabrics = allFabs
            };
            return View(model);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CollectionId,Description,ImagePath,MinThreshold,Quantity,SKU,StyleId,TechPackPath,Title,VariationId")] Product product, int FabricsQuantity)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                int saveId = product.Id;
                for (int i = 0; i < FabricsQuantity; i++)
                {
                    return RedirectToAction("create", "ProductFabricQuantities", new { id = product.Id });
                }
                return RedirectToAction("Index");
            }
            ViewData["CollectionId"] = new SelectList(_context.Collection, "Id", "Id", product.CollectionId);
            ViewData["StyleId"] = new SelectList(_context.Style, "Id", "Id", product.StyleId);
            ViewData["VariationId"] = new SelectList(_context.Variation, "Id", "Id", product.VariationId);
            return View(product);
        }
        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CollectionId"] = new SelectList(_context.Collection, "Id", "Id", product.CollectionId);
            ViewData["StyleId"] = new SelectList(_context.Style, "Id", "Id", product.StyleId);
            ViewData["VariationId"] = new SelectList(_context.Variation, "Id", "Id", product.VariationId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CollectionId,Description,ImagePath,MinThreshold,Quantity,SKU,StyleId,TechPackPath,Title,VariationId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["CollectionId"] = new SelectList(_context.Collection, "Id", "Id", product.CollectionId);
            ViewData["StyleId"] = new SelectList(_context.Style, "Id", "Id", product.StyleId);
            ViewData["VariationId"] = new SelectList(_context.Variation, "Id", "Id", product.VariationId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.SingleOrDefaultAsync(m => m.Id == id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
