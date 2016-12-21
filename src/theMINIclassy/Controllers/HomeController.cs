using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using theMINIclassy.Models;
using theMINIclassy.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;


namespace theMINIclassy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _environment;

        public HomeController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        [Authorize]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        [Authorize]
        public IActionResult Error()
        {
            return View();
        }

        [Authorize]
        public IActionResult Log(string searchLog)
        {
            ViewData["Logs"] = DateTime.UtcNow.Date.ToString();
            DirectoryInfo d = new DirectoryInfo(_environment.WebRootPath + "//nlogs");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.log"); //Getting Text files
            string str = "";
            List<string> list = new List<string>();
            List<string> list2 = new List<string>();
            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
            foreach (FileInfo file in Files)
            {
                //FileStream fileStream = new FileStream("~/nlogs/" + file.Name, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                
                list2 = new List<string>();
                string myfile = _environment.WebRootPath + "//nlogs//" + file.Name;
                try
                {
                    using (FileStream fileStream = new FileStream(myfile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (StreamReader streamReader = new StreamReader(fileStream))
                        {
                            while(streamReader.Peek() >= 0)
                            {
                                list2.Add(streamReader.ReadLine());
                            }
                            //allText = streamReader.ReadToEnd();
                        }
                    }
                }catch{ }



                str = file.Name;
                //string path = @"~\nglogs\" + file.Name;
                //StreamReader sr = new StreamReader(GenerateStreamFromString(path));
                //var logText = "";
                //while(sr.Peek() >= 0)
                //{
                //    logText += sr.ReadLine() + '@';

                //}
                dict.Add(str, list2);
                list.Add(str);
                //list.Add(Convert.ToString(file));
            }
            if (!String.IsNullOrEmpty(searchLog))
            {
                var temp = list.Where(s => s != null);
                list = temp.Where(s => s.ToUpper().Contains(searchLog.ToUpper())).ToList();
            }
            var model = new LogsViewModel
            {
                LogFileNames = dict,
                LogFileList = list
            };
            return View(model);
        }

        public Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

       
        [Authorize]
        public IActionResult SupplyInventory(string sortOrder, string searchString)
        {

            SupplyViewModel model = new SupplyViewModel();
            List<SupplyObj> supplies = new List<SupplyObj>();

                foreach (var item in _context.Fabric)
                {
                    SupplyObj supply = new SupplyObj();
                    supply.Id = item.Id;
                    supply.Title = item.Title;
                    supply.Quantity = item.Quantity;
                    supply.Type = "Fabrics";
                    supply.Measure = "yds";
                    supplies.Add(supply);
                    
                }


            foreach (var item in _context.Label)
                {
                    SupplyObj supply = new SupplyObj();
                    supply.Id = item.Id;
                    supply.Title = item.Title;
                    supply.Quantity = item.Quantity;
                    supply.Type = "Labels";
                    supply.Measure = "units";
                    supplies.Add(supply);
            }
            

                foreach (var item in _context.Tag)
                {
                    SupplyObj supply = new SupplyObj();
                    supply.Id = item.Id;
                    supply.Title = item.Title;
                    supply.Quantity = item.Quantity;
                    supply.Type = "Tags";
                    supply.Measure = "units";
                    supplies.Add(supply);
            }
            

                foreach (var item in _context.Notion)
                {
                    SupplyObj supply = new SupplyObj();
                    supply.Id = item.Id;
                    supply.Title = item.Title;
                    supply.Quantity = item.Quantity;
                    supply.Type = "Notions";
                    supply.Measure = "units";
                    supplies.Add(supply);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                var temp = supplies.Where(s => s.Title != null && s.Type != null);
                supplies = temp.Where(s => s.Title.ToUpper().Contains(searchString.ToUpper())
                                       || s.Type.ToUpper().Contains(searchString.ToUpper())).ToList();
            }

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Title_desc" : "";
            ViewData["TypeSortParm"] = sortOrder == "Type" ? "Type_desc" : "Type";
            ViewData["QuantSortParm"] = sortOrder == "Quantity" ? "Quant_desc" : "Quantity";
            switch (sortOrder)
            {
                case "Type":
                    supplies = supplies.OrderByDescending(s => s.Type).ToList();
                    break;

                case "Type_desc":
                    supplies = supplies.OrderBy(s => s.Type).ToList();
                    break;
                case "Quantity":
                    supplies = supplies.OrderBy(s => s.Quantity).ToList();
                    break;
                case "Quant_desc":
                    supplies = supplies.OrderByDescending(s => s.Quantity).ToList();
                    break;
                case "Title":
                    supplies = supplies.OrderBy(s => s.Title).ToList();
                    break;
                case "Title_desc":
                    supplies = supplies.OrderByDescending(s => s.Title).ToList();
                    break;
                default:
                    supplies = supplies.OrderBy(s => s.Type).ToList();
                    break;
            }
            model.Supplies = supplies;

            return View(model);
        }
    }
}
