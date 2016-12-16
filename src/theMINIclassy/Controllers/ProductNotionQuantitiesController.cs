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
    public class ProductNotionQuantitiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly UserManager<ApplicationUser> _userManger;

        public ProductNotionQuantitiesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManger = userManager;
        }
        [Authorize]
        // GET: ProductNotionQuantities
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductNotionQuantity.Include(p => p.Notion).Include(p => p.Product);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize]
        // GET: ProductNotionQuantities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productNotionQuantity = await _context.ProductNotionQuantity.SingleOrDefaultAsync(m => m.Id == id);
            if (productNotionQuantity == null)
            {
                return NotFound();
            }

            return View(productNotionQuantity);
        }
        [Authorize]
        // GET: ProductNotionQuantities/Create
        public IActionResult Create(int? id)
        {
            ViewData["NotionId"] = new SelectList(_context.Notion, "Id", "Title");
            ViewData["ProductId"] = id;
            return View();
        }
        [Authorize]
        // POST: ProductNotionQuantities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NotionId,ProductId,QtyNotionOnProduct")] ProductNotionQuantity productNotionQuantity)
        {
            productNotionQuantity.ProductId = productNotionQuantity.Id;
            productNotionQuantity.Id = 0;
                var user = _userManger.GetUserName(HttpContext.User);
                var productName = "";
                var productQuantity = 0;
                var notionName = "";
                decimal notionQuantity = 0;
                foreach(var item in _context.Product)
                {
                    if(productNotionQuantity.ProductId == item.Id)
                    {
                        productName = item.Title;
                        productQuantity = item.Quantity;
                    }
                }
                foreach(var item in _context.Notion)
                {
                    if(productNotionQuantity.NotionId == item.Id)
                    {
                        notionName = item.Title;
                        notionQuantity = item.Quantity;
                    }
                }
            if (ModelState.IsValid)
            {
                _context.Add(productNotionQuantity);
                await _context.SaveChangesAsync();
                logger.Info(user + " created " + productName + " with quantity of " + productQuantity + " and " + notionName + " with quantity of " + notionQuantity);
                return RedirectToAction("Index");
            }
            else
            {
                _context.Add(productNotionQuantity);
                await _context.SaveChangesAsync();
                logger.Info(user + " created " + productName + " with quantity of " + productQuantity + " and " + notionName + " with quantity of " + notionQuantity);
                return RedirectToAction("Details", "Products", new { id = productNotionQuantity.ProductId });
            }
            ViewData["NotionId"] = new SelectList(_context.Notion, "Id", "Title", productNotionQuantity.NotionId);
            ViewData["ProductId"] = productNotionQuantity.ProductId;
            return View(productNotionQuantity);
        }
        [Authorize]
        // GET: ProductNotionQuantities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productNotionQuantity = await _context.ProductNotionQuantity.SingleOrDefaultAsync(m => m.Id == id);
            var productName = "";
            var productQuantity = 0;
            var notionName = "";
            decimal notionQuantity = 0;
            foreach (var item in _context.Product)
            {
                if (productNotionQuantity.ProductId == item.Id)
                {
                    productName = item.Title;
                    productQuantity = item.Quantity;
                }
            }
            foreach (var item in _context.Notion)
            {
                if (productNotionQuantity.NotionId == item.Id)
                {
                    notionName = item.Title;
                    notionQuantity = item.Quantity;
                }
            }
            logger.Info("Current " + productName + " with quantity of " + productQuantity + " and " + notionName + " with quantity of " + notionQuantity);
            if (productNotionQuantity == null)
            {
                return NotFound();
            }
            ViewData["NotionId"] = new SelectList(_context.Notion, "Id", "Id", productNotionQuantity.NotionId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", productNotionQuantity.ProductId);
            return View(productNotionQuantity);
        }
        [Authorize]
        // POST: ProductNotionQuantities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NotionId,ProductId,QtyNotionOnProduct")] ProductNotionQuantity productNotionQuantity)
        {
            if (id != productNotionQuantity.Id)
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
                    var notionName = "";
                    decimal notionQuantity = 0;
                    foreach (var item in _context.Product)
                    {
                        if (productNotionQuantity.ProductId == item.Id)
                        {
                            productName = item.Title;
                            productQuantity = item.Quantity;
                        }
                    }
                    foreach (var item in _context.Notion)
                    {
                        if (productNotionQuantity.NotionId == item.Id)
                        {
                            notionName = item.Title;
                            notionQuantity = item.Quantity;
                        }
                    }
                    _context.Update(productNotionQuantity);
                    await _context.SaveChangesAsync();
                    logger.Info(user + " edited to " + productName + " with quantity of " + productQuantity + " and " + notionName + " with quantity of " + notionQuantity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductNotionQuantityExists(productNotionQuantity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Products", new { id = productNotionQuantity.ProductId });
            }
            ViewData["NotionId"] = new SelectList(_context.Notion, "Id", "Id", productNotionQuantity.NotionId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", productNotionQuantity.ProductId);
            return View(productNotionQuantity);
        }
        [Authorize]
        // GET: ProductNotionQuantities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productNotionQuantity = await _context.ProductNotionQuantity.SingleOrDefaultAsync(m => m.Id == id);
            if (productNotionQuantity == null)
            {
                return NotFound();
            }

            return View(productNotionQuantity);
        }
        [Authorize]
        // POST: ProductNotionQuantities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productNotionQuantity = await _context.ProductNotionQuantity.SingleOrDefaultAsync(m => m.Id == id);
            var productId = productNotionQuantity.ProductId;
            var user = _userManger.GetUserName(HttpContext.User);
            var productName = "";
            var productQuantity = 0;
            var notionName = "";
            decimal notionQuantity = 0;
            foreach (var item in _context.Product)
            {
                if (productNotionQuantity.ProductId == item.Id)
                {
                    productName = item.Title;
                    productQuantity = item.Quantity;
                }
            }
            foreach (var item in _context.Notion)
            {
                if (productNotionQuantity.NotionId == item.Id)
                {
                    notionName = item.Title;
                    notionQuantity = item.Quantity;
                }
            }
            logger.Info(user + " deleted " + productName + " and " + notionName);
            _context.ProductNotionQuantity.Remove(productNotionQuantity);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Products", new { id = productId });
        }
        [Authorize]
        private bool ProductNotionQuantityExists(int id)
        {
            return _context.ProductNotionQuantity.Any(e => e.Id == id);
        }
    }
}
