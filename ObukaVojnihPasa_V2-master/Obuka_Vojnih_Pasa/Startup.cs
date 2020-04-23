using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using log4net.Repository.Hierarchy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.OpenApi.Models;
using Obuka_Vojnih_Pasa.Middleware;
using Obuka_Vojnih_Pasa.Models;
using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Models.Extensions;
using Obuka_Vojnih_Pasa.Models.Repositories;
using Obuka_Vojnih_Pasa.Services;
using Obuka_Vojnih_Pasa.Services.Implementation;
using Obuka_Vojnih_Pasa.Services.Interafaces;

namespace Obuka_Vojnih_Pasa
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
            services.AddDbContext<ObukaPasaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IInstruktorService, InstruktorService>();
            services.AddScoped<IObukaService, ObukaService>();
            services.AddScoped<IPasService, PasService>();
            services.AddScoped<IZadatakService, ZadatakService>();
            services.AddScoped<IInstruktorService, InstruktorService>();
            services.AddScoped<IAngazovanjeService, AngazovanjeService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<Seeder>();
            services.AddControllersWithViews(
            ).AddJsonOptions(x => x.JsonSerializerOptions.MaxDepth = Int32.MaxValue);

           services.AddAutoMapper(this.GetType().Assembly);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() { Title = "PawPatrolAPI", Version = "v1" });
            });
            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
       
            services.AddSession();
            services.AddIdentity<ApplicationUser, IdentityRole>()
               /*(options =>
           {
               options.SignIn.RequireConfirmedEmail = true;
           })
           */
               .AddRoles<IdentityRole>()
              .AddDefaultTokenProviders()
              .AddEntityFrameworkStores<ObukaPasaContext>();
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
              
            }).AddXmlSerializerFormatters();

            services.AddAuthentication();
         
            services.AddAuthorization(options =>
            {
             
                options.AddPolicy("ZadatakPolicy",
                 policy => policy.RequireClaim("Unos zadatka", "true").RequireRole("Korisnik", "Admin"));
                options.AddPolicy("AngazovanjePolicy",
                 policy => policy.RequireClaim("Brisanje angažovanja", "true").RequireRole("Korisnik", "Admin"));
            });


          
          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Seeder seeder)
        {
            
          
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
        
            }

        //    seeder.Seed();
       

            //  app.UseHttpsRedirection();
     
            app.UseRouting();
            app.UseCustomExceptionMiddleware();
            app.UseStaticFiles();  
            app.UseSession();
            app.UseAuthentication();
    
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PawPatrolAPI v1");

            }
            );      
            app.UseEndpoints(endpoints =>
            {
         
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(name: "Error",
                                               "error",
                                               new { controller = "Home", action = "Error" });

                endpoints.MapControllerRoute(name: "PageNotFound",
                                                "pagenotfound",
                                                new { controller = "Home", action = "PageNotFound" });
            });
          
            
        }
    }
}
