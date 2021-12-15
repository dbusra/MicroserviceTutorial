using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;

namespace PlatformService.Data
{
    /*
        Question 1: Why we do not use constructor?
        It is a static class, we cannot use constructor dependency injection. 

        Question 2? Why we do use DbContext instead of IRepository? 
        This class is just for TESTING, we are gonna use this class eventually to generate migrations in sql server
    
    */

    /*preparing db and put some data into it*/
    public static class PrepareDb
    {
        public static void PreparePopulation(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());

            }

        }

        private static void SeedData(AppDbContext context)
        {
            if(!context.Platforms.Any())
            {
                Console.WriteLine("Seeding data...");

                context.Platforms.AddRange
                (
                    new Platform(){Name = "Dot Net", Publisher = "Microsoft", Cost= "Free"},
                    new Platform(){Name = "SQL Server Express", Publisher = "Microsoft", Cost= "Free"}, 
                    new Platform(){Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost= "Free"},
                    new Platform(){Name = "Oracle", Publisher = "Oracle Corporation", Cost= "Free"}
                );

                context.SaveChanges();
            }
            else 
            {
                Console.WriteLine("--> We already have data");
            }

        }

    }
}