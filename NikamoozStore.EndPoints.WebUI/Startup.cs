﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NikamoozStore.Core.Contracts.Categories;
using NikamoozStore.Core.Contracts.Orders;
using NikamoozStore.Core.Contracts.Payments;
using NikamoozStore.Core.Contracts.Products;
using NikamoozStore.EndPoints.WebUI.Models.Carts;
using NikamoozStore.Infrastructures.Dal.Categories;
using NikamoozStore.Infrastructures.Dal.Commons;
using NikamoozStore.Infrastructures.Dal.Orders;
using NikamoozStore.Infrastructures.Dal.Products;
using NikamoozStore.Services.ApplicatoinServices.Payments;

namespace NikamoozStore.EndPoints.WebUI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<NikamoozStoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("storeDb")));
            services.AddMemoryCache();
            services.AddSession();
            services.AddScoped<ProductRepository, EfProductRepository>();
            services.AddScoped<CategoryRepository, EfCategoryRepository>();
            services.AddScoped(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<OrderRepository, EfOrderRepository>();
            services.AddScoped<PaymentService, PayIrPaymentService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: null,
                template: "{category}/Page{pageNumber:int}",
                defaults: new { controller = "Product", action = "List" }
                );

                routes.MapRoute(
                name: null,
                template: "Page{pageNumber:int}",
                defaults: new
                {
                    controller = "Product",
                    action = "List",
                    productPage = 1
                }
                );
                routes.MapRoute(
                name: null,
                template: "{category}",
                defaults: new
                {
                    controller = "Product",
                    action = "List",
                    productPage = 1
                }
                );
                routes.MapRoute(
                name: null,
                template: "",
                defaults: new
                {
                    controller = "Product",
                    action = "List",
                    productPage = 1
                });
                routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
            });
        }
    }
}
