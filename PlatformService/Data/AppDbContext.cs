using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class AppDbContext : DbContext
    {
        /*AppDbContext --> database*/
        /*note: with 'base' we can use functionality of parent class, in this case DbContext*/
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) 
        {
            
        }

        public DbSet<Platform> Platforms { get; set; } // represents db 'table'

    }
}