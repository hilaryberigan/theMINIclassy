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
    public class CollaboratorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly UserManager<ApplicationUser> _userManger;

        public CollaboratorsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManger = userManager;
        }
        [Authorize]
        // GET: Collaborators
        public async Task<IActionResult> Index()
        {
            return View(await _context.Collaborator.ToListAsync());
        }
        [Authorize]
        // GET: Collaborators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaborator = await _context.Collaborator.SingleOrDefaultAsync(m => m.Id == id);
            if (collaborator == null)
            {
                return NotFound();
            }

            return View(collaborator);
        }
        [Authorize]
        // GET: Collaborators/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        // POST: Collaborators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Collaborator collaborator)
        {
            var user = _userManger.GetUserName(HttpContext.User);
            if (ModelState.IsValid)
            {
                _context.Add(collaborator);
                await _context.SaveChangesAsync();
                logger.Info(user + " created new Collaborator: " + collaborator.Name);
                return RedirectToAction("Index");
            }
            return View(collaborator);
        }
        [Authorize]
        // GET: Collaborators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaborator = await _context.Collaborator.SingleOrDefaultAsync(m => m.Id == id);
            logger.Info("Current Collaborator name: " + collaborator.Name);
            if (collaborator == null)
            {
                return NotFound();
            }
            return View(collaborator);
        }
        [Authorize]
        // POST: Collaborators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Collaborator collaborator)
        {
            var user = _userManger.GetUserName(HttpContext.User);
            if (id != collaborator.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collaborator);
                    await _context.SaveChangesAsync();
                    logger.Info(user + " changed Collaborator name to " + collaborator.Name);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollaboratorExists(collaborator.Id))
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
            return View(collaborator);
        }
        [Authorize]
        // GET: Collaborators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaborator = await _context.Collaborator.SingleOrDefaultAsync(m => m.Id == id);
            if (collaborator == null)
            {
                return NotFound();
            }

            return View(collaborator);
        }
        [Authorize]
        // POST: Collaborators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = _userManger.GetUserName(HttpContext.User);
            var collaborator = await _context.Collaborator.SingleOrDefaultAsync(m => m.Id == id);
            logger.Info(user + " deleted Collaborator " + collaborator.Name);
            _context.Collaborator.Remove(collaborator);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize]
        private bool CollaboratorExists(int id)
        {
            return _context.Collaborator.Any(e => e.Id == id);
        }
    }
}
