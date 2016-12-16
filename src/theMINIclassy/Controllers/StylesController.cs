using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using theMINIclassy.Data;
using theMINIclassy.Models;
using theMINIclassy.Models.ManageViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using NLog;
using Microsoft.AspNetCore.Identity;

namespace theMINIclassy.Controllers
{
    public class StylesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly UserManager<ApplicationUser> _userManger;

        public StylesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManger = userManager;
        }
        [Authorize]
        // GET: Styles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Style.ToListAsync());
        }
        [Authorize]
        // GET: Styles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var style = await _context.Style.SingleOrDefaultAsync(m => m.Id == id);
            if (style == null)
            {
                return NotFound();
            }

            List<PatternPiece> AllPatPieces = new List<PatternPiece>();
            foreach (var item in _context.PatternPiece)
            {
                AllPatPieces.Add(item);
            }
            List<PatPieceStyle> PPSwithThisStyle = new List<PatPieceStyle>();
            foreach (var item in _context.PatPieceStyle.Where(x=>x.StyleId == style.Id).ToList())
            {
                PPSwithThisStyle.Add(item);
            }
            var model = new PatPieceViewModel
            {
                Style = style,
                PatPieces = AllPatPieces,
                PatPieceStyles = PPSwithThisStyle,
            };
 
            return View(model);
        }
        [Authorize]
        // GET: Styles/Create
        public IActionResult Create()
        {
        
            return View();
        }
        [Authorize]
        [HttpPost]

        public async Task<IActionResult> AddPatternPieceToStyle([Bind("Id,Code,Title")] Style style)
        {
           if (ModelState.IsValid)
            {
                _context.Add(style);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "PatPieceStyles", new { Id = style.Id });
            }
            return RedirectToAction("Create");
        }
        [Authorize]
        // POST: Styles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Title")] Style style)
        {
            if (ModelState.IsValid)
            {
                _context.Add(style);
                await _context.SaveChangesAsync();
                var user = _userManger.GetUserName(HttpContext.User);
                logger.Info(user + " created " + style.Title);

                return RedirectToAction("Details", new { Id = style.Id });
            }
            return View(style);

        }
        [Authorize]
        // GET: Styles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var style = await _context.Style.SingleOrDefaultAsync(m => m.Id == id);
            logger.Info("Current Style Title " + style.Title);
            if (style == null)
            {
                return NotFound();
            }
            return View(style);
        }
        [Authorize]
        // POST: Styles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Title")] Style style)
        {
            if (id != style.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = _userManger.GetUserName(HttpContext.User);
                    _context.Update(style);
                    await _context.SaveChangesAsync();
                    logger.Info(user + " edited to " + style.Title);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StyleExists(style.Id))
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
            return View(style);
        }
        [Authorize]
        // GET: Styles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var style = await _context.Style.SingleOrDefaultAsync(m => m.Id == id);
            if (style == null)
            {
                return NotFound();
            }

            return View(style);
        }
        [Authorize]
        // POST: Styles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var style = await _context.Style.SingleOrDefaultAsync(m => m.Id == id);
            var user = _userManger.GetUserName(HttpContext.User);
            logger.Info(user + " deleted " + style.Title);
            _context.Style.Remove(style);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize]
        private bool StyleExists(int id)
        {
            return _context.Style.Any(e => e.Id == id);
        }
    }
}
