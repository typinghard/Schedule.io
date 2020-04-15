using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Schedule.io;
//using Schedule.io.Infra.RavenDB.Configs;
using Schedule.io.Models;
using MediatR;
using Schedule.io.Configs;
using Schedule.io.Infra.SqlServerDB.Configs;

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
            //services.AddScheduleIo(new ScheduleIoConfigurations(useEventSourcing: true));
            //services.UseScheduleIoRavenDb(new RavenDBConfig(new[] { "https://a.free.elvis.ravendb.cloud" },
            //                                                Configuration["RavenDB_Northwind:Database"],
            //                                                Configuration["RavenDB_Northwind:CertificadePath"]
            //                                                ));

            services.AddScheduleIo(new ScheduleIoConfigurations(useEventSourcing: false));
            services.UseScheduleIoSqlServerDb(new SqlServerDBConfig(Configuration["SqlServerDB:ConnectionString"]));
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
