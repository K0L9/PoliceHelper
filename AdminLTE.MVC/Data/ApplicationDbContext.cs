using System;
using System.Collections.Generic;
using System.Text;
using AdminLTE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdminLTE.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<LocalCommunity> LocalCommunities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //base.OnModelCreating(builder);
            //this.SeedUsers(builder);
            //this.SeedUserRoles(builder);

            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "administrator", NormalizedName = "administrator".ToUpper() });
        }
    }
}
