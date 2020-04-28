using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services;
using Services.Services;

namespace Cosultations
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<MedicServices>();
            services.AddScoped<SpecializareServices>();
            services.AddScoped<ConsultatiiServices>();
            services.AddScoped<PacientServices>();
            services.AddScoped<ServiciiServices>();
            services.AddScoped<UserAccountServices>();

            services.AddAuthentication("Consultation")
                    .AddCookie("Consultation", options =>
                    {
                        options.AccessDeniedPath = new PathString("/Account/Login");
                        options.LoginPath = new PathString("/Account/Login");
                    });
            services.AddScoped(serviceProvider =>
            {
                var contextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
                var context = contextAccessor.HttpContext;
                var mail = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? string.Empty;
                var userService = serviceProvider.GetService<UserAccountServices>();
              

                var user = userService.Get(mail);
               
                if (user.Id !=0)
                    return new CurrentUser(isAuthenticated: true)
                    {
                        Id = user.Id,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        
                    };
                else
                    return new CurrentUser(isAuthenticated: false);
            });
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
              
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Consultation}/{action=Index}/{id?}");
            });
        }
    }
}
