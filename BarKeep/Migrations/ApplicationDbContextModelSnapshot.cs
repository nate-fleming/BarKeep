﻿// <auto-generated />
using System;
using BarKeep.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BarKeep.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BarKeep.Models.AlcoholType", b =>
                {
                    b.Property<int>("AlcoholTypeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("AlcoholTypeId");

                    b.ToTable("AlcoholType");

                    b.HasData(
                        new
                        {
                            AlcoholTypeId = 1,
                            Name = "Burbon/Whisky"
                        },
                        new
                        {
                            AlcoholTypeId = 2,
                            Name = "Gin"
                        },
                        new
                        {
                            AlcoholTypeId = 3,
                            Name = "Rum"
                        },
                        new
                        {
                            AlcoholTypeId = 4,
                            Name = "Scotch"
                        },
                        new
                        {
                            AlcoholTypeId = 5,
                            Name = "Vodka"
                        },
                        new
                        {
                            AlcoholTypeId = 6,
                            Name = "Tequila"
                        },
                        new
                        {
                            AlcoholTypeId = 7,
                            Name = "Mezcal"
                        },
                        new
                        {
                            AlcoholTypeId = 8,
                            Name = "Absinthe"
                        },
                        new
                        {
                            AlcoholTypeId = 9,
                            Name = "Liqueur"
                        },
                        new
                        {
                            AlcoholTypeId = 10,
                            Name = "Vermouth"
                        },
                        new
                        {
                            AlcoholTypeId = 11,
                            Name = "Sherry"
                        });
                });

            modelBuilder.Entity("BarKeep.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = "00000000-ffff-ffff-ffff-ffffffffffff",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "23daea5e-a535-459e-a93b-130e54b57778",
                            Email = "nate@admin.com",
                            EmailConfirmed = true,
                            FirstName = "Nate",
                            LastName = "Fleming",
                            LockoutEnabled = false,
                            NormalizedEmail = "NATE@ADMIN.COM",
                            NormalizedUserName = "NATE@ADMIN.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEGLn4vh0b28jYDwv9rauy+ngzp8D+0xhGMIHZrRVGuzO8/EosLTn+z3ActAlkv+GFA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794577",
                            TwoFactorEnabled = false,
                            UserName = "nate@admin.com"
                        });
                });

            modelBuilder.Entity("BarKeep.Models.Cocktail", b =>
                {
                    b.Property<int>("CocktailId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AlcoholTypeId");

                    b.Property<int?>("DescriptorId");

                    b.Property<string>("Garnish");

                    b.Property<int>("GlasswareId");

                    b.Property<string>("ImgUrl");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Source");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("CocktailId");

                    b.HasIndex("AlcoholTypeId");

                    b.HasIndex("DescriptorId");

                    b.HasIndex("GlasswareId");

                    b.HasIndex("UserId");

                    b.ToTable("Cocktail");
                });

            modelBuilder.Entity("BarKeep.Models.Descriptor", b =>
                {
                    b.Property<int>("DescriptorId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired();

                    b.HasKey("DescriptorId");

                    b.ToTable("Descriptor");

                    b.HasData(
                        new
                        {
                            DescriptorId = 1,
                            Description = "Spirit Forward"
                        },
                        new
                        {
                            DescriptorId = 2,
                            Description = "Sweet"
                        },
                        new
                        {
                            DescriptorId = 3,
                            Description = "Dry"
                        },
                        new
                        {
                            DescriptorId = 4,
                            Description = "Smoky"
                        },
                        new
                        {
                            DescriptorId = 5,
                            Description = "Refeshing"
                        },
                        new
                        {
                            DescriptorId = 6,
                            Description = "Citrus"
                        },
                        new
                        {
                            DescriptorId = 7,
                            Description = "Floral"
                        },
                        new
                        {
                            DescriptorId = 8,
                            Description = "Sour"
                        },
                        new
                        {
                            DescriptorId = 9,
                            Description = "Bitter"
                        });
                });

            modelBuilder.Entity("BarKeep.Models.Favorite", b =>
                {
                    b.Property<int>("FavoriteId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CocktailId");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("FavoriteId");

                    b.HasIndex("CocktailId");

                    b.HasIndex("UserId");

                    b.ToTable("Favorite");
                });

            modelBuilder.Entity("BarKeep.Models.Glassware", b =>
                {
                    b.Property<int>("GlasswareId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImgUrl");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("GlasswareId");

                    b.ToTable("Glassware");

                    b.HasData(
                        new
                        {
                            GlasswareId = 1,
                            Name = "Coupe"
                        },
                        new
                        {
                            GlasswareId = 2,
                            Name = "Collins Glass"
                        },
                        new
                        {
                            GlasswareId = 3,
                            Name = "Copper Mug"
                        },
                        new
                        {
                            GlasswareId = 4,
                            Name = "Nick & Nora"
                        },
                        new
                        {
                            GlasswareId = 5,
                            Name = "Rocks Glass"
                        },
                        new
                        {
                            GlasswareId = 6,
                            Name = "Tiki Mug"
                        },
                        new
                        {
                            GlasswareId = 7,
                            Name = "Julep Cup"
                        },
                        new
                        {
                            GlasswareId = 8,
                            Name = "Wine Glass"
                        },
                        new
                        {
                            GlasswareId = 9,
                            Name = "Champagne Flute"
                        },
                        new
                        {
                            GlasswareId = 10,
                            Name = "Highball Glass"
                        },
                        new
                        {
                            GlasswareId = 11,
                            Name = "Martini Glass"
                        },
                        new
                        {
                            GlasswareId = 12,
                            Name = "Snifter"
                        },
                        new
                        {
                            GlasswareId = 13,
                            Name = "Margarita"
                        },
                        new
                        {
                            GlasswareId = 14,
                            Name = "Hurricane"
                        },
                        new
                        {
                            GlasswareId = 15,
                            Name = "Cordial"
                        });
                });

            modelBuilder.Entity("BarKeep.Models.Ingredient", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Amount")
                        .IsRequired();

                    b.Property<int>("CocktailId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("IngredientId");

                    b.HasIndex("CocktailId");

                    b.ToTable("Ingredient");
                });

            modelBuilder.Entity("BarKeep.Models.Instruction", b =>
                {
                    b.Property<int>("InstructionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CocktailId");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("Number");

                    b.HasKey("InstructionId");

                    b.HasIndex("CocktailId");

                    b.ToTable("Instruction");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("BarKeep.Models.Cocktail", b =>
                {
                    b.HasOne("BarKeep.Models.AlcoholType", "AlcoholType")
                        .WithMany()
                        .HasForeignKey("AlcoholTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BarKeep.Models.Descriptor")
                        .WithMany("Cocktails")
                        .HasForeignKey("DescriptorId");

                    b.HasOne("BarKeep.Models.Glassware", "Glassware")
                        .WithMany()
                        .HasForeignKey("GlasswareId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BarKeep.Models.ApplicationUser", "User")
                        .WithMany("Cocktails")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BarKeep.Models.Favorite", b =>
                {
                    b.HasOne("BarKeep.Models.Cocktail", "Cocktail")
                        .WithMany()
                        .HasForeignKey("CocktailId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BarKeep.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BarKeep.Models.Ingredient", b =>
                {
                    b.HasOne("BarKeep.Models.Cocktail")
                        .WithMany("Ingredients")
                        .HasForeignKey("CocktailId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BarKeep.Models.Instruction", b =>
                {
                    b.HasOne("BarKeep.Models.Cocktail")
                        .WithMany("Instructions")
                        .HasForeignKey("CocktailId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BarKeep.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BarKeep.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BarKeep.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BarKeep.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
