using ITPlanet.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ITPlanet.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public virtual DbSet<Models.User> Users { get; set; }
        public virtual DbSet<Models.Weather> Weathers { get; set; }
        public virtual DbSet<Models.Region> Regions { get; set; }
        public virtual DbSet<Models.RegionType> RegionTypes { get; set; }
        public virtual DbSet<Models.WeatherForecast> WeatherForecasts { get; set; }

        /// <inheritdoc />
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <inheritdoc />
        public ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            const int ADMIN_ID = 1;
            const int ROLE_ID = 1;

            builder.Entity<Role>()
                .HasData(new Role
                {
                    Id = ROLE_ID,
                    Name = "admin",
                    NormalizedName = "ADMIN"
                });

            var hasher = new PasswordHasher<User>();
            builder.Entity<Models.User>()
                .HasData(new User
                {
                    Id = ADMIN_ID,
                    LastName = "Admin",
                    FirstName = "Admin",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    PasswordHash = hasher.HashPassword(null, "123"),
                    SecurityStamp = string.Empty
                });

            builder.Entity<IdentityUserRole<int>>()
                .HasData(new IdentityUserRole<int>
                {
                    RoleId = ROLE_ID,
                    UserId = ADMIN_ID
                });

            builder.Entity<Models.RegionType>()
                .HasData(new[]
                {
                    new RegionType
                    {
                        Id = 1,
                        Type = "string1"
                    },
                    new RegionType
                    {
                        Id = 2,
                        Type = "string2"
                    },
                    new RegionType
                    {
                        Id = 3,
                        Type = "string3"
                    },
                    new RegionType
                    {
                        Id = 4,
                        Type = "string4"
                    },
                    new RegionType
                    {
                        Id = 5,
                        Type = "string5"
                    },
                });

            builder.Entity<Region>()
                .HasData(new[]
                {
                    new Region {
                        Id = 1,
                        Latitude = 22.22,
                        Longitude = 22.22,
                        Name = "Name1",
                        RegionTypeId = 1,
                        ParentRegion = "Name1",
                        UserId = 1,
                    },
                    new Region {
                        Id = 2,
                        Latitude = 33.33,
                        Longitude = 33.33,
                        Name = "Name2",
                        RegionTypeId = 1,
                        ParentRegion = "Name1",
                        UserId = 1
                    },
                    new Region {
                        Id = 3,
                        Latitude = 44.44,
                        Longitude = 44.44,
                        Name = "Name3",
                        RegionTypeId = 2,
                        ParentRegion = "Name2",
                        UserId = 1
                    },
                    new Region {
                        Id = 4,
                        Latitude = 55.55,
                        Longitude = 55.55,
                        Name = "Name4",
                        RegionTypeId = 3,
                        ParentRegion = "Name3",
                        UserId = 1
                    },
                    new Region {
                        Id = 5,
                        Latitude = 66.66,
                        Longitude = 66.66,
                        Name = "Name5",
                        RegionTypeId = 3,
                        ParentRegion = "Name4",
                        UserId = 1
                    }
                });

            builder.Entity<Models.Weather>()
                .HasData(new[]
                {
                    new Weather {
                        Id = 1,
                        Humidity = (float)11.11,
                        MeasurementDateTime = DateTime.UtcNow,
                        RegionId = 1,
                        RegionName = "Name1",
                        Temperature = (float)12.1,
                        PrecipitationAmount = 12,
                        WindSpeed = (float)15.2,
                        WeatherCondition = "RAIN",
                    },
                    new Weather {
                        Id = 2,
                        Humidity = (float)22.22,
                        MeasurementDateTime = DateTime.UtcNow,
                        RegionId = 2,
                        RegionName = "Name2",
                        Temperature = (float)12.1,
                        PrecipitationAmount = 13,
                        WindSpeed = (float)15.2,
                        WeatherCondition = "RAIN",
                    },
                    new Weather {
                        Id = 3,
                        Humidity = (float)17.11,
                        MeasurementDateTime = DateTime.UtcNow,
                        RegionId = 3,
                        RegionName = "Name3",
                        Temperature = (float)14.1,
                        PrecipitationAmount = 13,
                        WindSpeed = (float)15.2,
                        WeatherCondition = "RAIN",
                    },
                    new Weather {
                        Id = 4,
                        Humidity = (float)17.11,
                        MeasurementDateTime = DateTime.UtcNow,
                        RegionId = 4,
                        RegionName = "Name4",
                        Temperature = (float)72.1,
                        PrecipitationAmount = 12,
                        WindSpeed = (float)15.2,
                        WeatherCondition = "RAIN",
                    },
                    new Weather {
                        Id = 5,
                        Humidity = (float)19.11,
                        MeasurementDateTime = DateTime.UtcNow,
                        RegionId = 5,
                        RegionName = "Name5",
                        Temperature = (float)12.1,
                        PrecipitationAmount = 12,
                        WindSpeed = (float)15.2,
                        WeatherCondition = "RAIN",
                    },
                });

            builder.Entity<Models.WeatherForecast>()
                .HasData(new[]
                {
                    new WeatherForecast {
                        Id = 1,
                        DateTime =  DateTime.UtcNow,
                        Temperature = (float)12.2,
                        RegionId = 1,
                        WeatherCondition = "RAIN"
                    },
                     new WeatherForecast {
                        Id = 2,
                        DateTime =  DateTime.UtcNow,
                        Temperature = (float)17.2,
                        RegionId = 2,
                        WeatherCondition = "RAIN"
                    },
                      new WeatherForecast {
                        Id = 3,
                        DateTime = DateTime.UtcNow,
                        Temperature = (float)13.2,
                        RegionId = 3,
                        WeatherCondition = "RAIN"
                    },
                       new WeatherForecast {
                        Id = 4,
                        DateTime = DateTime.UtcNow,
                        Temperature = (float)12.2,
                        RegionId = 4,
                        WeatherCondition = "RAIN"
                    },
                        new WeatherForecast {
                        Id = 5,
                        DateTime = DateTime.UtcNow,
                        Temperature = (float)12.2,
                        RegionId = 5,
                        WeatherCondition = "RAIN"
                    },
                });
        }
    }
}
