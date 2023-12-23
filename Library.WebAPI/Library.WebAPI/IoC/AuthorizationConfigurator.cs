using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Library.DataAccess.Entities;
using Library.WebAPI.Settings;
using Microsoft.AspNetCore.DataProtection;
using Library.DataAccess;

namespace Library.WebAPI.IoC
{
    public static class AuthorizationConfigurator
    {
        public static void ConfigureServices(this IServiceCollection services, LibrarySettings settings)
        {
            IdentityModelEventSource.ShowPII = true;

            services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(settings.DatabaseConnectionString));

            services.AddIdentity<UserEntity, UserRoleEntity>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
            })
            .AddEntityFrameworkStores<LibraryDbContext>()
            .AddSignInManager<SignInManager<UserEntity>>()
            .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddInMemoryApiScopes(new[] { new ApiScope("api") })
                .AddInMemoryClients(new[]
                {
                    new Client()
                    {
                        ClientName = settings.ClientId,
                        ClientId = settings.ClientId,
                        Enabled = true,
                        AllowOfflineAccess = true,
                        AllowedGrantTypes = new List<string>()
                        {
                            GrantType.ClientCredentials,
                            GrantType.ResourceOwnerPassword
                        },
                        ClientSecrets = new List<Secret>()
                        {
                            new Secret(settings.ClientSecret.Sha256())
                        },
                        AllowedScopes = new List<string>() { "api" }
                    }
                })
                .AddAspNetIdentity<UserEntity>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = false;
                options.Authority = settings.IdentityServerUri;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                options.Audience = "api";
            });

            services.AddAuthorization();
        }

        public static void ConfigureApplication(IApplicationBuilder app)
        {
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
