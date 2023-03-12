using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaffleKing.Models;
using System.Diagnostics;

namespace RaffleKing.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; 
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult formview()
        {
            formdataview();
            return View();
        }

        [HttpPost]
        public IActionResult formview(basic basic)
        {
            using( var db = new RaffleContext())
            {
                db.basics.Add(basic);
                db.SaveChanges();
            }
            return View();
        }
        
        [HttpGet]
        public IActionResult formdataview()
        {
            using (var db = new RaffleContext())
            {
                var data = db.basics.ToList();
                ViewBag.values= data;
            }
            return View();
        }

    }
}