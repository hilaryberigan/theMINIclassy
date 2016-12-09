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
    public class NotionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotionsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Notions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Notion.ToListAsync());
        }

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

        // GET: Notions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,LastUpdated,MinThreshold,Quantity,Title")] Notion notion)
        {
            notion.LastUpdated = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(notion);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(notion);
        }

        // GET: Notions/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: Notions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,LastUpdated,MinThreshold,Quantity,Title")] Notion notion)
        {
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

        // POST: Notions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notion = await _context.Notion.SingleOrDefaultAsync(m => m.Id == id);
            _context.Notion.Remove(notion);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool NotionExists(int id)
        {
            return _context.Notion.Any(e => e.Id == id);
        }
    }
}
