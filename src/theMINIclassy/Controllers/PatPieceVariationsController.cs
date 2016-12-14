using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using theMINIclassy.Data;
using theMINIclassy.Models;

namespace theMINIclassy.Controllers
{
    public class PatPieceVariationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatPieceVariationsController(ApplicationDbContext context)
        {
            _context = context;    
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
            ViewData["PatPieceId"] = id;
            ViewData["VariationId"] = new SelectList(_context.Variation, "Id", "Title");
            return View();
        }

        // POST: PatPieceVariations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatPieceId,VariationId")] PatPieceVariation patPieceVariation)
        {
            patPieceVariation.PatPieceId = patPieceVariation.Id;
            patPieceVariation.Id = 0;
            if (ModelState.IsValid)
            {
                _context.Add(patPieceVariation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "PatternPieces", new { id = patPieceVariation.PatPieceId });
            }else
            {
                _context.Add(patPieceVariation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "PatternPieces", new { id = patPieceVariation.PatPieceId });
            }
            ViewData["PatPieceId"] = patPieceVariation.PatPieceId;
            ViewData["VariationId"] = new SelectList(_context.Variation, "Id", "Title", patPieceVariation.VariationId);
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
                    _context.Update(patPieceVariation);
                    await _context.SaveChangesAsync();
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
            var patPieceVariation = await _context.PatPieceVariation.SingleOrDefaultAsync(m => m.Id == id);
            var oldId = patPieceVariation.PatPieceId;
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
