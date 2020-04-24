using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Symptoms> SymptomsMix { get; set; }
        public DbSet<PreExistingConditions> ConditionsMix { get; set; }
        public DbSet<DemographicClassifications> DemographicClassification { get; set; }
        public DbSet<ParameterMLModelJunction> ParameterMLModelJustions { get; set; }
        public DbSet<ParametersJunctionModel> PatientProfile { get; set; }
        public DbSet<DoctorModel> Doctors { get; set; }
        public DbSet<AddressModel> Addresses { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                });
            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole 
                {
                    Name = "Doctor",
                    NormalizedName = "DOCTOR"
                });
            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole 
                {
                    Name = "Analyst",
                    NormalizedName = "ANALYST"
                });
        }
    }
}
