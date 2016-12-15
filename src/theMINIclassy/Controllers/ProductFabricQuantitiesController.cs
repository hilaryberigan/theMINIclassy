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
using Microsoft.Extensions.Logging;
using NLog;
using Microsoft.AspNetCore.Identity;

namespace theMINIclassy.Controllers
{
    public class ProductFabricQuantitiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly UserManager<ApplicationUser> _userManger;

        public ProductFabricQuantitiesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManger = userManager;
        }
        [Authorize]
        // GET: ProductFabricQuantities
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductFabricQuantity.Include(p => p.Fabric).Include(p => p.Product);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize]
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
        [Authorize]
        // GET: ProductFabricQuantities/Create
        public IActionResult Create(int? id)
        {
            ViewData["FabricId"] = new SelectList(_context.Fabric, "Id", "Title");
            ViewData["ProductId"] = id;
            return View();
        }
        [Authorize]
        // POST: ProductFabricQuantities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FabricId,ProductId,QtyFabricOnProduct")] ProductFabricQuantity productFabricQuantity)
        {
            var user = _userManger.GetUserName(HttpContext.User);
            var x = productFabricQuantity.Id;
            productFabricQuantity.ProductId = x;
            productFabricQuantity.Id = 0;
            var productName = "";
            var productQuantity = 0;
            var fabricName = "";
            decimal fabricQuantity = 0;
            foreach(var item in _context.Product)
            {
                if(productFabricQuantity.ProductId == item.Id)
                {
                    productName = item.Title;
                    productQuantity = item.Quantity;
                }
            }
            foreach(var item in _context.Fabric)
            {
                if(productFabricQuantity.FabricId == item.Id)
                {
                    fabricName = item.Title;
                    fabricQuantity = item.Quantity;
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(productFabricQuantity);
                await _context.SaveChangesAsync();
                logger.Info(user + " created " + productName + " with quantity of " + productQuantity + " and " + fabricName + " with quantity of " + fabricQuantity);
                return RedirectToAction("Index");
            }else
            {
                _context.Add(productFabricQuantity);
                await _context.SaveChangesAsync();
                logger.Info(user + " created " + productName + " with quantity of " + productQuantity + " and " + fabricName + " with quantity of " + fabricQuantity);
                return RedirectToAction("Details", "Products", new { id = productFabricQuantity.ProductId });
            }
            ViewData["FabricId"] = new SelectList(_context.Fabric, "Id", "Title", productFabricQuantity.FabricId);
            ViewData["ProductId"] = productFabricQuantity.ProductId;
            return View(productFabricQuantity);
        }
        [Authorize]
        // GET: ProductFabricQuantities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productFabricQuantity = await _context.ProductFabricQuantity.SingleOrDefaultAsync(m => m.Id == id);
            var productName = "";
            var productQuantity = 0;
            var fabricName = "";
            decimal fabricQuantity = 0;
            foreach (var item in _context.Product)
            {
                if (productFabricQuantity.ProductId == item.Id)
                {
                    productName = item.Title;
                    productQuantity = item.Quantity;
                }
            }
            foreach (var item in _context.Fabric)
            {
                if (productFabricQuantity.FabricId == item.Id)
                {
                    fabricName = item.Title;
                    fabricQuantity = item.Quantity;
                }
            }
            logger.Info("Current " + productName + " quantity " + productQuantity + " and " + fabricName + " quantity " + fabricQuantity);
            if (productFabricQuantity == null)
            {
                return NotFound();
            }
            ViewData["FabricId"] = new SelectList(_context.Fabric, "Id", "Id", productFabricQuantity.FabricId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", productFabricQuantity.ProductId);
            return View(productFabricQuantity);
        }
        [Authorize]
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
                    var user = _userManger.GetUserName(HttpContext.User);
                    var productName = "";
                    var productQuantity = 0;
                    var fabricName = "";
                    decimal fabricQuantity = 0;
                    foreach (var item in _context.Product)
                    {
                        if (productFabricQuantity.ProductId == item.Id)
                        {
                            productName = item.Title;
                            productQuantity = item.Quantity;
                        }
                    }
                    foreach (var item in _context.Fabric)
                    {
                        if (productFabricQuantity.FabricId == item.Id)
                        {
                            fabricName = item.Title;
                            fabricQuantity = item.Quantity;
                        }
                    }
                    _context.Update(productFabricQuantity);
                    await _context.SaveChangesAsync();
                    logger.Info(user + " edited to " + productName + " quantity " + productQuantity + " and " + fabricName + " quantity " + fabricQuantity);
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
        [Authorize]
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
        [Authorize]
        // POST: ProductFabricQuantities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productFabricQuantity = await _context.ProductFabricQuantity.SingleOrDefaultAsync(m => m.Id == id);
            var user = _userManger.GetUserName(HttpContext.User);
            var productName = "";
            var productQuantity = 0;
            var fabricName = "";
            decimal fabricQuantity = 0;
            foreach (var item in _context.Product)
            {
                if (productFabricQuantity.ProductId == item.Id)
                {
                    productName = item.Title;
                    productQuantity = item.Quantity;
                }
            }
            foreach (var item in _context.Fabric)
            {
                if (productFabricQuantity.FabricId == item.Id)
                {
                    fabricName = item.Title;
                    fabricQuantity = item.Quantity;
                }
            }
            logger.Info(user + " deleted " + productName + " and " + fabricName);
            var productId = productFabricQuantity.ProductId;
            _context.ProductFabricQuantity.Remove(productFabricQuantity);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details","Products", new { id = productId });
        }
        [Authorize]
        private bool ProductFabricQuantityExists(int id)
        {
            return _context.ProductFabricQuantity.Any(e => e.Id == id);
        }
    }
}
