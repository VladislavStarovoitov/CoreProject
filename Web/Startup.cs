using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Data;
using Web.Models;
using Web.Services;
using Microsoft.Extensions.Logging;
using Common;
using Web.Middlewares;
using Web.Constraints;
using Web.Filters;
using Web.ModelBinders;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Web.AuthorizationRequirements;
using Microsoft.AspNetCore.Authorization;

namespace Web
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IAuthorizationHandler, AgeHandler>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AgeLimit", policy => policy.Requirements.Add(new AgeRequirement(18)));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 0;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IEmailSender, EmailSender>();
            
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(IEFilterAttribute));
                options.ModelBinderProviders.Insert(0, new BirthDateModelBinderProvider());
            })
                .AddDataAnnotationsLocalization()
                .AddViewLocalization();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddFile();

            app.UseMiddleware<CultureMiddleware>();
            app.UseMiddleware<LoggingMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStatusCodePagesWithReExecute("/Home/ErrorStatus/{0}");

            app.UseStaticFiles();

            app.UseAuthentication();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}",
                    defaults: null,
                    constraints: new { urlConstraint = new UrlConstraint("/About") });
            });
        }
    }
}
