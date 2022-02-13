using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;

namespace PlatformService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration,IWebHostEnvironment environment)
        {
            Configuration = configuration;

            /*  order to determine what environment we are running in, we need to inject something called 'hosting environment' into Startup 
                and get access the environment that way
                IConfiguration, IWebHostEnvironment are injected into classes and are available to us*/
            _environment = environment;
        }

        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //!dependency injection
            // if(_environment.IsProduction())
            // {
                Console.WriteLine("--> Using SqlServer Db");
                services.AddDbContext<AppDbContext>(opt =>
                    opt.UseSqlServer(Configuration.GetConnectionString("PlatformsConnection")));

            // }
            // else
            // {
            //     Console.WriteLine("--> Using InMem Db");
            //     services.AddDbContext<AppDbContext>(opt =>
            //         opt.UseInMemoryDatabase("InMem")); // InMem: name of database. 'random'
            // }
            services.AddScoped<IPlatformRepository, PlatformRepository>(); // if somebody 'asks' IPlatformRepository we'll give them 'PlatformRepository', concrete class

            services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); /*mapping Model to Dto*/
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PlatformService", Version = "v1" });
            });

            Console.WriteLine($"--> CommandServiceEndpoint {Configuration["CommandService"]}");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlatformService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // we pass IApplicationBuilder to PreparePopulation method in order for us to be able use AppDbContext,
            // we pass AppDbContext through IApplicationBuilder
            // PrepareDb.PreparePopulation(app,env.IsProduction()); 
        }
    }
}
