using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HYS.API.EmailService;
using HYS.API.Identity;
using HYS.API.JwtTokenHandler;
using HYS.Application.Services.Concrete;
using HYS.Application.Services.Interfaces;
using HYS.Domain.Context;
using HYS.Domain.Entities;
using HYS.Persistence.Repositories.Concrete;
using HYS.Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HYS.API
{
    public class Startup
    {
        public IConfiguration _configuration;
        readonly string myAllowOrigins = "_myAllowOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();
            services.AddScoped<IServiceCollection, ServiceCollection>();
         

            services.AddDbContext<ApplicationContext>(_ => _.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddScoped<JwtTokenHandler.JwtTokenGenerator>();
            services.AddDbContext<AppDbContext>(_ => _.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));
            //services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<AppDbContext>()
            //    .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);
            services.AddIdentityCore<User>(opt =>
                {
                    opt.User.RequireUniqueEmail = true;
                })
               
                .AddEntityFrameworkStores<AppDbContext>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                            .GetBytes(Configuration["JWTSettings:TokenKey"]))
                    };
                });
            
            services.Configure<IdentityOptions>(options =>
            {
                // password
                //options.Password.RequireDigit = true;
                //options.Password.RequireLowercase = true;
                //options.Password.RequireUppercase = true;
                //options.Password.RequiredLength = 6;
                //options.Password.RequireNonAlphanumeric = true;

                // Lockout                
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers = true;

                // options.User.AllowedUserNameCharacters = "";
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt=>
            //{
            //    opt.RequireHttpsMetadata=false;
            //    opt.TokenValidationParameters= new TokenValidationParameters
            //    {
            //        ValidIssuer = "http://localhost",
            //        ValidAudience = "http://localhost",
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Muratmuratmurat.")),
            //        ValidateIssuerSigningKey = true,
            //        ValidateLifetime = true,
            //        ClockSkew = TimeSpan.Zero
            //    };
            //});
            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.LoginPath = "/account/login";
            //    options.LogoutPath = "/account/logout";
            //    options.AccessDeniedPath = "/account/accessdenied";
            //    options.SlidingExpiration = true;
            //    options.ExpireTimeSpan = TimeSpan.FromDays(30);
            //    options.Cookie = new CookieBuilder
            //    {
            //        HttpOnly = true,
            //        Name = ".ShopApp.Security.Cookie",
            //        SameSite = SameSiteMode.Strict
            //    };
            //});
            services.AddAuthorization();
            services.AddControllers();
            services.AddCors(opt =>
            {
                opt.AddPolicy(
                    name: myAllowOrigins,
                    Builder => { Builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();});
            });
            //services.AddScoped<IProductService, ProductManager>();
            services.AddTransient<IDbConnection>(con => new SqlConnection(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews();
            services.AddScoped<IEmailSender, SmtpEmailSender>(i =>
                new SmtpEmailSender(
                    _configuration["EmailSender:Host"],
                    _configuration.GetValue<int>("EmailSender:Port"),
                    _configuration.GetValue<bool>("EmailSender:EnableSSL"),
                    _configuration["EmailSender:UserName"],
                    _configuration["EmailSender:Password"])
            );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HYS.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HYS.API v1"));
            }
            app.UseAuthentication();
            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(myAllowOrigins);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
