using System;
using System.Collections.Generic;
using System.Text;
using BarKeep.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BarKeep.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<AlcoholType> AlcoholType { get; set; }

        public DbSet<Descriptor> Descriptor { get; set; }

        public DbSet<Cocktail> Cocktail { get; set; }

        public DbSet<Favorite> Favorite { get; set; }

        public DbSet<CocktailDescriptor> CocktailDescriptor { get; set; }

        public DbSet<Glassware> Glassware { get; set; }

        public DbSet<Ingredient> Ingredient { get; set; }

        public DbSet<Instruction> Instruction { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Create a new user for Identity Framework
            ApplicationUser user = new ApplicationUser
            {
                FirstName = "Nate",
                LastName = "Fleming",
                UserName = "nate@admin.com",
                NormalizedUserName = "NATE@ADMIN.COM",
                Email = "nate@admin.com",
                NormalizedEmail = "NATE@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794577",
                Id = "00000000-ffff-ffff-ffff-ffffffffffff"
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            // Create Alcohol Types
            modelBuilder.Entity<AlcoholType>().HasData(
                new AlcoholType()
                {
                    AlcoholTypeId = 1,
                    Name = "Burbon/Whisky"
                },
                new AlcoholType()
                {
                    AlcoholTypeId = 2,
                    Name = "Gin"
                },
                new AlcoholType()
                {
                    AlcoholTypeId = 3,
                    Name = "Rum"
                },
                new AlcoholType()
                {
                    AlcoholTypeId = 4,
                    Name = "Scotch"
                },
                new AlcoholType()
                {
                    AlcoholTypeId = 5,
                    Name = "Vodka"
                },
                new AlcoholType()
                {
                    AlcoholTypeId = 6,
                    Name = "Tequila"
                },
                new AlcoholType()
                {
                    AlcoholTypeId = 7,
                    Name = "Mezcal"
                },
                new AlcoholType()
                {
                    AlcoholTypeId = 8,
                    Name = "Absinthe"
                },
                new AlcoholType()
                {
                    AlcoholTypeId = 9,
                    Name = "Liqueur"
                },
                new AlcoholType()
                {
                    AlcoholTypeId = 10,
                    Name = "Vermouth"
                },
                new AlcoholType()
                {
                    AlcoholTypeId = 11,
                    Name = "Sherry"
                }
            );

            // Create Desciptors
            modelBuilder.Entity<Descriptor>().HasData(
                new Descriptor()
                {
                    DescriptorId = 1,
                    Description = "Spirit Forward"
                },
                new Descriptor()
                {
                    DescriptorId = 2,
                    Description = "Sweet"
                },
                new Descriptor()
                {
                    DescriptorId = 3,
                    Description = "Dry"
                },
                new Descriptor()
                {
                    DescriptorId = 4,
                    Description = "Smoky"
                },
                new Descriptor()
                {
                    DescriptorId = 5,
                    Description = "Refeshing"
                },
                new Descriptor()
                {
                    DescriptorId = 6,
                    Description = "Citrus"
                },
                new Descriptor()
                {
                    DescriptorId = 7,
                    Description = "Floral"
                },
                new Descriptor()
                {
                    DescriptorId = 8,
                    Description = "Sour"
                },
                new Descriptor()
                {
                    DescriptorId = 9,
                    Description = "Bitter"
                },
                new Descriptor()
                {
                    DescriptorId = 10,
                    Description = "Herbal"
                }
                );

            // Create Glassware
            modelBuilder.Entity<Glassware>().HasData(
                new Glassware()
                {
                    GlasswareId = 1,
                    Name = "Coupe"
                },
                new Glassware()
                {
                    GlasswareId = 2,
                    Name = "Collins Glass"
                },
                new Glassware()
                {
                    GlasswareId = 3,
                    Name = "Copper Mug"
                },
                new Glassware()
                {
                    GlasswareId = 4,
                    Name = "Nick & Nora"
                },
                new Glassware()
                {
                    GlasswareId = 5,
                    Name = "Rocks Glass"
                },
                new Glassware()
                {
                    GlasswareId = 6,
                    Name = "Tiki Mug"
                },
                new Glassware()
                {
                    GlasswareId = 7,
                    Name = "Julep Cup"
                },
                new Glassware()
                {
                    GlasswareId = 8,
                    Name = "Wine Glass"
                },
                new Glassware()
                {
                    GlasswareId = 9,
                    Name = "Champagne Flute"
                },
                new Glassware()
                {
                    GlasswareId = 10,
                    Name = "Highball Glass"
                },
                new Glassware()
                {
                    GlasswareId = 11,
                    Name = "Martini Glass"
                },
                new Glassware()
                {
                    GlasswareId = 12,
                    Name = "Snifter"
                },
                new Glassware()
                {
                    GlasswareId = 13,
                    Name = "Margarita"
                },
                new Glassware()
                {
                    GlasswareId = 14,
                    Name = "Hurricane"
                },
                new Glassware()
                {
                    GlasswareId = 15,
                    Name = "Cordial"
                }
                );

        }
    }
}
