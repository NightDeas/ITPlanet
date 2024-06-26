﻿// <auto-generated />
using System;
using ITPlanet.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ITPlanet.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ITPlanet.Data.Models.Region", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long?>("Id"));

                    b.Property<double?>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double?>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ParentRegion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("RegionTypeId")
                        .HasColumnType("bigint");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RegionTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Regions");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Latitude = 22.219999999999999,
                            Longitude = 22.219999999999999,
                            Name = "Name1",
                            ParentRegion = "Name1",
                            RegionTypeId = 1L,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2L,
                            Latitude = 33.329999999999998,
                            Longitude = 33.329999999999998,
                            Name = "Name2",
                            ParentRegion = "Name1",
                            RegionTypeId = 1L,
                            UserId = 1
                        },
                        new
                        {
                            Id = 3L,
                            Latitude = 44.439999999999998,
                            Longitude = 44.439999999999998,
                            Name = "Name3",
                            ParentRegion = "Name2",
                            RegionTypeId = 2L,
                            UserId = 1
                        },
                        new
                        {
                            Id = 4L,
                            Latitude = 55.549999999999997,
                            Longitude = 55.549999999999997,
                            Name = "Name4",
                            ParentRegion = "Name3",
                            RegionTypeId = 3L,
                            UserId = 1
                        },
                        new
                        {
                            Id = 5L,
                            Latitude = 66.659999999999997,
                            Longitude = 66.659999999999997,
                            Name = "Name5",
                            ParentRegion = "Name4",
                            RegionTypeId = 3L,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("ITPlanet.Data.Models.RegionType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("RegionTypes");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Type = "string1"
                        },
                        new
                        {
                            Id = 2L,
                            Type = "string2"
                        },
                        new
                        {
                            Id = 3L,
                            Type = "string3"
                        },
                        new
                        {
                            Id = 4L,
                            Type = "string4"
                        },
                        new
                        {
                            Id = 5L,
                            Type = "string5"
                        });
                });

            modelBuilder.Entity("ITPlanet.Data.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "admin",
                            NormalizedName = "ADMIN"
                        });
                });

            modelBuilder.Entity("ITPlanet.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "60d19c36-d149-484d-89ae-66b5a993e7d7",
                            Email = "admin@example.com",
                            EmailConfirmed = true,
                            FirstName = "Admin",
                            LastName = "Admin",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@EXAMPLE.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAIAAYagAAAAEP/rkAw8sXmOEq61vmIRrml6kwl7FxKq/oP0t7BwbNuo24m2q/faHAsY/6DqsO5u5g==",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "",
                            TwoFactorEnabled = false,
                            UserName = "Admin"
                        });
                });

            modelBuilder.Entity("ITPlanet.Data.Models.Weather", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<float>("Humidity")
                        .HasColumnType("real");

                    b.Property<DateTime>("MeasurementDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<float>("PrecipitationAmount")
                        .HasColumnType("real");

                    b.Property<long>("RegionId")
                        .HasColumnType("bigint");

                    b.Property<string>("RegionName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Temperature")
                        .HasColumnType("real");

                    b.Property<string>("WeatherCondition")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("WindSpeed")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Weathers");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Humidity = 11.11f,
                            MeasurementDateTime = new DateTime(2024, 4, 13, 7, 19, 20, 361, DateTimeKind.Utc).AddTicks(7297),
                            PrecipitationAmount = 12f,
                            RegionId = 1L,
                            RegionName = "Name1",
                            Temperature = 12.1f,
                            WeatherCondition = "RAIN",
                            WindSpeed = 15.2f
                        },
                        new
                        {
                            Id = 2L,
                            Humidity = 22.22f,
                            MeasurementDateTime = new DateTime(2024, 4, 13, 7, 19, 20, 361, DateTimeKind.Utc).AddTicks(7305),
                            PrecipitationAmount = 13f,
                            RegionId = 2L,
                            RegionName = "Name2",
                            Temperature = 12.1f,
                            WeatherCondition = "RAIN",
                            WindSpeed = 15.2f
                        },
                        new
                        {
                            Id = 3L,
                            Humidity = 17.11f,
                            MeasurementDateTime = new DateTime(2024, 4, 13, 7, 19, 20, 361, DateTimeKind.Utc).AddTicks(7307),
                            PrecipitationAmount = 13f,
                            RegionId = 3L,
                            RegionName = "Name3",
                            Temperature = 14.1f,
                            WeatherCondition = "RAIN",
                            WindSpeed = 15.2f
                        },
                        new
                        {
                            Id = 4L,
                            Humidity = 17.11f,
                            MeasurementDateTime = new DateTime(2024, 4, 13, 7, 19, 20, 361, DateTimeKind.Utc).AddTicks(7308),
                            PrecipitationAmount = 12f,
                            RegionId = 4L,
                            RegionName = "Name4",
                            Temperature = 72.1f,
                            WeatherCondition = "RAIN",
                            WindSpeed = 15.2f
                        },
                        new
                        {
                            Id = 5L,
                            Humidity = 19.11f,
                            MeasurementDateTime = new DateTime(2024, 4, 13, 7, 19, 20, 361, DateTimeKind.Utc).AddTicks(7309),
                            PrecipitationAmount = 12f,
                            RegionId = 5L,
                            RegionName = "Name5",
                            Temperature = 12.1f,
                            WeatherCondition = "RAIN",
                            WindSpeed = 15.2f
                        });
                });

            modelBuilder.Entity("ITPlanet.Data.Models.WeatherForecast", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("RegionId")
                        .HasColumnType("bigint");

                    b.Property<float>("Temperature")
                        .HasColumnType("real");

                    b.Property<string>("WeatherCondition")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("WeatherForecasts");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            DateTime = new DateTime(2024, 4, 13, 7, 19, 20, 361, DateTimeKind.Utc).AddTicks(7332),
                            RegionId = 1L,
                            Temperature = 12.2f,
                            WeatherCondition = "RAIN"
                        },
                        new
                        {
                            Id = 2L,
                            DateTime = new DateTime(2024, 4, 13, 7, 19, 20, 361, DateTimeKind.Utc).AddTicks(7337),
                            RegionId = 2L,
                            Temperature = 17.2f,
                            WeatherCondition = "RAIN"
                        },
                        new
                        {
                            Id = 3L,
                            DateTime = new DateTime(2024, 4, 13, 7, 19, 20, 361, DateTimeKind.Utc).AddTicks(7338),
                            RegionId = 3L,
                            Temperature = 13.2f,
                            WeatherCondition = "RAIN"
                        },
                        new
                        {
                            Id = 4L,
                            DateTime = new DateTime(2024, 4, 13, 7, 19, 20, 361, DateTimeKind.Utc).AddTicks(7340),
                            RegionId = 4L,
                            Temperature = 12.2f,
                            WeatherCondition = "RAIN"
                        },
                        new
                        {
                            Id = 5L,
                            DateTime = new DateTime(2024, 4, 13, 7, 19, 20, 361, DateTimeKind.Utc).AddTicks(7341),
                            RegionId = 5L,
                            Temperature = 12.2f,
                            WeatherCondition = "RAIN"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ITPlanet.Data.Models.Region", b =>
                {
                    b.HasOne("ITPlanet.Data.Models.RegionType", "RegionType")
                        .WithMany("Regions")
                        .HasForeignKey("RegionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ITPlanet.Data.Models.User", "User")
                        .WithMany("Regions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RegionType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ITPlanet.Data.Models.Weather", b =>
                {
                    b.HasOne("ITPlanet.Data.Models.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("ITPlanet.Data.Models.WeatherForecast", b =>
                {
                    b.HasOne("ITPlanet.Data.Models.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("ITPlanet.Data.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("ITPlanet.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("ITPlanet.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("ITPlanet.Data.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ITPlanet.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("ITPlanet.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ITPlanet.Data.Models.RegionType", b =>
                {
                    b.Navigation("Regions");
                });

            modelBuilder.Entity("ITPlanet.Data.Models.User", b =>
                {
                    b.Navigation("Regions");
                });
#pragma warning restore 612, 618
        }
    }
}
