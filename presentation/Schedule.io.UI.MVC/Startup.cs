using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//using Schedule.io.Infra.RavenDB.Configs;
using MediatR;
using Schedule.io.Configs;
using Schedule.io.Infra.SqlServerDB.Configs;
using Schedule.io.Infra.RavenDB.Configs;
using System;

namespace Schedule.io.UI.Web
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
            services.AddControllersWithViews();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddMediatR(typeof(Startup));
            services.AddScheduleIo(new ScheduleIoConfigurations(useEventSourcing: true));
            //services.AddScheduleIoRavenDb(new RavenDBConfig(new[] { Configuration["RavebDb:Url"] },
            //                                                        Configuration["RavebDb:DataBase"],
            //                                                        Configuration["RavebDb:Certificate:FileName"]));

            services.AddScheduleioSqlServerDb(new SqlServerDBConfig(Configuration["SqlServer:ConnectionString"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseScheduleIoSqlServerDb(serviceProvider);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
