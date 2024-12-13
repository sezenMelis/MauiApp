using DenemeFileOrbis.library.Models;
using DenemeFileOrbis.library.Responses;

namespace DenemeWebApi.Services
{
    public interface IClientService
    {
        Task<ServiceResponse> AddClientAsync(Client client);
        Task<ServiceResponse> UpdateClientAsync(Client client);
        Task<ServiceResponse> DeleteClientAsync(int id);
        Task<List<Client>> GetAllClientsAsync();
        Task<Client> GetClientByIdAsync(int id);
        
    }
}
