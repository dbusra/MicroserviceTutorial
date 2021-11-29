using System;
using System.Collections.Generic;
using System.Linq;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly AppDbContext _context;

        public PlatformRepository(AppDbContext context) // AppDbContext is injected in Startup
        {
            _context = context;
        }
        public void CreatePlatform(Platform platform)
        {
            if (platform == null)
            {
                throw new ArgumentNullException(nameof(platform));
            }

            _context.Platforms.Add(platform);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
             //_context.Platforms comes from AppDbContext -- DbSet
            return _context.Platforms.ToList();
        }

        public Platform GetPlatformById(int id)
        {
            return _context.Platforms.FirstOrDefault(f => f.Id == id);
        }

        public bool SaveChanges()
        {
            /* shorter: return (_context.SaveChanges() >= 0) */

            bool isSaved = false;
            int saved = _context.SaveChanges();

            if (saved >= 0)
            {
                isSaved = true;
            }
               
            return isSaved;
        }
    }
}