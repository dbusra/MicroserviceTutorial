using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration) /*constructor dependency injection*/
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }


        public async Task SendPlatformToCommand(PlatformReadDto platform)
        {
            //StringContent : Provides Http content based on a string.

            var httpContent = new StringContent(
                JsonSerializer.Serialize(platform), // serialize platform service, so we can send over the wire basically.
                Encoding.UTF8,
                "application/json");

            // make a post async request to the client
            var response = await _httpClient.PostAsync($"{_configuration["CommandService"]}", httpContent); // we need to know where to send this post request temporarily we now hard code the uri to our service (we will put it config later)


            //string interpolation:  $"{_configuration["CommandService"]}"

            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to CommandService was OK!");
            }
            else
            {
                Console.WriteLine("--> Sync POST to CommandService was NOT OK!");
            }
        }
    }
}