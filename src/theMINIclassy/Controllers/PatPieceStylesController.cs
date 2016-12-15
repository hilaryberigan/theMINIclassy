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
    public class PatPieceStylesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly UserManager<ApplicationUser> _userManger;

        public PatPieceStylesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManger = userManager;
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
        public IActionResult Create(int? Id)
        {

            ViewData["StyleId"] = new SelectList(_context.Style, "Id", "Title");
            ViewData["PatPieceId"] = Id;
            
            return View();
        }

        //public IActionResult AddAnother(int Id)
        //{

        //    PatPieceStyle patPieceStyle = new PatPieceStyle();
        //    patPieceStyle.Style.Id = Id;
        //    return View(patPieceStyle);
        //}


        // POST: PatPieceStyles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatPieceId,StyleId")] PatPieceStyle patPieceStyle)
        {
            var user = _userManger.GetUserName(HttpContext.User);
            patPieceStyle.PatPieceId = patPieceStyle.Id;
            patPieceStyle.Id = 0;
            if (ModelState.IsValid)
            {
                _context.Add(patPieceStyle);
                await _context.SaveChangesAsync();
                var styleName = "";
                var patternName = "";
                foreach(var item in _context.Style)
                {
                    if(item.Id == patPieceStyle.StyleId)
                    {
                        styleName = item.Title;
                    }
                }
                foreach( var item in _context.PatternPiece)
                {
                    if(item.Id == patPieceStyle.PatPieceId)
                    {
                        patternName = item.Title;
                    }
                }
                logger.Info(user + " created " + styleName + " and " + patternName);
                return RedirectToAction("Details","PatternPieces", new { id = patPieceStyle.PatPieceId });
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
            var styleName = "";
            var patternName = "";
            var patPieceStyle = await _context.PatPieceStyle.SingleOrDefaultAsync(m => m.Id == id);
            foreach (var item in _context.Style)
            {
                if (item.Id == patPieceStyle.StyleId)
                {
                    styleName = item.Title;
                }
            }
            foreach (var item in _context.PatternPiece)
            {
                if (item.Id == patPieceStyle.PatPieceId)
                {
                    patternName = item.Title;
                }
            }
            logger.Info("Current Pattern Piece and Style: " + styleName + " " + patternName);
            if (patPieceStyle == null)
            {
                return NotFound();
            }
            ViewData["PatPieceId"] = new SelectList(_context.PatternPiece, "Id", "Id", patPieceStyle.PatPieceId);
            ViewData["StyleId"] = new SelectList(_context.Style, "Id", "Title", patPieceStyle.StyleId);
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
            var user = _userManger.GetUserName(HttpContext.User);
            if (id != patPieceStyle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var styleName = "";
                    var patternName = "";
                    foreach (var item in _context.Style)
                    {
                        if (item.Id == patPieceStyle.StyleId)
                        {
                            styleName = item.Title;
                        }
                    }
                    foreach (var item in _context.PatternPiece)
                    {
                        if (item.Id == patPieceStyle.PatPieceId)
                        {
                            patternName = item.Title;
                        }
                    }
                    _context.Update(patPieceStyle);
                    await _context.SaveChangesAsync();
                    logger.Info(user + " edited " + styleName + " and " + patternName);
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
                return RedirectToAction("Details","PatternPieces",new { id = patPieceStyle.PatPieceId });
            }
            ViewData["PatPieceId"] = new SelectList(_context.PatternPiece, "Id", "Id", patPieceStyle.PatPieceId);
            ViewData["StyleId"] = new SelectList(_context.Style, "Id", "Title", patPieceStyle.StyleId);
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
            var user = _userManger.GetUserName(HttpContext.User);
            var patPieceStyle = await _context.PatPieceStyle.SingleOrDefaultAsync(m => m.Id == id);
            var oldId = patPieceStyle.PatPieceId;
            var styleName = "";
            var patternName = "";
            foreach (var item in _context.Style)
            {
                if (item.Id == patPieceStyle.StyleId)
                {
                    styleName = item.Title;
                }
            }
            foreach (var item in _context.PatternPiece)
            {
                if (item.Id == patPieceStyle.PatPieceId)
                {
                    patternName = item.Title;
                }
            }
            logger.Info(user + " deleted " + styleName + " and " + patternName);
            _context.PatPieceStyle.Remove(patPieceStyle);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details","PatternPieces",new { id = oldId });
        }
        [Authorize]
        private bool PatPieceStyleExists(int id)
        {
            return _context.PatPieceStyle.Any(e => e.Id == id);
        }
    }
}
