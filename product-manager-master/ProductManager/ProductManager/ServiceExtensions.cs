using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProductManager.Constraints;
using ProductManager.Helpers;
using ProductManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager
{
    /// <summary>
    /// Service Extensions for adding more functionality to the application.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Helps Creating api versioning.
        /// </summary>
        /// <param name="services">.net core api Service Collection.</param>
        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
            });
        }

        /// <summary>
        /// Helps creating Authentication.
        /// </summary>
        /// <param name="services">.net core api Service Collection.</param>
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<ApiUser>(q => { q.User.RequireUniqueEmail = true; });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddEntityFrameworkStores<ProductManagerDBContext>()
                .AddDefaultTokenProviders();
        }

        /// <summary>
        /// Help creating Authentication with JWT token system.
        /// </summary>
        /// <param name="services">.net core api Service Collection.</param>
        /// <param name="Configuration">Reading configuration files.</param>
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration Configuration)
        {
            var jwtSettings = Configuration.GetSection(AppConfigurationKeys.JWT);

            var key = Environment.GetEnvironmentVariable(EnvironmentKeys.JWTKey);

            if(key == null){
                
                  Console.WriteLine("Using JWT key from appsettings.json");
                  key = Configuration.GetValue<string>(AppConfigurationKeys.JWTKey);
           
            }

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection(AppConfigurationKeys.JWTIssuer).Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                };
            });
        }
    }

}
