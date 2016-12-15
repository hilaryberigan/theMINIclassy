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
    public class LabelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly UserManager<ApplicationUser> _userManger;

        public LabelsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManger = userManager;
        }
        [Authorize]
        // GET: Labels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Label.ToListAsync());
        }
        [Authorize]
        // GET: Labels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var label = await _context.Label.SingleOrDefaultAsync(m => m.Id == id);
            if (label == null)
            {
                return NotFound();
            }

            return View(label);
        }
        [Authorize]
        // GET: Labels/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        // POST: Labels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,LastUpdated,MinThreshold,Quantity,Title")] Label label)
        {
            var user = _userManger.GetUserName(HttpContext.User);
            label.LastUpdated = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(label);
                await _context.SaveChangesAsync();
                logger.Info(user + " created new Label: " + label.Title);
                return RedirectToAction("Index");
            }
            return View(label);
        }
        [Authorize]
        // GET: Labels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var label = await _context.Label.SingleOrDefaultAsync(m => m.Id == id);
            logger.Info("Current Label Quantity: " + label.Title + ":" + label.Quantity);
            if (label == null)
            {
                return NotFound();
            }
            return View(label);
        }
        [Authorize]
        // POST: Labels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,LastUpdated,MinThreshold,Quantity,Title")] Label label)
        {
            var user = _userManger.GetUserName(HttpContext.User);
            label.LastUpdated = DateTime.Now;
            if (id != label.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(label);
                    await _context.SaveChangesAsync();
                    logger.Info(user + " edited " + label.Title + " quantity to: " + label.Quantity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabelExists(label.Id))
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
            return View(label);
        }
        [Authorize]
        // GET: Labels/Edit/5
        public async Task<IActionResult> EditQuantity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var label = await _context.Label.SingleOrDefaultAsync(m => m.Id == id);
            logger.Info("Current Label Quantity: " + label.Title + ":" + label.Quantity);
            if (label == null)
            {
                return NotFound();
            }
            return View(label);
        }
        [Authorize]
        // POST: Labels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditQuantity(int id, [Bind("Id,Quantity")] Label label)
        {

            var updateLabel = _context.Label.Where(x => x.Id == id).FirstOrDefault();
            updateLabel.LastUpdated = DateTime.Now;
            updateLabel.Quantity = label.Quantity;
            var user = _userManger.GetUserName(HttpContext.User);
 

            if (id != label.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(updateLabel);
                    await _context.SaveChangesAsync();
                    logger.Info(user + " edited " + label.Title + " Quantity to: " + label.Quantity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabelExists(label.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { id = label.Id });
            }
            return View(label);
        }
        [Authorize]
        // GET: Labels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var label = await _context.Label.SingleOrDefaultAsync(m => m.Id == id);
            if (label == null)
            {
                return NotFound();
            }

            return View(label);
        }
        [Authorize]
        // POST: Labels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = _userManger.GetUserName(HttpContext.User);
            var label = await _context.Label.SingleOrDefaultAsync(m => m.Id == id);
            logger.Info(user + " deleted " + label.Title);
            _context.Label.Remove(label);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize]
        private bool LabelExists(int id)
        {
            return _context.Label.Any(e => e.Id == id);
        }
    }
}
