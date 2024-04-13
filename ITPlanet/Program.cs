using AutoMapper.EquivalencyExpression;
using ITPlanet.Data.Data;
using ITPlanet.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace ITPlanet;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            options.UseNpgsql(connectionString, optionsBuilder =>
            {
                optionsBuilder.EnableRetryOnFailure(10);
                optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
        }, ServiceLifetime.Singleton, ServiceLifetime.Singleton).AddDatabaseDeveloperPageExceptionFilter();
        #region JwtComment

        //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //    .AddJwtBearer(config =>
        //    {
        //        var jwtIssuer = builder.Configuration["Jwt:Issuer"];

        //        config.RequireHttpsMetadata = false;
        //        config.SaveToken = true;
        //        config.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuer = true,
        //            ValidIssuer = ,
        //            ValidateAudience = false,
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey =
        //                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        //        };
        //    });
        #endregion

        builder.Services.AddAuthorization(options =>
        {
            // Настройка политики авторизации
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });

        #region Identity
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
            options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        })
        .AddCookie(IdentityConstants.ApplicationScheme, o =>
        {
            o.Cookie.HttpOnly = true;
            o.ExpireTimeSpan = TimeSpan.FromHours(8);

            o.LoginPath = "/";
            o.AccessDeniedPath = "/";
            o.SlidingExpiration = true;

            o.Events = new CookieAuthenticationEvents
            {
                OnValidatePrincipal = SecurityStampValidator.ValidatePrincipalAsync,
                OnRedirectToLogin = (RedirectContext<CookieAuthenticationOptions> context) =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                },
                OnRedirectToLogout = (RedirectContext<CookieAuthenticationOptions> context) =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                },
                OnRedirectToAccessDenied = (RedirectContext<CookieAuthenticationOptions> context) =>
                {
                    context.Response.StatusCode = 403;
                    return Task.CompletedTask;
                }
            };
        })
        .AddCookie(IdentityConstants.ExternalScheme, o =>
        {
            o.Cookie.Name = IdentityConstants.ExternalScheme;
            o.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        })
        //.AddCookie(IdentityConstants.TwoFactorRememberMeScheme, o =>
        //{
        //    o.Cookie.Name = IdentityConstants.TwoFactorRememberMeScheme;
        //    o.Events = new CookieAuthenticationEvents
        //    {
        //        OnValidatePrincipal = SecurityStampValidator.ValidateAsync<ITwoFactorSecurityStampValidator>
        //    };
        //})
        //.AddCookie(IdentityConstants.TwoFactorUserIdScheme, o =>
        //{
        //    o.Cookie.Name = IdentityConstants.TwoFactorUserIdScheme;
        //    o.Events = new CookieAuthenticationEvents
        //    {
        //        OnRedirectToReturnUrl = _ => Task.CompletedTask
        //    };
        //    o.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        //})
        ;

        // Hosting doesn't add IHttpContextAccessor by default
        builder.Services.AddHttpContextAccessor();
        // Identity services
        builder.Services.TryAddScoped<IUserValidator<User>, UserValidator<User>>();
        builder.Services.TryAddScoped<IPasswordValidator<User>, PasswordValidator<User>>();
        builder.Services.TryAddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        builder.Services.TryAddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();
        builder.Services.TryAddScoped<IRoleValidator<Role>, RoleValidator<Role>>();
        // No interface for the error describer so we can add errors without rev'ing the interface
        builder.Services.TryAddScoped<IdentityErrorDescriber>();
        builder.Services.TryAddScoped<ISecurityStampValidator, SecurityStampValidator<User>>();
        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<SecurityStampValidatorOptions>, PostConfigureSecurityStampValidatorOptions>());
        builder.Services.TryAddScoped<ITwoFactorSecurityStampValidator, TwoFactorSecurityStampValidator<User>>();
        builder.Services.TryAddScoped<IUserClaimsPrincipalFactory<User>, UserClaimsPrincipalFactory<User, Role>>();
        builder.Services.TryAddScoped<IUserConfirmation<User>, DefaultUserConfirmation<User>>();
        builder.Services.TryAddScoped<UserManager<User>>();
        builder.Services.TryAddScoped<SignInManager<User>>();
        builder.Services.TryAddScoped<RoleManager<Role>>();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
            options.SignIn.RequireConfirmedAccount = false;

            // Password settings.
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 0;
            options.Password.RequiredUniqueChars = 0;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.RequireUniqueEmail = true;
        });

        var identity = new IdentityBuilder(typeof(User), typeof(Role), builder.Services);
        identity
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        #endregion

        builder.Services.AddAutoMapper(config =>
        {
            config.AddCollectionMappers();
            config.AddProfile<Mapper.MapperProfiles>();
        });

        builder.Services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
        });
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

            var securityRequirementsSection = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            };

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Cookie,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(securityRequirementsSection);
        });

        builder.Services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();


        var app = builder.Build();

        var dbContext = app.Services.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();

        app.UseMigrationsEndPoint();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

            c.OAuthClientId("your_client_id");
            c.OAuthAppName("Swagger UI");
            c.OAuthUsePkce();
        });

        app.UseHealthChecks("/health");

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers()
           .RequireAuthorization();

        app.Run();
    }

    private sealed class PostConfigureSecurityStampValidatorOptions : IPostConfigureOptions<SecurityStampValidatorOptions>
    {
        public PostConfigureSecurityStampValidatorOptions(TimeProvider timeProvider)
        {
            TimeProvider = timeProvider;
        }

        private TimeProvider TimeProvider { get; }

        public void PostConfigure(string? name, SecurityStampValidatorOptions options)
        {
            options.TimeProvider ??= TimeProvider;
        }
    }
}