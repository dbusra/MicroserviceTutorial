using System;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    /*differentiate platform's PlatformController and command's PlatformController with '/c/' routing*/
    [Route("api/c/[controller]")] 
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController()
        {
            
        }

        public ActionResult TestInBoundConnection()
        {
            Console.WriteLine("--> Inbound POST # Command Service");

            return Ok("Inbound test of from Platforms Controller");

        }

    }
}