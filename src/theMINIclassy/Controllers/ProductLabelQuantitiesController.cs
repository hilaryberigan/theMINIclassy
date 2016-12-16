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
    public class ProductLabelQuantitiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly UserManager<ApplicationUser> _userManger;

        public ProductLabelQuantitiesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManger = userManager;
        }
        [Authorize]
        // GET: ProductLabelQuantities
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductLabelQuantity.Include(p => p.Label).Include(p => p.Product);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize]
        // GET: ProductLabelQuantities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productLabelQuantity = await _context.ProductLabelQuantity.SingleOrDefaultAsync(m => m.Id == id);
            if (productLabelQuantity == null)
            {
                return NotFound();
            }

            return View(productLabelQuantity);
        }
        [Authorize]
        // GET: ProductLabelQuantities/Create
        public IActionResult Create(int? id)
        {
            ViewData["LabelId"] = new SelectList(_context.Label, "Id", "Title");
            ViewData["ProductId"] = id;
            return View();
        }
        [Authorize]
        // POST: ProductLabelQuantities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LabelId,ProductId,QtyLabelOnProduct")] ProductLabelQuantity productLabelQuantity)
        {
            var user = _userManger.GetUserName(HttpContext.User);
            productLabelQuantity.ProductId = productLabelQuantity.Id;
            productLabelQuantity.Id = 0;
            var productName = "";
            var productQuantity = 0;
            var labelName = "";
            decimal labelQuantity = 0;
            foreach(var item in _context.Product)
            {
                if(productLabelQuantity.ProductId == item.Id)
                {
                    productName = item.Title;
                    productQuantity = item.Quantity;
                }
            }
            foreach(var item in _context.Label)
            {
                if(productLabelQuantity.LabelId == item.Id)
                {
                    labelName = item.Title;
                    labelQuantity = item.Quantity;
                }
            }
            
            if (ModelState.IsValid)
            {
                _context.Add(productLabelQuantity);
                await _context.SaveChangesAsync();
                logger.Info(user + " created " + productName + " with quantity of " + productQuantity + " and " + labelName + " with quantity of " + labelQuantity);
                return RedirectToAction("Index");
            }
            else
            {
                _context.Add(productLabelQuantity);
                await _context.SaveChangesAsync();
                logger.Info(user + " created " + productName + " with quantity of " + productQuantity + " and " + labelName + " with quantity of " + labelQuantity);
                return RedirectToAction("Details", "Products", new { id = productLabelQuantity.ProductId });
            }
            ViewData["LabelId"] = new SelectList(_context.Label, "Id", "Title", productLabelQuantity.LabelId);
            ViewData["ProductId"] = productLabelQuantity.ProductId;
            return View(productLabelQuantity);
        }
        [Authorize]
        // GET: ProductLabelQuantities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productLabelQuantity = await _context.ProductLabelQuantity.SingleOrDefaultAsync(m => m.Id == id);
            var productName = "";
            var productQuantity = 0;
            var labelName = "";
            decimal labelQuantity = 0;
            foreach (var item in _context.Product)
            {
                if (productLabelQuantity.ProductId == item.Id)
                {
                    productName = item.Title;
                    productQuantity = item.Quantity;
                }
            }
            foreach (var item in _context.Label)
            {
                if (productLabelQuantity.LabelId == item.Id)
                {
                    labelName = item.Title;
                    labelQuantity = item.Quantity;
                }
            }
            logger.Info("Current " + productName + " with quantity of " + productQuantity + " and " + labelName + " with quantity of " + labelQuantity);
            if (productLabelQuantity == null)
            {
                return NotFound();
            }
            ViewData["LabelId"] = new SelectList(_context.Label, "Id", "Id", productLabelQuantity.LabelId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", productLabelQuantity.ProductId);
            return View(productLabelQuantity);
        }
        [Authorize]
        // POST: ProductLabelQuantities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LabelId,ProductId,QtyLabelOnProduct")] ProductLabelQuantity productLabelQuantity)
        {
            if (id != productLabelQuantity.Id)
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
                    var labelName = "";
                    decimal labelQuantity = 0;
                    foreach (var item in _context.Product)
                    {
                        if (productLabelQuantity.ProductId == item.Id)
                        {
                            productName = item.Title;
                            productQuantity = item.Quantity;
                        }
                    }
                    foreach (var item in _context.Label)
                    {
                        if (productLabelQuantity.LabelId == item.Id)
                        {
                            labelName = item.Title;
                            labelQuantity = item.Quantity;
                        }
                    }
                    logger.Info(user + " edited to " + productName + " with quantity of " + productQuantity + " and " + labelName + " with quantity of " + labelQuantity);
                    _context.Update(productLabelQuantity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductLabelQuantityExists(productLabelQuantity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Products", new { id = productLabelQuantity.ProductId });
            }
            ViewData["LabelId"] = new SelectList(_context.Label, "Id", "Id", productLabelQuantity.LabelId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", productLabelQuantity.ProductId);
            return View(productLabelQuantity);
        }
        [Authorize]
        // GET: ProductLabelQuantities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productLabelQuantity = await _context.ProductLabelQuantity.SingleOrDefaultAsync(m => m.Id == id);
            if (productLabelQuantity == null)
            {
                return NotFound();
            }

            return View(productLabelQuantity);
        }
        [Authorize]
        // POST: ProductLabelQuantities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productLabelQuantity = await _context.ProductLabelQuantity.SingleOrDefaultAsync(m => m.Id == id);
            var productId = productLabelQuantity.ProductId;
            var user = _userManger.GetUserName(HttpContext.User);
            var productName = "";
            var productQuantity = 0;
            var labelName = "";
            decimal labelQuantity = 0;
            foreach (var item in _context.Product)
            {
                if (productLabelQuantity.ProductId == item.Id)
                {
                    productName = item.Title;
                    productQuantity = item.Quantity;
                }
            }
            foreach (var item in _context.Label)
            {
                if (productLabelQuantity.LabelId == item.Id)
                {
                    labelName = item.Title;
                    labelQuantity = item.Quantity;
                }
            }
            logger.Info(user + " deleted " + productName + " and " + labelName);
            _context.ProductLabelQuantity.Remove(productLabelQuantity);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Products", new { id = productId });
        }
        [Authorize]
        private bool ProductLabelQuantityExists(int id)
        {
            return _context.ProductLabelQuantity.Any(e => e.Id == id);
        }
    }
}
