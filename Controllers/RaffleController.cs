using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using RaffleKing.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace RaffleKing.Controllers
{
    public class RaffleController : Controller
    {
        public IActionResult Index()
        {
            using (var db = new RaffleContext())
            {
                var raffleList = db.raffles.ToList();
                ViewData["UserShortCode"] = HttpContext.Session.GetString("UserShortCode");
                ViewData["username"] = HttpContext.Session.GetString("username");
                ViewBag.RaffleList = raffleList;
            }
            return View();
        }

        [HttpGet]
        public IActionResult RaffelDetailView(int id)
        {
            using (var db = new RaffleContext())
            {
                var Raffle  = db.raffles.Find(id);
                var RaffleDetails = db.raffleDetails.Where(c=> c.RD_Raffle_Id == Raffle.ID).ToList();
                ViewBag.Raffle = Raffle;
                ViewBag.RaffleDetails = RaffleDetails;
            }
            return View();
        }
          

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("UserShortCode");
            return RedirectToAction("Index", "Raffle");
        }


        [HttpPost]
        public async Task<IActionResult> Login(Profile profile)
        {
            profile.U_Email = profile.U_Email.ToLower();
            using (var db = new RaffleContext())
            {

                var checkExists = db.profiles.Where(c => c.U_Email == profile.U_Email && c.U_Password == profile.U_Password).FirstOrDefault();

                if (checkExists != null)
                {
                    var usershort = checkExists.U_UserNameShortCode;
                    var username = checkExists.U_Name;

                    HttpContext.Session.SetString("UserShortCode", usershort);
                    HttpContext.Session.SetString("username", username);  

                    ViewBag.message = "found"; 
                    return RedirectToAction("Index", "Raffle");
                }
                {
                    ViewBag.message = "no found";
                } 
            }
            return View();
        }


        public async Task<IActionResult> Login()
        { 

            return View();
        }

         

        public async Task<IActionResult> Registration()
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(Profile profile)
        { 
                using (var db =  new RaffleContext())
                {
                    profile.U_Email = profile.U_Email.ToLower();
                    var checkExists = db.profiles.Where(c => c.U_Email == profile.U_Email).Count();
                    var checkExists2 = db.profiles.Where(c => c.U_UserNameShortCode == profile.U_UserNameShortCode).Count();
                    if (checkExists+checkExists2 == 0)
                    {
                        Profile profiles = new Profile();
                        profiles.U_Email = profile.U_Email;
                        profiles.U_Name = profile.U_Name;
                        profiles.U_UserNameShortCode = profile.U_UserNameShortCode;
                        profiles.U_Phone = profile.U_Phone;
                        profiles.U_Password = profile.U_Password;
                        profiles.U_RoleType = "user";
                        db.profiles.Add(profiles);
                        db.SaveChanges();
                        ViewBag.message = "success";
                        return View("Login");
                    }
                    else
                    {
                        ViewBag.message = "Not success Try with different email or shortcode";
                    } 
            } 
            return View();
        }

        public async Task<IActionResult> Profile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Profile(Profile profile)
        {
            return View();
        }

        public ActionResult AddtoCart(String blockids)
        {
			String[] blocks = blockids.Split(",");

			return View();
        }
    }
}
