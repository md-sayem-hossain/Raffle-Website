using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using RaffleKing.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics.Metrics;

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
            var usershortcode = HttpContext.Session.GetString("UserShortCode");
            using (var db = new RaffleContext())
            {
                var user = db.profiles.Where(c => c.U_UserNameShortCode == usershortcode).FirstOrDefault();
                ViewBag.profile = user;
            }
            ViewBag.Message = TempData["Message"];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Profile(Profile profile)
        {
            profile.U_RoleType = "user";
            using (var db = new RaffleContext())
            {
                var user = db.profiles.Find(profile.Id);
                if (user != null) { 
                    user.U_Email = profile.U_Email;
                    user.U_Phone = profile.U_Phone;
                    user.U_Password = profile.U_Password;
                    user.U_UserNameShortCode = profile.U_UserNameShortCode;
                    user.U_Name = profile.U_Name;
                    user.U_RoleType=profile.U_RoleType;

                    db.profiles.Update(user);
                    await db.SaveChangesAsync();
                    TempData["Message"] = "Update Successfully";
                }
            }
            return RedirectToAction("Profile", "Raffle");
        }

        public ActionResult AddtoCart(String blockids, int raffleid)
        {

			blockids = blockids.Remove(blockids.Length - 1, 1);
			String[] Raffleblocks = blockids.Split(",");

			var usershortcode = HttpContext.Session.GetString("UserShortCode");

			using (var db = new RaffleContext())
            {
				var user = db.profiles.Where(c => c.U_UserNameShortCode == usershortcode).FirstOrDefault();

				var raffle = db.raffles.Find(raffleid);

				List<RaffleDetails> raffleDetails = new List<RaffleDetails>(); 
                for(int i=0;i< Raffleblocks.Length;i++)
                {
					var singleraffleDetails = db.raffleDetails.Where(c => c.RD_Raffle_Id == raffleid && c.Id == Convert.ToInt32(Raffleblocks[i])).FirstOrDefault();

                    if(singleraffleDetails != null)
                    {
                        raffleDetails.Add(singleraffleDetails);
                    }
				}
                ViewBag.raffleDetails = raffleDetails;
                ViewBag.user = user;
                ViewBag.totalCost = Raffleblocks.Length * raffle.R_TicketPrice; 
                ViewBag.singleCost = raffle.R_TicketPrice; 
                ViewBag.raffle = raffle; 
			} 

			return View();
        }

        public async Task<IActionResult> Winners()
        { 
            using (var db = new RaffleContext())
            {
                var winners = db.raffleDetails.Where(c => c.RD_Winners != "none").FirstOrDefault();
                ViewBag.winners = winners;
            } 
            return View();
        }
    }
}
