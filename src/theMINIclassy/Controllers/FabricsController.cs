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
    public class FabricsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly UserManager<ApplicationUser> _userManger;


        public FabricsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManger = userManager;
        }
        [Authorize]
        // GET: Fabrics
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fabric.ToListAsync());
        }
        [Authorize]
        // GET: Fabrics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fabric = await _context.Fabric.SingleOrDefaultAsync(m => m.Id == id);
            if (fabric == null)
            {
                return NotFound();
            }

            return View(fabric);
        }
        [Authorize]
        // GET: Fabrics/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        // POST: Fabrics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Content,Description,LastUpdated,MinThreshold,Quantity,Title")] Fabric fabric)
        {
            var user = _userManger.GetUserName(HttpContext.User);
            fabric.LastUpdated = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(fabric);
                await _context.SaveChangesAsync();
                logger.Info(user + " Created new Fabric called: " + fabric.Title);
                return RedirectToAction("Index");
            }
            return View(fabric);
        }
        [Authorize]
        public async Task<IActionResult> EditQuantity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var fabric = await _context.Fabric.SingleOrDefaultAsync(m => m.Id == id);
            logger.Info("Current quantity of " + fabric.Title + ": " + fabric.Quantity);
            if (fabric == null)
            {
                return NotFound();
            }
            return View(fabric);
        }
        [Authorize]
        // POST: Fabrics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditQuantity(int id, [Bind("Id, Quantity")] Fabric fabric)
        {
            var updateFabric = _context.Fabric.Where(x => x.Id == id).FirstOrDefault();
            updateFabric.Quantity = fabric.Quantity;
            updateFabric.LastUpdated = DateTime.Now;
            var user = _userManger.GetUserName(HttpContext.User);

            if (id != fabric.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(updateFabric);
                    await _context.SaveChangesAsync();
                    logger.Info(user + " Edited Fabric: " + fabric.Title + " to quantity of: " + fabric.Quantity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FabricExists(fabric.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("SupplyInventory", "Home", new { id = fabric.Id });
            }
            return View(fabric);
        }
        [Authorize]
        // GET: Fabrics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fabric = await _context.Fabric.SingleOrDefaultAsync(m => m.Id == id);
            logger.Info("Current quantity of " + fabric.Title + ": " + fabric.Quantity);
            if (fabric == null)
            {
                return NotFound();
            }
            return View(fabric);
        }

        [Authorize]
        // POST: Fabrics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Content,Description,LastUpdated,MinThreshold,Quantity,Title")] Fabric fabric)
        {
            var user = _userManger.GetUserName(HttpContext.User);
            fabric.LastUpdated = DateTime.Now;
            if (id != fabric.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    _context.Update(fabric);
                    await _context.SaveChangesAsync();
                    logger.Info(user + " Edited Fabric: " + fabric.Title + " to quantity of: " + fabric.Quantity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FabricExists(fabric.Id))
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
            return View(fabric);
        }

        [Authorize]

        // GET: Fabrics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fabric = await _context.Fabric.SingleOrDefaultAsync(m => m.Id == id);
            if (fabric == null)
            {
                return NotFound();
            }

            return View(fabric);
        }
        [Authorize]
        // POST: Fabrics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = _userManger.GetUserName(HttpContext.User);
            var fabric = await _context.Fabric.SingleOrDefaultAsync(m => m.Id == id);
            logger.Info(user + " deleted: " + fabric.Title);
            _context.Fabric.Remove(fabric);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize]
        private bool FabricExists(int id)
        {
            return _context.Fabric.Any(e => e.Id == id);
        }
    }
}
