using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ProductManager.Helpers;
using ProductManager.IRepository;
using ProductManager.Repository;
using ProductManager.Services;
using ProductManagerCacheService.Service;
using ProductManagerServiceLayer.IServices;
using ProductManagerServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace ProductManager
{
    public class Startup
    {
        private readonly string UIOrigins = "_UIOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: UIOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:3000","http://localhost")
                                      .AllowAnyMethod()
                                      .AllowAnyHeader();

                                  });
            });

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICustomerService,CustomerService>();
            services.AddTransient<ISellerService, SellerService>();
            services.AddTransient<IPurchaseService, PurchaseService>();
            services.AddTransient<ISaleService, SaleService>();
            services.AddTransient<IDashboardService, DashboardService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IDashboardCacheService,DashboardCacheService>();


            services.AddMemoryCache();

            services.AddAuthentication();
            services.ConfigureIdentity();
            services.ConfigureJWT(Configuration);

            services.AddAutoMapper(typeof(MapperInitilizer));

            services.AddSingleton<ResourceManager, PMResourceManager>();

            services.AddControllers().AddDataAnnotationsLocalization();

            services.ConfigureVersioning();
              
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductManager", Version = "v1" });
            });

            services.AddDbContext<ProductManagerDBContext>(
       options => options.UseSqlServer("name=ConnectionStrings:ProductManagerDB"));
            services.AddScoped<IAuthManager, AuthManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductManager v1"));

                if (EnvironmentHelper.IsDevelopment)
                {
                    DevelopmentDataInitializer.InitializeDevelopmentData(app);
                }
              
            }

            Console.WriteLine("Running in =" + EnvironmentHelper.CurrentEnvironment);

            if (EnvironmentHelper.IsProduction)
            {
                ProductionDataInitializer.InitializeProductionData(app);
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(UIOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // What this code does?
            app.UseEndpoints(x => x.MapControllers());
        }
    }
}
