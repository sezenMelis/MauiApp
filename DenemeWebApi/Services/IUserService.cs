using DenemeFileOrbis.library.Responses;

namespace DenemeWebApi.Services
{
    public interface IUserService
    {
        Task<ServiceResponse> ValidateUserAsync(string username,string password);
    }
}
