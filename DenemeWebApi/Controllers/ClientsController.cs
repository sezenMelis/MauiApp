using DenemeFileOrbis.library.Responses;
using DenemeFileOrbis.library.Models;
using DenemeWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DenemeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {

        private readonly IClientService clientService;

        public ClientsController(IClientService clientService)
        {
            this.clientService = clientService;
        }


        [HttpGet("all")]
        public async Task<ActionResult<List<Client>>> GetAllClients()
        {
            var client = await clientService.GetAllClientsAsync();
            if (client is null)
                return NotFound("Client not found");
            else
                return Ok(client);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Client>> GetClientByIdAsync(int id)
        {
            var client = await clientService.GetClientByIdAsync(id);
            if (client is null)
                return NotFound("Client not found");
            else
                return Ok(client);
        }

        

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ServiceResponse>> DeleteClientAsync(int id)
        {
            var client = await clientService.GetClientByIdAsync(id);
            if (client == null)
                return NotFound("Client not found");

            var response = await clientService.DeleteClientAsync(client.Id);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse>> UpdateClientAsync(Client client)
        {
            var result = await clientService.GetClientByIdAsync(client.Id);
            if (result == null)
                return NotFound("Client not found");

            var response = await clientService.UpdateClientAsync(client);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse>> AddClientAsync(Client client)
        {
            if (client is null)
                return BadRequest("Bad request");

            var result = await clientService.AddClientAsync(client);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }









    }
}
