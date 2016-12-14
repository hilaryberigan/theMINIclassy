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

namespace theMINIclassy.Controllers
{
    public class PatPieceStylesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatPieceStylesController(ApplicationDbContext context)
        {
            _context = context;    
        }
        [Authorize]
        // GET: PatPieceStyles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PatPieceStyle.Include(p => p.PatternPiece).Include(p => p.Style);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize]
        // GET: PatPieceStyles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patPieceStyle = await _context.PatPieceStyle.SingleOrDefaultAsync(m => m.Id == id);
            if (patPieceStyle == null)
            {
                return NotFound();
            }

            return View(patPieceStyle);
        }
        [Authorize]
        // GET: PatPieceStyles/Create
        public IActionResult Create(int Id)
        {

            ViewData["SelectPatPiece"] = new SelectList(_context.PatternPiece, "Title", "Title");
            StyleViewModel model = new StyleViewModel();
            model.Style.Id = Id;
            
            return View(model);
        }
        [Authorize]
        public IActionResult AddAnother(int Id)
        {

            PatPieceStyle patPieceStyle = new PatPieceStyle();
            patPieceStyle.Style.Id = Id;
            return View(patPieceStyle);
        }
        [Authorize]
        // POST: PatPieceStyles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatPieceId,StyleId")] PatPieceStyle patPieceStyle)
        {

            if (ModelState.IsValid)
            {
                _context.Add(patPieceStyle);
                await _context.SaveChangesAsync();
                return RedirectToAction("AddAnother", new { Id = patPieceStyle.Style.Id });
            }
            ViewData["PatPieceId"] = new SelectList(_context.PatternPiece, "Id", "Id", patPieceStyle.PatPieceId);
            ViewData["StyleId"] = new SelectList(_context.Style, "Id", "Id", patPieceStyle.StyleId);
            return View(patPieceStyle);
        }
        [Authorize]
        // GET: PatPieceStyles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patPieceStyle = await _context.PatPieceStyle.SingleOrDefaultAsync(m => m.Id == id);
            if (patPieceStyle == null)
            {
                return NotFound();
            }
            ViewData["PatPieceId"] = new SelectList(_context.PatternPiece, "Id", "Id", patPieceStyle.PatPieceId);
            ViewData["StyleId"] = new SelectList(_context.Style, "Id", "Id", patPieceStyle.StyleId);
            return View(patPieceStyle);
        }

        [Authorize]

        // POST: PatPieceStyles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PatPieceId,StyleId")] PatPieceStyle patPieceStyle)
        {
            if (id != patPieceStyle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patPieceStyle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatPieceStyleExists(patPieceStyle.Id))
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
            ViewData["PatPieceId"] = new SelectList(_context.PatternPiece, "Id", "Id", patPieceStyle.PatPieceId);
            ViewData["StyleId"] = new SelectList(_context.Style, "Id", "Id", patPieceStyle.StyleId);
            return View(patPieceStyle);
        }
        [Authorize]
        // GET: PatPieceStyles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patPieceStyle = await _context.PatPieceStyle.SingleOrDefaultAsync(m => m.Id == id);
            if (patPieceStyle == null)
            {
                return NotFound();
            }

            return View(patPieceStyle);
        }
        [Authorize]
        // POST: PatPieceStyles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patPieceStyle = await _context.PatPieceStyle.SingleOrDefaultAsync(m => m.Id == id);
            _context.PatPieceStyle.Remove(patPieceStyle);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize]
        private bool PatPieceStyleExists(int id)
        {
            return _context.PatPieceStyle.Any(e => e.Id == id);
        }
    }
}
