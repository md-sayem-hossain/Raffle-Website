﻿using Microsoft.EntityFrameworkCore;

namespace RaffleKing.Models
{
    public class RaffleContext: DbContext
    {
        public DbSet<basic> basics { get; set; }
        public DbSet<raffle> raffles { get; set; }
        public DbSet<Profile> profiles { get; set; }
        public DbSet<RaffleDetails> raffleDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite(@"Data Source = database/raffle.db");
    }
}
