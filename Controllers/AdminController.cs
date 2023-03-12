﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RaffleKing.Models;

using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RaffleKing.Controllers
{
    public class AdminController : Controller
    {
        RaffleContext _db = new RaffleContext();

        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult AddRaffle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRaffle(raffle raffle, IFormFile R_ThumbnailImage, IFormFile R_MainImage)
        {

            //for image upload start
            var ThumbnailPath = "wwwroot/Images/" + R_ThumbnailImage.FileName;
            var ThumbnailPathForDB = "/Images/" + R_ThumbnailImage.FileName;
            var MainImagePath = "wwwroot/Images/" + R_MainImage.FileName;
            var MainImagePathForDB = "/Images/" + R_MainImage.FileName;
            using (var stream = System.IO.File.Create(ThumbnailPath))
            {
                await R_ThumbnailImage.CopyToAsync(stream);
            }
            using (var stream = System.IO.File.Create(MainImagePath))
            {
                await R_MainImage.CopyToAsync(stream);
            }
            raffle.R_ThumbnailImage = ThumbnailPathForDB;
            raffle.R_MainImage = MainImagePathForDB;
            raffle.R_BlockGenerated = false;
            //for image upload end

            await _db.raffles.AddAsync(raffle);
            await _db.SaveChangesAsync();

            return View();
        }

        public async Task<IActionResult> ViewRaffles()
        { using (var db = new RaffleContext())
            {
                var raffleList = db.raffles.ToList();
                ViewBag.RaffleList = raffleList;
            }
            return View();
        }



        public async Task<IActionResult> GenerateRaffleBlock(int raffleid)
        {

            using (var db = new RaffleContext())
            {
                var raffle = db.raffles.Find(raffleid);
                if (raffle == null)
                {
                    ViewBag.Message = "Error Getting Raffle ID";
                }
                else
                {
                    var Total = raffle.R_Total_Available + raffle.R_Total_Booked;

                    for (int i = 0; i < Total; i++)
                    {
                        RaffleDetails raffleDetails = new RaffleDetails();

                        raffleDetails.RD_Raffle_Id = raffle.ID;
                        raffleDetails.RD_User_Id = 0;
                        raffleDetails.RD_Raffle_block = raffle.R_BlockStartFrom + i;
                        raffleDetails.RD_BookedBy = "None";
                        raffleDetails.RD_Booked_Status = false;
                        await _db.raffleDetails.AddAsync(raffleDetails);
                    }
                    raffle.R_BlockGenerated = true;
                    _db.raffles.Update(raffle);
                    await _db.SaveChangesAsync();
                    ViewBag.Message = "Raffle Block Generated Succesfully";
                }
            }
            return RedirectToAction("ViewRaffles", "Admin");
        }



        public async Task<IActionResult> EditRaffle(int raffleid)
        {
            using (var db = new RaffleContext())
            {
                var raffle = db.raffles.Find(raffleid);
                ViewBag.raffle = raffle;
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> updateRaffle(raffle raffle, IFormFile R_ThumbnailImage, IFormFile R_MainImage)
        {
            using(var db = new RaffleContext())
            {
                var ra = db.raffles.Find(raffle.ID);

                if(R_ThumbnailImage == null)
                {
                    ra.R_MainImage =  ra.R_MainImage;
                }
                if (R_MainImage == null)
                {
                    ra.R_ThumbnailImage = ra.R_ThumbnailImage;
                }
                if(R_ThumbnailImage != null)
                {
                    var ThumbnailPath = "wwwroot/Images/" + R_ThumbnailImage.FileName;
                    var ThumbnailPathForDB = "/Images/" + R_ThumbnailImage.FileName;
                    using (var stream = System.IO.File.Create(ThumbnailPath))
                    {
                        await R_ThumbnailImage.CopyToAsync(stream);
                    }
                    ra.R_ThumbnailImage = ThumbnailPathForDB;
                }
                if (R_MainImage != null)
                {
                    var MainImagePath = "wwwroot/Images/" + R_MainImage.FileName;
                    var MainImagePathForDB = "/Images/" + R_MainImage.FileName;
                    using (var stream = System.IO.File.Create(MainImagePath))
                    {
                        await R_MainImage.CopyToAsync(stream);
                    }
                    ra.R_MainImage = MainImagePathForDB;
                }
                
                    ra.R_Title = raffle.R_Title;
                    ra.R_TicketPrice = raffle.R_TicketPrice;
                    ra.R_ShortDecription = raffle.R_ShortDecription;
                    ra.R_FullDecription = raffle.R_FullDecription;
                    ra.R_UniqueRaffleCode = raffle.R_UniqueRaffleCode;

                    db.raffles.Update(ra);

                    await db.SaveChangesAsync();
                   
            }

            return RedirectToAction("ViewRaffles", "Admin");
        }


        public async Task<IActionResult> ViewUsers()
        {
            using (var db = new RaffleContext())
            {
                var profiles = db.profiles.ToList();
                ViewBag.profiles = profiles;
                ViewBag.Message = TempData["Message"];
            }
            return View();
        }

        public async Task<IActionResult> EditUser(int userid)
        {
            using (var db = new RaffleContext())
            {
                var profile = db.profiles.Find(userid);
                ViewBag.profile = profile;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> updateprofile(Profile profile )
        {

            using (var db = new RaffleContext())
            {
                var profiles = db.profiles.Find(profile.Id);

                profiles.U_Name = profile.U_Name;
                profiles.U_Phone = profile.U_Phone;
                profiles.U_Email= profile.U_Email;
                profiles.U_Password= profile.U_Password;
                profiles.U_UserNameShortCode = profile.U_UserNameShortCode; 

                db.profiles.Update(profiles);

                await db.SaveChangesAsync();
                TempData["Message"] = "Update Success"; 
            }
            return Redirect("ViewUsers" ); 
        }

        public async Task<IActionResult> ViewRaffleBlock(int raffleid)
        {
            using (var db = new RaffleContext())
            {
                var raffleDetails = db.raffleDetails.Where(c => c.RD_Raffle_Id == raffleid).ToList();
                ViewBag.raffleDetails = raffleDetails;
            }
            return View();
        }
        
    }
}
