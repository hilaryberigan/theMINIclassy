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
    public class AddressesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly UserManager<ApplicationUser> _userManger;

        public AddressesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManger = userManager;
        }

        // GET: Addresses
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Address.ToListAsync());
        }
        [Authorize]
        // GET: Addresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Address.SingleOrDefaultAsync(m => m.Id == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }
        [Authorize]
        // GET: Addresses/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        // POST: Addresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ApartmentNumber,City,State,StreetName,StreetNumber,ZipCode")] Address address)
        {
            var user = _userManger.GetUserName(HttpContext.User);
            if (address.ApartmentNumber == null)
            {
                address.ApartmentNumber = "";
            }
            if (ModelState.IsValid)
            {
                _context.Add(address);
                await _context.SaveChangesAsync();
                logger.Info(user + " Created new Address" + address.GetFullAddr);
                return RedirectToAction("Create", "Customers");
            }
            return View(address);
        }
        [Authorize]
        // GET: Addresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Address.SingleOrDefaultAsync(m => m.Id == id);
            logger.Info("Current Address" + address.GetFullAddr);
            if (address == null)
            {
                return NotFound();
            }
            return View(address);
        }
        [Authorize]
        // POST: Addresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ApartmentNumber,City,State,StreetName,StreetNumber,ZipCode")] Address address)
        {
            var user = _userManger.GetUserName(HttpContext.User);
            if (id != address.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(address);
                    await _context.SaveChangesAsync();
                    logger.Info(user + " edited Address to: " + address.GetFullAddr);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.Id))
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
            return View(address);
        }
        [Authorize]
        // GET: Addresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Address.SingleOrDefaultAsync(m => m.Id == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }
        [Authorize]
        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = _userManger.GetUserName(HttpContext.User);
            var address = await _context.Address.SingleOrDefaultAsync(m => m.Id == id);
            logger.Info(user + " deleted " + address.GetFullAddr);
            _context.Address.Remove(address);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AddressExists(int id)
        {
            return _context.Address.Any(e => e.Id == id);
        }
    }
}
