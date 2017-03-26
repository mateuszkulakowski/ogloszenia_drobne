using OgloszeniaDrobne.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OgloszeniaDrobne.DAL
{
    public class DbContext : System.Data.Entity.DbContext
    {
        public DbContext() : base("BazaOgloszenia") { }

        public DbSet<Add> Adds { get; set; }

        public DbSet<BannedWord> BannedWords { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Models.Attribute> Attributes { get; set; }

        public DbSet<Dictionary> Dictionary { get; set; }

        public DbSet<AddAtrribute> AddAttributes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasOptional(b => b.ParentCategory)
                                  .WithMany(b => b.Children)
                                  .HasForeignKey(b => b.ParentCategoryID);
        }

    }


}