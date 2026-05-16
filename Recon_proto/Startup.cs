using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.ServiceModel;
using System;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Http.Features;

namespace Recon_proto
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
            services.AddDistributedMemoryCache(); // 🔥 REQUIRED

            services.AddSession(options =>
            {
                var timeout = Configuration.GetValue<int>("Appsettings:TimeoutMinutes");
                options.IdleTimeout = TimeSpan.FromMinutes(timeout); // logout after 20 minutes idle
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;

                options.Cookie.Name = ".Flexi.Session";
                options.Cookie.Path = "/";
                //// 🔥 CRITICAL FIX
                //options.Cookie.SameSite = SameSiteMode.None;
                //options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.Cookie.SameSite = SameSiteMode.None;
            //    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            //});

                services.AddHttpContextAccessor();
            services.AddRouting();
            services.AddRazorPages();
            services.AddSingleton(Configuration);
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 524288000; // Set the limit to 100MB or any other size
            });
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            //app.Use(async (context, next) =>
            //{
            //    var path = context.Request.Path.Value;

            //    if (!path.Contains("/Login") && !path.StartsWith("/Login") && !path.StartsWith("/css") && !path.StartsWith("/js"))
            //    {
            //        var user = context.Session.GetString("User");

            //        if (string.IsNullOrEmpty(user))
            //        {
            //            context.Response.Redirect("/recon_productionweb/Login/Login");
            //            return;
            //        }
            //    }

            //    await next();
            //});
            app.Use(async (context, next) =>
            {
                var path = context.Request.Path.Value;

                if (!path.StartsWith("/Login") && !path.StartsWith("/css") && !path.StartsWith("/js"))
                {
                    var user = context.Session.GetString("User");
                    //var user = context.Request.Cookies["User"];
                    if (string.IsNullOrEmpty(user))
                    {
                        var basePath = context.Request.PathBase;
                        context.Response.Redirect($"{basePath}/Login/Login");
                        return;
                    }
                }

                await next();
            });

           

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Login}/{id?}");
            });


        }
    }
}