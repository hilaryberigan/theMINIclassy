using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using theMINIclassy.Models;
using theMINIclassy.Data;

namespace theMINIclassy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult SupplyInventory()
        {

            SupplyViewModel model = new SupplyViewModel();
            List<SupplyObj> supplies = new List<SupplyObj>();

                foreach (var item in _context.Fabric)
                {
                    SupplyObj supply = new SupplyObj();
                    supply.Id = item.Id;
                    supply.Title = item.Title;
                    supply.Quantity = item.Quantity;
                    supply.Type = "Fabric";
                    supply.Measure = "yds";
                    supplies.Add(supply);
                    
                }


            foreach (var item in _context.Label)
                {
                    SupplyObj supply = new SupplyObj();
                    supply.Id = item.Id;
                    supply.Title = item.Title;
                    supply.Quantity = item.Quantity;
                    supply.Type = "Label";
                    supply.Measure = "units";
                    supplies.Add(supply);
            }
            

                foreach (var item in _context.Tag)
                {
                    SupplyObj supply = new SupplyObj();
                    supply.Id = item.Id;
                    supply.Title = item.Title;
                    supply.Quantity = item.Quantity;
                    supply.Type = "units";
                    supplies.Add(supply);
            }
            

                foreach (var item in _context.Notion)
                {
                    SupplyObj supply = new SupplyObj();
                    supply.Id = item.Id;
                    supply.Title = item.Title;
                    supply.Quantity = item.Quantity;
                    supply.Type = "Notion";
                    supply.Measure = "units";
                    supplies.Add(supply);
            }
            model.Supplies = supplies;

            return View(model);
        }
    }
}
