using Alisverislio_Task.DAL.Entities;
using Alisverislio_Task.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.DAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<Purchase> Purchases { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            base.OnModelCreating(modelBuilder);
            var adminPassword = "ba7816bf8f01cfea414140de5dae2223b00361a396177a9cb410ff61f20015ad"; 
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Username = "admin",
                Email = "a",
                Password = adminPassword,
                Name = "Admin",
                Surname = "User",
                Role = UserRole.Admin
            });

            modelBuilder.Entity<Share>()
              .HasOne(s => s.User)
              .WithMany(u => u.Shares)
              .HasForeignKey(s => s.UserId)
              .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
