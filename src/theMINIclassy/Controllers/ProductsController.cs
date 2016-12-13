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
            List<ProductFabricQuantity> PFQ = new List<ProductFabricQuantity>();
            foreach(var item in _context.ProductFabricQuantity)
            {
                PFQ.Add(item);
            }
            List<ProductNotionQuantity> PNQ = new List<ProductNotionQuantity>();
            foreach(var item in _context.ProductNotionQuantity)
            {
                PNQ.Add(item);
            }
            List<ProductLabelQuantity> PLQ = new List<ProductLabelQuantity>();
            foreach(var item in _context.ProductLabelQuantity)
            {
                PLQ.Add(item);
            }
            List<ProductTagQuantity> PTQ = new List<ProductTagQuantity>();
            foreach(var item in _context.ProductTagQuantity)
            {
                PTQ.Add(item);
            }
            List<Fabric> allFab = new List<Fabric>();
            foreach(var item in _context.Fabric)
            {
                allFab.Add(item);
            }
            List<Notion> allNot = new List<Notion>();
            foreach(var item in _context.Notion)
            {
                allNot.Add(item);
            }
            List<Tag> allTag = new List<Tag>();
            foreach(var item in _context.Tag)
            {
                allTag.Add(item);
            }
            List<Label> allLab = new List<Label>();
            foreach(var item in _context.Label)
            {
                allLab.Add(item);
            }
            var model = new ProductViewModel
            {
                Product = product,
                PFQuantities = PFQ,
                PNQuantities = PNQ,
                PLQuantities = PLQ,
                PTQuantities = PTQ,
                Fabrics = allFab,
                Notions = allNot,
                Tags = allTag,
                Labels = allLab
            };
            return View(model);
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
            List<Notion> allNots = new List<Notion>();
            foreach(var item in _context.Notion)
            {
                allNots.Add(item);
            }
            var model = new ProductViewModel
            {
                Product = new Product(),
                Collections = _context.Collection.ToList(),
                Fabrics = allFabs,
                Notions = allNots
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
