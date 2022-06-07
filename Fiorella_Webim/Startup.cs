using Fiorella_Webim.DAL;
using Fiorella_Webim.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Fiorella_Webim
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            string connectinString = _config.GetConnectionString("DefaultConnection");
            services.AddControllersWithViews();
            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(connectinString);
            });
            services.AddSession(opt =>
            {
                opt.IdleTimeout = TimeSpan.FromMinutes(20);
            });
            services.AddIdentity<AppUser, IdentityRole>(op =>
            {
                op.Password.RequiredLength = 6;
                op.Password.RequireDigit = true;
                op.Password.RequireLowercase = true;
                op.Password.RequireNonAlphanumeric = true;
                op.Password.RequireUppercase = true;
                
                op.User.RequireUniqueEmail = true;
                op.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
                op.Lockout.MaxFailedAccessAttempts = 3;
                op.Lockout.AllowedForNewUsers = true;//cehdtler falan olanda qiraga atmaq ucundur

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();//burda da hara
                                                                                   //store oldugunu gosterir
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();//ve burda da autentifikasiya istifade edirik
            app.UseAuthorization(); // eger authorize istifade edirsiznizse bunu yazmalisiniz
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "areas",
                    "{area:exists}/{controller=dashboard}/{action=Index}/{id?}"
                  );
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
