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

namespace theMINIclassy.Controllers
{
    public class PatternPiecesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatternPiecesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: PatternPieces
        public async Task<IActionResult> Index()
        {
            return View(await _context.PatternPiece.ToListAsync());
        }

        // GET: PatternPieces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patternPiece = await _context.PatternPiece.SingleOrDefaultAsync(m => m.Id == id);
            if (patternPiece == null)
            {
                return NotFound();
            }
            List<Style> allSty = new List<Style>();
            foreach(var item in _context.Style)
            {
                allSty.Add(item);
            }
            List<Variation> allVar = new List<Variation>();
            foreach(var item in _context.Variation)
            {
                allVar.Add(item);
            }
            List<PatPieceStyle> PPS = new List<PatPieceStyle>();
            foreach(var item in _context.PatPieceStyle)
            {
                PPS.Add(item);
            }
            List<PatPieceVariation> PPV = new List<PatPieceVariation>();
            foreach(var item in _context.PatPieceVariation)
            {
                PPV.Add(item);
            }
            var model = new PatPieceViewModel
            {
                PatternPiece = patternPiece,
                PatPieceStyles = PPS,
                PatPieceVariations = PPV,
                Styles = allSty,
                Variations = allVar
            };

            return View(model);
        }

        // GET: PatternPieces/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PatternPieces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] PatternPiece patternPiece)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patternPiece);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details","PatternPieces",new { id = patternPiece.Id });
            }
            return View(patternPiece);
        }

        // GET: PatternPieces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patternPiece = await _context.PatternPiece.SingleOrDefaultAsync(m => m.Id == id);
            if (patternPiece == null)
            {
                return NotFound();
            }
            return View(patternPiece);
        }

        // POST: PatternPieces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] PatternPiece patternPiece)
        {
            if (id != patternPiece.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patternPiece);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatternPieceExists(patternPiece.Id))
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
            return View(patternPiece);
        }

        // GET: PatternPieces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patternPiece = await _context.PatternPiece.SingleOrDefaultAsync(m => m.Id == id);
            if (patternPiece == null)
            {
                return NotFound();
            }

            return View(patternPiece);
        }

        // POST: PatternPieces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patternPiece = await _context.PatternPiece.SingleOrDefaultAsync(m => m.Id == id);
            _context.PatternPiece.Remove(patternPiece);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PatternPieceExists(int id)
        {
            return _context.PatternPiece.Any(e => e.Id == id);
        }
    }
}
