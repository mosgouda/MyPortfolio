using core.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace infrastructure
{
    public class DataContext: DbContext

    {

        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Owner>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<ProtifolioItem>().Property(x => x.Id).HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Owner>().HasData(
                new Owner()
                {
                    Id = Guid.NewGuid(),
                    FullName = "mohamed omar",
                    Avatar = "avatar.jpg",
                    Profile = ".net developer "
                }
                );

            


        }
        public DbSet<Owner> Owner { get; set; }
        public DbSet<ProtifolioItem> ProtifolioItems { get; set; }




    }
}
