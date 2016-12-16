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
    public class ProductTagQuantitiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly UserManager<ApplicationUser> _userManger;

        public ProductTagQuantitiesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManger = userManager;
        }
        [Authorize]
        // GET: ProductTagQuantities
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductTagQuantity.Include(p => p.Product).Include(p => p.Tag);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize]
        // GET: ProductTagQuantities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productTagQuantity = await _context.ProductTagQuantity.SingleOrDefaultAsync(m => m.Id == id);
            if (productTagQuantity == null)
            {
                return NotFound();
            }

            return View(productTagQuantity);
        }
        [Authorize]
        // GET: ProductTagQuantities/Create
        public IActionResult Create(int? id)
        {
            ViewData["ProductId"] = id;
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Title");
            return View();
        }
        [Authorize]
        // POST: ProductTagQuantities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,QtyTagOnProduct,TagId")] ProductTagQuantity productTagQuantity)
        {
            productTagQuantity.ProductId = productTagQuantity.Id;
            productTagQuantity.Id = 0;
            var user = _userManger.GetUserName(HttpContext.User);
            var productName = "";
            var productQuantity = 0;
            var tagName = "";
            decimal tagQuantity = 0;
            foreach(var item in _context.Product)
            {
                if(productTagQuantity.ProductId == item.Id)
                {
                    productName = item.Title;
                    productQuantity = item.Quantity;
                }
            }
            foreach(var item in _context.Tag)
            {
                if(productTagQuantity.TagId == item.Id)
                {
                    tagName = item.Title;
                    tagQuantity = item.Quantity;
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(productTagQuantity);
                await _context.SaveChangesAsync();
                logger.Info(user + " created " + productName + " with quantity of " + productQuantity + " and " + tagName + " with quantity of " + tagQuantity);
                return RedirectToAction("Index");
            }
            else
            {
                _context.Add(productTagQuantity);
                await _context.SaveChangesAsync();
                logger.Info(user + " created " + productName + " with quantity of " + productQuantity + " and " + tagName + " with quantity of " + tagQuantity);
                return RedirectToAction("Details", "Products", new { id = productTagQuantity.ProductId });
            }
            ViewData["ProductId"] = productTagQuantity.ProductId;
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Title", productTagQuantity.TagId);
            return View(productTagQuantity);
        }
        [Authorize]
        // GET: ProductTagQuantities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productTagQuantity = await _context.ProductTagQuantity.SingleOrDefaultAsync(m => m.Id == id);
            var productName = "";
            var productQuantity = 0;
            var tagName = "";
            decimal tagQuantity = 0;
            foreach (var item in _context.Product)
            {
                if (productTagQuantity.ProductId == item.Id)
                {
                    productName = item.Title;
                    productQuantity = item.Quantity;
                }
            }
            foreach (var item in _context.Tag)
            {
                if (productTagQuantity.TagId == item.Id)
                {
                    tagName = item.Title;
                    tagQuantity = item.Quantity;
                }
            }
            logger.Info("Current " + productName + " with quantity of " + productQuantity + " and " + tagName + " with quantity of " + tagQuantity);
            if (productTagQuantity == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", productTagQuantity.ProductId);
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Id", productTagQuantity.TagId);
            return View(productTagQuantity);
        }
        [Authorize]
        // POST: ProductTagQuantities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,QtyTagOnProduct,TagId")] ProductTagQuantity productTagQuantity)
        {
            if (id != productTagQuantity.Id)
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
                    var tagName = "";
                    decimal tagQuantity = 0;
                    foreach (var item in _context.Product)
                    {
                        if (productTagQuantity.ProductId == item.Id)
                        {
                            productName = item.Title;
                            productQuantity = item.Quantity;
                        }
                    }
                    foreach (var item in _context.Tag)
                    {
                        if (productTagQuantity.TagId == item.Id)
                        {
                            tagName = item.Title;
                            tagQuantity = item.Quantity;
                        }
                    }
                    _context.Update(productTagQuantity);
                    await _context.SaveChangesAsync();
                    logger.Info(user + " edited to " + productName + " with quantity of " + productQuantity + " and " + tagName + " with quantity of " + tagQuantity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductTagQuantityExists(productTagQuantity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Products", new { id = productTagQuantity.ProductId });
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", productTagQuantity.ProductId);
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Id", productTagQuantity.TagId);
            return View(productTagQuantity);
        }
        [Authorize]
        // GET: ProductTagQuantities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productTagQuantity = await _context.ProductTagQuantity.SingleOrDefaultAsync(m => m.Id == id);
            if (productTagQuantity == null)
            {
                return NotFound();
            }

            return View(productTagQuantity);
        }
        [Authorize]
        // POST: ProductTagQuantities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productTagQuantity = await _context.ProductTagQuantity.SingleOrDefaultAsync(m => m.Id == id);
            var productId = productTagQuantity.ProductId;
            var user = _userManger.GetUserName(HttpContext.User);
            var productName = "";
            var productQuantity = 0;
            var tagName = "";
            decimal tagQuantity = 0;
            foreach (var item in _context.Product)
            {
                if (productTagQuantity.ProductId == item.Id)
                {
                    productName = item.Title;
                    productQuantity = item.Quantity;
                }
            }
            foreach (var item in _context.Tag)
            {
                if (productTagQuantity.TagId == item.Id)
                {
                    tagName = item.Title;
                    tagQuantity = item.Quantity;
                }
            }
            logger.Info(user + " deleted " + productName + " and " + tagName);
            _context.ProductTagQuantity.Remove(productTagQuantity);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Products", new { id = productId });
        }
        [Authorize]
        private bool ProductTagQuantityExists(int id)
        {
            return _context.ProductTagQuantity.Any(e => e.Id == id);
        }
    }
}
