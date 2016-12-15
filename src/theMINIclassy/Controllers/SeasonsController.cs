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
    public class SeasonsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly UserManager<ApplicationUser> _userManger;

        public SeasonsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManger = userManager;
        }
        [Authorize]
        // GET: Seasons
        public async Task<IActionResult> Index()
        {
            return View(await _context.Season.ToListAsync());
        }
        [Authorize]
        // GET: Seasons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var season = await _context.Season.SingleOrDefaultAsync(m => m.Id == id);
            if (season == null)
            {
                return NotFound();
            }

            return View(season);
        }
        [Authorize]
        // GET: Seasons/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        // POST: Seasons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] Season season)
        {
            if (ModelState.IsValid)
            {
                var user = _userManger.GetUserName(HttpContext.User);
                logger.Info(user + " created " + season.Title);
                _context.Add(season);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(season);
        }
        [Authorize]
        // GET: Seasons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var season = await _context.Season.SingleOrDefaultAsync(m => m.Id == id);
            logger.Info("Current Season Title " + season.Title);
            if (season == null)
            {
                return NotFound();
            }
            return View(season);
        }
        [Authorize]
        // POST: Seasons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] Season season)
        {
            if (id != season.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = _userManger.GetUserName(HttpContext.User);

                    _context.Update(season);
                    await _context.SaveChangesAsync();
                    logger.Info(user + " edited to " + season.Title);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeasonExists(season.Id))
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
            return View(season);
        }
        [Authorize]
        // GET: Seasons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var season = await _context.Season.SingleOrDefaultAsync(m => m.Id == id);
            if (season == null)
            {
                return NotFound();
            }

            return View(season);
        }
        [Authorize]
        // POST: Seasons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var season = await _context.Season.SingleOrDefaultAsync(m => m.Id == id);
            var user = _userManger.GetUserName(HttpContext.User);
            logger.Info(user + " deleted " + season.Title);
            _context.Season.Remove(season);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize]
        private bool SeasonExists(int id)
        {
            return _context.Season.Any(e => e.Id == id);
        }
    }
}
