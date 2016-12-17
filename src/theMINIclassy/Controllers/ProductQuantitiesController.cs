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
    public class ProductQuantitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductQuantitiesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: ProductQuantities
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductQuantity.Include(p => p.Order).Include(p => p.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductQuantities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productQuantity = await _context.ProductQuantity.SingleOrDefaultAsync(m => m.Id == id);
            if (productQuantity == null)
            {
                return NotFound();
            }

            return View(productQuantity);
        }

        // GET: ProductQuantities/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Order, "Id", "OrderNumber");
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Title");
            return View();
        }

        // POST: ProductQuantities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderId,ProductId,QtyProductOnOrder")] ProductQuantity productQuantity)
        {
            productQuantity.OrderId = productQuantity.Id;
            productQuantity.Id = 0;
            if (ModelState.IsValid)
            {
                _context.Add(productQuantity);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }else
            {
                Dictionary<string, string> actionDict = new Dictionary<string, string>();
                int ID = 0;
                foreach(var item in _context.Product.ToList())
                {
                    if(item.Id == productQuantity.ProductId)
                    {
                        int temp = item.Quantity - productQuantity.QtyProductOnOrder;
                        if(temp > 0)
                        {
                            item.Quantity = temp;
                            ID = item.Id;
                            _context.Add(item);
                            await _context.SaveChangesAsync();
                            _context.Add(productQuantity);
                            await _context.SaveChangesAsync();
                            actionDict.Add(item.Title, "Product quantity decreased to " + item.Quantity + "&&");
                        }else
                        {
                            actionDict.Add(item.Title, "Not enough of this product to make order. Product quantity not updated and product not added to order.&&");
                        }
                    }
                }
                string temp2 = "";
                foreach(var item in actionDict.Keys)
                {
                    temp2 += item + " - " + actionDict[item];
                }
                return RedirectToAction("Result",new { id = ID, actionStr = temp2 });
            }
            ViewData["OrderId"] = new SelectList(_context.Order, "Id", "OrderNumber", productQuantity.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Title", productQuantity.ProductId);
            return View(productQuantity);
        }
        public IActionResult Result(int id,string actionStr)
        {
            Product tempProd = new Product();
            string[] tempArray = actionStr.Split(new string[] { "&&" }, StringSplitOptions.None);
            foreach (var item in _context.Product.ToList())
            {
                if(id == item.Id)
                {
                    tempProd = item;
                }
            }
            var model = new ExceptionViewModel
            {
                errors = tempArray,
                Product = tempProd
            };
            return View();
        }
        // GET: ProductQuantities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productQuantity = await _context.ProductQuantity.SingleOrDefaultAsync(m => m.Id == id);
            if (productQuantity == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Order, "Id", "OrderNumber", productQuantity.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Title", productQuantity.ProductId);
            return View(productQuantity);
        }

        // POST: ProductQuantities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderId,ProductId,QtyProductOnOrder")] ProductQuantity productQuantity)
        {
            if (id != productQuantity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productQuantity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductQuantityExists(productQuantity.Id))
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
            ViewData["OrderId"] = new SelectList(_context.Order, "Id", "Id", productQuantity.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", productQuantity.ProductId);
            return View(productQuantity);
        }

        // GET: ProductQuantities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productQuantity = await _context.ProductQuantity.SingleOrDefaultAsync(m => m.Id == id);
            if (productQuantity == null)
            {
                return NotFound();
            }

            return View(productQuantity);
        }

        // POST: ProductQuantities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productQuantity = await _context.ProductQuantity.SingleOrDefaultAsync(m => m.Id == id);
            _context.ProductQuantity.Remove(productQuantity);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProductQuantityExists(int id)
        {
            return _context.ProductQuantity.Any(e => e.Id == id);
        }
    }
}