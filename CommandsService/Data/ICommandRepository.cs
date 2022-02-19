using System.Collections.Generic;
using CommandsService.Models;

namespace CommandService.Data
{
    public interface ICommandRepository
    {
        bool SaveChanges();
        
        //Platforms related stuff
        IEnumerable<Platform> GetAllPlatforms(); // get all platforms from our command service
        void CreatePlatform(Platform platform); // we need to get platform data from elsewhere(platform service)
        bool PlatformExist(int platformId);

        //Commands related stuff
        IEnumerable<Command> GetAllCommandsForPlatform(int platformId);
        Command GetCommand(int platformId, int commandId); // get an individual command for a platform
        void CreateCommand(int platformId, Command command); // create a command for a specific platform
    } 
}