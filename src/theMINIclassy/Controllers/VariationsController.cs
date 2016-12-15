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
    public class VariationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly UserManager<ApplicationUser> _userManger;

        public VariationsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManger = userManager;
        }
        [Authorize]
        // GET: Variations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Variation.ToListAsync());
        }
        [Authorize]
        // GET: Variations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var variation = await _context.Variation.SingleOrDefaultAsync(m => m.Id == id);
            if (variation == null)
            {
                return NotFound();
            }

            return View(variation);
        }
        [Authorize]
        // GET: Variations/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        // POST: Variations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Title")] Variation variation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(variation);
                await _context.SaveChangesAsync();
                var user = _userManger.GetUserName(HttpContext.User);
                logger.Info(user + " created " + variation.Title);
                return RedirectToAction("Index");
            }
            return View(variation);
        }
        [Authorize]
        // GET: Variations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var variation = await _context.Variation.SingleOrDefaultAsync(m => m.Id == id);
            logger.Info("Current Variation Title " + variation.Title);
            if (variation == null)
            {
                return NotFound();
            }
            return View(variation);
        }
        [Authorize]
        // POST: Variations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Title")] Variation variation)
        {
            if (id != variation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(variation);
                    await _context.SaveChangesAsync();
                    var user = _userManger.GetUserName(HttpContext.User);
                    logger.Info(user + " edited to " + variation.Title);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VariationExists(variation.Id))
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
            return View(variation);
        }
        [Authorize]
        // GET: Variations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var variation = await _context.Variation.SingleOrDefaultAsync(m => m.Id == id);
            if (variation == null)
            {
                return NotFound();
            }

            return View(variation);
        }
        [Authorize]
        // POST: Variations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var variation = await _context.Variation.SingleOrDefaultAsync(m => m.Id == id);
            var user = _userManger.GetUserName(HttpContext.User);
            logger.Info(user + " deleted " + variation.Title);
            _context.Variation.Remove(variation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize]
        private bool VariationExists(int id)
        {
            return _context.Variation.Any(e => e.Id == id);
        }
    }
}
