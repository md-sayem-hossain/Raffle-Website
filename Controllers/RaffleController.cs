using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using RaffleKing.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics.Metrics;
using System.Net;
using System.Xml.Linq;
using System.Net.Sockets;

namespace RaffleKing.Controllers
{
    public class RaffleController : Controller
    {
        public IActionResult Index()
        {
            using (var db = new RaffleContext())
            {
                var raffleList = db.raffles.Where(c=>c.R_Active == true).ToList();
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
                ViewBag.blockids = blockids; 
                ViewBag.raffleid = raffleid; 
			}  
			return View();
        }

        public async Task<IActionResult> Winners()
        { 
            using (var db = new RaffleContext())
            {
                var winners = db.Carts.Where(c => c.winnerName != "none").ToList();

                List<Winnershow> winnershows = new List<Winnershow>();
                for(int i=0;i<winners.Count;i++)
                {
                    var raffle = db.raffles.Where(c => c.ID == winners[i].raffleid).FirstOrDefault();
                    var raffleblk = db.raffleDetails.Where(c => c.Id == winners[i].blockid).FirstOrDefault();
                    if(raffle !=null && raffleblk!=null)
                        {
                        Winnershow winnersh = new Winnershow();

                        winnersh.RaffleName = raffle.R_Title;
                        winnersh.TIcketNo = raffleblk.RD_Raffle_block;
                        winnersh.WinnerName = winners[i].winnerName;
                        winnersh.Country = winners[i].country;
                        winnersh.DrawnAt = raffle.R_DrawnAt;

                        winnershows.Add(winnersh);
                    } 
                }
                ViewBag.winners = winnershows;
            } 
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> AddtoCartSuccess(string Baddress, string country, string state,
            string zip, string cc_name, string cc_number, string cc_expiration, string cc_cvv, string blockids,
            int raffleid, string U_Email)
        {
            
            string[] Raffleblocks = blockids.Split(",");
            
            using (var db = new RaffleContext())
            {
                var user = db.profiles.Where(c => c.U_Email == U_Email).FirstOrDefault();

                for (int i = 0; i < Raffleblocks.Length; i++)
                {
                    Cart cart = new Cart();

                    cart.User_id = user.Id;
                    cart.User_Email = user.U_Email;
                    cart.address = Baddress;
                    cart.country = country;
                    cart.state = state;
                    cart.zip = zip;
                    cart.cc_name = cc_name;
                    cart.cc_number = cc_number;
                    cart.cc_expiration = cc_expiration;
                    cart.cc_cvv = cc_cvv;
                    cart.blockid = Convert.ToInt32(Raffleblocks[i]); 
                    cart.raffleid = raffleid;
                    cart.winnerName = "none";
                      
                    cart.eft_branch = "450105"; 
                    cart.eft_name = "MERCANTILE BANK"; 
                    cart.eft_number = "105 114 8448"; 
                    cart.eft_reference = HttpContext.Session.GetString("UserShortCode") + HttpContext.Session.GetString("username"); 
                    db.Carts.Add(cart);

                    var block = Convert.ToInt32(Raffleblocks[i]);
                    var raffledetails = db.raffleDetails.Where(c => c.Id == block).FirstOrDefault();

                    raffledetails.RD_Booked_Status = true;
                    raffledetails.RD_User_Id = user.Id;
                    raffledetails.RD_BookedBy = user.U_UserNameShortCode; 
                    db.raffleDetails.Update(raffledetails);
                }
                var ss = db.raffles.Find(raffleid);
                ss.R_Total_Booked = ss.R_Total_Booked+Raffleblocks.Length;
                db.raffles.Update(ss);
               await db.SaveChangesAsync();
            }

            return RedirectToAction("PaymentSuccess", "Raffle");
        }


        public async Task<IActionResult> PaymentSuccess()
        {
           
            return View();
        }

        public async Task<IActionResult> MyTickets()
        {
            //< th > Raffle Name </ th >
            //            < th > Ticket No </ th >
            //            < th > Price </ th >
            //            < th > Will Drawn At</ th >

            var usershortcode = HttpContext.Session.GetString("UserShortCode");

            
            List<mytickets> list = new List<mytickets>();

            using (var db = new RaffleContext())
            {
                var user = db.profiles.Where(c => c.U_UserNameShortCode == usershortcode).FirstOrDefault();
                var s = db.raffleDetails.Where(c => c.RD_User_Id == user.Id).ToList();
                
                for (int  i=0;i<s.Count;i++)
                {
                    mytickets mytickets = new mytickets();
                    mytickets.Ticket_No = s[i].RD_Raffle_block;

                    var raffle = db.raffles.Find(s[i].RD_Raffle_Id);

                    mytickets.Will_Drawn = raffle.R_DrawnAt;
                    mytickets.Price = raffle.R_TicketPrice;
                    mytickets.Raffle_Name = raffle.R_Title;

                    list.Add(mytickets);
                }
            }
            ViewBag.mytickets = list;
                return View();
        }
        
    }
}
