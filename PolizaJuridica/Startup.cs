using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using PolizaJuridica.Data;

namespace PolizaJuridica
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
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login/Logear/";
                });
            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                                                                    .AllowAnyMethod()
                                                                    .AllowAnyHeader()
                                                                    .WithOrigins("https://polizajuridica.com.mx")
                                                                    .WithOrigins("https://www.polizajuridica.com.mx")
                                                                    .WithOrigins("http://localhost:8081")
                                                                    .WithOrigins("http://localhost:8080")
                                                                    .WithOrigins("http://polizajuridicaweb.s3-website-us-east-1.amazonaws.com")
                                                                    .WithOrigins("https://www.polizajuridica.com.mx")));


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = "SessionCookiePJ";
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromDays(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            //services.AddDbContext<PolizaJuridicaDbContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("PolizaJuridicaDbContext")));

            // Conexion con driver MySQL
            services.AddDbContext<PolizaJuridicaDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("PolizaJuridicaDbContext"));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            app.UseStatusCodePages();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseCors("AllowAll");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseCookiePolicy();
            app.UseSession();
            app.UseMvc(routes =>
            {

                //routes.MapRoute(
                //    name: null,
                //    template: "Account/AccessDenied/{ReturnUrl?}",
                //    defaults: new { controller= "Home", action="Index"});


                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Logear}/{id?}");
            });
        }
    }
}
