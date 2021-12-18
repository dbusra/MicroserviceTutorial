using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")] // [Route("api/platforms")] [controller] this portion takes prefix of the controller ; so [controller] = platforms here

    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepository _repository;
        private readonly IMapper _mapper;

        // this is called constructor dependency injection
        public PlatformsController(IPlatformRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Getting platforms...");

            var platformItems =_repository.GetAllPlatforms();
            
            // we are mapping PlatformReadDto and from platformItems
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));

        }

         // resource URI : Name = "GetPlatformById" , by giving a name to this action we basically say this is the uri to take the resource that was created.
     [HttpGet("{platformId}", Name = "GetPlatformById")]   
     public ActionResult<PlatformReadDto> GetPlatformById(int platformId)
        {
            Console.WriteLine("--> Getting an individual platform..."); 
            var platformItem = _repository.GetPlatformById(platformId);

            if(platformItem != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platformItem));
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            Console.WriteLine("--> Creating an individual platform..."); 

            var platformModel = _mapper.Map<Platform>(platformCreateDto);

            _repository.CreatePlatform(platformModel);
            _repository.SaveChanges();

            // best practice: whenever you create a resource you should return back http 201 along with the resource that was created 
            // and also a URI to that resource / a location to that resource
            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);


            // resource URI : nameof(GetPlatformById)
            return CreatedAtRoute(nameof(GetPlatformById) , new { platformId = platformReadDto.Id}, platformReadDto);
            //return CreatedAtRoute(uri , resource id : platformId for GetPlatformById method, resource);


            
        }
    }
}