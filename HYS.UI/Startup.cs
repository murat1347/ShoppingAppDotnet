using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using HYS.Application.Services.Concrete;
using HYS.Application.Services.Interfaces;
using HYS.Domain.Context;
using HYS.Domain.Entities;
using HYS.Persistence.Repositories.Concrete;
using HYS.Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HYS.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(_ => _.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));
            //services.AddScoped<IProductService, ProductManager>();
            services.AddTransient<IDbConnection>(con => new SqlConnection(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
