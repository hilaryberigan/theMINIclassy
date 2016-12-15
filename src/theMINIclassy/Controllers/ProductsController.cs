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
        public async Task<IActionResult> EditProductQuantity(int id, [Bind("Id,Quantity")] Product product)
        {
            //Dictionary hold key: fabric id & value: amount to decrease supply by
            Dictionary<int, decimal> fabricDict = new Dictionary<int, decimal>();
            //Dictionary hold key: notion id & value: amount to decrease supply by
            Dictionary<int, decimal> notionDict = new Dictionary<int, decimal>();
            //Dictionary hold key: label id & value: amount to decrease supply by
            Dictionary<int, decimal> labelDict = new Dictionary<int, decimal>();
            //Dictionary hold key: tag id & value: amount to decrease supply by
            Dictionary<int, decimal> tagDict = new Dictionary<int, decimal>();
            //Exception Dictionary hold key: conflicting object's title && value: saying problem
            Dictionary<string, string> exceptionDict = new Dictionary<string, string>();

            var oldProduct = new Product();
            foreach (var item in _context.Product)
            {
                if(item.Id == product.Id)
                {
                    oldProduct = item;
                }
            }
            

            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    foreach(var item in _context.ProductFabricQuantity)
                    {
                        if(item.ProductId == product.Id)
                        {
                            if (product.Quantity - oldProduct.Quantity > 0)
                            {
                                var temp = item.QtyFabricOnProduct * (product.Quantity - oldProduct.Quantity);
                                foreach (var fabric in _context.Fabric)
                                {
                                    if (fabric.Id == item.FabricId)
                                    {
                                        var temp2 = fabric.Quantity - temp;
                                        if (temp2 >= 0)
                                        {
                                            fabricDict.Add(fabric.Id, temp2);
                                        }
                                        else
                                        {
                                            exceptionDict.Add(fabric.Title, "Not enough supply to make product.");
                                        }
                                    }
                                }
                            }else
                            {
                                exceptionDict.Add(product.Title, "Product quantity can only decrease when its added to order.");
                            }
                        }
                    }
                    foreach (var item in _context.ProductNotionQuantity)
                    {
                        if (item.ProductId == product.Id)
                        {
                            if (product.Quantity - oldProduct.Quantity > 0)
                            {
                                var temp = item.QtyNotionOnProduct * (product.Quantity - oldProduct.Quantity);
                                foreach (var notion in _context.Notion)
                                {
                                    if (notion.Id == item.NotionId)
                                    {
                                        var temp2 = notion.Quantity - temp;
                                        if (temp2 >= 0)
                                        {
                                            notionDict.Add(notion.Id, temp2);
                                        }
                                        else
                                        {
                                            exceptionDict.Add(notion.Title, "Not enough supply to make product.");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    foreach (var item in _context.ProductLabelQuantity)
                    {
                        if (item.ProductId == product.Id)
                        {
                            if (product.Quantity - oldProduct.Quantity > 0)
                            {
                                var temp = item.QtyLabelOnProduct * (product.Quantity - oldProduct.Quantity);
                                foreach (var label in _context.Label)
                                {
                                    if (label.Id == item.LabelId)
                                    {
                                        var temp2 = label.Quantity - temp;
                                        if (temp2 >= 0)
                                        {
                                            labelDict.Add(label.Id, temp2);
                                        }
                                        else
                                        {
                                            exceptionDict.Add(label.Title, "Not enough supply to make product.");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    foreach (var item in _context.ProductTagQuantity)
                    {
                        if (item.ProductId == product.Id)
                        {
                            if (product.Quantity - oldProduct.Quantity > 0)
                            {
                                var temp = item.QtyTagOnProduct * (product.Quantity - oldProduct.Quantity);
                                foreach (var tag in _context.Fabric)
                                {
                                    if (tag.Id == item.TagId)
                                    {
                                        var temp2 = tag.Quantity - temp;
                                        if (temp2 >= 0)
                                        {
                                            tagDict.Add(tag.Id, temp2);
                                        }
                                        else
                                        {
                                            exceptionDict.Add(tag.Title, "Not enough supply to make product.");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if(exceptionDict.Count > 0)
                    {
                        string errorStr = "";
                        foreach(var item in exceptionDict.Keys)
                        {
                            errorStr += item + " -- " + exceptionDict[item] + "&&";
                        }
                        return RedirectToAction("Error", new { prodId = product.Id, exceptStr = errorStr.Substring(0,errorStr.Length - 2) });
                    }
                    else
                    {
                        oldProduct.Quantity = product.Quantity;
                        _context.Update(oldProduct);
                        await _context.SaveChangesAsync();
                        //actions dictionary: key: title && value:action
                        Dictionary<string, string> actionDict = new Dictionary<string, string>();
                        actionDict.Add(oldProduct.Title, "Product quantity has been updated to " + oldProduct.Quantity);
                        foreach(var item in fabricDict.Keys)
                        {
                            foreach(var fabric in _context.Fabric.ToList())
                            {
                                if(item == fabric.Id)
                                {
                                    fabric.Quantity = fabricDict[item];
                                    _context.Fabric.Update(fabric);
                                    await _context.SaveChangesAsync();
                                    if (fabric.Quantity <= fabric.MinThreshold)
                                    {
                                        actionDict.Add(fabric.Title, "Fabric quantity updated, but stock is low: " + fabric.Quantity);
                                    }else
                                    {
                                        actionDict.Add(fabric.Title, "Updated quantity is now at: " + fabric.Quantity);
                                    }
                                }
                            }
                        }
                        foreach (var item in notionDict.Keys)
                        {
                            foreach (var notion in _context.Notion.ToList())
                            {
                                if (item == notion.Id)
                                {
                                    notion.Quantity = notionDict[item];
                                    _context.Update(notion);
                                    await _context.SaveChangesAsync();
                                    if (notion.Quantity <= notion.MinThreshold)
                                    {
                                        actionDict.Add(notion.Title, "Fabric quantity updated, but stock is low: " + notion.Quantity);
                                    }
                                    else
                                    {
                                        actionDict.Add(notion.Title, "Updated quantity is now at: " + notion.Quantity);
                                    }
                                }
                            }
                        }
                        foreach (var item in labelDict.Keys)
                        {
                            foreach (var label in _context.Label.ToList())
                            {
                                if (item == label.Id)
                                {
                                    label.Quantity = labelDict[item];
                                    _context.Update(label);
                                    await _context.SaveChangesAsync();
                                    if (label.Quantity <= label.MinThreshold)
                                    {
                                        actionDict.Add(label.Title, "Fabric quantity updated, but stock is low: " + label.Quantity);
                                    }
                                    else
                                    {
                                        actionDict.Add(label.Title, "Updated quantity is now at: " + label.Quantity);
                                    }
                                }
                            }
                        }
                        foreach (var item in tagDict.Keys)
                        {
                            foreach (var tag in _context.Tag.ToList())
                            {
                                if (item == tag.Id)
                                {
                                    tag.Quantity = tagDict[item];
                                    _context.Update(tag);
                                    await _context.SaveChangesAsync();
                                    if (tag.Quantity <= tag.MinThreshold)
                                    {
                                        actionDict.Add(tag.Title, "Fabric quantity updated, but stock is low: " + tag.Quantity);
                                    }
                                    else
                                    {
                                        actionDict.Add(tag.Title, "Updated quantity is now at: " + tag.Quantity);
                                    }
                                }
                            }
                        }
                        string actionStr = "";
                        foreach (var item in actionDict.Keys)
                        {
                            actionStr += item + " -- " + actionDict[item] + "&&";
                        }
                        return RedirectToAction("Success", new { prodId = product.Id, actionStr = actionStr.Substring(0, actionStr.Length - 2) });
                    }

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

             return RedirectToAction("Details", new { id = product.Id });

            }
            ViewData["CollectionId"] = new SelectList(_context.Collection, "Id", "Id", product.CollectionId);
            ViewData["StyleId"] = new SelectList(_context.Style, "Id", "Id", product.StyleId);
            ViewData["VariationId"] = new SelectList(_context.Variation, "Id", "Id", product.VariationId);
            return View(product);
        }
        public IActionResult Success(int prodId,string actionStr)
        {
            string[] tempArray = actionStr.Split(new string[] { "&&" }, StringSplitOptions.None);
            Product tempProd = new Product();
            foreach(var item in _context.Product)
            {
                if(item.Id == prodId)
                {
                    tempProd = item;
                }
            }
            var model = new ExceptionViewModel
            {
                Product = tempProd,
                errors = tempArray
            };
            return View(model);
        }
        public IActionResult Error(int prodId,string exceptStr)
        {
            string[] tempArray = exceptStr.Split(new string[] { "&&" }, StringSplitOptions.None);
            Product tempProd = new Product();
            foreach(var item in _context.Product)
            {
                if(item.Id == prodId)
                {
                    tempProd = item;
                }
            }
            var model = new ExceptionViewModel
            {
                Product = tempProd,
                errors = tempArray
            };
            return View(model);
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
