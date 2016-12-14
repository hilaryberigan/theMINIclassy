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
    public class CollectionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly UserManager<ApplicationUser> _userManger;

        public CollectionsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManger = userManager;
        }
        [Authorize]
        // GET: Collections
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Collection.Include(c => c.Collaborator).Include(c => c.Season);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize]
        // GET: Collections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collection = await _context.Collection.SingleOrDefaultAsync(m => m.Id == id);
            if (collection == null)
            {
                return NotFound();
            }

            return View(collection);
        }
        [Authorize]
        // GET: Collections/Create
        public IActionResult Create()
        {
            ViewData["CollaboratorId"] = new SelectList(_context.Collaborator, "Id", "Name");
            ViewData["SeasonId"] = new SelectList(_context.Season, "Id", "Title");
            return View();
        }
        [Authorize]
        // POST: Collections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,CollaboratorId,Month,SeasonId,Title")] Collection collection)
        {
            var user = _userManger.GetUserName(HttpContext.User);
            if (ModelState.IsValid)
            {
                _context.Add(collection);
                await _context.SaveChangesAsync();
                logger.Info(user + " created Collections: " + collection.Title);
                return RedirectToAction("Index");
            }
            ViewData["CollaboratorId"] = new SelectList(_context.Collaborator, "Id", "Id", collection.CollaboratorId);
            ViewData["SeasonId"] = new SelectList(_context.Season, "Id", "Id", collection.SeasonId);
            return View(collection);
        }
        [Authorize]
        // GET: Collections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collection = await _context.Collection.SingleOrDefaultAsync(m => m.Id == id);
            logger.Info("Current collection title: " + collection.Title);
            if (collection == null)
            {
                return NotFound();
            }
            ViewData["CollaboratorId"] = new SelectList(_context.Collaborator, "Id", "Id", collection.CollaboratorId);
            ViewData["SeasonId"] = new SelectList(_context.Season, "Id", "Id", collection.SeasonId);
            return View(collection);
        }
        [Authorize]
        // POST: Collections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,CollaboratorId,Month,SeasonId,Title")] Collection collection)
        {
            var user = _userManger.GetUserName(HttpContext.User);
            if (id != collection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collection);
                    await _context.SaveChangesAsync();
                    logger.Info(user + " edited Collection: " + collection.Title);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollectionExists(collection.Id))
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
            ViewData["CollaboratorId"] = new SelectList(_context.Collaborator, "Id", "Id", collection.CollaboratorId);
            ViewData["SeasonId"] = new SelectList(_context.Season, "Id", "Id", collection.SeasonId);
            return View(collection);
        }
        [Authorize]
        // GET: Collections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collection = await _context.Collection.SingleOrDefaultAsync(m => m.Id == id);
            if (collection == null)
            {
                return NotFound();
            }

            return View(collection);
        }
        [Authorize]
        // POST: Collections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = _userManger.GetUserName(HttpContext.User);
            var collection = await _context.Collection.SingleOrDefaultAsync(m => m.Id == id);
            _context.Collection.Remove(collection);
            await _context.SaveChangesAsync();
            logger.Info(user + " deleted Collection: " + collection.Title);
            return RedirectToAction("Index");
        }
        [Authorize]
        private bool CollectionExists(int id)
        {
            return _context.Collection.Any(e => e.Id == id);
        }
    }
}
