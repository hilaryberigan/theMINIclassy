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
    public class NotionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly UserManager<ApplicationUser> _userManger;

        public NotionsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManger = userManager;
        }
        [Authorize]
        // GET: Notions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Notion.ToListAsync());
        }
        [Authorize]
        // GET: Notions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notion = await _context.Notion.SingleOrDefaultAsync(m => m.Id == id);
            if (notion == null)
            {
                return NotFound();
            }

            return View(notion);
        }
        [Authorize]
        // GET: Notions/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        // POST: Notions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,LastUpdated,MinThreshold,Quantity,Title")] Notion notion)
        {
            var user = _userManger.GetUserName(HttpContext.User);
            notion.LastUpdated = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(notion);
                await _context.SaveChangesAsync();
                logger.Info(user + " created Notion: " + notion.Title);
                return RedirectToAction("Index");
            }
            return View(notion);
        }
        [Authorize]
        // GET: Notions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notion = await _context.Notion.SingleOrDefaultAsync(m => m.Id == id);
            logger.Info("Current" + notion.Title + " quantity: " + notion.Quantity);
            if (notion == null)
            {
                return NotFound();
            }
            return View(notion);
        }
        [Authorize]
        // POST: Notions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,LastUpdated,MinThreshold,Quantity,Title")] Notion notion)
        {
            var user = _userManger.GetUserName(HttpContext.User);
            notion.LastUpdated = DateTime.Now;
            if (id != notion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notion);
                    await _context.SaveChangesAsync();
                    logger.Info(user + " edited " + notion.Title + " Quantity to: " + notion.Quantity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotionExists(notion.Id))
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
            return View(notion);
        }
        [Authorize]
        public async Task<IActionResult> EditQuantity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notion = await _context.Notion.SingleOrDefaultAsync(m => m.Id == id);
            logger.Info("Current " + notion.Title + " quantity " + notion.Quantity);
            if (notion == null)
            {
                return NotFound();
            }
            return View(notion);
        }
        [Authorize]
        // POST: Notions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditQuantity(int id, [Bind("Id,Description,LastUpdated,MinThreshold,Quantity,Title")] Notion notion)
        {
            var user = _userManger.GetUserName(HttpContext.User);
            notion.LastUpdated = DateTime.Now;
            if (id != notion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notion);
                    await _context.SaveChangesAsync();
                    logger.Info(user + " edited " + notion.Title + " quantity to: " + notion.Quantity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotionExists(notion.Id))
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
            return View(notion);
        }
        [Authorize]
        // GET: Notions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notion = await _context.Notion.SingleOrDefaultAsync(m => m.Id == id);
            if (notion == null)
            {
                return NotFound();
            }

            return View(notion);
        }
        [Authorize]
        // POST: Notions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = _userManger.GetUserName(HttpContext.User);
            var notion = await _context.Notion.SingleOrDefaultAsync(m => m.Id == id);
            logger.Info(user + " deleted " + notion.Title);
            _context.Notion.Remove(notion);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize]
        private bool NotionExists(int id)
        {
            return _context.Notion.Any(e => e.Id == id);
        }
    }
}
