using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using theMINIclassy.Data;
using theMINIclassy.Models;
using Microsoft.Extensions.Logging;
using NLog;
using Microsoft.AspNetCore.Identity;

namespace theMINIclassy.Controllers
{
    public class PatPieceVariationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly UserManager<ApplicationUser> _userManger;

        public PatPieceVariationsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManger = userManager;
        }

        // GET: PatPieceVariations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PatPieceVariation.Include(p => p.PatternPiece).Include(p => p.Variation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PatPieceVariations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patPieceVariation = await _context.PatPieceVariation.SingleOrDefaultAsync(m => m.Id == id);
            if (patPieceVariation == null)
            {
                return NotFound();
            }

            return View(patPieceVariation);
        }

        // GET: PatPieceVariations/Create
        public IActionResult Create(int? id)
        {
           
            PatPieceVariation model = new PatPieceVariation
            {
                VariationId = id ?? default(int),
            };
            ViewData["PatPieceId"] = new SelectList(_context.PatternPiece, "Id", "Title");
            return View(model);
        }

        // POST: PatPieceVariations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatPieceId,VariationId")] PatPieceVariation patPieceVariation)
        {
            var user = _userManger.GetUserName(HttpContext.User);
            patPieceVariation.Id = 0;
            if (ModelState.IsValid)
            {
                _context.Add(patPieceVariation);
                await _context.SaveChangesAsync();
                var variationName = "";
                var patternName = "";

                foreach (var item in _context.Style)
                {
                    if (item.Id == patPieceVariation.VariationId)
                    {
                        variationName = item.Title;
                    }
                }
                foreach (var item in _context.PatternPiece)
                {
                    if (item.Id == patPieceVariation.PatPieceId)
                    {
                        patternName = item.Title;
                    }
                }
                logger.Info(user + " added " + patternName + " to " + variationName);
                return RedirectToAction("Details", "Variations", new { id = patPieceVariation.VariationId });
            }
            ViewData["PatPieceId"] = patPieceVariation.PatPieceId;
            ViewData["PatPieceId"] = new SelectList(_context.PatternPiece, "Id", "Title");
            return View(patPieceVariation);
        }

        // GET: PatPieceVariations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patPieceVariation = await _context.PatPieceVariation.SingleOrDefaultAsync(m => m.Id == id);
            var patVariation = "";
            var patPiece = "";
            foreach (var item in _context.Variation)
            {
                if (patPieceVariation.PatPieceId == item.Id)
                {
                    patVariation = item.Title;
                }
            }
            foreach (var item in _context.PatternPiece)
            {
                if (patPieceVariation.PatPieceId == item.Id)
                {
                    patPiece = item.Title;
                }
            }
            logger.Info("Current Pattern Piece Variation: " + patVariation + " and " + patPiece);
            if (patPieceVariation == null)
            {
                return NotFound();
            }
            ViewData["PatPieceId"] = new SelectList(_context.PatternPiece, "Id", "Id", patPieceVariation.PatPieceId);
            ViewData["VariationId"] = new SelectList(_context.Variation, "Id", "Title", patPieceVariation.VariationId);
            return View(patPieceVariation);
        }

        // POST: PatPieceVariations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PatPieceId,VariationId")] PatPieceVariation patPieceVariation)
        {
            if (id != patPieceVariation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = _userManger.GetUserName(HttpContext.User);
                    var patVariation = "";
                    var patPiece = "";
                    foreach (var item in _context.Variation)
                    {
                        if (patPieceVariation.PatPieceId == item.Id)
                        {
                            patVariation = item.Title;
                        }
                    }
                    foreach (var item in _context.PatternPiece)
                    {
                        if (patPieceVariation.PatPieceId == item.Id)
                        {
                            patPiece = item.Title;
                        }
                    }
                    _context.Update(patPieceVariation);
                    await _context.SaveChangesAsync();
                    logger.Info(user + " edited to " + patPiece + " and " + patVariation);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatPieceVariationExists(patPieceVariation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "PatternPieces", new { id = patPieceVariation.PatPieceId });
            }
            ViewData["PatPieceId"] = new SelectList(_context.PatternPiece, "Id", "Id", patPieceVariation.PatPieceId);
            ViewData["VariationId"] = new SelectList(_context.Variation, "Id", "Title", patPieceVariation.VariationId);
            return View(patPieceVariation);
        }

        // GET: PatPieceVariations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patPieceVariation = await _context.PatPieceVariation.SingleOrDefaultAsync(m => m.Id == id);
            if (patPieceVariation == null)
            {
                return NotFound();
            }

            return View(patPieceVariation);
        }

        // POST: PatPieceVariations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = _userManger.GetUserName(HttpContext.User);
            var patPieceVariation = await _context.PatPieceVariation.SingleOrDefaultAsync(m => m.Id == id);
            var oldId = patPieceVariation.PatPieceId;
            var patVariation = "";
            var patPiece = "";
            foreach (var item in _context.Variation)
            {
                if (patPieceVariation.PatPieceId == item.Id)
                {
                    patVariation = item.Title;
                }
            }
            foreach (var item in _context.PatternPiece)
            {
                if (patPieceVariation.PatPieceId == item.Id)
                {
                    patPiece = item.Title;
                }
            }
            logger.Info(user + " deleted " + patPiece + " and " + patVariation);
            _context.PatPieceVariation.Remove(patPieceVariation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "PatternPieces", new { id = oldId });
        }

        private bool PatPieceVariationExists(int id)
        {
            return _context.PatPieceVariation.Any(e => e.Id == id);
        }
    }
}
