using System;
using System.Collections.Generic;
using System.Linq;
using CommandsService.Data;
using CommandsService.Models;

namespace CommandService.Data
{
    public class CommandRepository : ICommandRepository
    {
        private readonly AppDbContext _context;

        public CommandRepository(AppDbContext context)
        {
            _context = context;
        }
        public void CreateCommand(int platformId, Command command)
        {
            if(command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            command.PlatformId = platformId;
            _context.Commands.Add(command);
        }

        public void CreatePlatform(Platform platform)
        {
            if(platform == null)
            {
                throw new ArgumentNullException(nameof(platform));
            }
            _context.Platforms.Add(platform);
        }

        public IEnumerable<Command> GetAllCommandsForPlatform(int platformId)
        {
            return _context.Commands
                    .Where(w => w.PlatformId == platformId)
                    .OrderBy(o => o.Platform.Name);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        public Command GetCommand(int platformId, int commandId)
        {
            return _context.Commands
                           .Where(w => w.PlatformId == platformId && w.Id == commandId)
                           .FirstOrDefault();
        }

        public bool PlatformExist(int platformId)
        {
            return _context.Platforms.Any(p => p.Id == platformId); // _context.Platforms.Where(p => p.Id == platformId).Any();
        }

        public bool SaveChanges()
        {
            //_context.SaveChanges() returns the number of state entries written to the database
            // how many entries are effected
            return _context.SaveChanges() >= 0;
        }
    }
}