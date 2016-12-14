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
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace theMINIclassy.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private IHostingEnvironment _environment;

        public ProductsController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        [Authorize]
        // GET: Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Product.Include(p => p.Collection).Include(p => p.Style).Include(p => p.Variation);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CollectionId,Description,ImagePath,MinThreshold,Quantity,SKU,StyleId,TechPackPath,Title,VariationId")] Product product, int FabricsQuantity, ICollection<IFormFile> files)
        {
            product.Quantity = 0;
            if (ModelState.IsValid)
            {
                _context.Add(product);
                var uploads = Path.Combine(_environment.WebRootPath, "uploads");
                var images = Path.Combine(_environment.WebRootPath, "images");
                int count = 0;
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        if (count == 0)
                        {

                            using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }
                            uploads += "\\" + file.FileName;
                            count++;
                        }
                        else
                        {
                            using (var fileStream = new FileStream(Path.Combine(images, file.FileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }
                            images += "\\" + file.FileName;
                        }
                    }
                }

                product.TechPackPath = uploads;
                product.ImagePath = images;
                await _context.SaveChangesAsync();
                int saveId = product.Id;
                return RedirectToAction("Details","Products", new { id = product.Id });
            }
            ViewData["CollectionId"] = new SelectList(_context.Collection, "Id", "Id", product.CollectionId);
            ViewData["StyleId"] = new SelectList(_context.Style, "Id", "Id", product.StyleId);
            ViewData["VariationId"] = new SelectList(_context.Variation, "Id", "Id", product.VariationId);
            return View(product);
        }
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> EditProductQuantity(int? id)
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
        [Authorize]
        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProductQuantity(int id, [Bind("Id,CollectionId,Description,ImagePath,MinThreshold,Quantity,SKU,StyleId,TechPackPath,Title,VariationId")] Product product)
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
        [Authorize]
        public IActionResult ProductInventory(string sortOrder, string searchString)
        {


            var products = _context.Product.Include(p => p.Collection).Include(p => p.Style).Include(p => p.Variation).ToList();
            
            if (!String.IsNullOrEmpty(searchString))
            {
                var temp = products.Where(s => s.Title != null && s.SKU != null);
                products = temp.Where(s => s.Title.ToUpper().Contains(searchString.ToUpper())
                                       || s.SKU.ToUpper().Contains(searchString.ToUpper())).ToList();
            }

            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Title_desc" : "";
            ViewData["SKUSortParm"] = sortOrder == "SKU" ? "SKU_desc" : "SKU";
            ViewData["QuantSortParm"] = sortOrder == "Quantity" ? "Quant_desc" : "Type";


            switch (sortOrder)
            {
                case "SKU":
                    products = products.OrderByDescending(s => s.SKU).ToList();
                    break;

                case "SKU_desc":
                    products = products.OrderBy(s => s.SKU).ToList();
                    break;
                case "Quantity":
                    products = products.OrderBy(s => s.Quantity).ToList();
                    break;
                case "Quant_desc":
                    products = products.OrderByDescending(s => s.Quantity).ToList();
                    break;
                case "Title":
                    products = products.OrderBy(s => s.Title).ToList();
                    break;
                case "Title_desc":
                    products = products.OrderByDescending(s => s.Title).ToList();
                    break;
                default:
                    products = products.OrderBy(s => s.SKU).ToList();
                    break;
            }

            return View(products);
        
    }
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
