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
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;    
        }
        [Authorize]
        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Order.Include(o => o.Customer);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize]
        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            List<ProductQuantity> ProdQuants = new List<ProductQuantity>();
            foreach (var item in _context.ProductQuantity.Where(x => x.Order.Id == id).ToList())
            {
                ProdQuants.Add(item);
            }

            var model = new OrderViewModel
            {
                Order = order,
                PQuantities = ProdQuants,            
            };

            return View(model);
        }
        [Authorize]
        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id");
            return View();
        }
        [Authorize]
        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,OrderDate,OrderNumber,OrderStatus,OriginatedFrom")] Order order)
        {
            order.OrderDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Orders", new { id = order.Id });
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id", order.CustomerId);
            return View(order);
        }
        [Authorize]
        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id, List<Product> conflictProds)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            List <Product> noConflicts = new List<Product>();
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id", order.CustomerId);
            ViewData["ConflictProds"] = conflictProds.Any() ? conflictProds : noConflicts;

            return View(order);
        }

        public async Task<List<Product>> UpdateProducts( Order order)
        {
            var prodOrd = _context.ProductQuantity.Where(x => x.Order.Id == order.Id).ToList();
            List<Product> productsOnOrder = new List<Product>();
            List<Product> conflictProds = new List<Product>();
            foreach (var item in prodOrd)
            {
                productsOnOrder.Add(item.Product);
                var savedProduct = _context.Product.Where(x => x.Id == item.Product.Id).FirstOrDefault();
                if (item.QtyProductOnOrder > savedProduct.Quantity)
                {
                    conflictProds.Add(item.Product);
                }
                else
                {
                    savedProduct.Quantity -= item.QtyProductOnOrder;
                    if (ModelState.IsValid)
                    {
                       _context.Update(savedProduct);
                       await _context.SaveChangesAsync();
                    }
                }
            }

            return conflictProds;
        }

        [Authorize]
        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,OrderDate,OrderNumber,OrderStatus,OriginatedFrom")] Order order)
        {

            List<Product> conflictProds = await UpdateProducts(order);
            if (conflictProds.Any())
            {
                return RedirectToAction("Edit", conflictProds);
            }


            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id", order.CustomerId);
            return View(order);
        }
        [Authorize]
        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        [Authorize]
        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.SingleOrDefaultAsync(m => m.Id == id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize]
        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
