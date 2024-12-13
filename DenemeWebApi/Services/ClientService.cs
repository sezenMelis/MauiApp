using DenemeFileOrbis.library.Models;
using DenemeFileOrbis.library.Responses;
using DenemeWebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace DenemeWebApi.Services
{
    public class ClientService : IClientService
    {
        private readonly AppDbContext appDbContext;

        public ClientService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<ServiceResponse> AddClientAsync(Client client)
        {
            if (client == null)
                return new ServiceResponse() { Message = "Bad Request", Success = false };

            var chk = await appDbContext.Clients.Where(p => p.Name.ToLower().Equals(client.Name.ToLower())).FirstOrDefaultAsync();
            if (chk is null)
            {
                appDbContext.Clients.Add(client);
                await appDbContext.SaveChangesAsync();
                return new ServiceResponse() { Message = "Client added", Success = true };
            }
            return new ServiceResponse() { Message = "Client already added", Success = false };
        }

        public async Task<ServiceResponse> DeleteClientAsync(int id)
        {
            var product = await appDbContext.Clients.FirstOrDefaultAsync(p => p.Id == id);
            if (product is null)
                return new ServiceResponse() { Message = "Client not found", Success = false };

            appDbContext.Clients.Remove(product);
            await appDbContext.SaveChangesAsync();
            return new ServiceResponse() { Message = "Client deleted", Success = true };
        }

        public async Task<List<Client>> GetAllClientsAsync()
        {
            var client = await appDbContext.Clients.ToListAsync();
            return client;
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            var client = await appDbContext.Clients.FirstOrDefaultAsync(p => p.Id == id);
            return client!;
        }

        public async Task<ServiceResponse> UpdateClientAsync(Client client)
        {
            var result = await appDbContext.Clients.FirstOrDefaultAsync(p => p.Id == client.Id);
            if (result is null)
                return new ServiceResponse() { Message = "Client not found", Success = false };

            result.Id = client.Id;
            result.Name = client.Name;
            result.LastName = client.LastName;
            result.Email = client.Email;
            result.Password = client.Password;
            result.Image = client.Image;

            await appDbContext.SaveChangesAsync();
            return new ServiceResponse() { Message = "Client updated", Success = true };
        }
    }
}
