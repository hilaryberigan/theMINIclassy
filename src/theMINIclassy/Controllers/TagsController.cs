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
    public class TagsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly UserManager<ApplicationUser> _userManger;

        public TagsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManger = userManager;
        }
        [Authorize]
        // GET: Tags
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tag.ToListAsync());
        }
        [Authorize]
        // GET: Tags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tag.SingleOrDefaultAsync(m => m.Id == id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }
        [Authorize]
        // GET: Tags/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        // POST: Tags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,LastUpdated,MinThreshold,Quantity,Title")] Tag tag)
        {
            tag.LastUpdated = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(tag);
                await _context.SaveChangesAsync();
                var user = _userManger.GetUserName(HttpContext.User);
                logger.Info(user + " created " + tag.Title);
                return RedirectToAction("Index");
            }
            return View(tag);
        }
        [Authorize]
        // GET: Tags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tag.SingleOrDefaultAsync(m => m.Id == id);
            logger.Info("Current Tag Title " + tag.Title);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }
        [Authorize]

        // POST: Tags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,LastUpdated,MinThreshold,Quantity,Title")] Tag tag)
        {
            tag.LastUpdated = DateTime.Now;
            if (id != tag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tag);
                    await _context.SaveChangesAsync();
                    var user = _userManger.GetUserName(HttpContext.User);
                    logger.Info(user + " edited to " + tag.Title);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagExists(tag.Id))
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
            return View(tag);
        }
        [Authorize]
        public async Task<IActionResult> EditQuantity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tag.SingleOrDefaultAsync(m => m.Id == id);
            logger.Info("Current quantity " + tag.Quantity);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }
        [Authorize]
        // POST: Tags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditQuantity(int id, [Bind("Id,Description,LastUpdated,MinThreshold,Quantity,Title")] Tag tag)
        {
            tag.LastUpdated = DateTime.Now;
            if (id != tag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tag);
                    await _context.SaveChangesAsync();
                    var user = _userManger.GetUserName(HttpContext.User);
                    logger.Info(user + " edited to " + tag.Quantity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagExists(tag.Id))
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
            return View(tag);
        }
        [Authorize]
        // GET: Tags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tag.SingleOrDefaultAsync(m => m.Id == id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }
        [Authorize]
        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tag = await _context.Tag.SingleOrDefaultAsync(m => m.Id == id);
            var user = _userManger.GetUserName(HttpContext.User);
            logger.Info(user + " deleted " + tag.Title);
            _context.Tag.Remove(tag);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize]
        private bool TagExists(int id)
        {
            return _context.Tag.Any(e => e.Id == id);
        }
    }
}
