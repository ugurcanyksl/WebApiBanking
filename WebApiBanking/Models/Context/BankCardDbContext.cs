using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBanking.Models.Entities;
using WebApiBanking.MySeed;

namespace WebApiBanking.Models.Context
{
    public class BankCardDbContext: DbContext
    {
        //public BankCardDbContext(DbContextOptions<BankCardDbContext> options)
        //: base(options)
        //{
        //}

        //public BankCardDbContext() : base("MyConnection")
        //{
        //    Database.SetInitializer(new SeedData());

        //}

        public DbSet<BankCardInfo> BankCards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-H93LQ50\\SQLEXPRESS;Database=BankDB;Integrated Security = True;");
            }
        }

        public BankCardDbContext(DbContextOptions<BankCardDbContext> options) : base(options)
        {
        }
        //Server=DESKTOP-H93LQ50\\SQLEXPRESS;Database=BankDB;Integrated Security = True;
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    
        //}
    }
}
