using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SignalR_SqlDependency.Data;
using SignalR_SqlDependency.Hubs;
using SignalR_SqlDependency.Services;

namespace SignalR_SqlDependency
{
    public class Startup
    {
        
        private readonly IServiceProvider serviceProvider;

        public Startup(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            Configuration = configuration;
            this.serviceProvider = serviceProvider;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("SQLServerDB"))
            );
                               
            services.AddScoped<IDatabaseChangeNofiticationService, SqlDependencyService>();
            // Agregar SignalR
            services.AddSignalR();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);            
        }
        /* Estamos inyectando una inyeccion de dependencias en IDatabaseChangeNotificationService para poder ser
         la configuracion de SQL DEPENDENCY*/
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            IDatabaseChangeNofiticationService notificationService
            )
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
            app.UseCookiePolicy();

            // Configurar nueva ruta ""/chatHub" de js
            app.UseSignalR(x =>
            {
                // Registro de los Hubs
                x.MapHub<ChatHub>("/chatHub");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            /* Realizamos solo una vez la configuracion de SQL DEPENDENCY para que no nos llegue varias la configuración */
            notificationService.Config();
        }
    }
}
