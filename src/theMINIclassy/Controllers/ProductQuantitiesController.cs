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
                Product newProd = new Product();
                foreach(var product in _context.Product.ToList())
                {
                    if(product.Id == productQuantity.ProductId)
                    {
                        int temp = product.Quantity - productQuantity.QtyProductOnOrder;
                        if(temp >= 0)
                        {
                            newProd = product;
                            newProd.Quantity = temp;
                            ID = product.Id;
                            _context.Update(newProd);
                            await _context.SaveChangesAsync();
                            _context.Add(productQuantity);
                            await _context.SaveChangesAsync();
                            actionDict.Add(product.Title, "Product quantity decreased to " + product.Quantity + "&&");
                            
                            if(newProd.MinThreshold >= newProd.Quantity)
                            {
                                actionDict.Add(Convert.ToString(newProd.Id) + " >> " + newProd.Title, "Product has reached or passed minimum threshold of " + newProd.MinThreshold + " and this product's quantity is now at " + newProd.Quantity + "&&");
                            }
                        }else
                        {
                            actionDict.Add(product.Title, "Not enough of this product to make order. Product quantity not updated and product not added to order.&&");
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
            List<string> tempList = actionStr.Split(new string[] { "&&" }, StringSplitOptions.None).ToList();
            if(tempList[tempList.Count - 1].Length == 0)
            {
                tempList.RemoveAt(tempList.Count - 1);
            }
            string[] tempArray = tempList.ToArray();
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
            return View(model);
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
            return RedirectToAction("Index","Orders");
        }

        private bool ProductQuantityExists(int id)
        {
            return _context.ProductQuantity.Any(e => e.Id == id);
        }
    }
}
